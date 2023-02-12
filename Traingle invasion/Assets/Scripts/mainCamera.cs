using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    
    private float shakeDuration = 0f;
    
    private float shakeMagnitude = 0.1f;
    
    private float dampingSpeed = 0.1f;
    
    Vector3 initialPosition;
    public bool isInCombat = false;

    public float followSpeed;
    public Transform target;

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
        } else{
            Vector3 newPos = new Vector3(target.position.x, -5f, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }

    public void TriggerShake() {
        shakeDuration = 0.02f;
    }
}
