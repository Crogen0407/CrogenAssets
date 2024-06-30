using Crogen.AgentFSM;
using UnityEngine;

public class PlayerIdleState : AgentState<PlayerStateEnum>
{
    public PlayerIdleState(Agent<PlayerStateEnum> agentBase, StateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("가만히 있는 중...");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("가만히 있는 중...");
        if (Input.GetKeyDown(KeyCode.A))
        {
            _stateMachine.ChangeState(PlayerStateEnum.Run);
        }
    }
}
