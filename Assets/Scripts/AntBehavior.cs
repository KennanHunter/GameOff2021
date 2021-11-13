using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AntBehavior : MonoBehaviour
{
    // Tutorial https://www.youtube.com/watch?v=X-iSQQgOd1A&ab_channel=SebastianLague

    public Transform target;  // What we will move towards
    private Rigidbody2D rb;  // Our rigidbody to apply force on

    // How we will move
    public float maxSpeed = 1.0f;
    public float steerStrength = 0.5f;
    public float wanderStrength = 0.25f;

    // Our preferences that determine where we will move
    public float avoidEnemyStrength = 2f;
    public float desirePlayersStrength = 3f;
    public float desireFoodStrength = 5f;

    // How we see the world
    [SerializeField]
    private float antSight = 3.0f;
    [SerializeField]
    private LayerMask playerLayers;
    [SerializeField]
    private LayerMask enemyLayers;
    [SerializeField]
    private LayerMask foodLayers;

    // Some variables that we might not need
    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;
    float rotationAngle;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = gameObject.transform;
    }

    private void Move()
    {
        rb.AddForce(desiredDirection * velocity * Time.deltaTime);
        rb.rotation = rotationAngle;
    }

    private void CalculateDesiredPosition()
    {
        Vector3 enemyVector = FindVector3TowardsClosestCollider(gameObject.transform.position, antSight, enemyLayers);
        Vector3 playerVector = FindVector3TowardsClosestCollider(gameObject.transform.position, antSight, playerLayers);
        Vector3 foodVector = FindVector3TowardsClosestCollider(gameObject.transform.position, antSight, foodLayers);

        target.position += enemyVector * avoidEnemyStrength * Time.deltaTime * -1;
        target.position += playerVector * desirePlayersStrength * Time.deltaTime;
        target.position += foodVector * desireFoodStrength * Time.deltaTime;
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
                    Debug.Log("Found Object " + found + " is closer. VectorTowards: " + vectorToClosest);
                }
            }

            return vectorToClosest;
        }

        return Vector3.zero;  // Return zero vector if no colliders found
    }

    // Update is called once per frame
    void Update()
    {
        desiredDirection = ((Vector2)target.position - position).normalized;
        //desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength);

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        rotationAngle = Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg;
        //transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));

        //rb.velocity = velocity;
    }

    private void FixedUpdate()
    {
        CalculateDesiredPosition();
        Move();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(target.transform.position, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gameObject.transform.position, antSight);
    }
}
