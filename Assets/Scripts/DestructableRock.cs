using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRock : MonoBehaviour
{
    [SerializeField]
    private float m_health = 20f;

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        Debug.Log(gameObject.name + " health = " + m_health);
        if (m_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
