using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = .025f;


    // Start is called before the first frame update
    void Start()
    {
        
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
