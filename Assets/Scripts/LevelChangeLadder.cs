using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangeLadder : MonoBehaviour
{
    [SerializeField]
    private string nextLevelName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
