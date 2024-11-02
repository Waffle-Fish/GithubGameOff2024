using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent doorEvent;

    public void Interact()
    {
        doorEvent.Invoke();
    }
}
