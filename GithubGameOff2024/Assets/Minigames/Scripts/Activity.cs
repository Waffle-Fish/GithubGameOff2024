using UnityEngine;

public class Activity : MonoBehaviour, IInteractable, IInteractableNPC
{
    [SerializeField] private Vector2 timeMinMax;
    public Vector2 _timeMinMax { get { return timeMinMax; } set { } }

    public void Interact()
    {

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
