using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string slotName;
    bool slotisFull;
    public bool isUnequipedSlot = true;
    Backpack backpackScript;
    public int slotEquipIndex;

    void Start(){
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
    }

    void Update(){
        if(transform.childCount == 0){
            slotisFull = false;
        } else{
            slotisFull = true;
        }
    }

    public void OnDrop(PointerEventData eventData){
        if(eventData.pointerDrag.transform.name == slotName && !slotisFull){
            ItemSlot draggable = eventData.pointerDrag.GetComponent<ItemSlot>();
            if(draggable != null){
                draggable.isEquiped = true;
                draggable.ChangeToEquiped(slotEquipIndex);
                backpackScript.AbillitiesEquipedInfo[slotEquipIndex].AbillitiesEquipedName = transform.GetChild(0).GetComponent<ItemSlot>().itemName;
                backpackScript.AbillitiesEquipedInfo[slotEquipIndex].AbillitiesEquipedLevel = transform.GetChild(0).GetComponent<ItemSlot>().level;
                backpackScript.AbillitiesEquipedInfo[slotEquipIndex].AbillitiesEquipedDesc = transform.GetChild(0).GetComponent<ItemSlot>().itemDescription;
                backpackScript.AbillitiesEquipedInfo[slotEquipIndex].isEquiped = true;
            }
        }
    }
}
