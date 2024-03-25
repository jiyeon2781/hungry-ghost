using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    [SerializeField] private string _foodAddress = "Assets/Prefabs/Item/Foods/";
    private bool _isUsing;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }
    public ItemData currentItemData;
    private GameObject _food;

    public void Start()
    {
        SetFoodPrefab();
    }

    public void SetFoodPrefab()
    {
        var rand = Random.Range(1, Managers.DataManager.ItemDataCount + 1);
        currentItemData = Managers.DataManager.GetItemDataUsingId(rand);
        _foodAddress += currentItemData.prefabName + ".prefab";

        Managers.ResourceManager.LoadAsync(_foodAddress, false, obj =>
        {
            _food = Instantiate(obj, transform);
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_foodAddress}\" GameObject"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 닿였어요!");
            if (currentItemData.isFavoriteFood)
                Managers.GameManager.CurrentScore += currentItemData.score;
            else 
                Managers.GameManager.CurrentScore -= currentItemData.score;
            Managers.PoolManager.Push(gameObject.GetComponentInParent<Food>());
            Destroy(_food);
        }
    }
}
