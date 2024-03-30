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
        var isDestroy = true;
        if (_showUIName == "RankingUI") isDestroy = false;
        Managers.UIManager.ShowUI<UIBase>(_showUIName, isDestroy);
    }

    public void OnClickBackButton()
    {
        Managers.UIManager.BackUI<UIBase>();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
