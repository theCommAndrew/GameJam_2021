using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform dropPoint = transform.GetChild(0).transform;
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if(!player.flying)
            {
                player.takeDamage(1);
                player.slowPlayer();
                player.transform.position = dropPoint.position;
            }


        }
        /*
        else if(other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(!enemy.flying)
            {
                enemy.die();
            }
        }
        */
    }
}
