using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private string _address = "Assets/Prefabs/UI/";
    private GameObject _currentUI = null;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("--UI_Root");
            if (root == null)
                root = new GameObject { name = "--UI_Root" };
            return root;
        }
    }

    public T ShowUI<T>(string name = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        var prefabAddress = _address + name + ".prefab";
        if (_currentUI != null)
            Object.Destroy(_currentUI);

        var obj = Managers.ResourceManager.Instantiate(prefabAddress, Root.transform);
        T ui = obj.GetComponent<T>();
        _currentUI = obj.gameObject;

        return ui;
    }
}
