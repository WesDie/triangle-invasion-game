using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public float refillvalue = 0.01f;
    public float refillvalueLimit = 0.0000005f;
    GameObject OverheatBar;
    Image OverheatBarImage;
    public bool limitIsReached = false;
    GameObject mainMenuObject;
    GameObject gameUIObject;
    GameObject gameoverUIObject;
    public bool OptionsMenuIsOpen = true;

    void Start()
    {
        Time.timeScale = 0;
        OverheatBar = GameObject.Find("OverheatBar");
        OverheatBarImage = OverheatBar.GetComponent<Image>();
        mainMenuObject = GameObject.Find("MainMenu");
        gameUIObject = GameObject.Find("GameUI");
        gameoverUIObject = GameObject.Find("GameOverUI");
        //gameoverUIObject.SetActive(false);
        //gameUIObject.SetActive(false);
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
}
