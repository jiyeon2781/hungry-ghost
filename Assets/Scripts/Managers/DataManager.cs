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
    public int FavoriteItemDataStartIdx { get { return 1; } }
    public int HateItemDataStartIdx { get { return GetFavoriteItemDataCount() + 1; } }

    public List<ItemData> LoadItemDatas()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("JsonFile/ItemData");
        var itemDatas = JsonUtility.FromJson<ItemDatas>(textAsset.text).FoodInfo;
        return itemDatas;
    }

    public ItemData GetItemDataUsingId(int id)
    {
        return _itemDatas.FirstOrDefault(data => data.ID == id);
    }

    public int GetFavoriteItemDataCount()
    {
        return _itemDatas.Count(data => data.IsFavoriteFood);
    }

    public int GetHateItemDataCount()
    {
        return _itemDatas.Count(data => !data.IsFavoriteFood);
    }
}
