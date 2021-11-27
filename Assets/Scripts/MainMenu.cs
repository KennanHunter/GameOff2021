using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;

    [SerializeField]
    private string startLevelName;

    public void Play()
    {
        SceneManager.LoadScene(startLevelName);
    }
    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }
    public void Options()
    {
        SceneManager.LoadScene("options");
    }
}
