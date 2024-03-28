using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : UIBase
{
    [SerializeField] private UIText _scoreText;
    [SerializeField] private UIText _timeText;

    protected override void Init()
    {
        SetUITimeText();
        SetUIScoreText();
    }

    public void SetUITimeText()
    {
        _timeText.SetText(Managers.GameManager.PlayTime + "√ ");
    }

    public void SetUIScoreText()
    {
        _scoreText.SetText(Managers.GameManager.CurrentScore + "¡°");
    }
}
