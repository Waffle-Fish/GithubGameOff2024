using UnityEngine;

public class PlayerDefaultState : PlayerState
{
    public PlayerDefaultState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine) { }

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
        player.movement.MoveUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override bool ShouldInteract()
    {
        return true;
    }
}
