using UnityEngine;
using UnityEngine.Events;

public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent interactEvent;
    [SerializeField] private UnityEvent reinteractEvent;

    private bool toggled;

    public void Interact()
    {
        if (toggled)
            reinteractEvent.Invoke();
        else
            interactEvent.Invoke();

        toggled = !toggled;
    }
}
