using System;
using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneralFunctions : MonoBehaviour
{
    public static readonly Random randomGen = new Random();

    public static bool getPercentResult(int successChance){

        return randomGen.Next(100) < successChance;
    }
}