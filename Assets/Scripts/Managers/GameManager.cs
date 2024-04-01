using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public bool IsGamePlaying;
    public bool IsGamePaused;

    public int CurrentScore;
    public int PlayTime;
    public int MaxScoreRank = 5;

    public Action ChangeScore;

    private InGameUI _gameUI;

    private string _itemAddress = "Assets/Prefabs/Item/Food.prefab";
    private string _positionAddress = "Assets/Prefabs/Item/ItemPositions.prefab";

    private GameObject _positions;

    public void Initialze()
    {
        _gameUI = Managers.UIManager.ShowUI<InGameUI>();

        CurrentScore = 0;
        PlayTime = 60;
        IsGamePlaying = true;
        IsGamePaused = false;

        ChangeScore -= UpdateScore;
        ChangeScore += UpdateScore;

        // Position Setting and start 
        CreateItemPoolAndPosition();
        Play();
    }

    void CreateItemPoolAndPosition()
    {
        Managers.ResourceManager.LoadAsync(_itemAddress, false, obj =>
        {
            Managers.PoolManager.InitFoodPool(obj.gameObject);
            CreateItemPosition();
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_itemAddress}\" GameObject"));
    }

    async void CreateItemPosition()
    {
        _positions = await Managers.ResourceManager.InstantiateInAsync(_positionAddress);
    }

    public async void Play()
    {
        // playing game
        await UpdateTime();
        
        // end
        End();

    }

    private async UniTask UpdateTime()
    {
        while (PlayTime > 0)
        {
            await UniTask.WaitUntil(() => !IsGamePaused);
            await UniTask.Delay(1000);
            PlayTime -= 1;
            _gameUI.SetUITimeText();
        }
    }

    private void UpdateScore()
    {
        _gameUI.SetUIScoreText();
    }

    public async void End()
    {
        // TODO Game Over
        IsGamePlaying = false;
        SaveScore();

        Debug.Log("∞‘¿” ≥°!");
        await Managers.GameSceneManager.LoadSceneAsync(Enums.Scene.Result);
    }

    private void SaveScore()
    {
        var prevScore = -1;

        for (int i = 1; i <= MaxScoreRank; i++)
        {
            if (!PlayerPrefs.HasKey(i.ToString()))
            {
                if (prevScore < 0) PlayerPrefs.SetInt(i.ToString(), CurrentScore);
                else PlayerPrefs.SetInt(i.ToString(), prevScore);
                break;
            }

            var curRankScore = PlayerPrefs.GetInt(i.ToString());

            if (prevScore >= 0)
            {
                PlayerPrefs.SetInt(i.ToString(), prevScore);
                prevScore = curRankScore;
                continue;
            }

            if (curRankScore < CurrentScore)
            {
                PlayerPrefs.SetInt(i.ToString(), CurrentScore);
                prevScore = curRankScore;
                continue; 
            }
        }
    }
}
