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
        Player player = GetComponent<Player>();
    }
    private void Update()
    {
        if (maxHealth > 0)
        {
            player.moveSpeed = 2f;
        }
    }

}
