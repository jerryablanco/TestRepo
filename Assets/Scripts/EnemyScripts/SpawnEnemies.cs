
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private int totalNumberOfEnemies = 10;
    [SerializeField]
    private int maxNumberOfEnemies = 50;
    [SerializeField]
    //private SpawnPoints spawnPoints; (get spawn points class)
    private Vector2[] spawnPoints;
    
    [SerializeField]
    private float initialSpawnDelay = 2f;
    [SerializeField]
    private float difficultyTimer = 3f;
    [SerializeField]
    private float minimumSpawnDelay = .1f;
    private float currentSpawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        InvokeRepeating("SpawnEnemy", 0f, currentSpawnDelay);
        StartCoroutine(SpawnTimerIncrease());
    }


    private void SpawnEnemy()
    {
        GameObject enemy = EnemyPool.enemyPoolInstance.GetEnemy();
        Vector2 spawnPoint = GetNewSpawnPoint();
        enemy.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, 0);
        enemy.SetActive(true);
    }

    private IEnumerator SpawnTimerIncrease()
    {
        yield return new WaitForSeconds(difficultyTimer);
        currentSpawnDelay = currentSpawnDelay - .1f;
        if (currentSpawnDelay < minimumSpawnDelay) {
            currentSpawnDelay = minimumSpawnDelay;
        }
        totalNumberOfEnemies++;
        if (totalNumberOfEnemies > maxNumberOfEnemies) {
            totalNumberOfEnemies = maxNumberOfEnemies;
        }
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 0f, currentSpawnDelay);
    }


    //Todo: refactor to be vector 3 (was using different method before)
    private Vector2 GetNewSpawnPoint()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 13);
        return spawnPoints[randomNumber];
        //return spawnPoints.locations[randomNumber];
    }

}