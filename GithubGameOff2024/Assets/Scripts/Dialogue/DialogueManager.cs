using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;
    private bool _isDialogueComplete = true;

    void Start()
    {
        sentences = new Queue<string>();

    }
    public void StartDialogue(Dialogue dialogue)
    {
        _isDialogueComplete = false;
        animator.SetBool("isOpen", true);
        sentences.Clear();
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return false;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        return true;
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        _isDialogueComplete = true;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space) && !_isDialogueComplete)
    //    {
    //        DisplayNextSentence();
    //    }
    //}

    public bool IsDialogueComplete()
    {
        return _isDialogueComplete;
    }
}
