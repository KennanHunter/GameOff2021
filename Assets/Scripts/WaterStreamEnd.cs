using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStreamEnd : MonoBehaviour
{
    public Transform otherEnd;
    public GameObject waterPusher_prefab = null;
    [SerializeField]
    private float pusherGapDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(otherEnd != null)
        {
            Vector3 distance = otherEnd.transform.position - transform.position;
            Vector3 forwards = transform.up;
            float angleToOtherEnd = Vector3.SignedAngle(distance.normalized, forwards, Vector3.right);
            Debug.Log("Water stream angle: " + angleToOtherEnd);

            for (int i = 0; i < distance.magnitude; i++)
            {
                Vector3 relativeSpawn =
                    new Vector3(distance.x / distance.magnitude * i * pusherGapDistance,
                    distance.y / distance.magnitude * i * pusherGapDistance,
                    0);
                GameObject waterStream = Instantiate(waterPusher_prefab,
                    transform.position + relativeSpawn,
                    Quaternion.Euler(0, 0, angleToOtherEnd));
                waterStream.GetComponent<WaterPusher>().setDirection(distance);
            }
        }
    }
}
