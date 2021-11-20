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
        if(this == null)
            return;

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
        if(this == null)
            return;

        Image slotImage = gameObject.transform.GetChild(index).GetChild(0).GetComponent<Image>();
        slotImage.sprite = newSprite;
        slotImage.enabled = true;        
    }

}
