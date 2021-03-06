using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    public float explosionDamage = 30f;
    public float explosionRange = 5f;
    public float explosionForce = 5f;
    [SerializeField]
    public AudioClip ExplosionSound;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Explode on contact with Enemy or Destructable rock (if fast enough)
        if(collision.gameObject.GetComponent<EnemyController>() || collision.gameObject.GetComponent<DestructableRock>())
        {
            if (rb.velocity.magnitude > 0.1f)
            {
                // Play explosion animation
                if(!GetComponent<ParticleSystem>().isPlaying)
                {
                    gameObject.GetComponent<ParticleSystem>().Play();
                    if (GetComponent<AudioSource>())
                    {
                        GetComponent<AudioSource>().Play();
                    }

                    //Debug.Log("Playing animation");
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }

                // Detect enemies in range that are in the "Enemy" Layer
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);

                // Apply damage to enemies
                foreach (Collider2D enemy in hitEnemies)
                {
                    // Apply damage to Enemy Controllers
                    if (enemy.GetComponent<EnemyController>())
                    {
                        enemy.GetComponent<EnemyController>().TakeDamage(explosionDamage);
                    }
                    // Apply damage to Destructable Rocks
                    if (enemy.GetComponent<DestructableRock>())
                    {
                        enemy.GetComponent<DestructableRock>().TakeDamage(explosionDamage);
                    }
                    // Apply force to Rigidbodies
                    if (enemy.GetComponent<Rigidbody2D>())
                    {
                        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                        Vector2 directionToForce = (enemy.transform.position - transform.position).normalized;
                        enemyRb.AddForce(directionToForce * explosionForce, ForceMode2D.Impulse);
                    }
                }
              
                //Destroy(gameObject);
            }
        }
    }
}
