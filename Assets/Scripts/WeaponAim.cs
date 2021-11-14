 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class WeaponAim : MonoBehaviour
{

    private Camera cam;
    Vector3 mousePosition;

    private void Awake() {
        cam = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //transform.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}