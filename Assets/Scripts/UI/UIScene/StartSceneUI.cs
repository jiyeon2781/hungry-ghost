using UnityEngine;
using Cysharp.Threading.Tasks;

public class StartSceneUI : UIBase
{
    protected override void Init()
    {

    }

    public async void StartGameAsync()
    {
        await Managers.GameSceneManager.LoadSceneAsync(Enums.Scene.InGame);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}