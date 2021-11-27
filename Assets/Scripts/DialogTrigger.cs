using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            if(dialogueManager.getIsDone())
            {
                canvas.SetActive(true);
                dialogueManager.StartDialogue(dialogue);
            }
        }
    }

    private void FixedUpdate()
    {
        if(dialogueManager.getIsDone())
        {
            canvas.SetActive(false);
        }
    }
}
