using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rapidGun : Weapon{

    protected override void Awake() {
        fireDelta = .15f;
        bulletDamage = 2;
        bulletSpeed = 30f;
        ammoReserve.maxClip = 25;
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.maxCapacity = 150;
        ammoReserve.stock = ammoReserve.maxCapacity;
        ammoReserve.ammoPerShot = 1;

        reloadTime = 3;
    }

}
