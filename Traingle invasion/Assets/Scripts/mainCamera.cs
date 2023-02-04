using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    
    private float shakeDuration = 0f;
    
    private float shakeMagnitude = 0.1f;
    
    private float dampingSpeed = 0.1f;
    
    Vector3 initialPosition;

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
        
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake() {
        shakeDuration = 0.02f;
    }
}
