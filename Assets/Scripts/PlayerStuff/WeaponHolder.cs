 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ammo;

public class WeaponHolder : MonoBehaviour
{
    private Vector3 mousePosition;

    void FixedUpdate()
    {
        // aim at mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

}