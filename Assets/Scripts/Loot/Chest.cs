using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    public BattleSystem battleSystem;
    private LevelInfo info; 

    private void Start(){
        info = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelInfo>();
        battleSystem.OnBattleEnd += open;
    }

    private void open(object sender, System.EventArgs e){
        
        int rand = Random.Range(0, info.roomRewards.Length);
        Instantiate(info.roomRewards[rand], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
