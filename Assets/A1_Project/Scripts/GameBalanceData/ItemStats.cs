using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[SerializeField]
[CreateAssetMenu(fileName = "ItemStats", menuName = "GameData/ItemStats")]
public class ItemStats: ScriptableObject
{
    public itemStatsDict itemStatsDictionary = new itemStatsDict();
}

[Serializable]
public class itemStatsDict : SerializableDictionary<string, itemInfo> { }
[Serializable]
public class itemInfo : SerializableDictionary<string, string> { }