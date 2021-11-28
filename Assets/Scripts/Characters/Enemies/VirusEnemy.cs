using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemy : Enemy
{

    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        moveSpeed = 0f;
        lootChance = 100;
        Player player = GetComponent<Player>();
        deathAnimation = "VirusEnemyDie";
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


}
