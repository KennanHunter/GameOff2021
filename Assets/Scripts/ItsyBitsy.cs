using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsyBitsy : MonoBehaviour
{
    public GameObject youWinLadder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyController>())
        {
            if(!GetComponent<EnemyController>().isAlive)
            {
                GameObject winladder = Instantiate(youWinLadder);
                winladder.GetComponent<LevelChangeLadder>().nextLevelName = "YouWon";
            }
        }
    }
}
