using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    public int totalWood;
    public float currentWater;
    public int carrots;


    public void WaterLimit(float water)
    {
        if (currentWater <= 50)
        {
            currentWater += water;
        }

    }
}
