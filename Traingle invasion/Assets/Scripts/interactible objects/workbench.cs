using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workbench : MonoBehaviour
{
    private Animator anim;
    public bool canEnterWorkbench;
    mainCamera mainCameraScript;
    Movement playerScript;
    Backpack backpackScript;

    void Start(){
        anim = GetComponent<Animator>();
        mainCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<mainCamera>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
    }

    void Update()
    {
        if(canEnterWorkbench == true && Input.GetKeyDown (KeyCode.W)){
            playerScript.runSpeed = 0f;
            StartCoroutine(DoorAnimationEnter());

            mainCameraScript.isInWorkbench = true;
        } 

        if(mainCameraScript.isInWorkbench == true && Input.GetKeyDown (KeyCode.Q) && mainCameraScript.isFullInWorkbench == true){
            backpackScript.UpdateAbbilities();
            mainCameraScript.isInWorkbench = false;
            StartCoroutine(DoorAnimationLeave());
        }
    }

    public void CloseWorkbench(){
        if(mainCameraScript.isInWorkbench == true && mainCameraScript.isFullInWorkbench == true){
            backpackScript.UpdateAbbilities();
            mainCameraScript.isInWorkbench = false;
            StartCoroutine(DoorAnimationLeave());
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

    IEnumerator DoorAnimationEnter()
    {
        anim.Play("dooropen");
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder = -4; //Player
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(7).GetComponent<SpriteRenderer>().sortingOrder = -3; //playerGun
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject.SetActive(false); //PlayerLight
        anim.Play("doorclose");
        GameObject.Find("UI/FX").transform.GetChild(4).gameObject.SetActive(true); //CanvasWorkbench
    }

    IEnumerator DoorAnimationLeave()
    {
        GameObject.Find("UI/FX").transform.GetChild(4).gameObject.SetActive(false); //CanvasWorkbench
        yield return new WaitForSeconds(2);
        anim.Play("dooropen");
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder = 0; //Player
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(7).GetComponent<SpriteRenderer>().sortingOrder = 1; //playerGun
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).gameObject.SetActive(true); //PlayerLight
        anim.Play("doorclose");
        playerScript.runSpeed = 10f;
    }
}
