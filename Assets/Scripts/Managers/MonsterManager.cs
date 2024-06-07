using Assembly_CSharp;
using Assets;
using Assets.ObjectPooling;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterCombat;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterMovement;
using Assets.Scripts.InterfacesAndImplementations.Monster.MonsterStats;
using Assets.Scripts.Monsters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject wolfPrefab;
    public GameObject snakePrefab;
    public ObjectPool wolfPool;
    public ObjectPool snakePool;
    public int SnakePathId = 2;
    public int WolfPathId = 1;
    public GoldManager goldManager ;
    public LivesManager livesManager;
    void Start()
    {
        wolfPool.prefab = wolfPrefab;
        snakePool.prefab = snakePrefab;

        wolfPool.Initialize(50);
        snakePool.Initialize(50);
    }

    public GameObject SpawnWolf()
    {
        GameObject wolf = wolfPool.Get();
        wolf.transform.position = new Vector3(-256, 93, 0);
        InitializeWolf(wolf);
        return wolf;
    }

    public GameObject SpawnSnake()
    {
        GameObject snake = snakePool.Get();
        snake.transform.position = new Vector3(-253, 9, 0);
        InitializeSnake(snake);
        return snake;
    }



    private void InitializeWolf(GameObject wolf)
    {
        Troll wolfComponent = wolf.GetComponent<Troll>();
        MonsterCombat monsterCombat = new MonsterCombat();
        CheckPointMonsterMovement monsterMovement = new CheckPointMonsterMovement();
        MonsterStats monsterStats = new MonsterStats();
        // set listeners
        monsterStats.OnHpZeroOrBelow += () => HandleMonsterHpZeroOrBelow(wolf);
        monsterMovement.OnLastCheckpointReached += (gameObject) => HandleMonsterReachedLastCheckpoint(gameObject);

        monsterStats.SetHealthBar(wolfComponent.healthBar);
        wolfComponent.InitializeWolf(monsterCombat, monsterMovement, monsterStats, WolfPathId);
       
    }


    private void InitializeSnake(GameObject snake)
    {
        Medusa snakeComponent = snake.GetComponent<Medusa>();
        MonsterCombat monsterCombat = new MonsterCombat();
        CheckPointMonsterMovement monsterMovement = new CheckPointMonsterMovement();
        MonsterStats monsterStats = new MonsterStats();

        
        monsterStats.OnHpZeroOrBelow += () => HandleMonsterHpZeroOrBelow(snake);
        monsterMovement.OnLastCheckpointReached += (gameObject) => HandleMonsterReachedLastCheckpoint(gameObject);
        monsterStats.SetHealthBar(snakeComponent.healthBar);
        snakeComponent.InitializeSnake(monsterCombat, monsterMovement, monsterStats,SnakePathId);
        
    }

   
    private void HandleMonsterHpZeroOrBelow(GameObject gameObject)
    {
        if (gameObject.GetComponent<Troll>())
        {

            var wolf = gameObject.GetComponent<Troll>();
            goldManager.AddGold(wolf.MonsterStats.GoldValue);
            wolf.MonsterStats.Reset();
            // Return to pool
            wolfPool.Return(gameObject);
            // Reset stats
            gameObject.GetComponent<Troll>().MonsterStats.Reset();
            
        }
        else if (gameObject.GetComponent<Medusa>())
        {
            var snake = gameObject.GetComponent<Medusa>();
            goldManager.AddGold(snake.MonsterStats.GoldValue);
            // Return to pool
            snake.MonsterStats.Reset();
            snakePool.Return(gameObject);
            // Reset stats
            gameObject.GetComponent<Medusa>().MonsterStats.Reset();
        }
       
    }

    private void HandleMonsterReachedLastCheckpoint(GameObject monster)
    {

        if (monster.GetComponent<Troll>() != null)
        {
            var wolf = monster.GetComponent<Troll>();
            wolf.MonsterStats.Reset(); 
            wolfPool.Return(monster);
        }
            
        else if (monster.GetComponent<Medusa>() != null)
        {
            var snake = monster.GetComponent<Medusa>();
            snake.MonsterStats.Reset(); 
            snakePool.Return(monster);
        }
           
        livesManager.LoseLife();
    }
}
