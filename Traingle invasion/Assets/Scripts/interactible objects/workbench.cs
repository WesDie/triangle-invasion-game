using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workbench : MonoBehaviour
{
    private Animator anim;
    public bool canEnterWorkbench;
    mainCamera mainCameraScript;

    void Start(){
        anim = GetComponent<Animator>();
        mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mainCamera>();
    }

    void Update()
    {
        // if(waveSpawnerScript.levelComplete == true && !portalisActive){
        //     //anim.Play("PortalFadeIn");
        //     portalisActive = true;
        // }
        if(canEnterWorkbench == true && Input.GetKeyDown (KeyCode.W)){
            mainCameraScript.isInWorkbench = true;
            Debug.Log("Enter workbench");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            canEnterWorkbench = true;
        }
     }
     
     void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            canEnterWorkbench = false;
        }
     }
}
