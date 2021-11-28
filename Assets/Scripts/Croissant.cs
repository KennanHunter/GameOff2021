using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Croissant : MonoBehaviour
{
    [SerializeField]
    private float healthToRestore = 20.0f;

    public bool isCarried = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If we are carried and touched by ant, ignore
        if(collision.collider.GetComponent<AntBehavior>() && isCarried)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.collider.GetComponent<PlayerController>())
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            // Don't eat croissant if health is full
            if (playerController.health >= playerController.maxHealth)
            {
                return;
            }
            collision.collider.GetComponent<PlayerController>().HealDamage(healthToRestore);

            if (GetComponent<AudioSource>())
            {
                //Debug.Log("Playing audio: " + GetComponent<AudioSource>().clip.name);
                GetComponent<AudioSource>().Play();
            }

            Destroy(gameObject);
        }
    }
}
