using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGamePlaying;
    public int CurrentScore;
    public int PlayTime;

    // private GameObject _objUI;
    private InGameUI _gameUI;

    public void Initialze()
    {
        // TODO Init
        _gameUI = Managers.UIManager.ShowUI<InGameUI>("InGameUI");

        CurrentScore = 0;
        PlayTime = 60;
        IsGamePlaying = true;

        Play();
    }
    public void Play()
    {
        // Position Setting and start 

        // playing game

        // end

    }

    public void End()
    {
        // TODO Game Over
        IsGamePlaying = false;
    }
}
