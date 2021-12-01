using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Kino;

public class VirusEnemy : Enemy
{
    private DigitalGlitch digitalGlitchEffect;
    private AnalogGlitch analogGlitchEffect;
    private Action[] actions;
    private int rand;
    private Action desiredAction;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        maxHealth = 8;
        health = maxHealth;
        moveSpeed = 0f;
        lootChance = 100;
        deathAnimation = "VirusEnemyDie";
        actions = new Action[] { reverseControls, slowPlayer };
        rand = Random.Range(0, actions.Length);
        desiredAction = actions[rand];
        //print(desiredAction.Method.Name);
        digitalGlitchEffect = Camera.main.GetComponent<DigitalGlitch>();
        analogGlitchEffect = Camera.main.GetComponent<AnalogGlitch>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (alive)
        {
            digitalGlitchEffect.intensity = 0.1f;
            analogGlitchEffect.scanLineJitter = 0.1f;
            analogGlitchEffect.colorDrift = 0.1f;
            desiredAction.Invoke();

        }
        else
        {
            digitalGlitchEffect.intensity = 0;
            analogGlitchEffect.scanLineJitter = 0;
            analogGlitchEffect.colorDrift = 0;
            revertPlayer();
        }
    }

    public void reverseControls()
    {
        if (player.movement.x > 0)
        {
            player.movement.x = -1;
        }
        else if (player.movement.x < 0)
        {
            player.movement.x = 1;
        }
        if (player.movement.y > 0)
        {
            player.movement.y = -1;
        }
        else if (player.movement.y < 0)
        {
            player.movement.y = 1;
        }
    }

    public void slowPlayer()
    {
        player.moveSpeed = 3f;
    }



    public void revertPlayer()
    {
        player.moveSpeed = player.maxSpeed;
    }

}
