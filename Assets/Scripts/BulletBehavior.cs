using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "wall")
        {
            Destroy(gameObject, .25f);
        }

        if (col.gameObject.tag == "character")
        {
            Character character = col.gameObject.GetComponent<Character>();
            print("Bullet hit character");
            character.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
