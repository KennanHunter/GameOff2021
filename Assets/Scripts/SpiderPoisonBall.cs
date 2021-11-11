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
    public LayerMask enemyLayers;
    [SerializeField]
    public float attackDamage = 5.0f;
    [SerializeField]
    public float timeBetweenAttacks = 1.0f;
    [SerializeField]
    public float spikeSightRange = 5f;

    private float attackTimer = 0f;

    private void FixedUpdate()
    {
        // Rotate spikes to be scary
        transform.Rotate(0, 0, attackDamage * 90 * Time.deltaTime);

        // Push away from parent (Spider)
        Vector2 outDirection = (transform.position - transform.parent.position);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(outDirection * Time.deltaTime * attackDamage, ForceMode2D.Force);

        // Push spikes away from other spikes
        Collider2D[] otherEnemyUnits = Physics2D.OverlapCircleAll(gameObject.transform.position, attackRange, enemyLayers);
        // Find closest otherEnemyUnit and push away from it
        float distanceFromOthers = 20f;
        float shortestDistance = 20f;
        Collider2D closestCollider = null;
        foreach (Collider2D enemy in otherEnemyUnits)
        {
            distanceFromOthers = (transform.position - enemy.GetComponentInParent<Transform>().position).magnitude;
            if(distanceFromOthers < shortestDistance)
            {
                shortestDistance = distanceFromOthers;
                closestCollider = enemy;
                Debug.Log("Closest Enemy: " + closestCollider);
            }
        }
        Vector2 pushDirection = transform.position - closestCollider.GetComponentInParent<Transform>().position;
    }

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
}
