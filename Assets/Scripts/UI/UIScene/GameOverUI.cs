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
            _subText.SetText("������ ä�Ҹ� �ʹ� ���� �Ծ����..");
            SetImage(false);
        }
        else if (Managers.GameManager.CurrentScore >= 100)
        {
            _subText.SetText("������ �����ϴ� ������ ���� �Ծ �ʹ� �ູ�ؿ�!");
            SetImage(true);
        }
        _scoreText.SetText(Managers.GameManager.CurrentScore.ToString() + "��");
    }

    private void SetImage(bool isHappy)
    {
        if (!isHappy) Managers.ResourceManager.LoadSprite(_address + _sadGhostImageName, sprite => _ghostImg.sprite = sprite);
        else Managers.ResourceManager.LoadSprite(_address + _happyGhostImageName, sprite => _ghostImg.sprite = sprite);
    }

}
