using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    public Vector3 moveFireUp = new Vector3(0, .004f, 0);
    [SerializeField] private BattleSystem entryTrigger;
    private bool isActive = false;

    private void Awake() {
        entryTrigger.OnBattleEnd += startFire;
    }

    void Update()
    {
        if (!UIScripts.gameIsPaused && isActive)
        {
            this.transform.position += moveFireUp;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if(tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.takeDamage(player.maxHealth);
        }

        if(tag == "enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.takeDamage(enemy.maxHealth);
        }
    }

    private void startFire(object sender, System.EventArgs e){
        print("activating fire");
        isActive = true;
    }
}
