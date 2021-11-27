using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPusher : MonoBehaviour
{
    [SerializeField]
    private float streamPushForce = 5f;
    private Vector2 pushDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerController>().setInExternalForce(true);
            //Debug.Log("Player in stream");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(pushDirection * streamPushForce * Time.deltaTime, ForceMode2D.Force);
        }
    }

    public void setDirection(Vector2 dir)
    {
        pushDirection = dir;
    }
}
