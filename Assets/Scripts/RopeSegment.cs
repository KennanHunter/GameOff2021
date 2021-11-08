using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove, connectedBelow;

    void Start()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();

        // If there is a rope segment above us
        if(aboveSegment != null)
        {
            aboveSegment.connectedBelow = gameObject; // Tell the rope segment above us that we are below it
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y; // Vertical length of sprite
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1); 
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);  // This rope segment is the top
        }
    }
}
