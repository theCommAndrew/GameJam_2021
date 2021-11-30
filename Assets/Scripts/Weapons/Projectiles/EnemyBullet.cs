using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.takeDamage(damage);
        }

        if (tag != "enemy" && tag != "backend" && tag != "bullet")
        {
            Destroy(gameObject);
        }
    }
}