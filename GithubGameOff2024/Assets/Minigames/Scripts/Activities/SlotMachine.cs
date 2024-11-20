using UnityEngine;

public class SlotMachine : MonoBehaviour, IInteractable, IInteractableNPC
{
    public Cinemachine.CinemachineVirtualCamera activityCam;
    public Vector3 standOffset;

    public Vector2 _timeMinMax { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public GameObject Interact()
    {
        activityCam.Priority = activityCam.Priority == 30 ? 0 : 30;
        return gameObject;
    }

    public bool NPCInteract()
    {
        return false;
    }

    public Vector3 GetPosition()
    {
        return transform.position + standOffset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + standOffset, 0.5f);
    }
}
