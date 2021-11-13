using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croissant : MonoBehaviour
{
    [SerializeField]
    private float healthToRestore = 20.0f;

    public bool isCarried = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>())
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            //if(playerController.)
            collision.collider.GetComponent<PlayerController>().HealDamage(healthToRestore);
            Destroy(gameObject);
        }
    }
}
