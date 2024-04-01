using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : UIBase
{
    [SerializeField] private UIButton _playButton;
    protected override void Init()
    {
        _playButton.OnClickButton += PlayGame;
    }
    private void OnDestroy()
    {
        _playButton.OnClickButton -= PlayGame;
    }

    private void PlayGame()
    {
        Managers.GameManager.IsGamePaused = false;
    }
}
