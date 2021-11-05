using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void Start()
    {    }

    void Update()
    {    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Player")
        {
            Character character = col.gameObject.GetComponent<Character>();
            //print("Bullet hit player");
            character.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}