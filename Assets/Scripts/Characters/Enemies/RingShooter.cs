using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingShooter : Enemy
{

    public float actionInverval = 2;
    public int bulletDamage = 1;
    public float bulletSpeed = 15f;
    private float actionTimer = 0;

    void Start()
    {
        maxHealth = 40;
        health = maxHealth;
        lootChance = 50;
        moveSpeed = 0f;
    }

    private void Update() {
        actionTimer += Time.deltaTime;
        if(actionTimer >= actionInverval)
        {
            actionTimer = 0;    
            shootSpread(bulletPrefab, firePoint, bulletDamage, bulletSpeed, 360, 12);
        }
    }

    
}
