using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all character game objects
public abstract class Character : MonoBehaviour
{
    public float moveSpeed{get; set;}
    public int maxHealth{get; set;}
    public int health{get; set;}
    public Rigidbody2D rb;
    public GameObject player;

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }    
    }

    public void heal(int healing)
    {
        health = Math.Min(maxHealth, health + healing);
    }

    public virtual void Die()
    {
        //
    }

    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    void Update(){}

}