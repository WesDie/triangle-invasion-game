using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 20;
    public bool Upgrade1 = false;
    public ParticleSystem smallExplosionFX;
    GameObject mainCamera;
    public ParticleSystem upgrade1ExplosionFX;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.Translate((transform.up * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);

            mainCamera.GetComponent<mainCamera>().TriggerShake();
                
            if(Upgrade1 == false){
                Instantiate(smallExplosionFX, transform.position, Quaternion.identity);
            } else {
                Instantiate(upgrade1ExplosionFX, transform.position, Quaternion.identity);
            }
        }
    }

}
