using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaserEnemy : Enemy
{

    //animation
    public Animator animator;
    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        moveSpeed = 1f;

    }

    public override void die()
    {
        IAstarAI enemyMovement = GetComponent<IAstarAI>();
        enemyMovement.canMove = false;
        animator.Play("ChaserDie");
        Destroy(gameObject, 1f);
    }
}
