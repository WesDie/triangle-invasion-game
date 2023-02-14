using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{

    Backpack backpackScript;

    ItemsGrid itemGridScript;
    // Start is called before the first frame update
    void Start()
    {
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            backpackScript.abillityInfo[0].hasFound = true;
            Destroy(gameObject);
        }
    }

}
