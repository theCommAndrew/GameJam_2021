using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void Start()
    {  
        damage = 1;
    }

    void Update()
    {    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if(tag == "Player")
        {
            Character character = col.gameObject.GetComponent<Character>();
            //print("Bullet hit player");
            character.takeDamage(damage);
        }

        if(tag != "enemy" && tag != "backend")
        {
            Destroy(gameObject);
        }
    }
}