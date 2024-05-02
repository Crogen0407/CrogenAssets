using System;
using System.Numerics;

namespace Crogen.AgentFSM
{
    public interface IMovement<T> where T : Enum
    {
        public Vector3 Velocity { get; }
        public bool IsGround { get; }
        public void Initialize(Agent<T> agent);
        public void SetMovement(Vector3 movement, bool isRotation = true);
        public void StopImmediately();
        public void SetDestination(Vector3 destination);
        public void GetKnockback(Vector3 force);
    }
}