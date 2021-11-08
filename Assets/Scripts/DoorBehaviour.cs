using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite openSprite;
    public Sprite closedSprite;
    [SerializeField] private BattleSystem battleSystem;
    private void Start() {
        doorIsOpen(true);
        battleSystem.OnBattleStart += BattleSystem_OnBattleStart;
        battleSystem.OnBattleEnd += BattleSystem_OnBattleEnd;
    }

    private void BattleSystem_OnBattleStart(object sender, System.EventArgs e){
        doorIsOpen(false);
        battleSystem.OnBattleStart -= BattleSystem_OnBattleStart;
    }

    private void BattleSystem_OnBattleEnd(object sender, System.EventArgs e){
        doorIsOpen(true);
        battleSystem.OnBattleStart -= BattleSystem_OnBattleEnd;
    }

    public void doorIsOpen(bool open){
        if(open){
            spriteRenderer.sprite = openSprite;
            gameObject.SetActive(false);
        }
        else{
            spriteRenderer.sprite = closedSprite;
            gameObject.SetActive(true);
        }
    }
}