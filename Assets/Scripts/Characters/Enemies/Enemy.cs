using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public int contactDamage{get; set;} // damage done when running into player
    public float bulletForce{get; set;}
    public int lootChance{get; set;} // chance to drop something on death. int from 0-100
    public GameObject enemyBulletPrefab; // enemy projectile
    public GameObject heartPickupPrefab; // healing item

    protected Enemy(){
        bulletForce = 20f;
        lootChance = 50;
        contactDamage = 1;  
    }
    void Start()
    {    }

    void Update()
    {    }
  
    void FixedUpdate()
    {
        // look at player
        Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public override void die(){
        moveSpeed = 0;
        Destroy(gameObject, .5f);
        if(generalFunctions.getPercentResult(lootChance))
            dropLoot();

    }

    public void dropLoot(){

        GameObject heart = Instantiate(heartPickupPrefab, this.transform.position, this.transform.rotation);
        // make sure hearts always face upwards when dropped
        Rigidbody2D heartBody = heart.GetComponent<Rigidbody2D>();
        heartBody.SetRotation(0);
    }

    // contact damage to player
    private void OnCollisionEnter2D(Collision2D col) {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            Character character = other.GetComponent<Character>();
            character.takeDamage(contactDamage);
        }
    }
    
    public virtual void shoot(){
        GameObject bullet = Instantiate(enemyBulletPrefab, this.transform.position, this.transform.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(this.transform.up * bulletForce, ForceMode2D.Impulse);
    }
}