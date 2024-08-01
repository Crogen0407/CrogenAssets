using Crogen.AgentFSM.Movement;
using Crogen.AgentFSM;
using UnityEngine;

public class Player : Agent
{
	private PlayerMovement _playerMovement;

	private void Awake()
	{
		Initialize<PlayerStateEnum>();
		_playerMovement = Movement as PlayerMovement;
		Movement.Initialize(this);
	}

	protected override void Update()
	{
		base.Update();
		Vector3 movement = ((Vector3.right * Input.GetAxisRaw("Horizontal")) + (Vector3.forward * Input.GetAxisRaw("Vertical"))).normalized;
		if (!Mathf.Approximately(movement.sqrMagnitude, 0.0f))
		{
			Movement.SetMovement(movement, true);
			StateMachine.ChangeState(PlayerStateEnum.Run);
		}
		else
		{
			StateMachine.ChangeState(PlayerStateEnum.Idle);
		}
	}
}