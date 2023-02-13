using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string slotName;
    public GameObject mainEquipedAbObject;

    public void OnDrop(PointerEventData eventData){
            ItemSlot draggable = eventData.pointerDrag.GetComponent<ItemSlot>();
            if(draggable != null){
                for (int i = 0; i < 4; i++)
                {
                    mainEquipedAbObject = GameObject.FindGameObjectWithTag("EquipedAbilitiesSlots").transform.GetChild(i).gameObject;
                    
                    if(mainEquipedAbObject.transform.childCount == 0){
                        draggable.isEquiped = true;
                        draggable.ReloadGrid();
                        transform.SetParent(mainEquipedAbObject.transform);
                        break;
                    }
                }
            }
    }
}
