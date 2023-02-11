using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    private float ropeSegLen = 0.20f;
    private int segmentLength = 50;
    private float lineWidth = 0.15f;

    public Transform startPoint;
    public Transform endPoint;
    public Transform player;

    private Vector3 playerPos;
    public int indexPlayerPos;

    public GameObject followTarget;
    void Start()
    {
        Vector3 ropeStartPoint = new Vector3(0, 0, 0);
        lineRenderer = GetComponent<LineRenderer>();

        for (int i = 0; i < segmentLength; i++)
        {
            ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    void Update()
    {
        DrawRope();

        float xStart = startPoint.position.x;
        float xEnd = endPoint.position.x;
        float currX = followTarget.transform.position.x;

        float ratio = (currX - xStart) / (xEnd - xStart);
        if(ratio > 0){
            indexPlayerPos = (int)(segmentLength * ratio);
        }
        
    }

    private void FixedUpdate()
    {
        Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < segmentLength; i++)
        {
            RopeSegment firstSegment = ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = startPoint.position;
        ropeSegments[0] = firstSegment;

        RopeSegment endSegment = ropeSegments[segmentLength - 1];
        endSegment.posNow = endPoint.position;
        ropeSegments[segmentLength - 1] = endSegment;

        for (int i = 0; i < segmentLength - 1; i++)
        {
            RopeSegment firstSeg = ropeSegments[i];
            RopeSegment secondSeg = ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                ropeSegments[i + 1] = secondSeg;
            }

            if(indexPlayerPos > 0 && indexPlayerPos < segmentLength - 1 && i == indexPlayerPos){
                RopeSegment segment = ropeSegments[i];
                RopeSegment segment2 = ropeSegments[i + 1];
                segment.posNow = new Vector2(followTarget.transform.position.x, followTarget.transform.position.y);
                segment2.posNow = new Vector2(followTarget.transform.position.x, followTarget.transform.position.y);
                ropeSegments[i] = segment;
                ropeSegments[i + 1] = segment2;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[segmentLength];
        for (int i = 0; i < segmentLength; i++)
        {
            ropePositions[i] = ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }
}
