using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private string _address = "Assets/Prefabs/UI/";
    private GameObject _prevUI = null;
    private GameObject _currentUI = null;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("--UI_Root");
            if (root == null)
                root = new GameObject { name = "--UI_Root" };
            Object.DontDestroyOnLoad(root);
            return root;
        }
    }

    public T ShowUI<T>(string name = null, bool isDestroy = true) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        var prefabAddress = _address + name + ".prefab";
        if (_currentUI != null && isDestroy) Object.Destroy(_currentUI);
        else if (!isDestroy)
        {
            _prevUI = _currentUI;
            _prevUI.gameObject.SetActive(false);
        }

        var obj = Managers.ResourceManager.Instantiate(prefabAddress, Root.transform);
        T ui = obj.GetComponent<T>();
        _currentUI = obj.gameObject;

        return ui;
    }

    public T BackUI<T>() where T : UIBase
    {
        Object.Destroy(_currentUI);
        _currentUI = _prevUI;
        _currentUI.gameObject.SetActive(true);

        return _currentUI.GetComponent<T>();
    }
}
