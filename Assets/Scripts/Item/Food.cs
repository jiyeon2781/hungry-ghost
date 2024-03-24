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

    // Collider �浹 ���� �� ������ �����ϴ� �������� �ƴ���
    // �����ϴ� �����̸� +, �Ⱦ��ϴ� �����̸� -

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // TODO ���� Ǯ�� ����������(deactivated)
            Debug.Log("�÷��̾�� �꿴���!");
        }
    }
}
