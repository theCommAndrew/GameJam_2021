using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class rapidGun : Weapon{

    protected override void Awake() {
        fireDelta = .25f;
        bulletDamage = 3;
        bulletSpeed = 30f;
    }

}