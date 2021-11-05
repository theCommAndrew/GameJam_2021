using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Character
{
    void Start()
    {
        health = 20;   
        moveSpeed = 1f;
    }

    void Update()
    {    }

    void FixedUpdate() {
         Vector2 toTarget = player.transform.position - transform.position;
         
         transform.Translate(toTarget * moveSpeed * Time.deltaTime);
     }

    public override void Die(){
        Debug.Log("Blah, I am dead");
        Destroy(gameObject, .5f);
    }
}
