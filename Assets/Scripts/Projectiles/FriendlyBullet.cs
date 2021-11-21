using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        InRangeEnemy shieldEnemy = GetComponent<InRangeEnemy>();
        string tag = col.gameObject.tag;
        if (tag == "enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(damage);
        }

        if (tag == "shieldEnemy" && shieldEnemy.canBeDamaged == true)
        {
            shieldEnemy.takeDamage(damage);
        }

        if (tag != "Player" && tag != "backend" && tag != "bullet")
        {
            Destroy(gameObject);
        }
    }
}
