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
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
        //     if(transform.GetChild(i).transform.childCount != 1){
        //         transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
        //     }
        //     Debug.Log(i);
        // }

        int maxabillityValue = 0;

        for (int i = 0; i < backpackScript.abillityInfo.Length; i++)
        {
            if(backpackScript.abillityInfo[i].hasFound == true && transform.GetChild(i).childCount != 1){
                transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                if(transform.GetChild(i).childCount == 1){
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                } else if(transform.GetChild(i).childCount == 2){
                    transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                }
                transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = backpackScript.abillityInfo[i].abillityImage;
                transform.GetChild(i).GetChild(0).GetComponent<ItemSlot>().itemName = backpackScript.abillityInfo[i].abillityName;
                transform.GetChild(i).GetChild(0).GetComponent<ItemSlot>().itemDescription = backpackScript.abillityInfo[i].abillityDesc;
                transform.GetChild(i).GetChild(0).GetComponent<ItemSlot>().itemCost = backpackScript.abillityInfo[i].abillityCost;
            } else if(backpackScript.abillityInfo[i].hasFound == false && transform.GetChild(i).childCount != 0){
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(i).GetChild(1).GetComponent<Image>().sprite = backpackScript.abillityInfo[i].abillityImage;
            }
        }

        for (int i = maxabillityValue; i < 16; i++)
        {
            
        }
    }
}
