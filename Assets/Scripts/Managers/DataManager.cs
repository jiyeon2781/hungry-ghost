using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    private List<ItemData> itemDatas = new List<ItemData> ();

    public List<ItemData> LoadItemDatas()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JsonFile/ItemData");
        itemDatas = JsonUtility.FromJson<List<ItemData>>(textAsset.text);
        return itemDatas;
    }
}
