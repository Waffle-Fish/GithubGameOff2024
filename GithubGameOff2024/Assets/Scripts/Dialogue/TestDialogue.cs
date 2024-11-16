using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.IsInteractButtonPressed())
        {
            dialogueTrigger.TriggerDialogue();
        }
    }
}
