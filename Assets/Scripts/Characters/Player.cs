using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Camera cam;
    public bool canDash = true;
    public float dashDistance = 5f;
    private bool canTakeDamage = true;
    public LayerMask rayCastLayer = default;
    Vector2 movement;
    Vector2 mousePosition;
    private float dashCooldown;
    const float DASH_COOLDOWN_MAX = 1F;
    [SerializeField] private float invincibilityDurationSeconds = 1.5f;
    [SerializeField] private float invicibilityDeltaTime = 0.15f;
    public float spikeKnockbackpower = 200f;
    public float spikeKnockbackDuration = 1f;

    public event EventHandler playerDeath;
    public event EventHandler<UpdateHealthEvent> updateHealth;
    public class UpdateHealthEvent
    {
        public int playerHealth;
        public int maxHealth;
    }

    // weapon info
    public Weapon[] weapons;
    public int currentWeapon = 0;
    public AmmoInventory inventory;


    void Start()
    {
        maxHealth = 3;
        health = maxHealth;
        moveSpeed = 7f;

        cam = FindObjectOfType<Camera>();
        this.rb = this.GetComponent<Rigidbody2D>();

        updateHealth?.Invoke(this, new UpdateHealthEvent
        {
            playerHealth = health,
            maxHealth = maxHealth
        });
    }

    void Update()
    {
        if (!UIScripts.gameIsPaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && canDash && dashCooldown < 0)
            {
                dashCooldown = DASH_COOLDOWN_MAX;
                StartCoroutine(BecomeTemporarilyInvincible());
                Vector3 moveDir = new Vector3(movement.x, movement.y).normalized;
                Dash(moveDir);
            }

            dashCooldown -= Time.deltaTime;

            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetButton("Fire1"))
            {
                print($"firing {weapons[currentWeapon]}");
                weapons[currentWeapon].Fire(inventory);
            }

            if (Input.GetKeyDown(KeyCode.Tab) && weapons.Length > 1)
            {
                print("swapping weapon");
                weapons[currentWeapon].gameObject.SetActive(false);
                currentWeapon = (currentWeapon + 1) % 2;
                weapons[currentWeapon].gameObject.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);



        // Vector2 lookDirection = mousePosition - rb.position;
        // float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        // rb.rotation = angle;
    }

    public override void takeDamage(int damage)
    {
        if (!canTakeDamage) return;

        health -= damage;
        updateHealth?.Invoke(this, new UpdateHealthEvent
        {
            playerHealth = health,
            maxHealth = maxHealth
        });

        if (health <= 0)
        {
            die();
        }

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    public override void heal(int restoreAmount)
    {
        health += restoreAmount;
        updateHealth?.Invoke(this, new UpdateHealthEvent
        {
            playerHealth = health,
            maxHealth = maxHealth
        });
    }

    public override void die()
    {
        playerDeath?.Invoke(this, EventArgs.Empty);
    }

    private void Dash(Vector3 moveDir)
    {
        canDash = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, dashDistance, rayCastLayer);

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
        print("Invincible");
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
        if (other.tag == "Fire")
        {
            Destroy(gameObject);
            die();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "whip")
        {
            takeDamage(1);
            StartCoroutine(BecomeTemporarilyInvincible());
        }
        if (other.tag == "cobweb")
        {
            moveSpeed = 2f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.tag == "cobweb")
        {
            moveSpeed = 7f;
        }
    }

    private IEnumerator pausePlayerMovement()
    {
        moveSpeed = 3f;
        yield return new WaitForSeconds(.6f);
        moveSpeed = 7f;
    }
}
