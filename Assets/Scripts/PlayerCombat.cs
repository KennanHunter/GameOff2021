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

        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }



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
            // Apply damage to Web Segments
            if (enemy.GetComponent<RopeSegment>())
            {
                enemy.GetComponent<RopeSegment>().TakeDamage(attackDamage);
            }
            // Apply force to Rigidbodies
            if (enemy.GetComponent<Rigidbody2D>())
            {
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                Vector2 directionToForce = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(directionToForce * attackForce, ForceMode2D.Impulse);
            }
            // Apply damage to Destructable Rocks
            if (enemy.GetComponent<DestructableRock>())
            {
                enemy.GetComponent<DestructableRock>().TakeDamage(attackDamage);
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
         
        }
        
    }
}
