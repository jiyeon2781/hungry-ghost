using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager {
    private Dictionary<string, GameObject> _resources = new Dictionary<string, GameObject>();
    // Address, Object∏¶ ¿˙¿Â
    public ResourceManager() { _resources.Clear(); _resources = new Dictionary<string, GameObject>(); }

    private GameObject _object;

    public void LoadAsync(string address, bool isCaching, Action<GameObject> onSuccess, Action onFailed)
    {
        AsyncOperationHandle<GameObject> _handle;

        if (isCaching)
        {
            _resources.TryGetValue(address, out _object);
            if (_object != null) onSuccess.Invoke(_object);
        }

        _handle = Addressables.LoadAssetAsync<GameObject>(address);
        _handle.Completed += operation =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                _object = _handle.Result;
                Addressables.Release(_handle);
                if (isCaching) _resources.Add(address, _object);
                onSuccess.Invoke(_object);
            }
            else
                onFailed?.Invoke();
        };
    }



    public void Unload(GameObject obj)
    {
        Addressables.Release<GameObject>(obj);
    }
}