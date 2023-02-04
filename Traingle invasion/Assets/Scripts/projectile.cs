using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 20;

    void Start()
    {
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
        }
    }

}
