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
            Character character = col.gameObject.GetComponent<Character>();
            if(character.health != character.maxHealth){
                character.heal(restoreAmount);
                Destroy(gameObject);
            }            
        }
    }


}
