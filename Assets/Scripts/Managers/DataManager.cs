using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DataManager
{
    public void Init()
    {
        _itemDatas = LoadItemDatas();
    }

    private List<ItemData> _itemDatas;
    public int ItemDataCount { get { return _itemDatas.Count; } }

    public List<ItemData> LoadItemDatas()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JsonFile/ItemData");
        var itemDatas = JsonUtility.FromJson<ItemDatas>(textAsset.text).foodInfo;
        return itemDatas;
    }

    public ItemData GetItemDataUsingId(int id)
    {
        return _itemDatas.FirstOrDefault(data => data.id == id);
    }
}
