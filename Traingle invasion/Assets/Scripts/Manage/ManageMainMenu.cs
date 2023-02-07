using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManageMainMenu : MonoBehaviour
{

    public GameObject levelSelectObject;
    public GameObject mainMenuObject;
    public GameObject SettingsMenuObject;


    void Start(){
        levelSelectObject.SetActive(false);
        mainMenuObject.SetActive(true);
    }
    
    public void PlayGame(){
        levelSelectObject.SetActive(true);
        SettingsMenuObject.SetActive(false);

    }
    public void ShowOptions(){
        levelSelectObject.SetActive(false);
        SettingsMenuObject.SetActive(true);
    }



    public void SelectLevel1(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SelectLevel2(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void SelectLevel3(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void SelectLevel4(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void SelectLevel5(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 5);
    }
    public void SelectLevel6(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 6);  
    }


    
    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
