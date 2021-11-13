using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public int health;
    public int numofHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        numofHearts = (int)(playerController.maxHealth *0.1f);
        
    }
    void Update()
    {
        health = (int)(playerController.health *0.1f);
        for (int i = 0; i < hearts.Length; i++)
        {

            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numofHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
    }
}
