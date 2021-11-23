using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public BattleSystem battleSystem;
    private LevelInfo level;
    private void Start() {
        level = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelInfo>();
        gameObject.SetActive(false);
        battleSystem.OnBattleEnd += activate;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(level.getLevelNumber() + 1);
        }
    }

    private void activate(object sender, System.EventArgs e){
        gameObject.SetActive(true);
    }
}
