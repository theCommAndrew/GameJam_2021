using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private GameObject displayText;
    private TextMesh textField;
    [SerializeField] private int amount;

    private void Start() {
        textField = displayText.GetComponent<TextMesh>();
        textField.text = amount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var playerWeapons = col.gameObject.GetComponent<Player>().GetComponentInChildren<WeaponHolder>();
            Weapon weaponInHand = playerWeapons.weapons[playerWeapons.currentWeapon];
            amount -= weaponInHand.collectAmmo(amount);
            textField.text = amount.ToString();
            if(amount == 0){
                Destroy(gameObject);
            }         
        }
    }


}
