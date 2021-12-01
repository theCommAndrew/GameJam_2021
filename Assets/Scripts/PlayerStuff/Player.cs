using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class Player : Character
{
    // movement
    public Vector2 movement;
    Vector2 mousePosition;
    // dashing
    public GameObject dashEffect;
    public bool canDash = true;
    public float dashDistance = 5f;
    const float DASH_COOLDOWN_MAX = 1f;
    // damage and invincibility 
    private bool canTakeDamage = true;
    public static float extraDamage = 1;
    public static float reloadRecudtion = 1;
    public static float luck = 0;
    [SerializeField] private float invincibilityDurationSeconds = 1.5f;
    private float flashTimer = 0.15f;
    // event calls
    public static event EventHandler playerDeath;
    public static event Action<int, int> updateHealth = (playerHealth, maxHealth) => { };
    //animation
    public Animator animator;
    private SpriteRenderer playerSprite;
    private TextMesh callout;

    
    /*
    private static Player playerInstance;
    
    private void Awake() {
        DontDestroyOnLoad(this);
        if (playerInstance == null) {
            playerInstance = this;
        } 
        else {
            Destroy(gameObject);
        }
    }*/

    void Start()
    {
        DontDestroyOnLoad(this);
        maxHealth = 3;
        health = maxHealth;
        moveSpeed = 7f;
        maxSpeed = moveSpeed;

        this.rb = this.GetComponent<Rigidbody2D>();
        this.playerSprite = transform.GetChild(transform.childCount - 1).GetComponent<SpriteRenderer>();
        this.callout = GameObject.FindGameObjectWithTag("PlayerCallout").GetComponent<TextMesh>();

        updateHealth?.Invoke(health, maxHealth);
    }

    void Update()
    {
        if (!UIScripts.gameIsPaused && alive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

            if (mousePosition.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                callout.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (mousePosition.x >= transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                callout.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                Vector3 moveDir = new Vector3(movement.x, movement.y).normalized;
                Dash(moveDir);
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void takeDamage(int damage)
    {
        if (!canTakeDamage) return;
        health -= damage;
        updateHealth?.Invoke(health, maxHealth);
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerTookDamage);

        if (health <= 0)
        {
            die();
        }

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    public void heal(int restoreAmount)
    {
        health = Mathf.Min(health + restoreAmount, maxHealth);
        updateHealth?.Invoke(health, maxHealth);
    }

    public void increaseStat(PlayerStat statToIncrease)
    {
        switch (statToIncrease)
        {
            case PlayerStat.Health:
                maxHealth += 1;
                health = maxHealth;
                updateHealth?.Invoke(health, maxHealth);
                break;

            case PlayerStat.Speed:
                maxSpeed += 1f;
                moveSpeed = maxSpeed;
                break;

            case PlayerStat.Damage:
                extraDamage += .25f;
                break;

            case PlayerStat.Reload:
                reloadRecudtion *= .9f;
                break;

            case PlayerStat.Luck:
                luck += 10f;
                break;
        }

    }

    public void die()
    {
        alive = false;
        animator.Play("PlayerDie");
        playerDeath?.Invoke(this, EventArgs.Empty);
        StartCoroutine(delayDestroy());
    }

    private void Dash(Vector3 moveDir)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, dashDistance, LayerMask.GetMask("Walls")); // can set this as a variable if it needs to change

        float particleAngle = Vector3.Angle(moveDir, transform.up);
        particleAngle = moveDir.x > 0 ? -particleAngle : particleAngle;
        var dasheffectvar = Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, particleAngle));

        if (hit.collider == null)
            transform.position += moveDir * dashDistance;
        else
            transform.position = hit.point;
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.playerDash);

        StartCoroutine(dashCooldown());
    }

    public IEnumerator dashCooldown()
    {
        canDash = false;
        for (float i = 0; i < DASH_COOLDOWN_MAX; i += flashTimer)
        {
            playerSprite.material.color = playerSprite.material.color == Color.white ? Color.gray : Color.white;
            yield return new WaitForSeconds(flashTimer);
        }
        playerSprite.material.color = Color.white;
        canDash = true;
    }

    public IEnumerator BecomeTemporarilyInvincible()
    {

        canTakeDamage = false;
        for (float i = 0; i < invincibilityDurationSeconds; i += flashTimer)
        {
            playerSprite.material.color = playerSprite.material.color == Color.white ? Color.red : Color.white;
            yield return new WaitForSeconds(flashTimer);
        }
        playerSprite.material.color = Color.white;
        canTakeDamage = true;
    }
    /*
    public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Transform obj)
    {
        float timer = 0;
        while (knockbackDuration > timer)
        {
            Vector2 dir = (obj.transform.position - this.transform.position).normalized;
            timer += Time.deltaTime;
            rb.AddForce(-dir * knockbackPower);
        }
        yield return 0;
    }
    */

    public IEnumerator slowPlayerMovement()
    {
        moveSpeed *= .75f;
        yield return new WaitForSeconds(.8f);
        moveSpeed = maxSpeed;
    }

    public IEnumerator stopPlayerMovement()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(1f);
        moveSpeed = maxSpeed;
    }

    public void spawnIn()
    {
        StartCoroutine(stopPlayerMovement());
        animator.Play("PlayerSpawn");
    }

    private IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}