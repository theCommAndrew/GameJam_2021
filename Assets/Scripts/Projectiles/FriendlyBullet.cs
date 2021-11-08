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
        string tag = col.gameObject.tag;
        if (tag == "enemy")
        {
            Character character = col.gameObject.GetComponent<Character>();
            if(character.alive)
            {
                character.takeDamage(damage);
            }
        }

        if (tag != "Player" && tag != "backend")
        {
            Destroy(gameObject);
        }
    }
}
