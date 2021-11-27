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
    private const float dashCooldown = 3.0f;

    private float dashInvincibleTimer = 0f;
    private const float dashInvincibleCooldown = 0.5f;
    private bool invincible = false;

    // These are used for when the player is being forced around by others
    private bool inExternalForce = false;
    private float externalForceTimer = 0f;
    private const float externalForceTimerCooldown = 0.25f;

    [SerializeField]
    private Color colorLowHealth = Color.white;
    [SerializeField]
    private Color colorMedHealth = Color.white;
    [SerializeField]
    private Color colorHighHealth = Color.white;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void setInExternalForce(bool isForced)
    {
        inExternalForce = isForced;
        externalForceTimer = externalForceTimerCooldown;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
    }

    private void Move()
    {
        if(rb.velocity.magnitude < playerSpeed && !inExternalForce)
        {
            //rb.AddForce(new Vector2(movementInput.x * playerSpeed, movementInput.y * playerSpeed), ForceMode2D.Force);
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
        //Debug.Log(gameObject.name + " health = " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealDamage(float recoverHealth)
    {
        health += recoverHealth;
        //Debug.Log(gameObject.name + " Gained health = " + health);
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
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            gameObject.GetComponent<SpriteRenderer>().color = colorHighHealth;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (health <= maxHealth * 0.50)
        {
            gameObject.GetComponent<SpriteRenderer>().color = colorMedHealth;
        }
        if (health <= maxHealth * 0.25)
        {
            gameObject.GetComponent<SpriteRenderer>().color = colorLowHealth;
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

        if(inExternalForce)
        {
            if (externalForceTimer <= 0)
            {
                inExternalForce = false;
                //Debug.Log("Reset inExternalForce to false");
            }
            else
            {
                externalForceTimer -= Time.deltaTime;
                // Don't need to set inExternalForce, anything that forces us will do that
            }
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
