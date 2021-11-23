using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon{

    public int cone;
    public int bullets;
    protected override void Awake() {
        fireDelta = .5f;
        bulletDamage = 5;
        bulletSpeed = 15f;
        ammoReserve.maxClip = 2;
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.maxCapacity = 50;
        ammoReserve.stock = ammoReserve.maxCapacity;
        ammoReserve.ammoPerShot = 1;

        reloadTime = 1;

        cone = 30;
        bullets = 3;
    }

    public override void shoot(GameObject bulletPrefab, GameObject firePoint, int damage, float speed, Vector3 scale)
    {   
        float halfRange = cone/2;
        float step = cone/(bullets-1);
        for(float i = -1*halfRange; i <= halfRange; i += step)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation * Quaternion.Euler(0,0,i)) as GameObject;
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Bullet>().speed = speed;
        }
    }

}
