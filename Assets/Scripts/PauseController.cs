using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{

    public Button resumeButton;
    public Button menuButton;
    public Button quitButton;
    

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

       resumeButton = root.Q<Button>("Resume-Button");
        menuButton = root.Q<Button>("Menu-Button");
        quitButton = root.Q<Button>("Quit-Button");


        resumeButton.clicked += resumeButtonPressed;
        menuButton.clicked += menuButtonPressed;
        quitButton.clicked += quitButtonPressed;

    }

    void resumeButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void menuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void quitButtonPressed()
    {
        Application.Quit();
    }


}