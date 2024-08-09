using Crogen.AgentFSM.Movement;
using UnityEngine;

public class SpaceShipDestinationMovement : SpaceShipMovement, IDestinationMovement
{
	[SerializeField] private LayerMask _obstacle;
	[SerializeField] private float _findableDistance = 5f;

	private int _rotateSeed;
	private bool _isMove2Destination = true;
	private bool _isTurn2EmptySpace = false;
	public Vector3 currentDestination;
	private Vector3 _currentTruningDirection;

	public void SetDestination(Vector3 destination)
	{
		_isMove2Destination = true;
	}

	private void FixedUpdate()
	{
		if (!_isMove2Destination) return;
		if (Vector3.Distance(currentDestination, transform.position) < Time.fixedDeltaTime) _isMove2Destination = true;

		//Move
		_rigidbody.velocity = transform.forward * _moveSpeed;

		//Find Obstacle
		Debug.DrawRay(transform.position, transform.forward * _findableDistance, Color.green);
		if(Physics.Raycast(transform.position, transform.forward, out RaycastHit firstHit, _findableDistance * 2, _obstacle))
		{
			if (_isTurn2EmptySpace)
			{
				transform.rotation = 
					Quaternion.Lerp(
						transform.rotation,
						Quaternion.LookRotation(_currentTruningDirection),
						Time.fixedDeltaTime * _rotSpeed);
			}
			else
			{
				SetTurningRaycast();
			}
		}
		else if (_isTurn2EmptySpace)
		{
			_currentTruningDirection = (currentDestination - transform.position).normalized;
			transform.rotation =
					Quaternion.Lerp(
						transform.rotation,
						Quaternion.LookRotation(_currentTruningDirection),
						Time.fixedDeltaTime * _rotSpeed);
			_isTurn2EmptySpace = false;
		}
	}

	private void SetTurningRaycast()
	{
		for (int i = 0; i < 4; ++i)
		{
			float degValue = ((90 * i) + _rotateSeed) % 4; //0, 90, 180, 270
			float radValue = Mathf.Deg2Rad * degValue;

			Vector3 dirVec = new Vector3(Mathf.Cos(radValue), Mathf.Sin(radValue), 0).normalized;

			//Up, Right, Down, Left
			Debug.DrawRay(transform.position, transform.rotation * dirVec * _findableDistance, Color.green);
			if (!Physics.Raycast(transform.position, transform.rotation * dirVec, out RaycastHit lastHit, _findableDistance, _obstacle))
			{
				_currentTruningDirection = dirVec;
				++_rotateSeed;
				_isTurn2EmptySpace = true;
				break;
			}
		}
	}
}
