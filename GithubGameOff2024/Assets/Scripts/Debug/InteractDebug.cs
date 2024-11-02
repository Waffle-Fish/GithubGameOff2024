using UnityEngine;

public class InteractDebug : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacted");
    }
}
