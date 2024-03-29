using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UIBase
{
    [SerializeField] private Enums.Scene scene = Enums.Scene.Default;
    private Button button;
    private TMP_Text buttonText;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
    }

    protected override void Init()
    {

    }

    public void OnClickNextScene()
    {
        Managers.GameSceneManager.LoadScene(scene);
    }

    public void OnClickRankingUI()
    {
        Managers.UIManager.ShowUI<RankingUI>("RankingUI");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
