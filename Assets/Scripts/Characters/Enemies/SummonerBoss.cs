using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SummonerBoss : Enemy
{   
    private List<GameObject> spawners = new List<GameObject>();
    private float actionTimer = 0;
    public float actionInterval = 1.2f;
    public int bulletDamage = 1;
    public float bulletSpeed = 20f;
    private Action[] actions;
    private int rand;
    private float lookOffset = 90f;
    public float dashDistance = 5f;
    public GameObject dashEffect;
    public GameObject chaser;


    void Start()
    {;
        maxHealth = 200;
        health = maxHealth;
        moveSpeed = 1f;
        deathAnimation = "ChaserDie";

        actions = new Action[] { summon, move, fireRing };

        for(int i = 0; i < transform.childCount; i++)
        {
            spawners.Add(transform.GetChild(i).gameObject);
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
    }

    private void summon()
    {
        var enemyCount = GameObject.FindGameObjectsWithTag("enemy");
        if(enemyCount.Length < 4)
        {
            foreach(GameObject point in spawners)
            {
                GameObject minion = Instantiate(chaser, point.transform.position, Quaternion.identity);
                minion.GetComponent<Enemy>().spawn();
            }
        }
        else{
            move();
        }
        
    }

    private void move()
    {
        var moveDir = (Vector3)Random.insideUnitCircle.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, dashDistance, LayerMask.GetMask("Walls")); // can set this as a variable if it needs to change

        float particleAngle = Vector3.Angle(moveDir, transform.up);
        particleAngle = moveDir.x > 0 ? -particleAngle : particleAngle;
        var dasheffectvar = Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, particleAngle));

        if (hit.collider == null)
            transform.position += moveDir * dashDistance;
        else
            transform.position -= moveDir * dashDistance;
        animator.Play("RingShooterDash");
    }

    private void fireRing()
    {
       shootSpread(bulletPrefab, gameObject, bulletDamage, bulletSpeed, 360, 12);
    }

}
