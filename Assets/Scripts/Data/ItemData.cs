using System;
using System.Collections.Generic;

// favorite Item의 인덱스는 1부터 시작
// hate item은 favorite item count가 끝난 후 그 다음 인덱스부터 시작

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    public string prefabName;
    public bool isFavoriteFood;
    public int score;
    public string description;
    public string iconImagePath;
}

public class ItemDatas
{
    public List<ItemData> foodInfo;
}