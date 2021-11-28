using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    protected override void Awake()
    {
        fireDelta = .3f;
        bulletDamage = 5;
        bulletSpeed = 15f;
        ammoReserve.maxClip = 20;
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.maxCapacity = 120;
        ammoReserve.stock = ammoReserve.maxCapacity;
        ammoReserve.ammoPerShot = 1;

        reloadTime = 4;
    }
}
