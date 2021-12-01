using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour
{
    public Vector3 moveFireUp = new Vector3(0, .001f, 0);

    void Update()
    {
        StartCoroutine(FireSpreading());
    }

    public IEnumerator FireSpreading()
    {
        if (!UIScripts.gameIsPaused)
        {
            this.transform.position += moveFireUp;
            yield return new WaitForSeconds(30f);
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
}
