using UnityEngine;

public class NPCRoamState : NPCState
{
    private Vector3 _targetPos;

    public NPCRoamState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine) { }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        _waitTime = Random.Range(15f, 25f);

        _targetPos = npc.GetRandomActivity();
        npc.agent.SetDestination(_targetPos);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_time >= _waitTime)
        {
            npc.StateMachine.ChangeState(npc.IdleState);
        }

        if ((npc.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            npc.StateMachine.ChangeState(npc.ActivityState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
