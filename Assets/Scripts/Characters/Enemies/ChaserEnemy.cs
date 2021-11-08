using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChaserEnemy : Enemy
{
    private void Awake(){
        maxHealth = 20;
        health = maxHealth;   
        moveSpeed = 1f;
    }
    
    void Start()
    {    }

    void Update()
    {    }

    void FixedUpdate() {
        // follow player position
        /*
        Vector2 toTarget = player.transform.position - transform.position;
         
        transform.Translate(toTarget * moveSpeed * Time.deltaTime);
        */
     }

}
