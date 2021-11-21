using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBehavior : MonoBehaviour
{
    private Vector3 noRotate;
    // Start is called before the first frame update
    void Start()
    {
        noRotate = transform.eulerAngles;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        noRotate.y = transform.eulerAngles.y;
        transform.eulerAngles = noRotate;
    }
}
