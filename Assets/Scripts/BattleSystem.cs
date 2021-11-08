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
    public event EventHandler OnBattleStart;
    public event EventHandler OnBattleEnd;

    [SerializeField] private EntryAlert entryAlert;

    private void Awake() {
        state = State.Idle;
    }
    private void Start() {
        entryAlert.OnPlayerEnter += EntryAlert_OnPlayerEnter;
    }

    private void EntryAlert_OnPlayerEnter(object sender, System.EventArgs e){
        if(state == State.Idle){
            startBattle();
            entryAlert.OnPlayerEnter -= EntryAlert_OnPlayerEnter;
        }
    }

    private void startBattle(){
        Debug.Log("Starting Battle");
        state = State.Active;
        OnBattleStart?.Invoke(this, EventArgs.Empty);
    }
}
