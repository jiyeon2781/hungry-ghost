using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : UIBase
{
    [SerializeField] private UIText _scoreText;
    [SerializeField] private UIText _timeText;
    [SerializeField] private UIButton _pausedButton;

    protected override void Init()
    {
        SetUITimeText();
        SetUIScoreText();
        _pausedButton.OnClickButton += CompleteClickPausedButton;
    }

    private void OnDestroy()
    {
        _pausedButton.OnClickButton -= CompleteClickPausedButton;
    }

    public void SetUITimeText()
    {
        _timeText.SetText(Managers.GameManager.PlayTime + "√ ");
    }

    public void SetUIScoreText()
    {
        _scoreText.SetText(Managers.GameManager.CurrentScore.ToString() + "¡°");
    }

    public void CompleteClickPausedButton()
    {
        Managers.GameManager.IsGamePaused = true;
    }
}
