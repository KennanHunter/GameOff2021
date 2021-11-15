using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    public GameObject changeIfObjectDead;

    void FixedUpdate()
    {
        if(changeIfObjectDead == null)
        {
            SceneManager.LoadScene("GardenDungeon");
        }
    }
}
