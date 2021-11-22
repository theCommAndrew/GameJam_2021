using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20f;
    public Vector3 scale = new Vector3(.5f,.5f,0);

    void Start()
    {  
        Rigidbody2D bulletBody = GetComponent<Rigidbody2D>();

        this.transform.localScale = scale;

        bulletBody.velocity = transform.up * speed;
    }
   
}
