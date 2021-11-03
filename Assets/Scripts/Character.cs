using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float moveSpeed = .025f;
    public int health = 100;

    public void takeDamage(int damage)
    {
        health -= damage;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey("w")){
            pos.y += moveSpeed;
        }
        if(Input.GetKey("a")){
            pos.x -= moveSpeed;
        }
        if(Input.GetKey("s")){
            pos.y -= moveSpeed;
        }
        if(Input.GetKey("d")){
            pos.x += moveSpeed;
        }

        transform.position = pos;
    }






}