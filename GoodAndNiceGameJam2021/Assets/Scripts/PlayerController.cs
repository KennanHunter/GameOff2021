using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    private PlayerInput playerInput;
    //private InputAction moveAction;
    private Rigidbody2D rb;
    private Vector2 movementInput = Vector2.zero;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        //moveAction = playerInput.actions["Movement"];
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
    }

    private void FixedUpdate()
    {
        // Physics Calculations
        Move();

        // Player faces direction of movement - not functional yet
        //if(movementInput != Vector2.zero && movementInput.magnitude > 0.5)
        //{
        //    gameObject.transform.forward = new Vector3(movementInput.x, movementInput.y, 90);
        //}
    }
}
