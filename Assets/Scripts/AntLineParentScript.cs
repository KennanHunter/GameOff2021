using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLineParentScript : MonoBehaviour
{
    public GameObject target;
    public GameObject child_prefab;
    public List<GameObject> children;
    [SerializeField]
    private int numberOfChildren = 12;
    [SerializeField]
    private int rows = 4;


    // Start is called before the first frame update
    void Start()
    {
        children = new List<GameObject>();

        for (int i = 0;  i < numberOfChildren; ++i)
        {
            Vector3 relative_spawn = new Vector3(i % rows, i / rows, 0);
            GameObject temp = Instantiate(child_prefab, transform.position + relative_spawn, Quaternion.identity);
            children.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 5.0f;
    }
}
