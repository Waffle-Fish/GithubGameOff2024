using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeMachine : MonoBehaviour, IInteractable, IInteractableNPC
{
    public Cinemachine.CinemachineVirtualCamera activityCam;
    public Vector3 standOffset;
    public bool beingInteracted;
    private bool npcInteractedWith;

    public Vector2 _timeMinMax { get { return new Vector2(4f, 4.5f); } set { } }

    public GameObject Interact()
    {
        if (npcInteractedWith)
            return null;

        npcInteractedWith = false;
        activityCam.Priority = activityCam.Priority == 30 ? 0 : 30;
        beingInteracted = !beingInteracted;
        return gameObject;
    }

    public bool Taken()
    {
        return beingInteracted || npcInteractedWith;
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

    private void Update()
    {

    }
}
