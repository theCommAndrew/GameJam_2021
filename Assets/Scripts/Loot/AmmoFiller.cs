using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoFiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var playerWeapons = col.gameObject.GetComponent<Player>().GetComponentInChildren<WeaponHolder>();
            foreach(Weapon weapon in playerWeapons.weapons)
            {
                weapon.collectAmmo(weapon.ammoReserve.maxCapacity);
            }
            Destroy(gameObject);
            playerWeapons.updateUIAmmo();
        }
    }


}
