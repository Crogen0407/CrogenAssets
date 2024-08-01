using System;
using UnityEngine;

namespace Crogen.AgentFSM.Movement
{
    public interface IMovement
    {
        public Vector3 Velocity { get; set; }
        public Agent AgentBase { get; set; }
        public void Initialize(Agent agent);
        public void SetMovement(Vector3 movement, bool isRotation = true);
        public void StopImmediately();
        public void GetKnockback(Vector3 force);
    }
}