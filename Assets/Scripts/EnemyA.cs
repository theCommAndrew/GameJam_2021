using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Character
{
    public int damage; // damage done when running into player
    void Start()
    {
        maxHealth = 20;
        health = maxHealth;   
        moveSpeed = 1f;
        damage = 5;
    }

    void Update()
    {    }

    void FixedUpdate() {
        Vector2 toTarget = player.transform.position - transform.position;
         
        transform.Translate(toTarget * moveSpeed * Time.deltaTime);
     }

    public override void Die(){
        moveSpeed = 0;
        Destroy(gameObject, .5f);
    }

   private void OnCollisionEnter2D(Collision2D col) {
        GameObject other = col.gameObject;
        if (other.tag == "Player")
        {
            Character character = other.GetComponent<Character>();
            character.takeDamage(damage);
        }
    }
}
