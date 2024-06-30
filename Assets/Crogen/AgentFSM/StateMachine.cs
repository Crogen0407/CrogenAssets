using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public class StateMachine<T> where T : Enum
    {
        public Agent<T> _agentBase;
        public Dictionary<T, AgentState<T>> StateDictionary = new Dictionary<T, AgentState<T>>();
        public AgentState<T> CurrentState { get; private set; }
        
        public virtual void Initialize(Agent<T> agent)
        {
            _agentBase = agent;
            CurrentState = StateDictionary.First().Value;
            CurrentState?.Enter();
        }
        
        public virtual void ChangeState(T newState, bool forceMode = false)
        {
            if (_agentBase.CanStateChangeable == false && forceMode == false) return;
            if (_agentBase.isDead) return;

            CurrentState.Exit();
            CurrentState = StateDictionary[newState];
            CurrentState.Enter();
        }

        public void AddState(T stateEnum, AgentState<T> enemyState)
        {
            StateDictionary.Add(stateEnum, enemyState);
        }
    }
}