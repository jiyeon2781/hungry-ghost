using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int _playerCharacterCount = 1;
    [SerializeField] private string _address = "Assets/Prefabs/GhostPlayer/LittleGhost_LP";
    [SerializeField] private bool _isDebug = false;
    [SerializeField] private string _playerAddressInDebug = "Assets/Prefabs/GhostPlayer/LittleGhost_LP (1).prefab";

    private void Start()
    {
        if (!_isDebug) CreateRandomPlayer();
        else CreatePlayerInDebug();
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

    void CreatePlayerInDebug()
    {
        Managers.ResourceManager.LoadAsync(_playerAddressInDebug, false, obj =>
        {
            Instantiate(obj, transform);
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_playerAddressInDebug}\" GameObject"));
    }
}
