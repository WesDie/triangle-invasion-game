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
    public GameObject backpackUIObject;
    public int score;
    public int highScore;

    public Image OverheatBarEffectImage;
    public float effectOverheatBarFillValue = 0.5f;
    public bool inCombat = false;
    public mainCamera mainCameraScript;
    Backpack backpackScript;

    void Start()
    {
        OverheatBarImage = OverheatBar.GetComponent<Image>();
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
        gameoverUIObject.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.M) && inCombat == false){
            OpenBackpack();
        }
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

        if(effectOverheatBarFillValue <= 1){
            OverheatBarEffectImage.fillAmount = effectOverheatBarFillValue;
        } else{
            effectOverheatBarFillValue = 1;
        }

        if (inCombat == true){
            GetComponent<WaveSpawner>().enabled = true;
            mainCameraScript.isInCombat = true;

            GameObject.Find("pillarRight").GetComponent<Collider2D>().enabled = true;
            GameObject.Find("pillarLeft").GetComponent<Collider2D>().enabled = true;

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

    public void OpenBackpack(){
        Time.timeScale = 0;
        backpackUIObject.SetActive(true);
        gameUIObject.SetActive(false);
    }

    public void Closebackpack(){
        Time.timeScale = 1;
        backpackUIObject.SetActive(false);
        gameUIObject.SetActive(true);
        backpackScript.UpdateAbbilities();
    }

    public void OpenGameOverMenu(){
        Time.timeScale = 0;
        gameUIObject.SetActive(false);
        gameoverUIObject.SetActive(true);
    }

}
