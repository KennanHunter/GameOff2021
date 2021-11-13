using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;

    [SerializeField]
    private string mainLevelName;

    public void Play()
    {
        SceneManager.LoadScene(mainLevelName);
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
