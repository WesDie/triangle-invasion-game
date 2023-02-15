using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    private Animator anim;

    bool portalisActive = false;
    bool canGoTroughPortal;

    WaveSpawner waveSpawnerScript;

    void Start(){
        anim = GetComponent<Animator>();
        waveSpawnerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WaveSpawner>();
    }

    void Update()
    {
        if(waveSpawnerScript.levelComplete == true && !portalisActive){
            anim.Play("PortalFadeIn");
            portalisActive = true;
        }
        if(portalisActive && canGoTroughPortal == true && Input.GetKeyDown (KeyCode.W)){
            Debug.Log("Load Next Level");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            canGoTroughPortal = true;
        }
     }
     
     void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            canGoTroughPortal = false;
        }
     }
}
