using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float rotateSpeed = 720.0f;

    private float maxHealth = 100f;
    private float health = 100f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 movementInput = Vector2.zero;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(movementInput.x * playerSpeed, movementInput.y * playerSpeed);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health = " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Processing Inputs
        Vector3 rotate = new Vector3(movementInput.x, 0, movementInput.y);

        // Update color of Player based on remaining health
        if (health <= maxHealth * 0.75)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (health <= maxHealth * 0.50)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (health <= maxHealth * 0.25)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void FixedUpdate()
    {
        // Physics Calculations
        Move();

        // Player faces direction of movement
        if(movementInput != Vector2.zero && movementInput.magnitude > 0.5)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
