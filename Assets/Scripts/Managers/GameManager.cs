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

    public GameData GameData = new GameData();

    public Action ChangeScore;

    private InGameUI _gameUI;

    public void Initialze()
    {
        _gameUI = Managers.UIManager.ShowUI<InGameUI>();

        CurrentScore = 0;
        PlayTime = GameData.START_PLAY_TIME;
        IsGamePlaying = true;
        IsGamePaused = false;

        ChangeScore -= UpdateScore;
        ChangeScore += UpdateScore;

        CreateItemPoolAndPosition();
        Play();
    }

    void CreateItemPoolAndPosition()
    {
        Managers.ResourceManager.LoadAsync(GameData.FavoriteFoodAddress, false, obj =>
        {
            Managers.PoolManager.InitFoodPool(obj.gameObject, GameData.FAVORITE_FOOD_MAX_COUNT);
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{GameData.FavoriteFoodAddress}\" GameObject"));

        Managers.ResourceManager.LoadAsync(GameData.HateFoodAddress, false, obj =>
        {
            Managers.PoolManager.InitFoodPool(obj.gameObject, GameData.HATE_FOOD_MAX_COUNT, Enums.Item.Hate);
            CreateItemPosition();
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{GameData.HateFoodAddress}\" GameObject"));
    }

    async void CreateItemPosition()
    {
        await Managers.ResourceManager.InstantiateInAsync(GameData.PositionAddress);
    }

    public async void Play()
    {
        Managers.SoundManager.Play(GameData.InGamePathBGM, SoundManager.SoundType.BGM);

        await UpdateTime();
        
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
        IsGamePlaying = false;
        SaveScore();
        Managers.PoolManager.Clear();
        Managers.SoundManager.StopBGM();
        await Managers.GameSceneManager.LoadSceneAsync(Enums.Scene.Result);
    }

    private void SaveScore()
    {
        var prevScore = -1;

        for (int i = 1; i <= GameData.SCORE_RANK_MAX_COUNT; i++)
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
