using System;
using System.Collections.Generic;

// favorite Item의 인덱스는 1부터 시작
// hate item은 favorite item count가 끝난 후 그 다음 인덱스부터 시작

[Serializable]
public class ItemData
{
    public int ID;
    public string Name;
    public string PrefabName;
    public bool IsFavoriteFood;
    public int Score;
    public string Description;
    public string IconImagePath;
}

public class ItemDatas
{
    public List<ItemData> FoodInfo;
}