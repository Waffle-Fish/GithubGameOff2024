using UnityEngine;

public class Activity : MonoBehaviour, IInteractable, IInteractableNPC
{
    private bool interactedWith;
    private bool npcInteractedWith;
    [SerializeField] private Vector2 timeMinMax;
    public Vector2 _timeMinMax { get { return timeMinMax; } set { } }

    public GameObject Interact()
    {
        if (npcInteractedWith)
            return null;

        interactedWith = !interactedWith;
        return gameObject;
    }

    public bool NPCInteract()
    {
        npcInteractedWith = true;
        return false;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool Taken()
    {
        if (npcInteractedWith || interactedWith)
            return true;

        return false;
    }

    public void EndNPCInteract()
    {
        npcInteractedWith = false;
    }
}
