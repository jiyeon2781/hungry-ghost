using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HateFood : Food
{
    private void Awake()
    {
        _itemType = Enums.Item.Hate;
    }

    public override async void OnPool()
    {
        await SetFoodPrefab();
    }
    public async UniTask SetFoodPrefab()
    {

        var rand = Random.Range(Managers.DataManager.HateItemDataStartIdx, Managers.DataManager.GetHateItemDataCount() + 1);
        _currentItemData = Managers.DataManager.GetItemDataUsingId(rand);

        _isUsing = true;
        _food = await Managers.ResourceManager.InstantiateInAsync(_foodAddress + _currentItemData.prefabName + ".prefab", transform);
        await RotationFood();
    }

    public override void InteractionPlayer(GameObject player)
    {
        base.InteractionPlayer(player);
        Managers.GameManager.CurrentScore -= CurrentItemData.score;
    }
}
