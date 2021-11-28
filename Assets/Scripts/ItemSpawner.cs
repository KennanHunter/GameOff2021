using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnItemList;

    private float spawnTimer = 0f;
    [SerializeField]
    private float spawnTimerCooldown = 5.0f;

    public bool doSpawnItems = true;

    void Update()
    {
        if(doSpawnItems)
        {
            if (spawnTimer <= 0)
            {
                spawnTimer = spawnTimerCooldown;
                int i = (int)(Random.value * spawnItemList.Count);
                //Debug.Log("Spawn Item List Index: " + i);
                Instantiate(spawnItemList[i], gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
}
