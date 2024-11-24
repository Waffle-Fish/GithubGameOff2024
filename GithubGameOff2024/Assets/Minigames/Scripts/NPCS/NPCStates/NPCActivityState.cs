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
        {
            npc.activity = null;
            npc.StateMachine.ChangeState(npc.RoamState);
            return;
        }

        npc.activity.NPCInteract();
        _waitTime = Random.Range(npc.activity._timeMinMax.x, npc.activity._timeMinMax.y);
    }

    public override void ExitState()
    {
        base.ExitState();

        if (npc.activity != null)
            npc.activity.EndNPCInteract();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_time >= _waitTime)
        {
            npc.agent.SetDestination(npc.GetRandomNavPoint());
            npc.StateMachine.ChangeState(npc.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
