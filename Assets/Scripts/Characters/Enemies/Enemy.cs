using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public int contactDamage{get; set;} // damage done when running into player
    public float bulletForce{get; set;}
    public int lootChance{get; set;} // chance to drop something on death. int from 0-100
    [SerializeField] protected GameObject player{get; set;}
    [SerializeField] protected GameObject firePoint;
    [SerializeField] protected GameObject bulletPrefab; // enemy projectile
    [SerializeField] private GameObject heartPickupPrefab; // healing item

    public event EventHandler OnEnemySpawned;
    public event EventHandler OnEnemyKilled;

    void Awake()
    {
        gameObject.SetActive(false);
        bulletForce = 20f;
        lootChance = 50;
        contactDamage = 1;
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        // look at player
        Vector2 lookDirection = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void spawn(){
        gameObject.SetActive(true);
        OnEnemySpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void die()
    {
        moveSpeed = 0;
        OnEnemyKilled?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
        if(generalFunctions.getPercentResult(lootChance))
            dropLoot();
    }

    public void dropLoot()
    {
        GameObject heart = Instantiate(heartPickupPrefab, this.transform.position, this.transform.rotation);
        // make sure hearts always face upwards when dropped
        Rigidbody2D heartBody = heart.GetComponent<Rigidbody2D>();
        heartBody.SetRotation(0);
    }

    // contact damage to player
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            Character character = other.GetComponent<Character>();
            character.takeDamage(contactDamage);
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            Character character = other.GetComponent<Character>();
            character.takeDamage(contactDamage);
        }
    }
}