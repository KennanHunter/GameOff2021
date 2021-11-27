using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public AudioSource TypeSound;
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    private bool isDone = true;

    // Use this for initialization
    void Start()
    {
        TypeSound = GetComponent<AudioSource>();
        sentences = new Queue<string>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        isDone = false;
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        TypeSound.Play(); // PLAYS SOUND AT THE START
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        TypeSound.Stop(); // STOPS THE SOUND AS SOON AS THE TEXT STOPS ANIMATING
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        isDone = true;
    }

    public bool getIsDone()
    {
        return isDone;
    }

}
