using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 20;
        health = maxHealth;
        moveSpeed = 0;
    }
}
