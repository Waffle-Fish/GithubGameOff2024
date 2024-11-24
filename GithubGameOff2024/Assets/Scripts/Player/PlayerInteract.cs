using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform interactIcon;

    private Transform intertactableTransform;
    private Player player;
    private IInteractable interactable;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        interactIcon.parent = null;

        if (player == null)
            Debug.Log("Using the og player");
    }

    private void Update()
    {
        if(interactable != null)
        {
            interactIcon.transform.position = intertactableTransform.position + Vector3.up * 2;
        }

        if(InputManager.Instance.WasInteractButtonPressed())
        {
            if (player == null)
                return;

            if(player.StateMachine.CurrentPlayerState.ShouldInteract())
            {
                if (interactable != null)
                {
                    player.StateMachine.CurrentPlayerState.Interacted(interactable.Interact());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (intertactableTransform != null && Vector3.Distance(other.transform.position, transform.position) > Vector3.Distance(intertactableTransform.position, transform.position))
            return;

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
