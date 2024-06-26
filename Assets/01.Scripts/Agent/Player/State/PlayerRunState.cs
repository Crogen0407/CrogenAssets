﻿using Crogen.AgentFSM;
using UnityEngine;

public class PlayerRunState : AgentState<PlayerStateEnum>
{
    public PlayerRunState(Agent<PlayerStateEnum> agentBase, StateMachine<PlayerStateEnum> stateMachine, string animBoolName) : base(agentBase, stateMachine, animBoolName)
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
        if (Input.GetKeyUp(KeyCode.A))
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        else
        {
            Debug.Log("달리는 중...");
        }
    }
}
