using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPoolable
{
    [SerializeField] private string _foodAddress = "Assets/Prefabs/Item/Foods/";
    [SerializeField] private float _rotationSpeed = 0.5f;
    private bool _isUsing;
    private GameObject _food;

    public ItemData currentItemData;
    public bool IsUsing { get { return _isUsing; } set { _isUsing = value; } }

    public async void Start()
    {
        PositionManager.Instance.OnDestroyFood -= PositionManager.Instance.PopFoodDictionary;
        PositionManager.Instance.OnDestroyFood += PositionManager.Instance.PopFoodDictionary;

        await SetFoodPrefab();
    }

    public async UniTask SetFoodPrefab()
    {

        var rand = Random.Range(1, Managers.DataManager.ItemDataCount + 1);
        currentItemData = Managers.DataManager.GetItemDataUsingId(rand);

        _isUsing = true;
        _food = await Managers.ResourceManager.InstantiateInAsync(_foodAddress + currentItemData.prefabName + ".prefab", transform);
        await RotationFood();
    }

    public async UniTask RotationFood()
    {
        await UniTask.WaitUntil(() => _food != null);
        while (_isUsing)
        {
            await UniTask.Yield();
            if (_food == null) break;
            _food.transform.Rotate(0, _rotationSpeed, 0);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _isUsing)
        {
            _isUsing = false;
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
