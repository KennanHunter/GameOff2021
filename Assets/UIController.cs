using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Button startButton;
    public Button optionsButton;
    public Button creditsButton;

    // Start is called before the first frame update
    void Start()
    { 
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("Start-Button");
        optionsButton = root.Q<Button>("Options-Button");
        creditsButton = root.Q<Button>("Credits-Button");


        startButton.clicked += StartButtonPressed;
        optionsButton.clicked += OptionsButtonPressed;
        creditsButton.clicked += CreditsButtonPressed;

    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void OptionsButtonPressed()
    {
        SceneManager.LoadScene("options");
    }
    void CreditsButtonPressed()
    {
        SceneManager.LoadScene("credits");
    }

}