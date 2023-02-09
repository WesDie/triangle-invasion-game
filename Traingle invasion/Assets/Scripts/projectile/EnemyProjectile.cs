using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 20f;
    public float projectileDamage = 1f;
    public ParticleSystem explosionFX;


    ManageGame manageGameScript;

    void Start()
    {
        manageGameScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
    }

    void Update()
    {
        transform.Translate((transform.up * -speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            manageGameScript.effectOverheatBarFillValue =  manageGameScript.effectOverheatBarFillValue - 0.2f;
            
        }
        if (other.tag == "Ground")
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
