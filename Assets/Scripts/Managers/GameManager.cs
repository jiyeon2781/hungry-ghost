using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public bool IsGamePlaying;
    public int CurrentScore;
    public int PlayTime;

    public void Initialze()
    {
        // TODO Init
        CurrentScore = 0;
        PlayTime = 60;

        // UI Setting

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
