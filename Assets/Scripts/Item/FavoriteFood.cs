using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoriteFood : Food
{
    private void Awake()
    {
        _itemType = Enums.Item.Favorite;
    }

    public override async void OnPool()
    {
        await SetFavoriteFoodPrefab();
    }
    public async UniTask SetFavoriteFoodPrefab()
    {

        var rand = Random.Range(Managers.DataManager.FavoriteItemDataStartIdx, Managers.DataManager.GetFavoriteItemDataCount() + 1);
        _currentItemData = Managers.DataManager.GetItemDataUsingId(rand);

        _isUsing = true;
        _food = await Managers.ResourceManager.InstantiateInAsync(_foodAddress + _currentItemData.PrefabName + ".prefab", transform);
        await RotationFood();
    }

    public override void InteractionPlayer(GameObject player)
    {
        base.InteractionPlayer(player);
        Managers.GameManager.CurrentScore += _currentItemData.Score;
        Managers.GameManager.ChangeScore();
    }
}
