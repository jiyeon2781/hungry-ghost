using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager {
    private Dictionary<string, GameObject> _resources = new Dictionary<string, GameObject>();
    // Address, Object 저장
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

    public async UniTask<GameObject> InstantiateInAsync(string address, Transform parent = null, bool isCaching = false)
    {
        GameObject result = null;
        LoadAsync(address, isCaching, obj =>
        {
            result = UnityEngine.Object.Instantiate(obj, parent);
        }, () => Debug.LogError($"[ResourceManager] Failed to load \"{address}\" GameObject"));

        await UniTask.WaitUntil(() => result != null);
        return result;
    }

    public GameObject Instantiate(string address, Transform parent = null)
    {
        var prefab = Addressables.LoadAssetAsync<GameObject>(address).WaitForCompletion();
        Addressables.Release(prefab);

        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Failed to load {address} prefab");
            return null;
        }

        return UnityEngine.Object.Instantiate(prefab, parent);
    }

    public void Unload(GameObject obj)
    {
        Addressables.Release<GameObject>(obj);
    }

    public void LoadSprite(string address, Action<Sprite> onSuccess)
    {
        var sptire = Addressables.LoadAssetAsync<Sprite>(address).WaitForCompletion();
        if (sptire == null) Debug.LogError($"[ResourceManager] Failed to load {address} sprite");
        else onSuccess.Invoke(sptire);
    }

    public AudioClip LoadAudioClip(string address)
    {
        AudioClip audioClip = null;
        audioClip = Addressables.LoadAssetAsync<AudioClip>(address).WaitForCompletion();
        if (audioClip == null) Debug.LogError($"[ResourceManager] Failed to load {address} audio Clip");
        return audioClip;
    }
}