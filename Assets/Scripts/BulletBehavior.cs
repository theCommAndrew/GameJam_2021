using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage = 5;
    void Start()
    {    }

    void Update()
    {    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "wall")
        {
            print("bullet hit wall");
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "character")
        {
            Character character = col.gameObject.GetComponent<Character>();
            print("Bullet hit character");
            character.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
