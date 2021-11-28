using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public static bool YouLose = false;
    public GameObject LoseScreenUI;

    void Start()
    {

        LoseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        YouLose = false;
        playerController = GetComponentInParent<PlayerController>();

    }
    // Update is called once per frame
    void Update()
    {
        

        if (playerController.health <= 0)
        {
            LoseScreenUI.SetActive(true);
            Time.timeScale = 0.2f;
            YouLose = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (YouLose)
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("AnthillLevel");
    }
   
}
