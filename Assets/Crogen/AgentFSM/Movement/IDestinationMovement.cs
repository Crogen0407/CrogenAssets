using System;
using UnityEngine;

namespace Crogen.AgentFSM.Movement
{
	public interface IDestinationMovement : IMovement 
	{
		public void SetDestination(Vector3 destination);
	}
}