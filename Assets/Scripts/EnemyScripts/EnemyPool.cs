using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool enemyPoolInstance;

    [SerializeField]
    private GameObject pooledEnemy;
    private bool notEnoughMonstersInPool = true;

    private List<GameObject> enemies;

    //TODO Make Spawn Points class
    //public SpawnPoints spawnPoints;


    private void Awake()
    {
        enemyPoolInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    public GameObject GetEnemy()
    {
        if (enemies.Count > 0) {
            for (int i = 0; i < enemies.Count; i++) {
                if (!enemies[i].activeInHierarchy) {
                    return enemies[i];
                }
            }
            notEnoughMonstersInPool = true;
        }

        if (notEnoughMonstersInPool) {
            GameObject enemy = Instantiate(pooledEnemy);
            enemy.SetActive(false);
            enemies.Add(enemy);
            return enemy;
        }

        return null;
    }
}
