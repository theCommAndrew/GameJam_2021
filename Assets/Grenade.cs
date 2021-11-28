using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float radius = 5f;
    public float explosionForce = 700f;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        Enemy enemy = gameObject.GetComponent<Enemy>();
        var grenadeEffect = Instantiate(explosionEffect, this.transform.position, transform.rotation);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.tag == "enemy" && nearbyObject.attachedRigidbody)
            {
                nearbyObject.attachedRigidbody.AddForceAtPosition(new Vector2(0, 5), this.transform.position);
                enemy.takeDamage(10);
            }
        }


        Destroy(gameObject);
        Destroy(grenadeEffect, .5f);
    }
}
