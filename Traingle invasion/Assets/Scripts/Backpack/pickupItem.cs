using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{

    Backpack backpackScript;

    ItemsGrid itemGridScript;
    public int abillityIndex = 0;
    public Sprite abillityImage;

    [SerializeField] float speed = 7.5f;
    [SerializeField] float height = 0.15f;
    [SerializeField] float pickupDistance = 2.5f;

    Transform playerTransform;

    Vector2 pos;

    void Start()
    {
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pos = transform.position;
        GetComponent<SpriteRenderer>().sprite = abillityImage;
    }

    void Update()
    {

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if(distance > pickupDistance){
            float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
            transform.position = new Vector2(transform.position.x, newY);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        if(distance < 0.1f){
            backpackScript.abillityInfo[abillityIndex].hasFound = true;
            Destroy(gameObject);
        }

    }

}
