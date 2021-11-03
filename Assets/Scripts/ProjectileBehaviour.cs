using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0 ,0);
    }

    private void onTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        Character character = col.gameObject.GetComponent<Character>();
        if(character)
        {
            character.takeDamage(damage);
            Debug.Log("player was hit");
        }

        Destroy(gameObject);
    }

    

}
