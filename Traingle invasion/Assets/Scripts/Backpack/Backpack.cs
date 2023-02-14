using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    public GameObject backpackMainObject;

    GameObject mapMenu;
    GameObject abilltiesMenu;
    GameObject inventoryMenu;
    GameObject optionsMenu;
    GameObject quitMenu;

    Image mapButton;
    Image abilltiesButton;
    Image inventoryButton;
    Image optionsButton;
    Image quitButton;


    [System.Serializable]
    public class Abillities{
        public string abillityName;
        public Sprite abillityImage;
        public int abillityLevel;
        public string abillityDesc;
        public bool hasFound = false;
    }
    public Abillities[] abillityInfo;

    [System.Serializable]
    public class AbillitiesEquiped{
        public string AbillitiesEquipedName;
        public int AbillitiesEquipedLevel;
        public string AbillitiesEquipedDesc;
        public Sprite AbillitiesEquipedImage;
        public bool isEquiped = false;
    }
    public AbillitiesEquiped[] AbillitiesEquipedInfo = new AbillitiesEquiped[4];

    void Start(){
        mapMenu = backpackMainObject.transform.GetChild(6).gameObject;
        abilltiesMenu = backpackMainObject.transform.GetChild(7).gameObject;
        inventoryMenu = backpackMainObject.transform.GetChild(8).gameObject;
        optionsMenu = backpackMainObject.transform.GetChild(9).gameObject;
        quitMenu = backpackMainObject.transform.GetChild(10).gameObject;

        mapButton = backpackMainObject.transform.GetChild(0).GetComponent<Image>();
        abilltiesButton = backpackMainObject.transform.GetChild(1).GetComponent<Image>();
        inventoryButton = backpackMainObject.transform.GetChild(2).GetComponent<Image>();
        optionsButton = backpackMainObject.transform.GetChild(3).GetComponent<Image>();
        quitButton = backpackMainObject.transform.GetChild(4).GetComponent<Image>();
    }

    public void UpdateAbbilities(){
        for (int i = 0; i < 4; i++)
        {
            Image effectImageGame = GameObject.Find("EffectImage" + (i + 1)).GetComponent<Image>();
            if(AbillitiesEquipedInfo[i].isEquiped == true){
                effectImageGame.enabled = true;
                effectImageGame.sprite = AbillitiesEquipedInfo[i].AbillitiesEquipedImage;
            } else {
                effectImageGame.enabled = false;
            }
        }
    }

    public void OpenMapmenu(){
        mapMenu.SetActive(true);
        abilltiesMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);

        mapButton.color = new Color(255f, 255f, 255f, 1f);
        abilltiesButton.color = new Color(255f, 255f, 255f, 0.5f);
        inventoryButton.color = new Color(255f, 255f, 255f, 0.5f);
        optionsButton.color = new Color(255f, 255f, 255f, 0.5f);
        quitButton.color = new Color(255f, 255f, 255f, 0.5f);
    }

    public void OpenAbillitiesmenu(){
        mapMenu.SetActive(false);
        abilltiesMenu.SetActive(true);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);

        mapButton.color = new Color(255f, 255f, 255f, 0.5f);
        abilltiesButton.color = new Color(255f, 255f, 255f, 1f);
        inventoryButton.color = new Color(255f, 255f, 255f, 0.5f);
        optionsButton.color = new Color(255f, 255f, 255f, 0.5f);
        quitButton.color = new Color(255f, 255f, 255f, 0.5f);

        for (int i = 0; i < abillityInfo.Length; i++){
            
        }
    }

    public void OpenInventorymenu(){
        mapMenu.SetActive(false);
        abilltiesMenu.SetActive(false);
        inventoryMenu.SetActive(true);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(false);

        mapButton.color = new Color(255f, 255f, 255f, 0.5f);
        abilltiesButton.color = new Color(255f, 255f, 255f, 0.5f);
        inventoryButton.color = new Color(255f, 255f, 255f, 1f);
        optionsButton.color = new Color(255f, 255f, 255f, 0.5f);
        quitButton.color = new Color(255f, 255f, 255f, 0.5f);
    }

    public void OpenSettingsmenu(){
        mapMenu.SetActive(false);
        abilltiesMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(true);
        quitMenu.SetActive(false);

        mapButton.color = new Color(255f, 255f, 255f, 0.5f);
        abilltiesButton.color = new Color(255f, 255f, 255f, 0.5f);
        inventoryButton.color = new Color(255f, 255f, 255f, 0.5f);
        optionsButton.color = new Color(255f, 255f, 255f, 1f);
        quitButton.color = new Color(255f, 255f, 255f, 0.5f);
    }

    public void OpenQuitmenu(){
        mapMenu.SetActive(false);
        abilltiesMenu.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        quitMenu.SetActive(true);

        mapButton.color = new Color(255f, 255f, 255f, 0.5f);
        abilltiesButton.color = new Color(255f, 255f, 255f, 0.5f);
        inventoryButton.color = new Color(255f, 255f, 255f, 0.5f);
        optionsButton.color = new Color(255f, 255f, 255f, 0.5f);
        quitButton.color = new Color(255f, 255f, 255f, 1f);
    }
}
