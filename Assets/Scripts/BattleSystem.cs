using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private enum State{
        Idle,
        Active,
        Completed
    }
    private State state;
    [SerializeField] private Enemy[] enemiesArray;
    [SerializeField] private EntryAlert entryAlert;
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;
    private int enemyCount = 0;
    

    private void Awake() {
        state = State.Idle;
    }
    
    private void Start() {
        entryAlert.OnPlayerEnter += EntryAlert_OnPlayerEnter;
        foreach(Enemy enemy in enemiesArray){
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
        foreach( Enemy enemy in enemiesArray){
            enemy.spawn();
        }

        if(enemyCount > 0){
            state = State.Active;
            OnBattleStart?.Invoke(this, EventArgs.Empty);
        }
    }
}
