using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabTank : Enemy
{   
    private List<GameObject> cannons = new List<GameObject>();
    public float actionInterval = 2;
    public int bulletDamage = 1;
    public float bulletSpeed = 20f;
    private float shotTimer = 0;

    void Start()
    {;
        maxHealth = 100;
        health = maxHealth;
        moveSpeed = 1f;

        for(int i = 0; i < transform.childCount; i++)
        {
            cannons.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update() {
        shotTimer += Time.deltaTime;
        if(shotTimer >= actionInterval)
        {
            shotTimer = 0;    
            shootCannons();
        }
    }

    private void shootCannons()
    {
        foreach(GameObject cannon in cannons)
        {
            shootSpread(bulletPrefab, cannon, bulletDamage, bulletSpeed, 30, 3);
        }
    }

}
