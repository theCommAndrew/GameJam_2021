using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class Weapon : MonoBehaviour
{
    // Contains definitions for a default gun
    // shooting info
    [SerializeField] private GameObject bulletPrefab;
    public int bulletDamage = 5;
    public float bulletSpeed = 20f;
    public Vector3 bulletSize = new Vector3(.4f, .4f, 0);
    [SerializeField] private GameObject firePoint;
    // ammo info
    public struct GunAmmo{
        public int stock;
        public int maxCapacity;
        public int ammoPerShot;
    }
    public GunAmmo ammoReserve = new GunAmmo(); 
    // shot timing
    private float myTime = 0f;
    public float fireDelta;  
    // pickup stuff
    private bool pickupAllowed = false;

    protected virtual void Awake() {
        fireDelta = 0.5f;
        ammoReserve.stock = 100;
        ammoReserve.maxCapacity = 100;
        ammoReserve.ammoPerShot = 1;
    }

    protected virtual void Update() {
        myTime += Time.deltaTime;

        if(pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            WeaponHolder playerWeapons = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponHolder>();
            playerWeapons.addWeapon(this);
        }
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

    public bool Fire(){
        if(myTime > fireDelta)
        {
            int shotsFired = spendAmmo(ammoReserve.ammoPerShot);
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

    public (int, int) GetCurrentAmmo(){
        return (ammoReserve.stock, ammoReserve.maxCapacity);
    }

    // add [amount] ammo to stock, returns amount added
    public int collectAmmo(int amount){
        int collect = Mathf.Min(amount, ammoReserve.maxCapacity - ammoReserve.stock);
        ammoReserve.stock += collect;
        return collect;
    }    

    // spend [amount] ammo, returns amount spent    
    public int spendAmmo(int amount){
        int spend = Mathf.Min(amount, ammoReserve.stock);
        ammoReserve.stock -= spend;
        return spend;
    }

    public Sprite getImage(){
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
