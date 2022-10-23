using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType MyType;
    public float mySpeed = 2f;
    public int myHealth = 100;
    Transform moveToPos;
    EnemyManager _EM;       //calls another script into this script

    [Header("AI")]              //creates a new header
    public PatrolType myPatrol;
    int patrolPoint = 0;        //needed for linear patrol movement
    bool reverse = false;       //needed for repeat patrol movement
    Transform startPos;         //needed for repeat patrol movement
    Transform endPos;           //needed for repeat patrol movement

    void Start()
    {
        _EM = FindObjectOfType<EnemyManager>();     //Immediately searches when game starts
        SetUp();
        StartCoroutine(move());
        SetupAI();
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
}