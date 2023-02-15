using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Waves{
        public int amountOfEnemies;
        public float waveSpeed;
        public float timeBetweenWave;
        public GameObject enemyType1;
        public GameObject enemyType2;
        public GameObject enemyType3;
    }
    public Waves[] waveInfo;

    [Header("Other wave settings")]
    public GameObject[] enemySpawns;

    [Header("Debuging info")]
    public int currentWave = -1;
    public int enmiesKilledInWave;
    public float ttime;

    [Header("Object settings")]
    public GameObject waveCompleteText;
    public GameObject levelCompleteObject;
    public Text currentWaveText;
    

    bool endWaves;
    float timeBetweenCurrentWave;
    float timeBeforeFirstWave;
    int enemiesSpawnedInWave;
    bool waveComplete = false;
    public bool levelComplete = false;

    List<GameObject> availableSpawns = new List<GameObject>();

    void Start()
    {
        currentWaveText.enabled = true;
        timeBetweenCurrentWave = Time.time + 5f;
        timeBeforeFirstWave = Time.time + 5f;
        currentWaveText.text = "Wave 1: 0/" + waveInfo[0].amountOfEnemies;
        var tmp = new Waves();
        waveCompleteText.SetActive(false);
    }


    void Update()
    {
        ttime = Time.time;

        if(waveInfo.Length == currentWave){
            Debug.Log("Level Complete");
            endWaves = true;
        }

        if(levelComplete == true){
            //levelCompleteObject.SetActive(true);
            //Time.timeScale = 0;
        }

        if(Time.time >= timeBetweenCurrentWave && endWaves == false){
            currentWave++;
            CancelInvoke("SpawnWave");
            InvokeRepeating("SpawnWave", 0f, waveInfo[currentWave].waveSpeed);
            timeBetweenCurrentWave = Time.time + waveInfo[currentWave].timeBetweenWave;
            enmiesKilledInWave = 0;
            enemiesSpawnedInWave = 0;
            waveComplete = false;
        } else if (Time.time >= timeBetweenCurrentWave && endWaves == true){
            levelComplete = true;
        }

        if(Time.time >= timeBeforeFirstWave && levelComplete == false && endWaves == false){
            currentWaveText.text = "Wave " + (currentWave + 1) + ": " + enmiesKilledInWave + "/" + waveInfo[currentWave].amountOfEnemies;
        } else{
            currentWaveText.text = "Wave 0: 0/0";
        }
    }

    void SpawnWave(){
        if(enemiesSpawnedInWave <= waveInfo[currentWave].amountOfEnemies - 1){

            int num2 = 0;
            for (int i = 0; i < enemySpawns.Length; i++)
            {
                if(enemySpawns[i].GetComponent<EnemySpawns>().isSpawnActive == true){
                    availableSpawns.Add(enemySpawns[i]);
                    num2++;
                }
            }

            int num1 = Random.Range(0, num2);
            int num3 = Random.Range(0, 2);
            enemiesSpawnedInWave++;
            if(num3 == 0){
                Instantiate(waveInfo[currentWave].enemyType1, availableSpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
            } else if (num3 == 1){
                Instantiate(waveInfo[currentWave].enemyType2, availableSpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
            } else if (num3 == 2){
                Instantiate(waveInfo[currentWave].enemyType3, availableSpawns[num1].transform.position, Quaternion.Euler(new Vector3(180, 0, 0)));
            }
            
            availableSpawns.Clear();

        } else if(waveComplete == false && enmiesKilledInWave == waveInfo[currentWave].amountOfEnemies && endWaves == false){
            CancelInvoke("SpawnWave");
            waveComplete = true;
            Debug.Log("Wave Complete");
            waveCompleteText.GetComponent<TMP_Text>().text = "WAVE " + (currentWave + 1) + " COMPLETED";
            waveCompleteText.SetActive(true);
            Invoke("DisableCompleteText", 3f);
            timeBetweenCurrentWave = Time.time + 7.5f;
        }
    }

    void DisableCompleteText(){
        waveCompleteText.SetActive(false);
    }
}
