using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;


public abstract class Item : SerializedMonoBehaviour
{
    public ItemObserverOfficer itemObserverOfficer;

    public int id;
    public string name;
    public ItemTypes itemType;
    public float health;
    public int leftLifeTime, lifeDuration;
    public DateTime producedTime, deathTime;


    public void Start()
    {
        Produced(DateTime.Now);
        //print("producedTime: "+producedTime + " deathTime: "+deathTime);
    }

    void Produced(DateTime date)
    {
        producedTime = date;
        deathTime = producedTime.AddSeconds(lifeDuration);
    }
}
