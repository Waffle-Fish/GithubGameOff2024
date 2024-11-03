using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private IInteractable idleActivity;
    
    public NPCStateMachine StateMachine { get; set; }
    public NPCIdleState IdleState { get; set; }
    public NPCRoamState RoamState { get; set; }
    public NPCTalkState TalkState { get; set; }

    private void Awake()
    {
        StateMachine = new NPCStateMachine();

        IdleState = new NPCIdleState(this, StateMachine);
        RoamState = new NPCRoamState(this, StateMachine);
        TalkState = new NPCTalkState(this, StateMachine);
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
}
