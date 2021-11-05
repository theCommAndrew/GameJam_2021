using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : Bullet
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

        if (col.gameObject.tag == "enemy")
        {
            Character character = col.gameObject.GetComponent<Character>();
            print("Bullet hit enemy");
            character.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
