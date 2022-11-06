using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using UnityEngine;

public enum EnemyType
{
    OneHand, TwoHand, Archer
}

public enum PatrolType
{
    Linear, Random, Loop
}

public class EnemyManager : Singleton<EnemyManager>
{
    //Using square brackets, it calls in an array. 
    public Transform[] spawnPoints;     //Contains the amount of spawnpoints
    public List<GameObject> enemies;    //Contains all the enemies in the scene
    public GameObject[] enemyTypes;     //Contains Enemy Types
    public string[] enemyNames;

    public float spawnDelay = 2f;
    public int spawnCount = 10;         // need to add value to change in order to work

    public string killCondition = "Two";

    GameManager _GM;
    UIManager _UI;

    void Start()
    {
        _GM = FindObjectOfType<GameManager>();
        _UI = FindObjectOfType<UIManager>();
        StartCoroutine(SpawnDelay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnEnemy();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            KillAllEnemies();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            KillSpecificEnemy(killCondition);
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (_GM.gameState == GameState.Playing)
            SpawnEnemy();

        if (enemies.Count <= spawnCount)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    /// <summary>
    /// Spawns one enemy
    /// </summary>
    void SpawnEnemy()
    {
        //Random.Range to get a random range (<Minimum>, <Maximum>)
        //Spawns an enemy. (<The object>, <the position>)

        int enemyNumber = Random.Range(0, enemyNames.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyTypes[enemyNumber], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation, transform);
        enemies.Add(enemy);
        //print(enemies.Count);
    }
    /// <summary>
    /// Spawns all enemies to the spawnpoints
    /// </summary>
    void SpawnEnemies()
    {
        int enemyNumber = Random.Range(0, enemyNames.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPoints[i].position, spawnPoints[i].rotation, transform);
            enemies.Add(enemy);
            _UI.UpdateEnemyCount(enemies.Count);
        }
    }
    /// <summary>
    /// Kills specific enemy
    /// </summary>
    /// <param name="_enemy"></param>
    public void KillEnemy(GameObject _enemy)
    {
        //kills an enemy. Destroy the GameObject
        if (enemies.Count == 0)
            return;

        Destroy(_enemy);
        enemies.Remove(_enemy);

        //Destroys the first enemy
        //Destroy(enemies[0]);
        //enemies.RemoveAt(0);

        //Destroys the last one

        //Destroy(enemies[enemies.Count-1]);
        //enemies.RemoveAt(enemies.Count-1);

        print(enemies.Count);
    }

    /// <summary>
    /// Kills all enemies within our scene
    /// </summary>
    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;

        int eCount = enemies.Count;
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }
    /// <summary>
    /// kills an enemy of specified condition
    /// </summary>
    /// <param name="_condition">The condition of the enemy we want to kill</param>
    void KillSpecificEnemy(string _condition)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
            {
                KillEnemy(enemies[i]);
            }
        }
    }

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}