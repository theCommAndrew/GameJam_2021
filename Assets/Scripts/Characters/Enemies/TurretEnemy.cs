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
        // shoot on interval
        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)
        {
            shotTimer = 0;               
            shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed, bulletSize);
        }
    }
}
