using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private string _address = "Assets/Prefabs/UI/";
    private UIBase _currentUI = null;
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
        T ui = null;

        Managers.ResourceManager.LoadAsync(prefabAddress, false, obj =>
        {
            Object.Instantiate(obj, Root.transform);
            ui = obj.GetComponentInChildren<T>();
            _currentUI = ui;
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{name}\" UI"));
        return ui;
    }
}
