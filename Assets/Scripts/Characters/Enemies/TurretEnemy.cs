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

    //animation
    //public Animator animator;

    void Start()
    {
        maxHealth = 25;
        health = maxHealth;
        shotInterval = 2;
        deathAnimation = "TurretEnemyDie";
    }
    void FixedUpdate()
    {
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

    void Update()
    {
        // adding Line Of Sight
        // Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        // RaycastHit2D LOS = Physics2D.Raycast(firePoint.transform.position, lookDirection, Mathf.Infinity);

        // shoot on interval
        shotTimer += Time.deltaTime;
        if (shotTimer >= shotInterval && alive)// && LOS.collider.tag == "Player") 
        {
            shotTimer = 0;
            animator.Play("TurretEnemyShoot");
            shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed, bulletSize);
        }
    }

}
