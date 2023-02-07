using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int[] enemiesWavesAndAmount;
    public GameObject normalEnemy;
    public GameObject rareEnemy;
    public GameObject legendaryEnemy;

    void Start()
    {
        InvokeRepeating("SpawnWave1", 0.05f, enemiesWavesAndAmount[0]);
    }


    void Update()
    {
        if(enemiesWavesAndAmount[0] == 0){
            enemiesWavesAndAmount[0] = 1;
            SpawnWave2();
        }
        if(enemiesWavesAndAmount[1] == 0){

        }
        if(enemiesWavesAndAmount[2] == 0){

        }

    }

    void SpawnWave1(){
        Instantiate(normalEnemy, new Vector3(Random.Range(-7, 7), 1, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
        enemiesWavesAndAmount[0]--;
    }

    void SpawnWave2(){
        Instantiate(normalEnemy, new Vector3(Random.Range(-7, 7), 1, 0), Quaternion.Euler(new Vector3(180, 0, 0)));
    }

    void SpawnWave3(){

    }

    void SpawnWave4(){

    }
}
