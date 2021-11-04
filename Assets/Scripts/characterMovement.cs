using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");

        // Vector3 pos = transform.position;

        // if(Input.GetKey("w")){
        //     pos.y += moveSpeed;
        // }
        // if(Input.GetKey("a")){
        //     pos.x -= moveSpeed;
        // }
        // if(Input.GetKey("s")){
        //     pos.y -= moveSpeed;
        // }
        // if(Input.GetKey("d")){
        //     pos.x += moveSpeed;
        // }

        // transform.position = pos;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


        void OnCollisionEnter2D(Collision2D collision)
        {
        if(collision.gameObject.tag == "ground")
        {
            print("player was hit");
        }
        }
}
