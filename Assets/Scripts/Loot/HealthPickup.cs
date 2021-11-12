using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int restoreAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Character character = col.gameObject.GetComponent<Character>();
            Player player = col.gameObject.GetComponent<Player>();
            if (player.health != player.maxHealth)
            {
                print("Should be healing 1 here");
                player.heal(restoreAmount);
                Destroy(gameObject);

            }
        }
    }


}
