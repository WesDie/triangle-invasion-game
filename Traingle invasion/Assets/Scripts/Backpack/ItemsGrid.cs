using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsGrid : MonoBehaviour
{
    Backpack backpackScript;
    public GameObject slotPrefab;
    private Vector3 originalSize;

    void Start(){
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
    }

    void OnEnable()
    {
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
        ReloadBackpackAb();
    }


    public void ReloadBackpackAb(){
        for (int i = 0; i < backpackScript.abillityInfo.Length; i++)
        {
            if(backpackScript.abillityInfo[i].hasFound == true && transform.GetChild(i).childCount != 0){
                transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = backpackScript.abillityInfo[i].abillityImage;
                transform.GetChild(i).GetChild(0).GetComponent<ItemSlot>().itemName = backpackScript.abillityInfo[i].abillityName;
                transform.GetChild(i).GetChild(0).GetComponent<ItemSlot>().itemDescription = backpackScript.abillityInfo[i].abillityDesc;
            }
        }
    }
}
