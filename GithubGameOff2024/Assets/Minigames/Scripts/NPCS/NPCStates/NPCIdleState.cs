using UnityEngine;

public class NPCIdleState : NPCState
{

    public NPCIdleState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

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

        Debug.Log(time);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
