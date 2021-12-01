using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;

    public AudioClip hit;
    public AudioClip switchWeapons;
    public AudioClip playerShootSound;
    public AudioClip playerTookDamage;
    public AudioClip playerReloadSound;
    public AudioClip playerDash;
    public AudioClip pickupItem;
    public AudioClip enemyShoot;


    public static SfxManager sfxInstance;

    private void Awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
