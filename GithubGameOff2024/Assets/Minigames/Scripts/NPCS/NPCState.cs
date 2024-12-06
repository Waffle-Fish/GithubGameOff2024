using UnityEngine;

public class NPCState
{
    protected NPC npc;
    protected NPCStateMachine npcStateMachine;
    protected float _time;
    protected float _waitTime;

    public NPCState(NPC npc, NPCStateMachine npcStateMachine)
    {
        this.npc = npc;
        this.npcStateMachine = npcStateMachine;
    }

    public virtual void EnterState() 
    {
        _time = 0;
        npc.interupted = false;
    }

    public virtual void ExitState() { }
    public virtual void FrameUpdate() 
    {
        _time += Time.deltaTime;
    }

    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}
