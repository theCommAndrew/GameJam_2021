using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStats;

public class PickupItem : MonoBehaviour
{
    public PlayerStat stat;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.increaseStat(stat);         
            Destroy(gameObject);
        }
    }
}
