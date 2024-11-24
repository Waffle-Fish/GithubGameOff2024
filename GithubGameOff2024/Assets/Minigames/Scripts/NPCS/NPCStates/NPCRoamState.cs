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
        _waitTime = Random.Range(15f, 25f);

        if(!(npc.activity != null && npc.interupted))
        {
            _targetPos = npc.GetRandomActivity();
        }


        if (npc.activity == null)
            npc.StateMachine.ChangeState(npc.IdleState);

        npc.agent.SetDestination(_targetPos);
        _targetPos = npc.agent.destination;
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (_time >= _waitTime || npc.activity.Taken())
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
