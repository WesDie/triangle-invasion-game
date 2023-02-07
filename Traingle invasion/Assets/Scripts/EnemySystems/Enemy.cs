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
    GameObject mainCamera;

    Movement playerScript;
    EnemySpawn enemeySpawnScript;
    GameObject gameScoretext;
    public float Health;
    GameObject gameManagerObject;
    public bool isTargeted = false;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
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

        if(Health <= 0){
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
            if(other.gameObject.GetComponent<projectile>().Upgrade1 == false){
                Health = Health - 10;
            } else if (other.gameObject.GetComponent<projectile>().Upgrade1 == true){
                Health = Health - 100;
            }

        }
        if (other.tag == "Finish")
        {
            Destroy(gameObject);
            Instantiate(ExplosionFX, transform.position, Quaternion.identity);
            mainCamera.GetComponent<mainCamera>().TriggerShake();
            gameManagerObject.GetComponent<ManageGame>().OpenGameOverMenu();
            playerScript.gameOverisOn = true;

            Time.timeScale = 0;
        }
    }
}
