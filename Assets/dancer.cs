using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dancer : MonoBehaviour
{
    private float animationTimer = 0f;
    private float animationChangeCooldown = 5f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Caterpillar", -1, 0f);
    }

    void Update()
    {
        if(animationTimer <= 0)
        {
            int i = Random.Range(0, 3);
            switch(i)
            {
                case 0:
                    {
                        anim.Play("Caterpillar", -1, 0f);
                    }
                    break;
                case 1:
                    {
                        anim.Play("Caterpillar2", -1, 0f);
                    }
                    break;
                case 2:
                    {
                        anim.Play("Caterpillar3", -1, 0f);
                    }
                    break;
            }
            animationTimer = animationChangeCooldown;
            //Debug.Log(i);
        }
        else
        {
            animationTimer -= Time.deltaTime;
        }
    }
}
