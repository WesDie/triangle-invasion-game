using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject EnemyRarePrefab;
    public GameObject EnemyLegendaryPrefab;

    float timeBetweenEnemies = 1;

    public int enemiesDestroyed = 0;
    public int randomEnemiesSpawn = 4;
    public int score = 0;

    void Start(){
        InvokeRepeating("EnemiesSpawn", 0, timeBetweenEnemies);
    }
    
    void EnemiesSpawn()
    {
        if(enemiesDestroyed > 10 && enemiesDestroyed < 20){
            randomEnemiesSpawn = 3;
        } else if(enemiesDestroyed > 25 && enemiesDestroyed < 50){
            randomEnemiesSpawn = 2;

            int enemyTimeRare = Random.Range(1, 3);
            if (enemyTimeRare == 1)
            {
                Instantiate(EnemyRarePrefab, new Vector3(Random.Range(-8, 8), 6, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
            }
        } else if(enemiesDestroyed > 50){
            randomEnemiesSpawn = 1;

            int enemyTimeRare = Random.Range(1, 3);
             if (enemyTimeRare == 1)
            {
                Instantiate(EnemyRarePrefab, new Vector3(Random.Range(-8, 8), 6, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
            }

            int enemyTimeLegendary = Random.Range(1, 10);
             if (enemyTimeRare == 1)
            {
                Instantiate(EnemyLegendaryPrefab, new Vector3(Random.Range(-8, 8), 6, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
            }
        }

        int enemyTime = Random.Range(1, randomEnemiesSpawn);

        if (enemyTime == 1)
        {
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-8, 8), 6, 0), Quaternion.Euler(new Vector3(180, 0, 0)));

            if(timeBetweenEnemies != 0)
            {
                timeBetweenEnemies = timeBetweenEnemies - 0;
            }
        }
    }
}
