using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Contains definitions for a default gun
    // shooting info
    [SerializeField] private GameObject bulletPrefab;
    public int bulletDamage = 5;
    public float bulletSpeed = 20f;
    [SerializeField] private GameObject firePoint;
    // ammo info
    public struct GunAmmo{
        public int inClip;
        public int maxClip;
        public int stock;
        public int maxCapacity;
        public int ammoPerShot;
    }
    public GunAmmo ammoReserve = new GunAmmo();
    // shot timing
    private float myTime = 0f;
    public float fireDelta;  
    public float reloadTime;
    public bool reloading = false;
    // pickup stuff
    private bool pickupAllowed = false;
    private WeaponHolder playerWeapons;

    protected virtual void Awake()
    {
        fireDelta = 0.5f;
        ammoReserve.maxClip = 10;
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.maxCapacity = 100;
        ammoReserve.stock = ammoReserve.maxCapacity;
        ammoReserve.ammoPerShot = 1;

        reloadTime = 2;
    }

    private void Start() {
        playerWeapons = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponHolder>();
    }

    protected virtual void Update()
    {
        myTime += Time.deltaTime;

        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
        {
            WeaponHolder playerWeapons = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponHolder>();
            playerWeapons.addWeapon(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupAllowed = false;
        }
    }

    public bool Fire()
    {
        if (myTime > fireDelta)
        {
            int shotsFired = spendAmmo(ammoReserve.ammoPerShot);
            if (shotsFired >= 1)
            {
                shoot(bulletPrefab, firePoint, bulletDamage, bulletSpeed);
                myTime = 0.0f;
            }
            else if(ammoReserve.inClip == 0 && ammoReserve.stock > 0 && !reloading){
                reloading = true;
                StartCoroutine(reload());
            }
            return shotsFired > 0;
        }
        return false;
    }

    public virtual void shoot(GameObject bulletPrefab, GameObject firePoint, int damage, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Bullet>().speed = speed;
    }

    public (int, int) GetCurrentAmmo(){
        return (ammoReserve.inClip, ammoReserve.stock);
    }

    // add [amount] ammo to stock, returns amount added
    public int collectAmmo(int amount)
    {
        int collect = Mathf.Min(amount, ammoReserve.maxCapacity - ammoReserve.stock);
        ammoReserve.stock += collect;
        return collect;
    }

    // spend [amount] ammo, returns amount spent    
    public int spendAmmo(int amount){
        int spend = Mathf.Min(amount, ammoReserve.inClip);
        ammoReserve.inClip -= spend;
        return spend;
    }

    public Sprite getImage()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public IEnumerator reload()
    {
        yield return new WaitForSeconds(this.reloadTime);

        int loadingAmmo = Mathf.Min(ammoReserve.maxClip - ammoReserve.inClip, ammoReserve.stock);
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.stock -= loadingAmmo;

        reloading = false;
        playerWeapons.updateUIAmmo();
    }
}
