using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Tutorial: https://www.youtube.com/watch?v=yQiR2-0sbNw&ab_channel=juul1a

    public Rigidbody2D hook;
    public GameObject prefabRopeSegment;
    public int numLinks = 5;
    public Rigidbody2D lastSegment;

    void Start()
    {
        GenerateRope();
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
        Rigidbody2D previousBody = hook;
        for(int i = 0; i < numLinks; ++i)
        {
            GameObject newSegment = Instantiate(prefabRopeSegment);
            lastSegment = previousBody;
            newSegment.transform.parent = transform;
            newSegment.transform.position = transform.position;
            HingeJoint2D hj = newSegment.GetComponent<HingeJoint2D>();
            hj.connectedBody = previousBody;

            previousBody = newSegment.GetComponent<Rigidbody2D>();
        }
    }
}
