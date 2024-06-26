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
        _ = Managers.ResourceManager.InstantiateInAsync(_address, transform);
    }
}
