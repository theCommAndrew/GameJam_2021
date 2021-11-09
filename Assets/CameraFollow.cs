using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> getCameraPosition;

    public void Setup(Func<Vector3> getCameraPosition){
        this.getCameraPosition = getCameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = getCameraPosition();
        cameraPos.z = transform.position.z;
        transform.position = cameraPos;
    }
}
