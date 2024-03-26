using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGamePlaying;
    public int CurrentScore;
    public int PlayTime;

    private GameObject _objUI;

    public void Initialze()
    {
        // TODO Init
        CurrentScore = 0;
        PlayTime = 60;

        // UI Setting
        var ui = Managers.UIManager.ShowUI<InGameUI>("InGameUI");
        _objUI = ui.gameObject;

        // Game Start
        Play();
    }

    public void Play()
    {
        // TODO Game Start
        IsGamePlaying = true;
        
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
