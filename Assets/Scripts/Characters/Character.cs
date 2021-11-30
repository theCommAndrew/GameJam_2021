using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all character game objects
public abstract class Character : MonoBehaviour
{
    public float moveSpeed;

    public float maxSpeed;
    public bool alive { get; set; }
    public int maxHealth { get; set; }
    public int health { get; set; }
    public bool flying = false;
    protected Rigidbody2D rb;


    protected Character()
    {
        alive = true;
    }
}