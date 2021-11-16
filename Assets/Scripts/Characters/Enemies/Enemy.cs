using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Character
{
    // objects
    [SerializeField] protected Player player{get; set;}
    [SerializeField] protected GameObject firePoint;
    [SerializeField] protected GameObject bulletPrefab; // enemy projectile
    [SerializeField] private GameObject heartPickupPrefab; // healing item
    [SerializeField] private GameObject ammoPickup;

    // movement/combat params
    public int lootChance{get; set;} // chance to drop something on death. int from 0-100
    public int contactDamage{get; set;} // damage done when running into player
    public float rotationSpeed = 5;
    public float knockbackPower = 300;
    public float knockbackDuration = 1;
    public float pauseDuration = .3f;

    // events
    public event EventHandler OnEnemySpawned;
    public event EventHandler OnEnemyKilled;

    void Awake()
    {
        gameObject.SetActive(false);
        lootChance = 50;
        contactDamage = 1;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // look at player
        float offset = 90f;
        Vector3 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - offset));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void spawn()
    {
        // fire OnEnemySpawned event
        gameObject.SetActive(true);
        OnEnemySpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void die()
    {
        moveSpeed = 0;
        OnEnemyKilled?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
        if (generalFunctions.getPercentResult(lootChance))
            dropLoot();
    }

    public void dropLoot()
    {
        if(generalFunctions.getPercentResult(30))
        {
            GameObject heart = Instantiate(heartPickupPrefab, this.transform.position, Quaternion.Euler(0,0,0));
        }
        else{
            GameObject ammo = Instantiate(ammoPickup, this.transform.position, Quaternion.Euler(0,0,0));
        }
    }

    // contact damage to player
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            StartCoroutine(player.Knockback(knockbackDuration, knockbackPower, this.transform));
            player.takeDamage(contactDamage);
            StartCoroutine(delayMovement());
            StartCoroutine(player.BecomeTemporarilyInvincible());
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            player.takeDamage(contactDamage);
        }
    }

    private IEnumerator delayMovement()
    {
        IAstarAI enemyMovement = GetComponent<IAstarAI>();
        enemyMovement.canMove = false;
        yield return new WaitForSeconds(pauseDuration);
        enemyMovement.maxSpeed = 4f;
        enemyMovement.canMove = true;
        yield return new WaitForSeconds(.8f);
        enemyMovement.maxSpeed = 8f;
    }
}