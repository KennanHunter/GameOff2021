using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Class1 : MonoBehaviour
{


    public Button creditsbackButton;
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        creditsbackButton = root.Q<Button>("Credits-Back");


        creditsbackButton.clicked += CreditsBackButtonPressed;
    }


    
    void CreditsBackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}