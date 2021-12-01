using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;  // Reset time scale before leaving the level
        SceneManager.LoadScene("MainMenu");
    }
}
