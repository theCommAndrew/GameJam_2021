using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCamera : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    private Vector3 cameraStartPos;
    private Vector3 cameraEndPos;
    private float lerpTime = 0f;
    private bool triggerTransition = false;

    private void LateUpdate() {
        if(triggerTransition){
            Camera.main.transform.position = Vector3.Lerp(cameraStartPos, cameraEndPos, lerpTime);

            if(lerpTime >= 1f){
                triggerTransition = false;
            }
            else{
                lerpTime += Time.deltaTime;
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if(player){
           triggerTransition = true;
           cameraStartPos = Camera.main.transform.position;
           cameraEndPos = transform.parent.position;
           // account for side room flip around y axis 
           cameraEndPos.z = cameraEndPos.z > 0 ? cameraEndPos.z *= -1 : cameraEndPos.z; 
           lerpTime = 0f;
        }
    }
}
