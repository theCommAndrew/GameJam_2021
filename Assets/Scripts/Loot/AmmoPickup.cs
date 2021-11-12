using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ammo;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private GameObject displayText;
    private TextMesh textField;
    [SerializeField] private int amount;
    [SerializeField] private AmmoType type;

    private void Start() {
        textField = displayText.GetComponent<TextMesh>();
        textField.text = amount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            amount -= player.inventory.Collect(type, amount);
            textField.text = amount.ToString();
            if(amount == 0){
                Destroy(gameObject);
            }         
        }
    }


}
