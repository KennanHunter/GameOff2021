using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLookAtPlayer : MonoBehaviour
{
    private Transform target;
    private float rotateSpeed = 720.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Find player's position (Transform)
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 towardsPlayer = target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, towardsPlayer);
        transform.rotation =
                Quaternion.RotateTowards(gameObject.transform.rotation,
                toRotation, rotateSpeed * Time.deltaTime);
    }
}
