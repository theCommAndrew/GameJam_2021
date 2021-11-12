using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    private Func<Vector3> getCameraPosition;

    public void Setup(Func<Vector3> getCameraPosition){
        this.getCameraPosition = getCameraPosition;
    }

    void Update()
    {
        Vector3 cameraPos = getCameraPosition();
        cameraPos.z = transform.position.z;

        Vector3 cameraDir = (cameraPos - transform.position).normalized;
        float distance = Vector3.Distance(cameraPos, transform.position);;

        if(distance > 0)
        {
            Vector3 newPosition = transform.position = transform.position + cameraDir * distance * cameraSpeed * Time.deltaTime;

            float distanceAfterMove = Vector3.Distance(newPosition, cameraPos);
            transform.position = distanceAfterMove > distance ? cameraPos : newPosition;
        }
        
    }
}
