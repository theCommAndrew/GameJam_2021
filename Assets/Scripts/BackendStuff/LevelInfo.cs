using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int levelNumber;
    public GameObject[] mainRooms;
    public GameObject[] sideRooms;
    public GameObject finalRoom;
    public GameObject[] enemyLayouts;
    public GameObject[] enemyDrops;
    public GameObject[] roomRewards;
    public GameObject[] bossRewards;
    public int rooms = 0;


    private void Start()
    {
        Enemy.OnEnemyKilled += (int lootChance, Transform enemyLoc) => dropLoot(lootChance, enemyLoc);
    }

    public void dropLoot(int lootChance, Transform enemyLoc)
    {
        if (getPercentResult(lootChance))
        {
            if (getPercentResult(30))
            {
                Instantiate(enemyDrops[0], enemyLoc.position, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(enemyDrops[1], enemyLoc.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    private bool getPercentResult(int chance)
    {
        float check = roll(100);
        return check <= chance;
    }

    private float roll(int range)
    {
        return Random.Range(0, range);
    }

    public int getLevelNumber()
    {
        return levelNumber;
    }
}
