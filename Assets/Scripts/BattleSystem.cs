using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class BattleSystem : MonoBehaviour
{
    private enum State{
        Idle,
        Active,
        Completed
    }
    private State state;
    private FloorTemplates templates;
    private int rand;
    private List<GameObject> enemiesArray = new List<GameObject>();
    private EntryAlert entryAlert;
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;
    private int enemyCount = 0;
    

    private void Awake() {
        state = State.Idle;
    }
    
    private void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<FloorTemplates>();
        unpackEnemies();

        entryAlert = gameObject.GetComponent<EntryAlert>();
        entryAlert.OnPlayerEnter += EntryAlert_OnPlayerEnter;
        foreach(GameObject e in enemiesArray){
            Enemy enemy = e.GetComponent<Enemy>();
            enemy.OnEnemySpawned += Enemy_OnSpawn;
            enemy.OnEnemyKilled += Enemy_OnDeath;
        }
    }

    private void EntryAlert_OnPlayerEnter(object sender, System.EventArgs e){
        if(state == State.Idle){
            startBattle();
            entryAlert.OnPlayerEnter -= EntryAlert_OnPlayerEnter;
        }
    }

    private void Enemy_OnSpawn(object sender, System.EventArgs e){
        enemyCount += 1;
    }

    private void Enemy_OnDeath(object sender, System.EventArgs e){
        enemyCount -= 1;
        if(enemyCount == 0){
            state = State.Completed;
            OnBattleEnd?.Invoke(this, EventArgs.Empty);
        }
    }

    private void startBattle(){
        Destroy(this.GetComponent<BoxCollider2D>());

        foreach( GameObject enemy in enemiesArray){
            enemy.GetComponent<Enemy>().spawn();
        }

        if(enemyCount > 0){
            state = State.Active;
            OnBattleStart?.Invoke(this, EventArgs.Empty);
        }
    }

    private void unpackEnemies(){
        rand = Random.Range(0, templates.enemyLayouts.Length);
        GameObject enemyLayout = Instantiate(templates.enemyLayouts[rand], transform.parent.position, Quaternion.identity);    
        for(int i = 0; i < enemyLayout.transform.childCount; i++)
        {
            var enemy = enemyLayout.transform.GetChild(i).gameObject ;
            enemiesArray.Add(enemy);
        }

    }
}
