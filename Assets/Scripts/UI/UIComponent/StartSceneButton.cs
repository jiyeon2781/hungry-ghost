using UnityEngine;
using Cysharp.Threading.Tasks;

public class StartSceneButton : MonoBehaviour
{
    public async void StartGameAsync()
    {
        await Managers.GameSceneManager.LoadSceneAsync(Enums.Scene.InGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}