using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayer : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;
    public float maxSpeed=10;
    Vector2 mouseForce;
    Vector3 lastPosition;
    float distanceToObject;
    ManageGame manageGameScript;

	public int reflections;
	public float maxLength;
    private LineRenderer lineRenderer;
	private Vector2 direction;


    void Start()
    {
        manageGameScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(manageGameScript.inCombat == false){

            Ray2D ray = new Ray2D(transform.position, transform.up);

            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, transform.position);
            float remainingLength = maxLength;


        if (Input.GetMouseButton(0)){
            for (int i = 0; i < reflections; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
                if (hit) 
                {
                    if (hit.collider.tag == "Mirror"){
                        lineRenderer.positionCount += 1;
                        lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                        remainingLength -= Vector2.Distance(ray.origin, hit.point);
                        ray = new Ray2D(hit.point + hit.normal * 0.001f, Vector2.Reflect(ray.direction, hit.normal));
                        //if (hit.collider.tag == "Mirror")
                        //    break;

                    if(selectedObject){
                        distanceToObject = Vector3.Distance(transform.position, selectedObject.transform.position);
                    }
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (selectedObject)
                    {
                        mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
                        mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
                        lastPosition = mousePosition;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
                        if (targetObject)
                        {
                            selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                            distanceToObject = Vector3.Distance(transform.position, targetObject.transform.position);
                            offset = selectedObject.transform.position - mousePosition;
                        }
                    }
                    } else if(selectedObject){
                    selectedObject.velocity = Vector2.zero;
                    selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
                    selectedObject = null;
                    }
                }
                else
                {
                    // lineRenderer.positionCount += 1;
                    // lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
                }
            }
        }

            if (Input.GetMouseButtonUp(0) && selectedObject)
            {
                selectedObject.velocity = Vector2.zero;
                selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
                selectedObject = null;
            }
            if(distanceToObject < 2){
                selectedObject = null;
            }
        }
    }

    void FixedUpdate(){
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}
