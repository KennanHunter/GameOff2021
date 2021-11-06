using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    public Transform attackPoint;
    [SerializeField]
    public float attackRange = 0.5f;
    [SerializeField]
    public float attackForce = 5.0f;
    [SerializeField]
    public LayerMask enemyLayers;

    public void OnAttack()
    {
        // Play attack animation

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Apply damage to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
            enemy.attachedRigidbody.AddForce(transform.rotation.eulerAngles * attackForce, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
