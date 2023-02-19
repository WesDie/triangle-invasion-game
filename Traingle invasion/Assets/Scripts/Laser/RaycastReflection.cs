using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastReflection : MonoBehaviour
{
	public int reflections;
	public float maxLength;

	private LineRenderer lineRenderer;
	private Vector2 direction;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		Ray2D ray = new Ray2D(transform.position, transform.right);

		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, transform.position);
		float remainingLength = maxLength;

		for (int i = 0; i < reflections; i++)
		{
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
			if (hit) 
			{
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
				remainingLength -= Vector2.Distance(ray.origin, hit.point);
				ray = new Ray2D(hit.point + hit.normal * 0.001f, Vector2.Reflect(ray.direction, hit.normal));
				if (hit.collider.tag != "Mirror")
					break;
			}
			else
			{
				lineRenderer.positionCount += 1;
				lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
			}
		}
	}
}