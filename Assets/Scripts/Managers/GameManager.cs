using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public bool IsGamePlaying;
    public int CurrentScore;
    public int PlayTime;

    public void GameStart()
    {
        // TODO Game Start
        IsGamePlaying = true;
        CurrentScore = 0;
        PlayTime = 60;
    }

    public void GameOver()
    {
        // TODO Game Over
        IsGamePlaying = false;
    }
}
