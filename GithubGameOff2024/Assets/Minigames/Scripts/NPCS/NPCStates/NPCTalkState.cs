using UnityEngine;

public class NPCTalkState : NPCState
{
    public NPCTalkState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine) { }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EnterState()
    {
        base.EnterState();
        npc.agent.isStopped = true;
        npc.dialogueManager.StartDialogue(npc.dialogue[npc.dialogueIndex]);
    }

    public override void ExitState()
    {
        base.ExitState();
        npc.agent.isStopped = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
