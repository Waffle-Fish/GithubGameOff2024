using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public IInteractableNPC activity;
    public float maxRoamDistance = 20f;

    public NPCStateMachine StateMachine { get; set; }
    public NPCIdleState IdleState { get; set; }
    public NPCRoamState RoamState { get; set; }
    public NPCTalkState TalkState { get; set; }
    public NPCActivityState ActivityState { get; set; }

    private void Awake()
    {
        StateMachine = new NPCStateMachine();

        IdleState = new NPCIdleState(this, StateMachine);
        RoamState = new NPCRoamState(this, StateMachine);
        TalkState = new NPCTalkState(this, StateMachine);
        ActivityState = new NPCActivityState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentNPCState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentNPCState.PhysicsUpdate();
    }

    public Vector3 GetRandomNavPoint()
    {
        Vector3 samplePos = transform.position + Random.insideUnitSphere * maxRoamDistance;

        NavMeshHit hit;
        NavMesh.SamplePosition(samplePos, out hit, maxRoamDistance, 1);

        Vector3 newDestination = hit.position;

        return newDestination;
    }

    public Vector3 GetRandomActivity()
    {
        activity = NPCManager.Instance.GetRandomActivity();
        return activity.GetPosition();
    }
}
