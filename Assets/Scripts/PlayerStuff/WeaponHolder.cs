 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class WeaponHolder : MonoBehaviour
{
    private Vector3 mousePosition;

    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeapon = 0;


    void Update()
    {
        // aim at mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // shoot on M1
        if(Input.GetButton("Fire1"))
        {
            weapons[currentWeapon].Fire();
        }

        // swap weapons
        if(Input.GetKeyDown(KeyCode.Tab) && weapons.Count > 1)
        {
            weapons[currentWeapon].gameObject.SetActive(false);
            currentWeapon = (currentWeapon + 1) % 2;
            weapons[currentWeapon].gameObject.SetActive(true);
        }
    }

}