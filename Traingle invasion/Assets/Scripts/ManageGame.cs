using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public float refillvalue = 0.01f;
    public float refillvalueLimit = 0.0000005f;
    public GameObject OverheatBar;
    public Image OverheatBarImage;
    public bool limitIsReached = false;
    public GameObject mainMenuObject;
    public GameObject gameUIObject;
    public GameObject gameoverUIObject;
    public bool OptionsMenuIsOpen = true;
    EnemySpawn enemySpawnScript;

    void Start()
    {
        Time.timeScale = 0;
        OverheatBarImage = OverheatBar.GetComponent<Image>();
        gameoverUIObject.SetActive(false);
        gameUIObject.SetActive(false);
        enemySpawnScript = gameObject.GetComponent<EnemySpawn>();
    }


    void FixedUpdate()
    {
        if(OverheatBarImage.fillAmount < 0.01){
            limitIsReached = false;
        } else if (OverheatBarImage.fillAmount > 0.2){
            limitIsReached = true;
        }

        if(limitIsReached == false){
            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount + refillvalueLimit;

        } else if (limitIsReached == true){
            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount + refillvalue;
        }
    }

    public void PlayGame(){
        Time.timeScale = 1;
        mainMenuObject.SetActive(false);
        gameUIObject.SetActive(true);
        OptionsMenuIsOpen = false;
        enemySpawnScript.enabled = true;
    }

    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void GotoMainMenu(){
        Time.timeScale = 0;
        mainMenuObject.SetActive(true);
        gameUIObject.SetActive(false);
        gameoverUIObject.SetActive(false);
        OptionsMenuIsOpen = false;
    }

    public void OpenGameOverMenu(){
        Time.timeScale = 0;
        mainMenuObject.SetActive(false);
        gameUIObject.SetActive(false);
        gameoverUIObject.SetActive(true);
        OptionsMenuIsOpen = false;
    }
}
