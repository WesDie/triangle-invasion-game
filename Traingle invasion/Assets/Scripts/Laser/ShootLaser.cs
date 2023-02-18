using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    void Update(){
        if(beam != null){
            Destroy(GameObject.Find("laser Beam"));
        }
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
