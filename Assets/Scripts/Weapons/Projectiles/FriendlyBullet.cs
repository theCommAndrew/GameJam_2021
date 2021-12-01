using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : Bullet
{
    public GameObject hitPrefab;
    public Animation animator;
    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            enemy.takeDamage((int)(damage * Player.extraDamage));
        }

        if (tag != "Player" && tag != "backend" && tag != "bullet")
        {
            var hitindicator = Instantiate(hitPrefab, transform.position, Quaternion.identity);
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.hit);
            Destroy(gameObject);
            Destroy(hitindicator, .5f);
        }
    }
}
