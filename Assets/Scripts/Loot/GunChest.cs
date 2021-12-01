using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{

    public BattleSystem battleSystem;
    private LevelInfo info; 

    private void Start(){
        info = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelInfo>();
        battleSystem.OnBattleEnd += open;
    }

    private void open(object sender, System.EventArgs e){
        
        int rand = Random.Range(0, info.weapons.Length);
        Instantiate(info.weapons[rand], transform.position, Quaternion.Euler(new Vector3(0,0,-90)));
        Destroy(gameObject);
    }
}