using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    public float health;
    public int numofHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        numofHearts = (int)(playerController.maxHealth *0.1f);
        
    }
    void Update()
    {
        health = playerController.health *0.1f;
      
        int j = 0;
        for (int i = 0; i < hearts.Length; i++)
        {
            //this determines the health level to show
            if(i < health)
            {
                j++; 
                hearts[i].sprite = fullHeart;
            }else
            {
                hearts[i].sprite = emptyHeart;
            }
           
         


           //This determines the number of heart containers
           if (i < numofHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
        //this determines if its a half heart or not
        if (health % 1 == 0.5)
        {
            hearts[j-1].sprite = halfHeart;
        }
    }

}
