using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingUI : UIBase
{
    [SerializeField] private List<UIText> textLists;

    protected override void Init()
    {
        SetRankTexts();
    }

    private void SetRankTexts()
    {
        for (int i = 1; i <= Managers.GameManager.GameData.SCORE_RANK_MAX_COUNT; i++)
        {
            if (!PlayerPrefs.HasKey(i.ToString())) break;
            textLists[i-1].SetText(i + ". " + PlayerPrefs.GetInt(i.ToString()));
        }
    }
}
