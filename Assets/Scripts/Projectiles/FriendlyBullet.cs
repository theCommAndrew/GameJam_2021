using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(damage);
        }

        if(tag != "Player" && tag != "backend" && tag != "bullet")
        {
            Destroy(gameObject);
        }
    }
}
