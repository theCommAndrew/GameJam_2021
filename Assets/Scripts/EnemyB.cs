using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Character
{
    public GameObject player;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float shotInterval;
    private float shotTimer = 0;

    void Start()
    {
        health = 25;   
        shotInterval = 3;
    }

    void Update()
    {    
        // shoot
        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)
        {
            shotTimer = 0;               
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
            bulletBody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // look at player
        Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;


    }

    public override void Die(){
        Debug.Log("Blah, I am dead");
        Destroy(gameObject, .5f);
    }
}
