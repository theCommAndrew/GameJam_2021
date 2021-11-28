using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaserEnemy : Enemy
{

    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        moveSpeed = 1f;
        deathAnimation = "ChaserDie";

    }
}
