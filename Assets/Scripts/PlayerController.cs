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
    [SerializeField]
    private float dashForce = 10.0f;

    public float maxHealth = 100f;
    public float health = 100f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 movementInput = Vector2.zero;
    public Animator animator;

    private float dashTimer = 0f;
    private float dashCooldown = 3.0f;

    private float dashInvincibleTimer = 0f;
    private float dashInvincibleCooldown = 0.5f;
    private bool invincible = false;

    //private Color colorLowHealth = new Color(255, 51, 51, 1);
    //private Color colorMedHealth = new Color(255, 102, 102, 1);
    //private Color colorHighHealth = new Color(255, 204, 204, 1);

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
        if(movementInput.magnitude < 1)
        {
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y) * 0.5f;
        }

        if(rb.velocity.magnitude < playerSpeed)
        {
            rb.velocity = new Vector2(movementInput.x * playerSpeed, movementInput.y * playerSpeed);
        }
        else
        {
            rb.AddForce(new Vector2(movementInput.x * playerSpeed, movementInput.y * playerSpeed), ForceMode2D.Force);
        }
    }

    public void OnDash()
    {
        if(dashTimer > 0)
        {
            return;
        }
        dashTimer = dashCooldown;
        dashInvincibleTimer = dashInvincibleCooldown;

        rb.AddForce(transform.up * dashForce, ForceMode2D.Impulse);
    }

    public void TakeDamage(float damage)
    {
        if(invincible)
        {
            return;
        }

        health -= damage;
        Debug.Log(gameObject.name + " health = " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealDamage(float recoverHealth)
    {
        health += recoverHealth;
        Debug.Log(gameObject.name + " Gained health = " + health);
        if (health > maxHealth)
        {
            health = maxHealth;
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
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (health <= maxHealth * 0.50)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        if (health <= maxHealth * 0.25)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));
        //Debug.Log(rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (dashTimer <= 0)
        {
            dashTimer = 0;
        }
        else
        {
            dashTimer -= Time.deltaTime;
        }

        if (dashInvincibleTimer <= 0)
        {
            dashInvincibleTimer = 0;
            invincible = false;
        }
        else
        {
            dashInvincibleTimer -= Time.deltaTime;
            invincible = true;
        }

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
