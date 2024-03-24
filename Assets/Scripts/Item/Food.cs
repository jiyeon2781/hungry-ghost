using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    [SerializeField] private string _foodAddress = "Assets/Prefabs/Item/Foods/";
    private bool _isUsing;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }
    public ItemData currentItemData;

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
            Instantiate(obj, transform);
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_foodAddress}\" GameObject"));
    }

    // Collider 충돌 진행 시 유령이 좋아하는 음식인지 아닌지
    // 좋아하는 음식이면 +, 싫어하는 음식이면 -

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // TODO 원래 풀로 보내버리기(deactivated)
            Debug.Log("플레이어와 닿였어요!");
        }
    }
}
