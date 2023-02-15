using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotFoundItem : MonoBehaviour
{
    GameObject itemDescriptionText;
    GameObject itemDescriptionTextTitle;

    void Start(){
        itemDescriptionText = GameObject.Find("AbDescText");
        itemDescriptionTextTitle = GameObject.Find("AbDescTitleText");
    }

    public void ShowInfoOfNotFoundItem(){
        itemDescriptionTextTitle.GetComponent<TMP_Text>().text = "????";
        itemDescriptionText.GetComponent<TMP_Text>().text = "Item Not Found!";
    }

}
