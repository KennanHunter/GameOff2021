using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntBehavior : MonoBehaviour
{
    // Tutorial https://www.youtube.com/watch?v=X-iSQQgOd1A&ab_channel=SebastianLague

    public float maxSpeed = 2.0f;
    public float steerStrength = 2.0f;
    public float wanderStrength = 1.0f;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;

    private Rigidbody2D rb;
    private void Start()
    {
        if(GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            Debug.Log("Rb: " + rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength);

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg;
        //transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));
        if(rb.velocity.magnitude <= maxSpeed)
        {
            rb.velocity = velocity;
        }
        else
        {
            //rb.AddForce(position, ForceMode2D.Force);
        }
    }
}
