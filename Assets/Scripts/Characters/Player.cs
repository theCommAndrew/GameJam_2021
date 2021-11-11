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


    public event EventHandler<UpdateHealthEvent> updateHealth;
    public class UpdateHealthEvent
    {
        public int playerHealth;
        public int maxHealth;
    }

    //shooting variables
    public GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    public float fireDelta = 0.5f;
    public int bulletDamage = 5;
    public float bulletSpeed = 20f;
    public Vector3 bulletSize = new Vector3(.5f, .5f, 0);
    private float nextFire = 0.1f;
    private float myTime = 0.0f;

    void Start()
    {
        maxHealth = 3;
        health = maxHealth;
        moveSpeed = 7f;

        cam = FindObjectOfType<Camera>();

        updateHealth?.Invoke(this, new UpdateHealthEvent
        {
            playerHealth = health,
            maxHealth = maxHealth
        });
    }

    void Update()
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

        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;

            shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed, bulletSize);

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
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
        health = Math.Min(maxHealth, health + restoreAmount);
        updateHealth?.Invoke(this, new UpdateHealthEvent
        {
            playerHealth = health,
            maxHealth = maxHealth
        });
    }

    public override void die()
    {
        Time.timeScale = 0;
        alive = false;
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
            timer += Time.deltaTime;
            Vector2 dir = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-dir * knockbackPower);
        }
        yield return 0;
    }
}
