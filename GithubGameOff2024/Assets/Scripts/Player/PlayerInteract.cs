using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform interactIcon;

    private Transform intertactableTransform;
    private IInteractable interactable;

    private void Start()
    {
        interactIcon.parent = null;
    }

    private void Update()
    {
        if(interactable != null)
        {
            interactIcon.transform.position = intertactableTransform.position + Vector3.up * 2;
        }

        if(InputManager.Instance.WasInteractButtonPressed())
        {
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable newInteract;
        if(other.TryGetComponent(out newInteract) || other.transform.root.TryGetComponent(out newInteract))
        {
            interactable = newInteract;
            interactIcon.gameObject.SetActive(true);
            interactIcon.transform.position = other.gameObject.transform.position + Vector3.up * 2;
            intertactableTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable newInteract;
        if (other.TryGetComponent(out newInteract) || other.transform.root.TryGetComponent(out newInteract))
        {
            if(newInteract == interactable)
            {
                interactable = null;
                interactIcon.gameObject.SetActive(false);
                intertactableTransform = null;
            }
        }
    }
}
