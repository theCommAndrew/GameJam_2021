using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    public float shotInterval;
    public int bulletDamage = 1;
    public float bulletSpeed = 30f;
    public Vector3 bulletSize = new Vector3(.75f, .5f, 0);
    private float shotTimer = 0;


    void Start()
    {
        maxHealth = 25;   
        health = maxHealth;
        shotInterval = 2;
    }

    void Update()
    {    
        // adding Line Of Sight
        // Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        // RaycastHit2D LOS = Physics2D.Raycast(firePoint.transform.position, lookDirection, Mathf.Infinity);

        // shoot on interval
        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)// && LOS.collider.tag == "Player") 
        {
            shotTimer = 0;    
            shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed, bulletSize);
        }
    }
}
