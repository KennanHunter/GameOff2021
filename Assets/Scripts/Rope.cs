using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Tutorial: https://www.youtube.com/watch?v=yQiR2-0sbNw&ab_channel=juul1a

    public Rigidbody2D hook;
    public GameObject prefabRopeSegment;
    public int numLinks = 5;
    // Last segment is initalized to hook but updated in the GenerateRope()
    public GameObject poisonBall;
    public Rigidbody2D lastSegment;

    void Start()
    {
        // Generates rope segments and sets the lastSegment member variable
        GenerateRope();
        //lastSegment = GetLastSegment();
        //Debug.Log("Last Segment After Generation: " + lastSegment + " from fn: " + GetLastSegment());
        lastSegment = poisonBall.GetComponent<Rigidbody2D>();
    }

    public Rigidbody2D GetLastSegment()
    {
        if(lastSegment)
        {
            return lastSegment;
        }
        else
        {
            Debug.Log("Failed to find last web rope segment" + lastSegment);
            return null;
        }
    }

    private void GenerateRope()
    {
        // The first rigid body we want to attach the rope to
        Rigidbody2D previousBody = hook;

        for(int i = 0; i < numLinks; ++i)
        {
            // Make new rope segment
            GameObject newSegment = Instantiate(prefabRopeSegment);
            // Move new segment to the Rope GameObject's transform
            newSegment.transform.parent = transform;
            // Segment will start at Rope's position and move after being generated
            newSegment.transform.position = transform.position;

            // Connect new segment's hinge joint to the previous segment
            HingeJoint2D hj = newSegment.GetComponent<HingeJoint2D>();
            hj.connectedBody = previousBody;

            // Set previous body to this segment's rigidbody for the next segment to use
            previousBody = newSegment.GetComponent<Rigidbody2D>();
            lastSegment = newSegment.GetComponent<Rigidbody2D>();
        }

        GameObject newPoisonBall = Instantiate(poisonBall);
        newPoisonBall.transform.parent = transform;
        newPoisonBall.transform.position = transform.position;
        HingeJoint2D poisonBallHinge = newPoisonBall.GetComponent<HingeJoint2D>();
        poisonBallHinge.connectedBody = previousBody;
        //Debug.Log("Last Segment: " + lastSegment);
    }
}
