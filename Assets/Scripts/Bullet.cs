using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Rigidbody2D bulletBody;

    void Start()
    {  
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.velocity = transform.forward * speed;
    }
    void Update()
    {    }
   
}
