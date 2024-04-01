using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UIBase
{
    [SerializeField] private Enums.Scene _scene = Enums.Scene.Default;
    [SerializeField] private string _showUIName = "RankingUI";

    private Button _button;
    private TMP_Text _buttonText;

    public Action OnClickButton;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponentInChildren<TMP_Text>();
    }

    protected override void Init()
    {

    }

    public void OnClickOpenScene()
    {
        Managers.GameSceneManager.LoadScene(_scene);
    }

    public void OnClickShowUI()
    {
        OnClickButton?.Invoke();
        Managers.UIManager.ShowUI<UIBase>(_showUIName);
    }

    public void OnClickShowUIDoNotDestroy()
    {
        OnClickButton?.Invoke();
        Managers.UIManager.ShowUI<UIBase>(_showUIName, false);
    }

    public void OnClickBackButton()
    {
        OnClickButton?.Invoke();
        Managers.UIManager.BackUI<UIBase>();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
