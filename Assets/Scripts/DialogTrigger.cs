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

    [SerializeField]
    private bool triggerOnceOnly = false;
    private bool didTriggered = false;

    private void Start()
    {
        canvas.SetActive(false);
        didTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            if(dialogueManager.getIsDone())
            {
                canvas.SetActive(true);
                dialogueManager.StartDialogue(dialogue);
                didTriggered = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if(dialogueManager.getIsDone())
        {
            canvas.SetActive(false);
        }
        if(triggerOnceOnly && didTriggered && dialogueManager.getIsDone())
        {
            Destroy(gameObject);
        }
    }
}
