using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public string prefabName;
    public bool isFavoriteFood;
    public int score;
    public string description;
}

public class ItemDatas
{
    public List<ItemData> foodInfo;
}