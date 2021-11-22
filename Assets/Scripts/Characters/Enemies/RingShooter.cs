using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingShooter : Enemy
{

    public float shotInterval = 2;
    public int bulletDamage = 1;
    public float bulletSpeed = 15f;
    private float shotTimer = 0;
    private float dashTimer = 0;

    void Start()
    {
        maxHealth = 40;
        health = maxHealth;
        lootChance = 50;
        moveSpeed = 0f;
    }

    private void Update() {
        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)
        {
            shotTimer = 0;    
            shootSpread(bulletPrefab, firePoint, bulletDamage, bulletSpeed, 360, 12);
        }
    }

    
}
