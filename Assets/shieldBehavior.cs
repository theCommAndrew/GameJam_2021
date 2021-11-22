using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBehavior : MonoBehaviour
{
    private Vector3 noRotate;
    public bool canBeDamaged;
    // Start is called before the first frame update
    void Start()
    {
        noRotate = transform.eulerAngles;
        canBeDamaged = false;
    }

    void LateUpdate()
    {
        noRotate.y = transform.eulerAngles.y;
        transform.eulerAngles = noRotate;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "bullet" && canBeDamaged == false)
        {
            print("Better luck next time");
        }
        if (other.tag == "Player")
        {
            print("Can be damaged");
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            canBeDamaged = true;
        }
    }

}
