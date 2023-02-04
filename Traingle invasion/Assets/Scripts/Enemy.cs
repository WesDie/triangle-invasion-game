using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed = -1;

    GameObject gameOverText;
    GameObject gameOverTextBackground;
    GameObject GameOverTextRestart;
    GameObject GameManager;
    GameObject player;
    public ParticleSystem ExplosionFX;
    public ParticleSystem smallExplosionFX;
    GameObject mainCamera;

    Movement playerScript;
    EnemySpawn enemeySpawnScript;
    GameObject gameScoretext;
    public float Health;
    GameObject gameOverUI;

    void Start()
    {
        gameOverUI = GameObject.Find("GameOverUI");
        gameScoretext = GameObject.Find("Scoretext");
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        enemeySpawnScript = GameManager.GetComponent<EnemySpawn>();
        playerScript = player.GetComponent<Movement>();
    }


    void Update()
    {
        transform.Translate((transform.up * speed * Time.deltaTime));

        if(Health == 0){
            Instantiate(ExplosionFX, transform.position, Quaternion.identity);
            mainCamera.GetComponent<mainCamera>().TriggerShake();
            enemeySpawnScript.enemiesDestroyed = enemeySpawnScript.enemiesDestroyed + 1;
            enemeySpawnScript.score = enemeySpawnScript.score + 10;
            gameScoretext.GetComponent<Text>().text = "Score: " + enemeySpawnScript.score.ToString();

            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            if(Health != 10){
                mainCamera.GetComponent<mainCamera>().TriggerShake();
                Instantiate(smallExplosionFX, transform.position, Quaternion.identity);
            }

            Health = Health - 10;
        }
        if (other.tag == "Finish")
        {
            Destroy(gameObject);
            Instantiate(ExplosionFX, transform.position, Quaternion.identity);
            mainCamera.GetComponent<mainCamera>().TriggerShake();
            gameOverUI.SetActive(true);
            playerScript.gameOverisOn = true;

            Time.timeScale = 0;
        }
    }
}
