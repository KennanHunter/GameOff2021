using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPoisonBall : MonoBehaviour
{
    [SerializeField]
    public float attackRange = 1f;
    [SerializeField]
    public LayerMask playerLayers;
    [SerializeField]
    public float attackDamage = 20.0f;
    [SerializeField]
    public float timeBetweenAttacks = 1.0f;

    private float attackTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;  // Decrement attackTimer
        }
        else
        {
            attackTimer = 0;
        }

        // If attackTimer hasn't timed out yet, ignore attack input
        if (attackTimer > 0)
        {
            return;
        }

        
        // Detect players in range that are in the "Player" Layer
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, attackRange, playerLayers);

        // Apply damage to enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            // Apply damage to Enemy Controllers
            if (enemy.GetComponent<PlayerController>())
            {
                enemy.GetComponent<PlayerController>().TakeDamage(attackDamage);
            }
            attackTimer = timeBetweenAttacks;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
