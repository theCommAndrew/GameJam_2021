using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyBoy : Enemy
{
    public float shotInterval;
    public int bulletDamage = 1;
    public float bulletSpeed = 30f;
    public Vector3 bulletSize = new Vector3(4, 4, 0);
    private float shotTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15;
        health = maxHealth;
        moveSpeed = 0f;
        deathAnimation = "ShootyBoyDie";
    }
    void Update()
    {
        shotTimer += Time.deltaTime;
        if (shotTimer >= shotInterval && alive)// && LOS.collider.tag == "Player") 
        {
            shotTimer = 0;
            animator.Play("ShootyBoyShoot");
            shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed, bulletSize);
        }
    }
}
