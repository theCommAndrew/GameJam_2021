using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public bool left;
    public bool main = true;
    private LevelInfo templates;
    private int rand;

    private void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<LevelInfo>();
        if(main)
        {
            Invoke("spawnMain", 0.1f);
        }
        else
        {
            Invoke("spawnSide", 0.1f);
        }
        
    }

    private void spawnMain(){
        if(templates.rooms < 8)
        {
            rand = Random.Range(0, templates.mainRooms.Length);
            float flipped = (Random.value > 0.5f) ? 0 : 180;
            Instantiate(templates.mainRooms[rand], transform.position, Quaternion.identity);
            templates.rooms++;
        }
        else{
            Instantiate(templates.finalRoom, transform.position, Quaternion.identity);
            }

        Destroy(gameObject);
    }

    private void spawnSide(){
        rand = Random.Range(0, templates.sideRooms.Length);
        float flipped = left ? 0 : 180;
        Instantiate(templates.sideRooms[rand], transform.position, Quaternion.Euler(0,flipped,0));

        Destroy(gameObject);
    }

}
