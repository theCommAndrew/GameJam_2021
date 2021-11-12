using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public AmmoType ammoType;
    public int ammoPerShot = 1;
    public int bulletDamage = 5;
    public float bulletSpeed = 20f;
    public Vector3 bulletSize = new Vector3(.5f, .5f, 0);
    [SerializeField] private GameObject firePoint;
    // shot timing
    float myTime;
    public float fireDelta;

    protected virtual void Awake() {
        myTime = 0.0f;
        fireDelta = 0.5f;
    }

    protected virtual void Update() {
        myTime += Time.deltaTime;
    }

    public bool Fire(AmmoInventory ammo){
        print($"waiting until {myTime} > {fireDelta}");
        if(myTime > fireDelta)
        {
            print("timing right");
            int shotsFired = ammo.Spend(ammoType, ammoPerShot);
            print(shotsFired);
            if(shotsFired >= 1){
                print("shooting bullet");
                shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed, bulletSize);
                myTime = 0.0f;
            }
            return shotsFired > 0;
        }
        return false;
    }

    public virtual void shoot(GameObject bulletPrefab, GameObject firePoint, int damage, float speed, Vector3 scale){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().speed = speed;
        bullet.GetComponent<Bullet>().scale = scale;

        print("bullet away");
    }
}
