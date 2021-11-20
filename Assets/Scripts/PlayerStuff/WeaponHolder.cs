using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class WeaponHolder : MonoBehaviour
{
    private Vector3 mousePosition;

    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeapon = 0;

    // firing event
    public static event Action<int, int> ammoChangedEvent = (stock, maxCapaticy) => {};

    private void Start() {
        updateUIAmmo();
    }

    void Update()
    {
        if(!UIScripts.gameIsPaused)
        {
            // aim at mouse position
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 lookDirection = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // shoot on M1
            if(Input.GetButton("Fire1"))
            {   
                if(weapons[currentWeapon].Fire())
                    updateUIAmmo();
            }

            // swap weapons
            if(Input.GetKeyDown(KeyCode.Tab) && weapons.Count > 1)
            {
                weapons[currentWeapon].gameObject.SetActive(false);
                currentWeapon = (currentWeapon + 1) % 2;
                weapons[currentWeapon].gameObject.SetActive(true);
                updateUIAmmo();
            }
        }
    }

    public void addWeapon(Weapon weaponToAdd){
        if(weapons.Count >= 2)
        {
            dropWeapon(weapons[currentWeapon]);
            weapons[currentWeapon] = weaponToAdd;
        }
        else{
            weapons.Add(weaponToAdd);
            weapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon += 1;
        }
            
        weaponToAdd.transform.parent = transform;
        weaponToAdd.transform.position = transform.position;
        weaponToAdd.transform.rotation = transform.rotation;
        weaponToAdd.GetComponent<SpriteRenderer>().sortingOrder = 25;
        updateUIAmmo();
    }

    private void dropWeapon(Weapon weaponToDrop)
    {   
        weaponToDrop.GetComponent<SpriteRenderer>().sortingOrder = 0;
        weaponToDrop.transform.rotation = Quaternion.Euler(0,0,-90);
        weaponToDrop.transform.parent = null;
        weapons[currentWeapon] = null;
    }

    private void updateUIAmmo()
    {
        ammoChangedEvent?.Invoke(weapons[currentWeapon].ammoReserve.stock, weapons[currentWeapon].ammoReserve.maxCapacity);
    }

}