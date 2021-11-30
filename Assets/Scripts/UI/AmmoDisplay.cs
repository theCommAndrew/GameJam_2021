using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{

    private Text textField;

    void Awake(){
        textField = gameObject.GetComponent<Text>();

        WeaponHolder.ammoChangedEvent += (stock, maxCapacity) => changeAmmoDisplay(stock, maxCapacity);
    }

    private void changeAmmoDisplay(int stock, int maxCapacity)
    {
        if(textField != null)
            textField.text = $"{stock}/{maxCapacity}";
    }

    

}
