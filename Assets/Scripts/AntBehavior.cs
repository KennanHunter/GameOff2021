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
        Collider2D[] otherEnemyUnits = Physics2D.OverlapCircleAll(gameObject.transform.position, antSight, enemyLayers);
        if(otherEnemyUnits.Length > 0)  // If we found any enemies
        {
            Vector2 vectorToClosestEnemy = otherEnemyUnits[0].transform.position - gameObject.transform.position;
            foreach(Collider2D enemy in otherEnemyUnits)
            {
                float distanceToEnemy = (enemy.transform.position - gameObject.transform.position).magnitude;
                if(distanceToEnemy < vectorToClosestEnemy.magnitude)
                {
                    vectorToClosestEnemy = enemy.transform.position - gameObject.transform.position;
                    Debug.Log("enemy " + enemy + " is closer");
                }
            }
            target.position += (Vector3)vectorToClosestEnemy * avoidEnemyStrength * Time.deltaTime * -1;
            Debug.Log(gameObject + " is avoiding in this direction: " +
                (Vector3)vectorToClosestEnemy * avoidEnemyStrength * Time.deltaTime * -1);
        }

        Collider2D[] otherPlayerUnits = Physics2D.OverlapCircleAll(gameObject.transform.position, antSight, playerLayers);
        if (otherPlayerUnits.Length > 0)  // If we found any players
        {
            Vector2 vectorToClosestPlayer = otherPlayerUnits[0].transform.position - gameObject.transform.position;
            foreach (Collider2D player in otherPlayerUnits)
            {
                float distanceToEnemy = (enemy.transform.position - gameObject.transform.position).magnitude;
                if (distanceToEnemy < vectorToClosestEnemy.magnitude)
                {
                    vectorToClosestEnemy = enemy.transform.position - gameObject.transform.position;
                    Debug.Log("enemy " + enemy + " is closer");
                }
            }
            target.position += (Vector3)vectorToClosestEnemy * avoidEnemyStrength * Time.deltaTime * -1;
            Debug.Log(gameObject + " is avoiding in this direction: " +
                (Vector3)vectorToClosestEnemy * avoidEnemyStrength * Time.deltaTime * -1);
        }
    }

    public Vector3 FindVector3TowardsCollider()
    {
        Collider2D[] otherEnemyUnits = Physics2D.OverlapCircleAll(gameObject.transform.position, antSight, enemyLayers);
        if (otherEnemyUnits.Length > 0)  // If we found any enemies
        {
            Vector2 vectorToClosestEnemy = otherEnemyUnits[0].transform.position - gameObject.transform.position;
            foreach (Collider2D enemy in otherEnemyUnits)
            {
                float distanceToEnemy = (enemy.transform.position - gameObject.transform.position).magnitude;
                if (distanceToEnemy < vectorToClosestEnemy.magnitude)
                {
                    vectorToClosestEnemy = enemy.transform.position - gameObject.transform.position;
                    Debug.Log("enemy " + enemy + " is closer");
                }
            }
            target.position += (Vector3)vectorToClosestEnemy * avoidEnemyStrength * Time.deltaTime * -1;
            Debug.Log(gameObject + " is avoiding in this direction: " +
                (Vector3)vectorToClosestEnemy * avoidEnemyStrength * Time.deltaTime * -1);
        }
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
