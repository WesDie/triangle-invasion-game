using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class mainCamera : MonoBehaviour
{
    
    private float shakeDuration = 0f;
    
    private float shakeMagnitude = 0.1f;
    
    private float dampingSpeed = 0.1f;
    
    Vector3 initialPosition;
    public Vector3 workbenchPos;
    public bool isInCombat = false;
    public bool isInWorkbench = false;
    public float followSpeed;
    public Transform target;
    int distanceFromScreen = 32;
    float distanceFromScreen1 = 32;
    float distancePerSecond = 100;
    float distancePerSecondExit = 200;
    public bool isFullInWorkbench = false;

    void OnEnable()
    {
        initialPosition = new Vector3(0, -5f, -10f);;
    }

    void Update()
    {
        if(isInCombat == true){
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition =  Vector3.Slerp(transform.position, initialPosition, 2f * Time.deltaTime);;
            }
        } else if(isInWorkbench == true && isFullInWorkbench == false){
            if (shakeDuration > 0)
            {
                transform.localPosition = workbenchPos + Random.insideUnitSphere * shakeMagnitude;
            
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition =  Vector3.Slerp(transform.position, workbenchPos, 2f * Time.deltaTime);
                if(distanceFromScreen < 380 && isFullInWorkbench == false){
                    distanceFromScreen1 += Time.deltaTime * distancePerSecond;
                    distanceFromScreen = Mathf.FloorToInt(distanceFromScreen1);
                    GetComponent<UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera>().assetsPPU = distanceFromScreen;
                } else {
                    GetComponent<UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera>().assetsPPU = 380;
                    isFullInWorkbench = true;
                }
            }
        } else if(isInWorkbench == false && isFullInWorkbench == true){
            if (shakeDuration > 0)
            {
                Vector3 newPos = new Vector3(target.position.x, -5f, -10f);
                transform.localPosition = newPos + Random.insideUnitSphere * shakeMagnitude;
            
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                Vector3 newPos = new Vector3(target.position.x, -5f, -10f);
                shakeDuration = 0f;
                transform.localPosition =  Vector3.Slerp(transform.position, newPos, 0.5f * Time.deltaTime);
                if(distanceFromScreen > 32){
                    distanceFromScreen1 -= Time.deltaTime * distancePerSecondExit;
                    distanceFromScreen = Mathf.FloorToInt(distanceFromScreen1);
                    GetComponent<UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera>().assetsPPU = distanceFromScreen;
                } else {
                    GetComponent<UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera>().assetsPPU = 32;
                    isFullInWorkbench = false;
                    isInWorkbench = false;
                }
            }  
        }else if(isInCombat == false && isInWorkbench == false && isFullInWorkbench == false){
            Vector3 newPos = new Vector3(target.position.x, -5f, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }

    public void TriggerShake() {
        shakeDuration = 0.02f;
    }
}
