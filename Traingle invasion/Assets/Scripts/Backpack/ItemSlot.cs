using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler 
{
    public bool isEquiped = false;
    public string itemDescription;
    public int level;
    public string itemName;
    public float itemCost;

    int currentEquipedSlot;

    Vector3 startPos;
    Backpack backpackScript;
    Image thisImage;
    GameObject mainEquipedAbObject;
    GameObject mainUnEquipedAbObject;

    GameObject itemDescriptionText;
    GameObject itemDescriptionTextTitle;

    

    void Start(){
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();

        itemDescriptionText = GameObject.Find("AbDescText");
        itemDescriptionTextTitle = GameObject.Find("AbDescTitleText");

        thisImage = GetComponent<Image>();
        startPos = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(isEquiped == false){
            thisImage.raycastTarget = false;
        }
    }


    public void OnDrag(PointerEventData eventData){
        if(isEquiped == false){
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        if(isEquiped == false){
            thisImage.raycastTarget = true;
            gameObject.GetComponent<RectTransform>().localPosition = startPos;
        }
    }

    public void ChangeToEquiped(int slotEquipIndex){
        for (int i = 3; i > -1; i--)
        {
            if(i == slotEquipIndex){
                currentEquipedSlot = slotEquipIndex;
                mainEquipedAbObject = GameObject.FindGameObjectWithTag("EquipedAbilitiesSlots").transform.GetChild(i).gameObject;
                        
                if(mainEquipedAbObject.transform.childCount == 0){
                    transform.SetParent(mainEquipedAbObject.transform);
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    public void ChangeToUnEquiped(){
        for (int i = 17; i > -1; i--)
        {
            mainUnEquipedAbObject = GameObject.FindGameObjectWithTag("UnEquipedAbilitiesSlots").transform.GetChild(0).transform.GetChild(i).gameObject;
                    
            if(mainUnEquipedAbObject.transform.childCount == 0){
                transform.SetParent(mainUnEquipedAbObject.transform);
                transform.GetChild(0).gameObject.SetActive(false);
            }
            backpackScript.AbillitiesEquipedInfo[currentEquipedSlot].AbillitiesEquipedName = null;
            backpackScript.AbillitiesEquipedInfo[currentEquipedSlot].AbillitiesEquipedLevel = 0;
            backpackScript.AbillitiesEquipedInfo[currentEquipedSlot].AbillitiesEquipedDesc = null;
            backpackScript.AbillitiesEquipedInfo[currentEquipedSlot].isEquiped = false;
            isEquiped = false;
            thisImage.raycastTarget = true;
            gameObject.GetComponent<RectTransform>().localPosition = startPos;
        }
    }

    public void ShowInfoOfItem(){
        itemDescriptionTextTitle.GetComponent<TMP_Text>().text = itemName;
        itemDescriptionText.GetComponent<TMP_Text>().text = itemDescription;
    }

}
