using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title, Playing, Paused, GameOver
}
public enum Difficulty
{
    Easy, Medium, Hard
}

public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score;
    public int scoreMultiplier;

    void SetUp()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                scoreMultiplier = 1;
                break;

            case Difficulty.Medium:
                scoreMultiplier = 2;
                break;

            case Difficulty.Hard:
                scoreMultiplier = 3;
                break;

        }
    }
}