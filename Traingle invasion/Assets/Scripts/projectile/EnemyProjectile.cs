using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 20f;
    public float projectileDamage = 1f;
    public ParticleSystem explosionFX;
    public enum damageType
    {
        effectDamage, 
        overheatDamage, 
        healthDamage
    }

    public damageType selectedDamageType;


    ManageGame manageGameScript;
    Movement playerScript;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
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
            if(selectedDamageType == damageType.effectDamage){
                manageGameScript.effectOverheatBarFillValue =  manageGameScript.effectOverheatBarFillValue - projectileDamage;
            } else if(selectedDamageType == damageType.overheatDamage){
                manageGameScript.OverheatBarImage.fillAmount = manageGameScript.OverheatBarImage.fillAmount - projectileDamage;
            } else if(selectedDamageType == damageType.healthDamage){
                playerScript.health = playerScript.health - projectileDamage;
            }

            Destroy(gameObject);
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            
        }
        if (other.tag == "Ground")
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
