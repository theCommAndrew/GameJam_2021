using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Camera cam;
    public GameObject bulletPrefab; 
    Vector2 movement;
    Vector2 mousePosition;

    public float bulletForce = 20f;

    void Start(){
        health = 50;
        moveSpeed = 5f;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation) as GameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(this.transform.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public override void Die(){
        Destroy(gameObject, .5f);
    }
}
