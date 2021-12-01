using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float extraHeight;
    private Player player;
    private float cameraHeight;
    private bool followingPlayer = false;


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        cameraHeight = Camera.main.transform.position.z - extraHeight;
    }

    void Update()
    {
        if(followingPlayer && player.alive)
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraHeight);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            followingPlayer = !followingPlayer;
        }
    }
}
