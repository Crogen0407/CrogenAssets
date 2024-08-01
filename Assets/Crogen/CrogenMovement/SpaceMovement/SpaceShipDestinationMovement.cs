using Crogen.AgentFSM.Movement;
using UnityEngine;

public class SpaceShipDestinationMovement : SpaceShipMovement, IDestinationMovement
{
	[SerializeField] private LayerMask _obstacle;
	[SerializeField] private float _findableDistance = 5f;

	private bool _isMove2Destination = false;
	private bool _isTurn2EmptySpace = false;
	private Vector3 _currentDestination;
	private Vector3 _currentTurningDirection;

	public void SetDestination(Vector3 destination)
	{
		_isMove2Destination = true;
	}

	private void FixedUpdate()
	{
		if (!_isMove2Destination) return;
		if (Vector3.Distance(_currentDestination, transform.position) < Time.fixedDeltaTime) _isMove2Destination = true;

		//Move


		//Find Obstacle
		if (_isTurn2EmptySpace) return;
		if(Physics.Raycast(transform.position, transform.forward, out RaycastHit firstHit, _findableDistance, _obstacle))
		{
			for (int i = 0; i < 4; ++i)
			{

				float degValue = (90 * i); //0, 90, 180, 270
				float radValue = Mathf.Deg2Rad * degValue;

				Vector3 dirVec = new Vector3(Mathf.Cos(radValue), Mathf.Sin(radValue), 0);

				//Up, Right, Down, Left
				if (Physics.Raycast(transform.position, transform.rotation * dirVec, out RaycastHit lastHit, _findableDistance, _obstacle))
				{
					_isTurn2EmptySpace = true;
					break;
				}
			}
		}
	}
}
