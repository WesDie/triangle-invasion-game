using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawns : MonoBehaviour
{
    public bool isSpawnActive = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            isSpawnActive = false;
            
            Invoke("ReactivateSpawn", 2f);
        }
    }

    void ReactivateSpawn(){
        isSpawnActive = true;
        CancelInvoke("ReactivateSpawn");
    }
}
