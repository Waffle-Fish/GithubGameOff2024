using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform interactIcon;

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
        if(InputManager.Instance.WasInteractButtonPressed())
        {
            if (player == null)
                return;

            if(player.StateMachine.CurrentPlayerState.ShouldInteract())
            {
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable newInteract;
        if(other.TryGetComponent(out newInteract))
        {
            interactable = newInteract;
            interactIcon.gameObject.SetActive(true);
            interactIcon.transform.position = other.gameObject.transform.position + Vector3.up * 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable newInteract;
        if (other.TryGetComponent(out newInteract))
        {
            if(newInteract == interactable)
            {
                interactable = null;
                interactIcon.gameObject.SetActive(false);
            }
        }
    }
}
