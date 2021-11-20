using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{

    private TextMeshProUGUI textField;

    void Awake(){
        textField = gameObject.GetComponent<TextMeshProUGUI>();

        WeaponHolder.ammoChangedEvent += (stock, maxCapacity) => changeAmmoDisplay(stock, maxCapacity);
    }

    private void changeAmmoDisplay(int stock, int maxCapacity)
    {
        textField.text = $"{stock}/{maxCapacity}";
    }

}
