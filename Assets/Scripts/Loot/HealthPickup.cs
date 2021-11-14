using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int restoreAmount;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            if(player.health != player.maxHealth){
                player.heal(restoreAmount);
                Destroy(gameObject);
            }              
        }
    }
}
