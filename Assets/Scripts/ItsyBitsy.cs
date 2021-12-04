using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsyBitsy : MonoBehaviour
{
    public GameObject youWinLadder;
    private bool didWeDie;
    // Start is called before the first frame update
    void Start()
    {
        youWinLadder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyController>() && !didWeDie)
        {
            if(!GetComponent<EnemyController>().isAlive)
            {
                //Debug.Log("Itsy is dead");
                youWinLadder.SetActive(true);
                didWeDie = true;
            }
        }
    }
}
