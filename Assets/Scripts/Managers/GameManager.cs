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

    private string _favoriteFoodAddress = "Assets/Prefabs/Item/FavoriteFood.prefab";
    private string _hateFoodAddress = "Assets/Prefabs/Item/HateFood.prefab";
    private string _positionAddress = "Assets/Prefabs/Item/ItemPositions.prefab";
    private string _pathBgm = "Assets/Sounds/BGM/InGame.wav";

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

        CreateItemPoolAndPosition();
        Play();
    }

    void CreateItemPoolAndPosition()
    {
        Managers.ResourceManager.LoadAsync(_favoriteFoodAddress, false, obj =>
        {
            // TODO 추후 데이터 수정
            Managers.PoolManager.InitFoodPool(obj.gameObject, 5);
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_favoriteFoodAddress}\" GameObject"));

        Managers.ResourceManager.LoadAsync(_hateFoodAddress, false, obj =>
        {
            // TODO 추후 데이터 수정
            Managers.PoolManager.InitFoodPool(obj.gameObject, 5, Enums.Item.Hate);
            CreateItemPosition();
        }, () => Debug.LogError($"[ResourceManager] Failed Loading \"{_hateFoodAddress}\" GameObject"));
    }

    async void CreateItemPosition()
    {
        _positions = await Managers.ResourceManager.InstantiateInAsync(_positionAddress);
    }

    public async void Play()
    {
        Managers.SoundManager.Play(_pathBgm, SoundManager.SoundType.BGM);

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
