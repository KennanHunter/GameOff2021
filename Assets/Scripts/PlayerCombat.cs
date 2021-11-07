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

    private float attackTimer = 0f;

    public void OnAttack()
    {
        // If attackTimer hasn't timed out yet, ignore attack input
        if(attackTimer > 0)
        {
            return;
        }

        // Play attack animation

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Apply damage to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            if(enemy.GetComponent<Rigidbody2D>())
            {
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                enemyRb.AddForce(transform.up * attackForce, ForceMode2D.Impulse);
            }
            if(enemy.GetComponent<EnemyController>())
            {
                //  Currently failing to deal damage
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
                attackTimer = timeBetweenAttacks;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            attackTimer = 0;
        }
    }
}
