using UnityEngine;

public class NPCActivityState : NPCState
{
    public NPCActivityState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine) { }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();

        if (npc.activity.Taken())
            npc.StateMachine.ChangeState(npc.RoamState);

        npc.activity.NPCInteract();
        _waitTime = Random.Range(npc.activity._timeMinMax.x, npc.activity._timeMinMax.y);
    }

    public override void ExitState()
    {
        base.ExitState();

        npc.activity = null;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_time >= _waitTime)
        {
            npc.StateMachine.ChangeState(npc.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
