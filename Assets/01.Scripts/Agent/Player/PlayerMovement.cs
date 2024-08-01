using UnityEngine;
using Crogen.AgentFSM.Movement;
using Crogen.AgentFSM;

public class PlayerMovement : MonoBehaviour, IGroundMovement
{
	[SerializeField] private float _speed = 5;
	public bool IsGround { get; set; }
	public Vector3 Velocity { get; set; }
	public bool IsJumping { get; set; }
	public Agent AgentBase { get; set; }

	private Rigidbody _rigidbody;

	public void Initialize(Agent agent)
	{
		_rigidbody = GetComponent<Rigidbody>();
		AgentBase = agent;
	}

	public void OnJump()
	{
	}

	public void SetDestination(Vector3 destination)
	{
	}

	public void SetMovement(Vector3 movement, bool isRotation = true)
	{
		if(isRotation) transform.forward = movement;
		_rigidbody.velocity = movement * _speed;
	}

	public void StopImmediately()
	{
	}

	public void GetKnockback(Vector3 force)
	{

	}
}
