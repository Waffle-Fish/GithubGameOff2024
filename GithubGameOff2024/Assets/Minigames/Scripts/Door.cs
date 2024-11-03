using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable, IInteractableNPC
{
    public UnityEvent doorEvent;

    [SerializeField] private Vector2 timeMinMax;
    public Vector2 _timeMinMax { get { return timeMinMax; } set { } }

    public void Interact()
    {
        doorEvent.Invoke();
    }

    public bool NPCInteract()
    {
        return false;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
