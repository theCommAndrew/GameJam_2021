using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Character
{
    public Animator animator;
    protected String deathAnimation;
    // objects
    [SerializeField] protected Player player { get; set; }
    [SerializeField] protected GameObject firePoint;
    [SerializeField] protected GameObject bulletPrefab; // enemy projectile

    // movement/combat params
    public int lootChance = 10; // chance to drop something on death. int from 0-100
    public int contactDamage = 1; // damage done when running into player
    public float rotationSpeed = 5;
    public float knockbackPower = 300;
    public float knockbackDuration = 1;
    public float pauseDuration = .3f;

    // events
    public event EventHandler OnEnemySpawned;
    public static event Action<int, Transform> OnEnemyKilled;

    void Awake()
    {
        gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }


    public virtual void shoot(GameObject bulletPrefab, GameObject firePoint, int damage, float speed, Vector3 scale)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().speed = speed;
        bullet.GetComponent<Bullet>().scale = scale;
    }

    public void shootSpread(GameObject bulletPrefab, GameObject firePoint, int damage, float speed, int cone, int bullets)
    {
        float halfRange = cone / 2;
        float step = cone / (bullets - 1);
        for (float i = -1 * halfRange; i <= halfRange; i += step)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation * Quaternion.Euler(0, 0, i)) as GameObject;
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Bullet>().speed = speed;
        }
    }

    public void spawn()
    {
        OnEnemySpawned?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(true);
        StartCoroutine(spawnEnemy());
    }

    public IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(1f);
        var pathfinder = gameObject.GetComponent<AIDestinationSetter>();
        if (pathfinder != null)
            pathfinder.target = player.transform;
    }

    public void die()
    {
        if (this.alive)
        {
            this.alive = false;
            moveSpeed = 0;
            IAstarAI enemyMovement = gameObject.GetComponent<IAstarAI>();
            if (enemyMovement != null)
            {
                enemyMovement.canMove = false;
            }
            OnEnemyKilled?.Invoke(lootChance, this.transform);
            gameObject.GetComponent<Animator>().Play(deathAnimation);
            Destroy(gameObject, 1f);
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

    public virtual void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
        else
        {
            StartCoroutine(delayMovement());
        }
    }

    private IEnumerator delayMovement()
    {
        IAstarAI enemyMovement = GetComponent<IAstarAI>();
        if (enemyMovement != null)
        {
            enemyMovement.canMove = false;
            yield return new WaitForSeconds(pauseDuration);
            enemyMovement.maxSpeed = 4f;
            enemyMovement.canMove = true;
            yield return new WaitForSeconds(.8f);
            enemyMovement.maxSpeed = 8f;
        }
    }

}