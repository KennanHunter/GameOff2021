using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject CreditsBack;

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
