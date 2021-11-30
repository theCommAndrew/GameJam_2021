using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class Player : Character
{
    // movement
    Vector2 movement;
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
    [SerializeField] private float invincibilityDurationSeconds = 1.5f;
    [SerializeField] private float invicibilityDeltaTime = 0.25f;
    public float spikeKnockbackpower = 200f;
    public float spikeKnockbackDuration = 1f;
    // event calls
    public static event EventHandler playerDeath;
    public static event Action<int, int> updateHealth = (playerHealth, maxHealth) => { };
    //animation
    public Animator animator;
    private SpriteRenderer playerSprite;
    private TextMesh callout;

    void Start()
    {
        //DontDestroyOnLoad(this);
        maxHealth = 3;
        health = maxHealth;
        moveSpeed = 7f;

        this.rb = this.GetComponent<Rigidbody2D>();
        this.playerSprite = transform.GetChild(transform.childCount - 1).GetComponent<SpriteRenderer>();
        this.callout = GameObject.FindGameObjectWithTag("PlayerCallout").GetComponent<TextMesh>();

        updateHealth?.Invoke(health, maxHealth);
    }

    void Update()
    {
        if (!UIScripts.gameIsPaused)
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

    public override void takeDamage(int damage)
    {
        if (!canTakeDamage) return;

        health -= damage;
        updateHealth?.Invoke(health, maxHealth);

        if (health <= 0)
        {
            die();
        }

        StartCoroutine(BecomeTemporarilyInvincible(Color.red));
    }

    public override void heal(int restoreAmount)
    {
        health = Mathf.Min(health + restoreAmount, maxHealth);
        updateHealth?.Invoke(health, maxHealth);
    }

    public void increaseStat(PlayerStat statToIncrease)
    {
        switch(statToIncrease)
        {
            case PlayerStat.Health:
                maxHealth += 1; 
                health = maxHealth;
                updateHealth?.Invoke(health, maxHealth);
                break;

            case PlayerStat.Speed:
                moveSpeed += 1f;
                break;
            
            case PlayerStat.Damage:
                extraDamage += .25f;
                break;

            case PlayerStat.Reload:
                reloadRecudtion *= .9f;
                break;
        }
        
    }

    public override void die()
    {
        animator.Play("PlayerDie");
        playerDeath?.Invoke(this, EventArgs.Empty);
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

        StartCoroutine(dashCooldown());
        StartCoroutine(BecomeTemporarilyInvincible(Color.gray));
    }

    public IEnumerator dashCooldown(){
        canDash = false;
        yield return new WaitForSeconds(DASH_COOLDOWN_MAX);
        canDash = true;
    }
    public IEnumerator BecomeTemporarilyInvincible(Color flashColor)
    {
        canTakeDamage = false;
        for (float i = 0; i < invincibilityDurationSeconds; i += invicibilityDeltaTime)
        {
            playerSprite.material.color = playerSprite.material.color == Color.white ? flashColor : Color.white;
            yield return new WaitForSeconds(invicibilityDeltaTime);
        }
        playerSprite.material.color = Color.white;
        canTakeDamage = true;
    }

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
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "spike")
        {
            StartCoroutine(Knockback(spikeKnockbackDuration, spikeKnockbackpower, other.transform));
            slowPlayer();
            takeDamage(1);
        }
    }

    public void slowPlayer(){
        StartCoroutine(slowPlayerMovement());
    }

    private IEnumerator slowPlayerMovement()
    {   
        moveSpeed = 5f;
        yield return new WaitForSeconds(.8f);
        moveSpeed = 7f;
    }
    public IEnumerator delayDeath()
    {
        yield return new WaitForSeconds(3);
    }
}
