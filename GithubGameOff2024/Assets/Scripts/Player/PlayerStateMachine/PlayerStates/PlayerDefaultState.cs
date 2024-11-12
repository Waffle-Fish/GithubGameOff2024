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
        if(player.interactingObject != null)
        {
            player.interactingObject.GetComponent<IInteractable>().Interact();
            player.targetGroup.RemoveMember(player.interactingObject.transform);
            player.interactingObject = null;
        }
        player.interactionCamera.Priority = 5;
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

    public override void Interacted(GameObject interactObj)
    {
        base.Interacted(interactObj);

        if (interactObj == null)
            return;

        player.interactingObject = interactObj;

        switch (interactObj.layer)
        {
            case 11: // npc
                player.StateMachine.ChangeState(player.TalkState);
                break;
            case 12: // activity
                player.StateMachine.ChangeState(player.ActivityState);
                break;
        }

        player.interactionCamera.Priority = 15;
        player.targetGroup.AddMember(interactObj.transform, 1, 1);
    }
}
