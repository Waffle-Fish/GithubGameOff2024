using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
    public void TriggerDialogue(Dialogue dialogue)
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
    public bool isDialogueComplete()
    {
        return FindFirstObjectByType<DialogueManager>().IsDialogueComplete();
    }
}
