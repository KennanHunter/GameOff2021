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

    private void Update()
    {
        // Processing Inputs
        Vector3 rotate = new Vector3(movementInput.x, 0, movementInput.y);
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
