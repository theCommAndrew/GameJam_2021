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
        lootChance = 75;
        moveSpeed = 0f;
        deathAnimation = "BasicEnemyDie";
    }
    

    private void Update() {
        actionTimer += Time.deltaTime;
        if(actionTimer >= actionInverval)
        {
            actionTimer = 0;    
            shootSpread(bulletPrefab, firePoint, bulletDamage, bulletSpeed, 360, 12);
        }

        if (alive)
        {
            // look at player
            float offset = 0f;
            Vector3 lookDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - offset));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

    }

    
}
