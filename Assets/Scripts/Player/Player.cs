using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int _playerCharacterCount = 1;
    [SerializeField] private string _address = "Assets/Prefabs/GhostPlayer/LittleGhost_LP";

    private void Start()
    {
        CreateRandomPlayer();
    }

    void CreateRandomPlayer()
    {
        int random = Random.Range(1, _playerCharacterCount + 1);
        _address += " (" + random + ").prefab";
        Managers.ResourceManager.LoadAsync(_address, false, obj =>
        {
            Instantiate(obj, transform);
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_address}\" GameObject"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food")) {
            // TODO 원래 풀로 보내버리기(deactivated)
            Debug.Log("닿였어요!");
            Managers.PoolManager.Push(other.gameObject.GetComponentInParent<Food>());
        }
    }
}
