using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    private Transform target;
    private float targetDistance;
    private Vector3 targetDirection;

    [SerializeField]
    public float timeBetweenAttacks = 5.0f;
    private float attackTimer = 0f;

    [SerializeField]
    private GameObject ropeObject;

    [SerializeField]
    private float propelForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Find player's position (Transform)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void ThrowWeb()
    {
        if (attackTimer > 0)
        {
            return;
        }

        // Spawn web string
        GameObject webRope = Instantiate(ropeObject);
        Rope webRopeScript = webRope.GetComponent<Rope>();
        Rigidbody2D lastSegmentRb = webRopeScript.lastSegment.GetComponent<Rigidbody2D>();

        // Attach one end to spider
        webRope.transform.position = gameObject.transform.position;

        // Add force to the last segment
        if(lastSegmentRb)
        {
            lastSegmentRb.AddForce(targetDirection * propelForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Cant add force to null: " + lastSegmentRb);
        }

        // Reset Attack Timer
        attackTimer = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        targetDistance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        targetDirection = (gameObject.transform.position - target.transform.position).normalized;

        if (targetDistance < 5 )
        {
            ThrowWeb();
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;  // Decrement attackTimer
        }
        else
        {
            attackTimer = 0;
        }
    }
}
