using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AntBehavior : MonoBehaviour
{
    // Tutorial https://www.youtube.com/watch?v=X-iSQQgOd1A&ab_channel=SebastianLague

    public Transform target;  // What we will move towards
    private Rigidbody2D rb;  // Our rigidbody to apply force on
    Vector3 desiredDirection;  // Direction to target
    Quaternion desiredRotation;  // desired rotation

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
        target = gameObject.transform;
    }

    private void Move()
    {
        desiredDirection = (target.transform.position - gameObject.transform.position).normalized;
        float velocity = maxSpeed;
        rb.AddForce(desiredDirection * velocity * Time.deltaTime);
    }

    private void Rotate()
    {
        desiredRotation = Quaternion.LookRotation(Vector3.forward, desiredDirection);
        float rotateSpeed = 7200f;
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
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
                    //Debug.Log("Found Object " + found + " is closer. VectorTowards: " + vectorToClosest);
                }
            }

            return vectorToClosest;
        }

        return Vector3.zero;  // Return zero vector if no colliders found
    }

    private void FixedUpdate()
    {
        CalculateDesiredPosition();
        Move();
        Rotate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(target.transform.position, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(gameObject.transform.position, antSight);
    }
}
