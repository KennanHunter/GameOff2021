using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float maxHealth = 100f;

    public void TakeDamage(float damage)
    {
        Debug.Log("Damaged, health = " + health);
        health -= damage;
        if(health < 0)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= maxHealth / 2)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
