using System;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public abstract class AgentPattern : MonoBehaviour
    {
        private object _agentBase;

        public void Initialized(Agent agentbase)
		{
            _agentBase = agentbase;
        }
    }
}