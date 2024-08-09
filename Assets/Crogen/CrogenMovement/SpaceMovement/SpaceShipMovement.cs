using UnityEngine;
using Crogen.AgentFSM.Movement;
using Crogen.AgentFSM;

public class SpaceShipMovement : MonoBehaviour, IFloatingMovement
{
	[Header("Speed")]
	[SerializeField] protected float _moveSpeed = 10f;
	[SerializeField] protected float _rotSpeed = 25f;

	protected Rigidbody _rigidbody;

	public Agent AgentBase { get; set; }
	public Vector3 Velocity { get => _rigidbody.velocity; set => _rigidbody.velocity = value; }


	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void Initialize(Agent agent)
	{
		AgentBase = agent;
	}

	public void GetKnockback(Vector3 force)
	{
	}

	public void SetMovement(Vector3 movement, bool isRotation = true)
	{
		Velocity = movement * _moveSpeed;

		//Rotate
		if (!isRotation || Mathf.Approximately(movement.sqrMagnitude, 0)) return;
		transform.forward = movement;
	}

	public void StopImmediately()
	{
	}
}
