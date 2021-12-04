using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;

    [SerializeField]
    private string startLevelName;

    private void Awake()
    {
        //if (PlayerPrefs.GetFloat("gameVolume") >= 1)
        //{
        //    PlayerPrefs.SetFloat("gameVolume", 0.25f);
        //    Debug.Log("We set volume to" + PlayerPrefs.GetFloat("gameVolume"));
        //    AudioListener.volume = PlayerPrefs.GetFloat("gameVolume");
        //}
        //else
        //{
        //    //Debug.Log("We set volume to" + PlayerPrefs.GetFloat("gameVolume"));
        //}
    }

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
