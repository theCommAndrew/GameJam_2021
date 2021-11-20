using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // movement
    Vector2 movement;
    // dashing
    public bool canDash = true;
    public float dashDistance = 5f;
    private bool canTakeDamage = true;
    private float dashCooldown;
    const float DASH_COOLDOWN_MAX = 1F;
    // damage and invincibility
    [SerializeField] private float invincibilityDurationSeconds = 1.5f;
    [SerializeField] private float invicibilityDeltaTime = 0.15f;
    public float spikeKnockbackpower = 200f;
    public float spikeKnockbackDuration = 1f;
    // event calls
    public static event EventHandler playerDeath;
    public static event Action<int,int> updateHealth = (playerHealth, maxHealth) => {};


    void Start()
    {
        maxHealth = 3;
        health = maxHealth;
        moveSpeed = 7f;

        this.rb = this.GetComponent<Rigidbody2D>();

        updateHealth?.Invoke(health, maxHealth);
    }

    void Update()
    {
        if (!UIScripts.gameIsPaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if(Input.GetKeyDown(KeyCode.Space) && canDash && dashCooldown < 0)
            {
                dashCooldown = DASH_COOLDOWN_MAX;
                StartCoroutine(BecomeTemporarilyInvincible());
                Vector3 moveDir = new Vector3(movement.x, movement.y).normalized;
                Dash(moveDir);
            }

            dashCooldown -= Time.deltaTime;
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

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    public override void heal(int restoreAmount)
    {
        health += restoreAmount;
        updateHealth?.Invoke(health, maxHealth);
    }

    public override void die()
    {
        playerDeath?.Invoke(this, EventArgs.Empty);
    }

    private void Dash(Vector3 moveDir)
    {
        canDash = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, dashDistance, default); // can set this as a variable if it needs to change

        if (hit.collider == null)
        {
            transform.position += moveDir * dashDistance;
        }
        else
        {
            transform.position = hit.point;
        }

        canDash = true;
    }
    public IEnumerator BecomeTemporarilyInvincible()
    {
        canTakeDamage = false;
        for (float i = 0; i < invincibilityDurationSeconds; i += invicibilityDeltaTime)
        {
            yield return new WaitForSeconds(invicibilityDeltaTime);
        }
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
            StartCoroutine(pausePlayerMovement());
            takeDamage(1);
        }
    }

    private IEnumerator pausePlayerMovement()
    {
        moveSpeed = 5f;
        yield return new WaitForSeconds(.8f);
        moveSpeed = 7f;
    }
}
