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
        ReloadBackpackAb();
    }


    void ReloadBackpackAb(){
        for (int i = 0; i < backpackScript.abillityInfo.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab);
            originalSize = slotPrefab.transform.localScale;
            slot.transform.SetParent(transform);
            slot.transform.localScale = originalSize;
            slot.transform.GetChild(0).GetComponent<Image>().sprite = backpackScript.abillityInfo[i].abillityImage;
        }
    }
}
