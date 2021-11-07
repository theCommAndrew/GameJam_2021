using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int restoreAmount;
    void Start()
    {
        restoreAmount = 1;
    }

    void Update()
    { }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Character character = col.gameObject.GetComponent<Character>();
            character.heal(restoreAmount);
            Destroy(gameObject);
        }
    }


}
