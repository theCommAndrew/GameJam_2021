using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class rapidGun : Weapon{

    protected override void Awake() {
        fireDelta = .15f;
        bulletDamage = 4;
        bulletSpeed = 30f;
        ammoReserve.stock = 200;
        ammoReserve.maxCapacity = 200;
        ammoReserve.ammoPerShot = 1;
    }

}
