using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryAlert : MonoBehaviour
{

    public event EventHandler OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();
        if(player != null){
            OnPlayerEnter?.Invoke(this, EventArgs.Empty);
        }
    }

}
