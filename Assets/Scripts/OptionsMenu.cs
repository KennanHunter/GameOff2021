using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("gameVolume"))
        {
            PlayerPrefs.SetFloat("gameVolume", 0.25f);
            Load();
        }
        else
        {
            Load();
        }
    }
    [SerializeField] Slider volumeSlider;
    public void ChangeVolume ()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("gameVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("gameVolume", volumeSlider.value);
    }
}
