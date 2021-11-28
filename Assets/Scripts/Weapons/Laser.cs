using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;

    //gunstats

    /*protected override void Awake()
    {
        bulletDamage = 1;
        ammoReserve.maxClip = 100;
        ammoReserve.inClip = ammoReserve.maxClip;
        ammoReserve.maxCapacity = 500;
        ammoReserve.stock = ammoReserve.maxCapacity;
        ammoReserve.ammoPerShot = 1;

        reloadTime = 4;
    }*/


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            EnableLaser();
        }
        if (Input.GetButton("Fire1"))
        {
            UpdateLaser();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            DisableLaser();
        }
    }

    void EnableLaser()
    {
        lineRenderer.enabled = true;
    }
    void UpdateLaser()
    {
        var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, firePoint.position);

        lineRenderer.SetPosition(1, mousePos);
    }
    void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}
