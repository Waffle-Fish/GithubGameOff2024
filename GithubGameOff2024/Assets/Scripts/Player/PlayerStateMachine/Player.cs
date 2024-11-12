using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public PlayerMovement movement;
    public GameObject interactingObject;
    public Cinemachine.CinemachineVirtualCamera interactionCamera;
    public Cinemachine.CinemachineTargetGroup targetGroup;

    #region state machine stuff
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerFishingState FishingState { get; set; }
    public PlayerDefaultState DefaultState { get; set; }
    public PlayerTalkState TalkState { get; set; }
    public PlayerActivityState ActivityState { get; set; }
    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        DefaultState = new PlayerDefaultState(this, StateMachine);
        FishingState = new PlayerFishingState(this, StateMachine);
        TalkState = new PlayerTalkState(this, StateMachine);
        ActivityState = new PlayerActivityState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(DefaultState);
    }

    private void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentPlayerState.PhysicsUpdate();
    }
}
