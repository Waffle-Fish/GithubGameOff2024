using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeMachine : MonoBehaviour, IInteractable, IInteractableNPC
{
    public Cinemachine.CinemachineVirtualCamera activityCam;
    public rcadeManager arcadeManager;
    public Vector3 standOffset;
    public bool beingInteracted;
    private bool npcInteractedWith;

    public Vector2 _timeMinMax { get { return new Vector2(15f, 30f); } set { } }

    public GameObject Interact()
    {
        if (npcInteractedWith)
            return null;

        npcInteractedWith = false;
        activityCam.Priority = activityCam.Priority == 30 ? 0 : 30;
        beingInteracted = !beingInteracted;

        if (beingInteracted)
            TurnOn();
        else
            TurnOff();

        return gameObject;
    }

    public bool Taken()
    {
        return beingInteracted || npcInteractedWith;
    }

    public bool NPCInteract()
    {
        npcInteractedWith = true;
        TurnOn();
        return false;
    }

    public void EndNPCInteract() 
    {
        npcInteractedWith = false;
        TurnOff();
    }

    public Vector3 GetPosition()
    {
        return transform.position + standOffset;
    }

    public void TurnOn()
    {
        arcadeManager.TurnOn(!npcInteractedWith);
    }

    public void TurnOff()
    {
        arcadeManager.TurnOff();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + standOffset, 0.5f);
    }

    private void Update()
    {

    }
}
