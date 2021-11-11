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

    private float spikePushSightDistance = 5.0f;
    private float attackTimer = 0f;

    private void FixedUpdate()
    {
        // Rotate spikes to be scary
        transform.Rotate(0, 0, attackDamage * 360 * Time.deltaTime);

        // Push away from parent (Spider)
        Vector2 outDirection = (transform.position - transform.parent.position);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(outDirection * Time.deltaTime * attackDamage, ForceMode2D.Force);

        // Push spikes away from other spikes and enemies
        // Scan for other enemy units within spikeSightDistance
        Collider2D[] otherEnemyUnits = Physics2D.OverlapCircleAll(gameObject.transform.position, spikeSightRange, enemyLayers);
        // Set initial values, anything close should be within spikePushSightDistance
        float distanceFromOthers, shortestDistance = spikePushSightDistance;
        Collider2D closestCollider = GetComponent<Collider2D>();

        foreach (Collider2D enemy in otherEnemyUnits)
        {
            // If we're considering ourselves, ignore
            if (enemy.transform.position == transform.position)
            {
                continue;
            }

            distanceFromOthers = (transform.position - enemy.GetComponent<Transform>().position).magnitude;
            if (distanceFromOthers < shortestDistance)
            {
                shortestDistance = distanceFromOthers;
                closestCollider = enemy;
            }
        }
        Vector2 pushDirection = (transform.position - closestCollider.GetComponent<Transform>().position).normalized;
        rb.AddForce(pushDirection * Time.deltaTime * attackDamage * 500, ForceMode2D.Force);
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
