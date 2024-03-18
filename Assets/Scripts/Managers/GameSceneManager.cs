using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class GameSceneManager
{

    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Enums.Scene scene)
    {
        SceneManager.LoadScene(GetSceneName(scene));
    }

    public async UniTask LoadSceneAsync(Enums.Scene scene)
    {
        await SceneManager.LoadSceneAsync(GetSceneName(scene));
    }

    string GetSceneName(Enums.Scene type)
    {
        return Enum.GetName(typeof(Enums.Scene), type) + "Scene";
    }
}
