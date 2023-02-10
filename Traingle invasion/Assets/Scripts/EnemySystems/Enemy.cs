using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = -1;
    public ParticleSystem ExplosionFX;
    public float Health;
    public bool isTargeted = false;
    public float enemyEffectReward = 0.02f;

    [Header("Enemy Shoot Settings")]
    public bool enemyCanShoot = false;
    public float enemyShootSpeed = 0.5f;
    public GameObject shootProjectile;
    public float kickbackForce = 1f;


    GameObject mainCamera;
    Movement playerScript;
    ManageGame manageGameScript;
    WaveSpawner enemeyWaveScript;
    GameObject gameScoretext;
    GameObject gameOverText;
    GameObject gameOverTextBackground;
    GameObject GameOverTextRestart;
    GameObject GameManager;
    GameObject player;
    GameObject gameManagerObject;
    float projectileDamage = 0;
    Rigidbody2D body;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameScoretext = GameObject.Find("Scoretext");
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        manageGameScript = GameManager.GetComponent<ManageGame>();
        enemeyWaveScript = GameManager.GetComponent<WaveSpawner>();
        playerScript = player.GetComponent<Movement>();
        body = GetComponent<Rigidbody2D>();
        if(enemyCanShoot == true){
            Invoke("EnemyShoot", enemyShootSpeed);
        }
    }

    void EnemyShoot(){
        Instantiate(shootProjectile, transform.position, Quaternion.identity);

        body.AddForce(transform.up * -kickbackForce);
        body.drag = 10f;
        Invoke("EnemyShoot", enemyShootSpeed);
    }

    void Update()
    {
        transform.Translate((transform.up * speed * Time.deltaTime));

        if (Health <= 0)
        {
            Instantiate(ExplosionFX, transform.position, Quaternion.identity);
            mainCamera.GetComponent<mainCamera>().TriggerShake();
            enemeyWaveScript.enmiesKilledInWave = enemeyWaveScript.enmiesKilledInWave + 1;
            manageGameScript.score = manageGameScript.score + 10;
            manageGameScript.effectOverheatBarFillValue = manageGameScript.effectOverheatBarFillValue + enemyEffectReward;
            gameScoretext.GetComponent<Text>().text = "Score: " + manageGameScript.score.ToString();

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            projectileDamage = other.gameObject.GetComponent<projectile>().projectileDamage;
            Health = Health - projectileDamage;
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
