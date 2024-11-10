using UnityEngine;

public class InteractDebug : MonoBehaviour, IInteractable
{
    public GameObject Interact()
    {
        Debug.Log("Interacted");
        return gameObject;
    }
}
