using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float homingSpeed = 5;
    public float speed = 20;
    public float projectileHealth = 10;
    public bool Upgrade1 = false;
    public ParticleSystem smallExplosionFX;
    GameObject mainCamera;
    public ParticleSystem upgrade1ExplosionFX;
    public ParticleSystem homingBulletExplosionFX;
    public bool isHoming = false;
    private GameObject[] targets;
    private int targetNum;
    private bool hasTarget = false;
    public float projectileDamage = 10;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Destroy(gameObject, 5);

        if (isHoming == true)
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].GetComponent<Enemy>().isTargeted == false && hasTarget == false)
                {
                    targetNum = i;
                    targets[i].GetComponent<Enemy>().isTargeted = true;
                    hasTarget = true;
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (isHoming == true && hasTarget == true && targets.Length > 0)
        {
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -6), homingSpeed * Time.deltaTime);
            if (targets[targetNum] != null)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    targets[targetNum].transform.position,
                    homingSpeed * Time.deltaTime
                );
            }
            else if (targets[targetNum] == null)
            {
                HomingExplode();
                Destroy(gameObject);
            }
        }
        else if (isHoming == false)
        {
            transform.Translate((transform.up * speed * Time.deltaTime));
        }
        else
        {
            transform.Translate((transform.up * homingSpeed * Time.deltaTime));
            Invoke("HomingExplode", 0.5f);
        }
    }

    void HomingExplode()
    {
        Instantiate(homingBulletExplosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {

            mainCamera.GetComponent<mainCamera>().TriggerShake();

            projectileHealth = projectileHealth - 10;

            if (Upgrade1 == false)
            {
                Instantiate(smallExplosionFX, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(upgrade1ExplosionFX, transform.position, Quaternion.identity);
            }

            if(projectileHealth == 0){
                Destroy(gameObject);
            }
        }
    }
}
