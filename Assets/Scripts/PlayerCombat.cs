using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    public Transform attackPoint;
    [SerializeField]
    public float attackRange = 1f;
    [SerializeField]
    public float attackForce = 5.0f;
    [SerializeField]
    public float attackDamage = 25.0f;
    [SerializeField]
    public LayerMask enemyLayers;
    [SerializeField]
    public float timeBetweenAttacks = 1.0f;
    [SerializeField]
    public Animator animator;

    private float attackTimer = 0f;
    

    public void OnAttack()
    {
        // If attackTimer hasn't timed out yet, ignore attack input
        if(attackTimer > 0)
        {
            return;
        }

        // Play attack animation
        gameObject.GetComponent<ParticleSystem>().Play();
        animator.SetBool("IsAttacking", true);
        //animator.Play("Queen_bite");

        // Detect enemies in range that are in the "Enemy" Layer
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Apply damage to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            // Apply damage to Enemy Controllers
            if(enemy.GetComponent<EnemyController>())
            {
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
            // Apply force to Rigidbodies
            if(enemy.GetComponent<Rigidbody2D>())
            {
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                enemyRb.AddForce(transform.up * attackForce, ForceMode2D.Impulse);
            }
            
        }
        attackTimer = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;  // Decrement attackTimer
        }
        else
        {
            attackTimer = 0;
           animator.SetBool("IsAttacking", false);
        }
        
    }
}
