using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClear : MonoBehaviour
{
   [SerializeField] private BattleSystem entryTrigger;

    private void Awake() {
        entryTrigger.OnBattleEnd += clearText;
    }

    private void clearText(object sender, System.EventArgs e){
        Destroy(gameObject);
    }
}
