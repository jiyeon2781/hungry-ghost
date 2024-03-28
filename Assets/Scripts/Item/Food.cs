using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    [SerializeField] private string _foodAddress = "Assets/Prefabs/Item/Foods/";
    private bool _isUsing;
    private GameObject _food;

    public ItemData currentItemData;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }

    public void Start()
    {
        SetFoodPrefab();
    }

    public async void SetFoodPrefab()
    {

        var rand = Random.Range(1, Managers.DataManager.ItemDataCount + 1);
        currentItemData = Managers.DataManager.GetItemDataUsingId(rand);
        var _foodPrefabAddress = _foodAddress + currentItemData.prefabName + ".prefab";

        _food = await Managers.ResourceManager.InstantiateInAsync(_foodPrefabAddress, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(_food);
            if (currentItemData.isFavoriteFood)
                Managers.GameManager.CurrentScore += currentItemData.score;
            else 
                Managers.GameManager.CurrentScore -= currentItemData.score;
            Managers.GameManager.ChangeScore();
            Managers.PoolManager.Push(gameObject.GetComponentInParent<Food>());
            PositionManager.Instance.OnDestroyFood(this);
        }
    }
}
