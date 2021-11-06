using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    public float shotInterval;
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
            shoot();
        }
    }
}
