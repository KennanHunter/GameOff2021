using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField]
    //private float moveSpeed = 10.0f;
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float maxHealth = 100f;

    private Rigidbody2D rb; 

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health = " + health);
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= maxHealth * 0.75)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (health <= maxHealth * 0.50)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        if (health <= maxHealth * 0.25)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void FixedUpdate()
    {

        //rb.AddForce()
    }
}
