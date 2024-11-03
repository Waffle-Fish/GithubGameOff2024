using UnityEngine;

public class NPCRoamState : NPCState
{
    private Vector3 _targetPos;

    public NPCRoamState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        _waitTime = Random.Range(15f, 25f);

        _targetPos = npc.GetRandomNavPoint();
        Debug.Log(_targetPos);
        npc.agent.SetDestination(_targetPos);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_time >= _waitTime || (npc.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            npc.StateMachine.ChangeState(npc.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
