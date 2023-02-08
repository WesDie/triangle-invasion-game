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
    public GameObject gameUIObject;
    public GameObject gameoverUIObject;
    public int score;
    public int highScore;

    public float refillvalueEffect = 0.001f;
    public float refillvalueLimitEffect = 0.00000005f;
    public Image OverheatBarEffectImage;
    public bool limitIsReachedEffect = false;

    void Start()
    {
        OverheatBarImage = OverheatBar.GetComponent<Image>();
        gameoverUIObject.SetActive(false);
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


        if(OverheatBarEffectImage.fillAmount < 0.01){
            limitIsReachedEffect = false;
        } else if (OverheatBarEffectImage.fillAmount > 0.2){
            limitIsReachedEffect = true;
        }

        if(limitIsReachedEffect == false){
            OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount + refillvalueEffect;

        } else if (limitIsReachedEffect == true){
            OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount + refillvalueLimitEffect;
        }
    }

    public void Save(){
        SaveSystem.PlayerSaveData(this);
    }

    public void Load(){
        PlayerSaveData data = SaveSystem.LoadPlayer();

        highScore = data.highScore;
    }
    
    public void GotoMainMenu(){

    }

    public void OpenGameOverMenu(){
        Time.timeScale = 0;
        gameUIObject.SetActive(false);
        gameoverUIObject.SetActive(true);
    }

}
