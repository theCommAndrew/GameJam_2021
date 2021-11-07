using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Camera cam;
    public GameObject bulletPrefab;
    public UIScripts uiScripts;
    public float fireDelta = 0.5f;
    public bool canDash = true;
    public float dashDistance = 5f;
    public LayerMask rayCastLayer = default;
    Vector2 movement;
    Vector2 mousePosition;

    public float bulletForce = 20f;
    private float nextFire = 0.1f;
    private float myTime = 0.0f;

    void Start()
    {
        maxHealth = 3;
        health = maxHealth;
        moveSpeed = 7f;

        uiScripts = FindObjectOfType<UIScripts>();
        uiScripts.Start();
        uiScripts.updateHealthBar();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            Vector3 moveDir = new Vector3(movement.x, movement.y).normalized;
            Dash(moveDir);
        }

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation) as GameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(this.transform.up * bulletForce, ForceMode2D.Impulse);

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
        health -= damage;
        uiScripts.updateHealthBar();
        if (health <= 0)
        {
            die();
        }
    }

    public override void heal(int restoreAmount)
    {
        health = Math.Min(maxHealth, health + restoreAmount);
        uiScripts.updateHealthBar();
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
}
