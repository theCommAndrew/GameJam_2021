using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
 
    private void Awake() {

        WeaponHolder.upateActiveWeapon += (index) => updateActive(index);
        WeaponHolder.updateSlotImage += (index, newSprite) => updateSlotImage(index, newSprite);
    }

    private void updateActive(int selected) {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child =  gameObject.transform.GetChild(i);
            if(i == selected)
            {
                child.GetComponent<Outline>().effectColor = Color.white;
            }
            else
            {
                child.GetComponent<Outline>().effectColor = Color.black;
            }
        }
        
    }

    private void updateSlotImage(int index, Sprite newSprite)
    {
        var slot = gameObject.transform.GetChild(index);
        slot.GetChild(0).GetComponent<Image>().sprite = newSprite;
    }

}
