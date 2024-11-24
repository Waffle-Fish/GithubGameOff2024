using UnityEngine;

public class NPCIdleState : NPCState
{
    public NPCIdleState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine) { }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        //_waitTime = Random.Range(10f, 15f);
        _waitTime = Random.Range(1f, 2f);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if(_time >= _waitTime)
        {
            npc.StateMachine.ChangeState(npc.RoamState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
