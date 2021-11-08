using System;
using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralFunctions// : MonoBehaviour
{
    private Random randomGen = new Random();

    public bool getPercentResult(int successChance){
        randomGen = new Random();
        return randomGen.Next(100) < successChance;
    }

}