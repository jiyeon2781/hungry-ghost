using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : UIBase
{
    [SerializeField] private UIText _scoreText;
    [SerializeField] private UIText _timeText;

    protected override void Init()
    {
        SetUIText();
    }

    public void SetUIText()
    {
        _scoreText.SetText(Managers.GameManager.CurrentScore + "¡°");
        _timeText.SetText(Managers.GameManager.PlayTime + "√ ");
    }
}
