using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponHolder : MonoBehaviour
{
    public Vector3 mousePosition;
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeapon;
    private bool canFire;
    private TextMesh callout;
    private Player player;

    // UI events
    public static event Action<int, int> ammoChangedEvent = (stock, maxCapaticy) => { };
    public static event Action<int> upateActiveWeapon = (index) => { };
    public static event Action<int, Sprite> updateSlotImage = (index, newImage) => { };


    private void Start()
    {
        currentWeapon = 0;
        updateUIAmmo();
        updateUIActiveWeapon();
        updateInventorySprite(weapons[currentWeapon]);

        callout = GameObject.FindGameObjectWithTag("PlayerCallout").GetComponent<TextMesh>();
        player = transform.parent.GetComponent<Player>();
    }

    void Update()
    {
        if (!UIScripts.gameIsPaused && player.alive)
        {
            // aim at mouse position
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 lookDirection = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


            if (mousePosition.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, -angle);
            }
            else if (mousePosition.x >= transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
            }

            // shoot on M1
            if (Input.GetButton("Fire1") && !weapons[currentWeapon].reloading)
            {
                if (weapons[currentWeapon].Fire())
                    updateUIAmmo();
            }

            if (Input.GetKeyDown(KeyCode.R) && weapons[currentWeapon].ammoReserve.inClip != weapons[currentWeapon].ammoReserve.maxClip && !weapons[currentWeapon].reloading)
            {
                weapons[currentWeapon].reloading = true;
                StartCoroutine(weapons[currentWeapon].reload());
            }

            // swap weapons
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                setActiveWeapon(0);
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.switchWeapons);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
            {
                setActiveWeapon(1);
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.switchWeapons);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count > 2)
            {
                setActiveWeapon(2);
                SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.switchWeapons);
            }
        }
    }

    private void setActiveWeapon(int index)
    {
        weapons[currentWeapon].reloading = false;
        callout.text = "";

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }

        currentWeapon = index;

        updateUIAmmo();
        updateUIActiveWeapon();
    }

    public void addWeapon(Weapon weaponToAdd)
    {
        if (weapons.Count >= 3)
        {
            if (currentWeapon == 0)
                return;

            dropWeapon(weapons[currentWeapon]);
            weapons[currentWeapon] = weaponToAdd;
        }
        else
        {
            weapons.Add(weaponToAdd);
            weapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon += 1;
        }

        weaponToAdd.transform.parent = transform;
        weaponToAdd.transform.position = transform.position;
        weaponToAdd.transform.rotation = transform.rotation;
        weaponToAdd.GetComponent<SpriteRenderer>().sortingOrder = 25;
        updateUIAmmo();
        updateUIActiveWeapon();
        updateInventorySprite(weaponToAdd);
    }

    private void dropWeapon(Weapon weaponToDrop)
    {
        weaponToDrop.GetComponent<SpriteRenderer>().sortingOrder = 0;
        weaponToDrop.transform.rotation = Quaternion.Euler(0, 0, -90);
        weaponToDrop.transform.parent = null;
        weaponToDrop.GetComponent<SpriteRenderer>().sortingOrder = 15;
        weapons[currentWeapon] = null;
    }

    public void updateUIAmmo()
    {
        ammoChangedEvent?.Invoke(weapons[currentWeapon].ammoReserve.inClip, weapons[currentWeapon].ammoReserve.stock);
    }

    private void updateUIActiveWeapon()
    {
        upateActiveWeapon?.Invoke(currentWeapon);
    }

    private void updateInventorySprite(Weapon weapon)
    {
        updateSlotImage?.Invoke(currentWeapon, weapon.getImage());
    }

}