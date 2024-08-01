using System;
using UnityEngine;

namespace Crogen.AgentFSM.Movement
{
	public interface IGroundMovement2D : IMovement
	{
		public bool IsGround { get; set; }
	}
}
