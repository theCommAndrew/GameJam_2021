using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        fireDelta = .15f;
        bulletDamage = 20;
        bulletSpeed = 40f;
        ammoReserve.maxClip = 1;
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.maxCapacity = 10;
        ammoReserve.stock = ammoReserve.maxCapacity;
        ammoReserve.ammoPerShot = 1;

        reloadTime = 5;
    }
}
