using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipTrap : MonoBehaviour
{
    public float rotateSpeed = 45f;

    void Update()
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
    }

}
