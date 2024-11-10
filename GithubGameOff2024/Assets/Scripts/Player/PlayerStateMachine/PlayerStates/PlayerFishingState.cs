using UnityEngine;

public class PlayerFishingState : PlayerState
{
    public PlayerFishingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override bool ShouldInteract()
    {
        return base.ShouldInteract();
    }

    public override void Interacted(GameObject interactObj)
    {
        base.Interacted(interactObj);
    }
}
