using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeEnemy : Enemy
{
    public bool canBeDamaged;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        moveSpeed = 0;
        canBeDamaged = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Can be damaged");
        canBeDamaged = true;
    }
}
