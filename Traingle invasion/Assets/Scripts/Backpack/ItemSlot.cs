using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler 
{
    Backpack backpackScript;

    public Image thisImage;
    public bool isEquiped = false;

    void Start(){
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();

        thisImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(isEquiped == false){
            thisImage.raycastTarget = false;
            transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = false;
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
            transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = true;
        }
    }

    public void ReloadGrid(){
        transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = false;
        transform.parent.gameObject.GetComponent<GridLayoutGroup>().enabled = true;
    }

}
