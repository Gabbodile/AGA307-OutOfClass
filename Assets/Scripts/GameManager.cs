using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title, Playing, Paused, GameOver
}
public enum Difficulty
{
    Easy, Medium, Hard
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score;
    public int scoreMultiplier;
    float timer;

    //public static event Action<Difficulty> OnDifficultyChanged = null;

    private void Start()
    {
        timer = 0;
        SetUp();
        //OnDifficultyChanged?.Invoke(difficulty);
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            timer += Time.deltaTime;
            //_UI.UpdateTimer(timer);
        }
    }

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

    public void AddScore(int _score)
    {
        score = _score * scoreMultiplier;
        //_UI.UpdateScore(score);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeDifficulty(int _difficulty)
    {
        difficulty = (Difficulty)_difficulty;
        SetUp();
    }

    /*private void OnEnable()
    {
        Enemy.OnEnemyHit += OnEnemyHit;
        Enemy.OnEnemyDie += OnEnemyDie;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHit -= OnEnemyHit;
        Enemy.OnEnemyDie -= OnEnemyDie;
    }
    */

    void OnEnemyHit(GameObject _enemy)
    {
        AddScore(10);
    }
    void OnEnemyDie(GameObject _enemy)
    {
        AddScore(100);
    }
}