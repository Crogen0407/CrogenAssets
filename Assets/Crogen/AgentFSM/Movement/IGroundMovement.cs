using System;
using UnityEngine;

namespace Crogen.AgentFSM.Movement 
{
	public interface IGroundMovement : IMovement
	{
		public bool IsGround { get; set; }
		public void OnJump();
	}
}
