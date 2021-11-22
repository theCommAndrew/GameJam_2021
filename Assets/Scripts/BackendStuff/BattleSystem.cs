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
    private LevelInfo templates;
    private int rand;
    public GameObject[] uniqueLayouts;
    private List<Enemy> enemiesArray = new List<Enemy>();
    private EntryAlert entryAlert;
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;
    private int enemyCount = 0;
    

    private void Awake() {
        state = State.Idle;
    }
    
    private void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelInfo>();
        unpackEnemies();

        entryAlert = gameObject.GetComponent<EntryAlert>();
        entryAlert.OnPlayerEnter += EntryAlert_OnPlayerEnter;
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
            endBattle();
        }
    }

    private void startBattle(){
        foreach(Enemy enemy in enemiesArray){
            enemy.spawn();
        }

        if(enemyCount > 0){
            state = State.Active;
            OnBattleStart?.Invoke(this, EventArgs.Empty);
        }
        else{
            endBattle();
        }
    }

    private void endBattle(){
        state = State.Completed;
        OnBattleEnd?.Invoke(this, EventArgs.Empty);
        
        Destroy(gameObject);
    }

    private void unpackEnemies(){
        GameObject layout;
        if(uniqueLayouts.Length == 0)
        {
            rand = Random.Range(0, templates.enemyLayouts.Length);
            layout = templates.enemyLayouts[rand];
        }
        else
        {
            rand = Random.Range(0, uniqueLayouts.Length);
            layout = uniqueLayouts[rand];
        }
        
        
        GameObject enemyLayout = Instantiate(layout, transform.parent.position, transform.parent.parent.rotation);    
        for(int i = 0; i < enemyLayout.transform.childCount; i++)
        {
            var obj = enemyLayout.transform.GetChild(i).gameObject ;
        
            Enemy enemy = obj.GetComponent<Enemy>();
            if(enemy)
            {
                enemy.OnEnemySpawned += Enemy_OnSpawn;
                enemy.OnEnemyKilled += Enemy_OnDeath;
                enemiesArray.Add(enemy);
            }
        }
    }
}
