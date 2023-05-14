using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemActor : Item
{
    public float price;


    public void CalculateLeftLifeTime() 
    {
        TimeSpan timeDifference = deathTime - DateTime.Now;
        leftLifeTime = (int)timeDifference.TotalSeconds;

        CalculateHealth();
    }
    public void CalculateCurrentPrice() 
    {
        
    }

    void CalculateHealth() 
    {
        health = ((float)leftLifeTime/lifeDuration) * 100;
    }

}
