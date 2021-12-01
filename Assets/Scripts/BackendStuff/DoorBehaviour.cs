using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private float moveDistance;
    [SerializeField] private bool stayClosed = false;
    public Vector3 moveSpeed = new Vector3(.1f, 0, 0);
    private Transform doorL;
    private Transform doorR;

    private void Start() {
        doorL = transform.GetChild(0);
        doorR = transform.GetChild(1);

        battleSystem.OnBattleStart += BattleSystem_OnBattleStart;
        battleSystem.OnBattleEnd += BattleSystem_OnBattleEnd;
    }

    private void BattleSystem_OnBattleStart(object sender, System.EventArgs e){
        if(this == null)
            return;
        StartCoroutine(closeDoors());
        battleSystem.OnBattleStart -= BattleSystem_OnBattleStart;
    }

    private void BattleSystem_OnBattleEnd(object sender, System.EventArgs e){
        if(this == null)
            return;

        if(!stayClosed)
            StartCoroutine(openDoors());
        battleSystem.OnBattleEnd -= BattleSystem_OnBattleEnd;
    }

    private IEnumerator openDoors()
    {
        float moved = 0f;
        while(!UIScripts.gameIsPaused && moved <= moveDistance)
        {
            doorL.position -= moveSpeed;
            doorR.position += moveSpeed;
            moved += moveSpeed.x;
            yield return new WaitForSeconds(.01f);
        }
    }

    private IEnumerator closeDoors()
    {
        float moved = 0f;
        while(!UIScripts.gameIsPaused && moved <= moveDistance)
        {
            doorL.position += moveSpeed;
            doorR.position -= moveSpeed;
            moved += moveSpeed.x;
            yield return new WaitForSeconds(.01f);
        }
    }
}