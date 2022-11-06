using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour
{
    /*public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDie = null;*/ 

    public EnemyType MyType;
    public float mySpeed = 2f;
    public int myHealth = 100;
    Transform moveToPos;  

    [Header("AI")]              
    public PatrolType myPatrol;
    int patrolPoint = 0; 
    bool reverse = false;
    Transform startPos;
    Transform endPos;


    void Start()
    {
        SetUp();
        StartCoroutine(move());
        SetupAI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Hit(10);
        }
    }
    void SetUp()
    {
        switch (MyType)
        {
            case EnemyType.OneHand:
                myHealth = 100;
                mySpeed = 2f;
                myPatrol = PatrolType.Linear;
                break;

            case EnemyType.TwoHand:
                myHealth = 200;
                mySpeed = 1f;
                myPatrol = PatrolType.Loop;
                break;

            case EnemyType.Archer:
                myHealth = 50;
                mySpeed = 5f;
                myPatrol = PatrolType.Random;
                break;
        }
    }

    void Hit(int _damage)
    {
        myHealth -= _damage;
        if (myHealth <= 0)
            Die();
        else
            _GM.AddScore(10);
    }

        void SetupAI()
    {
        startPos = transform;
        endPos = _EM.GetRandomSpawnPoint();
        moveToPos = endPos;
    }

    IEnumerator move()
    {
        switch (myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoints[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoints.Length ? patrolPoint + 1 : 0;
                break;

            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnPoint();
                break;

            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;     //reverse a bool
                break;
        }

        transform.LookAt(moveToPos);
        while (Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * mySpeed);
            yield return null;
        }
    }
    public void Die()
    {
        StopAllCoroutines();
        _GM.AddScore(100);
        _EM.KillEnemy(this.gameObject);
    }
}