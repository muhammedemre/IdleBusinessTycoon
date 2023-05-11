using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int id;
    public string name;
    public float healt;

    public float producedTime, leftLifeTime;

    public Item() 
    {
        Produced(Time.time);
    }

    void Produced(float time) 
    {
        producedTime = time;
    }
}
