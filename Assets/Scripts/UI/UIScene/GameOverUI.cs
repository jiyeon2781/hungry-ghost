using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : UIBase
{
    private string _address = "Assets/Arts/UI/CharacterUI/";

    [SerializeField] private string _happyGhostImageName = "happy-ghost.png";
    [SerializeField] private string _sadGhostImageName = "sad-ghost.png";

    [SerializeField] private UIText _subText;
    [SerializeField] private UIText _scoreText;
    [SerializeField] private Image _ghostImg;

    protected override void Init()
    {
        if (Managers.GameManager.CurrentScore <= 0)
        {
            _subText.SetText("유령은 채소를 너무 많이 먹었어요..");
            SetImage(false);
        }
        else if (Managers.GameManager.CurrentScore >= 100)
        {
            _subText.SetText("유령은 좋아하는 음식을 많이 먹어서 너무 행복해요!");
            SetImage(true);
        }
        _scoreText.SetText(Managers.GameManager.CurrentScore.ToString() + "점");
    }

    private void SetImage(bool isHappy)
    {
        if (!isHappy) Managers.ResourceManager.LoadSprite(_address + _sadGhostImageName, sprite => _ghostImg.sprite = sprite);
        else Managers.ResourceManager.LoadSprite(_address + _happyGhostImageName, sprite => _ghostImg.sprite = sprite);
    }

}
