using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrabTank : Enemy
{   
    private List<GameObject> cannons = new List<GameObject>();
    private float actionTimer = 0;
    public float actionInterval = .5f;
    public int bulletDamage = 1;
    public float bulletSpeed = 20f;
    private Action[] actions;
    private int rand;
    private float lookOffset = 90f;


    void Start()
    {;
        maxHealth = 200;
        health = maxHealth;
        moveSpeed = 1f;
        deathAnimation = "BasicEnemyDie";

        actions = new Action[] { shootCannons, shootWave, fireRing };

        for(int i = 0; i < transform.childCount; i++)
        {
            cannons.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update() {
        actionTimer += Time.deltaTime;
        if(actionTimer >= actionInterval)
        {
            rand = Random.Range(0, actions.Length);
            actions[rand].Invoke();
            actionTimer = 0;
        }

        if(player.alive)
        {
            Vector3 lookDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - lookOffset));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void shootCannons()
    {
        foreach(GameObject cannon in cannons)
        {
            shootSpread(bulletPrefab, cannon, bulletDamage, bulletSpeed, 30, 3);
        }
    }

    private void shootWave()
    {
        shoot(bulletPrefab, gameObject, bulletDamage, bulletSpeed, new Vector3(30f,5f,1f));
    }

    private void fireRing()
    {
       shootSpread(bulletPrefab, gameObject, bulletDamage, bulletSpeed, 360, 24);
    }

}
