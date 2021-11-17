using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class Weapon : MonoBehaviour
{
    // Contains definitions for a default gun
    [SerializeField] private GameObject bulletPrefab;
    public AmmoType ammoType;
    public int ammoPerShot = 1;
    public int bulletDamage = 5;
    public float bulletSpeed = 20f;
    public Vector3 bulletSize = new Vector3(.4f, .4f, 0);
    [SerializeField] private GameObject firePoint;
    // shot timing
    float myTime = 0f;
    public float fireDelta;
    // pickup stuff
    private bool pickedUp = false;
    private bool pickupAllowed = false;

    protected virtual void Awake() {
        fireDelta = 0.5f;
    }

    protected virtual void Update() {
        myTime += Time.deltaTime;

        if(pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            print("being picked up");
            //transform.parent = other.GetComponentInChildren<WeaponAim>().transform;
        }
    }

    public bool Fire(AmmoInventory ammo){
        if(myTime > fireDelta)
        {
            int shotsFired = ammo.Spend(ammoType, ammoPerShot);
            if(shotsFired >= 1){
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            pickupAllowed = false;
        }
    }
}
