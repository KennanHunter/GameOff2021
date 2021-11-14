using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AntBehavior : MonoBehaviour
{
    // Tutorial https://www.youtube.com/watch?v=X-iSQQgOd1A&ab_channel=SebastianLague
    // By this point, we are so far off from the tutorial. Still a nice guide though

    [SerializeField]
    public Transform target;  // What we will move towards
    private Rigidbody2D rb;  // Our rigidbody to apply force on

    public Croissant myCroissant = null;
    public bool hasCroissant = false;

    // How we will move
    public float maxSpeed = 10.0f;
    public float moveSpeed = 2.0f;
    public float rotateSpeed = 720f * 2;
    public float steerStrength = 0.5f;
    public float wanderStrength = 0.25f;
    public float stoppingDistance = 0.25f;
    public float targetDistanceBounds = 3.0f;

    // Our preferences that determine where we will move
    public float avoidEnemyStrength = 2f;
    public float desirePlayersStrength = 3f;
    public float desireFoodStrength = 5f;

    // How we see the world
    [SerializeField]
    private float antSight = 5.0f;
    [SerializeField]
    private LayerMask playerLayers;
    [SerializeField]
    private LayerMask enemyLayers;
    [SerializeField]
    private LayerMask foodLayers;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target.parent = null;  // Unparent target from Ant Follower
        
        // I want the ants meander if they can't find any stimulus
        // Ideally Meander() will have the ant move forwards in the frontal 90 degrees randomly
    }

    private void Move()
    {
        // Slow velocity if too fast
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y) * 0.5f;
            return;
        }

        Vector3 forwardsDirection = transform.up;  // Our forwards (green) direction
        Vector2 randomNudge = Random.insideUnitCircle;
        forwardsDirection = forwardsDirection + new Vector3(randomNudge.x * wanderStrength, randomNudge.y * wanderStrength, 0);

        // If less than moveSpeed, instantly set velocity to movespeed forwards
        if(rb.velocity.magnitude < moveSpeed)
        {
            rb.velocity = new Vector2(forwardsDirection.normalized.x, forwardsDirection.normalized.y) * moveSpeed;
        }
        else
        {
            // Add some force on top
            rb.AddForce(forwardsDirection.normalized * moveSpeed * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void Rotate()
    {
        // Vector pointing towards desired Direction
        Vector3 desiredDirection = (target.transform.position - gameObject.transform.position).normalized;
        // Rotation from (0, 0, 0) to desiredDirection
        Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward, desiredDirection);
        // Apply rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
    }

    private void CalculateDesiredPosition()
    {
        // Find vectors to the closest stimuli of each Layer Type
        Vector3 enemyVector = FindVector3TowardsClosestCollider(gameObject.transform.position, antSight, enemyLayers);
        Vector3 playerVector = FindVector3TowardsClosestCollider(gameObject.transform.position, antSight, playerLayers);
        Vector3 foodVector = FindVector3TowardsClosestFood(gameObject.transform.position, antSight, foodLayers);

        // Set our target position away from enemies and towards Food and Players
        if((target.position - transform.position).magnitude < targetDistanceBounds)
        {
            target.position += enemyVector * avoidEnemyStrength * Time.deltaTime * -1;
            target.position += playerVector * desirePlayersStrength * Time.deltaTime;
            if(!hasCroissant)
            {
                target.position += foodVector * desireFoodStrength * Time.deltaTime;
            }
        }
    }

    public Vector3 FindVector3TowardsClosestCollider(Vector3 scanOriginPosition, float sightDistance, LayerMask layerToLookFor)
    {
        Collider2D[] foundColliderList = Physics2D.OverlapCircleAll(scanOriginPosition, sightDistance, layerToLookFor);
        if (foundColliderList.Length > 0)  // If we found any enemies
        {
            Vector2 vectorToClosest = foundColliderList[0].transform.position - scanOriginPosition;
            foreach (Collider2D found in foundColliderList)
            {
                float distanceToFound = (found.transform.position - scanOriginPosition).magnitude;
                if (distanceToFound < vectorToClosest.magnitude)
                {
                    vectorToClosest = found.transform.position - scanOriginPosition;
                    //Debug.Log("Found Object " + found + " is closer. VectorTowards: " + vectorToClosest);
                }
            }

            return vectorToClosest;
        }

        return Vector3.zero;  // Return zero vector if no colliders found
    }

    public Vector3 FindVector3TowardsClosestFood(Vector3 scanOriginPosition, float sightDistance, LayerMask layerToLookFor)
    {
        Collider2D[] foundColliderList = Physics2D.OverlapCircleAll(scanOriginPosition, sightDistance, layerToLookFor);
        if (foundColliderList.Length > 0)  // If we found any food
        {
            Vector2 vectorToClosest = foundColliderList[0].transform.position - scanOriginPosition;
            foreach (Collider2D found in foundColliderList)
            {
                // If there is a croissant
                // TODO: Replace croissant with FoodItem
                if (found.GetComponent<Croissant>() != null)
                {
                    // if Croissant is already being carried
                    if (found.GetComponent<Croissant>().isCarried)
                    {
                        // Ignore this croissant
                        continue;
                    }
                    else
                    {
                        // Distance to croissant
                        float distanceToFound = (found.transform.position - scanOriginPosition).magnitude;
                        if (distanceToFound < vectorToClosest.magnitude)
                        {
                            vectorToClosest = found.transform.position - scanOriginPosition;
                        }
                    }
                } 
            }

            return vectorToClosest;
        }

        return Vector3.zero;  // Return zero vector if no colliders found
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If you bump into croissant
        if(collision.collider.GetComponent<Croissant>())
        {
            if(collision.collider.GetComponent<Croissant>().isCarried)
            {
                return;
            }
            // Set Croissant to be carried by this ant
            myCroissant = collision.collider.GetComponent<Croissant>();
            collision.collider.GetComponent<Transform>().parent = gameObject.transform;
            collision.collider.GetComponent<Transform>().position = gameObject.transform.position;
            myCroissant.isCarried = true;
            hasCroissant = true;
        }
    }

    private void FixedUpdate()
    {
        CalculateDesiredPosition();
        Move();
        Rotate();

        if(myCroissant != null)
        {
            myCroissant.GetComponent<Transform>().position = gameObject.transform.position;
        }
        else
        {
            hasCroissant = false;
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(target.transform.position, 0.5f);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(gameObject.transform.position, antSight);
    //}
}
