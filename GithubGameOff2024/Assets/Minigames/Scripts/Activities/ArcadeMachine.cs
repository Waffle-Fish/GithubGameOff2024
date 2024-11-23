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

    public Vector2 _timeMinMax { get { return new Vector2(5f, 5f); } set { } }

    public GameObject Interact()
    {
        if (npcInteractedWith)
            return null;

        npcInteractedWith = false;
        activityCam.Priority = activityCam.Priority == 30 ? 0 : 30;
        beingInteracted = !beingInteracted;

        TurnOn(beingInteracted);

        return gameObject;
    }

    public bool Taken()
    {
        return beingInteracted || npcInteractedWith;
    }

    public bool NPCInteract()
    {
        TurnOn(true);
        Invoke(nameof(TurnOn), 5f);
        return false;
    }

    public Vector3 GetPosition()
    {
        return transform.position + standOffset;
    }

    public void TurnOn(bool on = false)
    {
        if (on)
            arcadeManager.TurnOn();
        else
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
