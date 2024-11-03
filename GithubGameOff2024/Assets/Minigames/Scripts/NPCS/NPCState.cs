using UnityEngine;

public class NPCState
{
    protected NPC npc;
    protected NPCStateMachine npcStateMachine;
    protected float time;

    public NPCState(NPC npc, NPCStateMachine npcStateMachine)
    {
        this.npc = npc;
        this.npcStateMachine = npcStateMachine;
    }

    public virtual void EnterState() 
    {
        time = 0;
    }

    public virtual void ExitState() { }
    public virtual void FrameUpdate() 
    {
        time += Time.deltaTime;
    }

    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}
