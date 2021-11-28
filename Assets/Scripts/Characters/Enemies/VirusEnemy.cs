using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemy : Enemy
{

    public Animator animator;
    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        moveSpeed = 0f;
        lootChance = 100;
        Player player = GetComponent<Player>();
    }
    private void Update()
    {
        if (alive)
        {
            player.canDash = false;
        }
        else
        {
            player.canDash = true;
        }
    }
    public override void die()
    {
        animator.Play("VirusEnemyDie");
        Destroy(gameObject, 1f);
    }

}
