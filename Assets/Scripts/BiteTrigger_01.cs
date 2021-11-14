using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteTrigger_01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetComponent<Animator>().Play("Queen_bite", -1, 0f);

        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            GetComponent<Animator>().Play("Queen_idle", -1, 0f);

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<Animator>().Play("Queen_dash", -1, 0f);

        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            GetComponent<Animator>().Play("Queen_idle", -1, 0f);
        }
    }
}
