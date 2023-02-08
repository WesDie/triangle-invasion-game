using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public int currentWave = 1;
    public int[] enemiesWavesAndAmount;
    public GameObject[] enemySpawns;
    public GameObject normalEnemy;
    public GameObject rareEnemy;
    public GameObject legendaryEnemy;
    public Text currentWaveText;
    public int enmiesKilledInWave;
    public float ttime;
    

    private bool endWaves;
    private float timeBetweenCurrentWave;
    private int enemiesSpawnedInWave;

    void Start()
    {
        timeBetweenCurrentWave = 5f;
        currentWaveText.text = "Wave 1: 0/" + enemiesWavesAndAmount[0];
    }


    void Update()
    {
        ttime = Time.time;

        if(enemiesWavesAndAmount.Length + 1 == currentWave){
            Debug.Log("Level Complete");
            endWaves = true;
        }

        if(Time.time >= timeBetweenCurrentWave && endWaves == false){
            if(currentWave == 1){
                InvokeRepeating("SpawnWave1", 0f, 2f);
                timeBetweenCurrentWave = Time.time + 30f;
                enmiesKilledInWave = 0;
                enemiesSpawnedInWave = 0;
                currentWave++;
            } else if(currentWave == 2){
                InvokeRepeating("SpawnWave2", 0f, 1f);
                CancelInvoke("SpawnWave1");
                timeBetweenCurrentWave = Time.time + 30f;
                enmiesKilledInWave = 0;
                enemiesSpawnedInWave = 0;
                currentWave++;
            }else if(currentWave == 3){
                InvokeRepeating("SpawnWave3", 0f, 0.5f);
                CancelInvoke("SpawnWave2");
                timeBetweenCurrentWave = Time.time + 30f;
                enmiesKilledInWave = 0;
                enemiesSpawnedInWave = 0;
                currentWave++;
            }else if(currentWave == 4){
                InvokeRepeating("SpawnWave4", 0f, 2f);
                CancelInvoke("SpawnWave3");
                timeBetweenCurrentWave = Time.time + 30f;
                enmiesKilledInWave = 0;
                enemiesSpawnedInWave = 0;
                currentWave++;
            }else if(currentWave == 5){
                InvokeRepeating("SpawnWave5", 0f, 2f);
                CancelInvoke("SpawnWave4");
                timeBetweenCurrentWave = Time.time + 30f;
                enmiesKilledInWave = 0;
                enemiesSpawnedInWave = 0;
                currentWave++;
            }
        }

        if(Time.time >= 5f){
            currentWaveText.text = "Wave " + (currentWave - 1) + ": " + enmiesKilledInWave + "/" + enemiesWavesAndAmount[currentWave - 2];
        } else{
            currentWaveText.text = "Wave 0: 0/0";
        }

    }

    void SpawnWave1(){
        if(enemiesSpawnedInWave <= enemiesWavesAndAmount[0] - 1){
            int num1 = Random.Range(0, enemySpawns.Length);
            enemiesSpawnedInWave++;
            Instantiate(normalEnemy, enemySpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        } else if(endWaves == true){
            Debug.Log("Level Complete");
        } else{
            Debug.Log("Wave 1 Complete");
        }
    }


    void SpawnWave2(){
        if(enemiesSpawnedInWave <= enemiesWavesAndAmount[1] - 1){
            int num1 = Random.Range(0, enemySpawns.Length);
            enemiesSpawnedInWave++;
            Instantiate(normalEnemy, enemySpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        } else if(endWaves == true){
            Debug.Log("Level Complete");
        } else{
            Debug.Log("Wave 2 Complete");
        }
    }

    void SpawnWave3(){
        if(enemiesSpawnedInWave <= enemiesWavesAndAmount[2] - 1){
            int num1 = Random.Range(0, enemySpawns.Length);
            enemiesSpawnedInWave++;
            Instantiate(normalEnemy, enemySpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        } else if(endWaves == true){
            Debug.Log("Level Complete");
        } else{
            Debug.Log("Wave 3 Complete");
        }
    }

    void SpawnWave4(){
        if(enemiesSpawnedInWave <= enemiesWavesAndAmount[3] - 1){
            int num1 = Random.Range(0, enemySpawns.Length);
            enemiesSpawnedInWave++;
            Instantiate(rareEnemy, enemySpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        } else if(endWaves == true){
            Debug.Log("Level Complete");
        } else{
            Debug.Log("Wave 4 Complete");
        }
    }
    void SpawnWave5(){
        if(enemiesSpawnedInWave <= enemiesWavesAndAmount[4] - 1){
            int num1 = Random.Range(0, enemySpawns.Length);
            enemiesSpawnedInWave++;
            Instantiate(legendaryEnemy, enemySpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        } else if(endWaves == true ){
            Debug.Log("Level Complete");
        } else{
            Debug.Log("Wave 5 Complete");
        }
    }
}
