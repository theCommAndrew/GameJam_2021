using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class RingShooter : Enemy
{

    private float actionTimer = 0;
    public float actionInverval = .25f;
    public int bulletDamage = 1;
    public float bulletSpeed = 25f;
    public float rotateSpeed = 90;
    public float dashDistance = 5f;
    public GameObject dashEffect;
    private Action[] actions;
    private int rand;

    

    void Start()
    {
        maxHealth = 40;
        health = maxHealth;
        lootChance = 75;
        moveSpeed = 0f;
        deathAnimation = "BasicEnemyDie";

        actions = new Action[]{fireRing, move};
    }
    

    private void Update() {
        if(alive)
        {
            actionTimer += Time.deltaTime;
            if(actionTimer >= actionInverval)
            {   
                rand = Random.Range(0,actions.Length);
                actions[rand].Invoke();
                actionTimer = 0;    
            }
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }
    }

    private void fireRing(){
        shootSpread(bulletPrefab, firePoint, bulletDamage, bulletSpeed, 360, 24);
    }

    private void move()
    {
        var moveDir = (Vector3)Random.insideUnitCircle.normalized;
        print(moveDir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, dashDistance, LayerMask.GetMask("Walls")); // can set this as a variable if it needs to change

        float particleAngle = Vector3.Angle(moveDir, transform.up);
        particleAngle = moveDir.x > 0 ? -particleAngle : particleAngle;
        print(particleAngle);
        var dasheffectvar = Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, particleAngle));

        if (hit.collider == null)
            transform.position += moveDir * dashDistance;         
        else
            transform.position -= moveDir * dashDistance;

        fireRing();
    }

    
}
