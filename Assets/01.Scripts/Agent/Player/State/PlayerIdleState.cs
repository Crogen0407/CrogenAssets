using Crogen.AgentFSM;
using UnityEngine;

public class PlayerIdleState : AgentState
{
    public PlayerIdleState(Agent agentBase, StateMachine stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("가만히 있는 중...");
    }
}
