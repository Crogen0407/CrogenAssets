using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public class StateMachine
    {
        public Agent _agentBase;
        public Dictionary<Enum, AgentState> StateDictionary = new Dictionary<Enum, AgentState>();
        public AgentState CurrentState { get; private set; }
        
        public virtual void Initialize(Agent agent)
        {
            _agentBase = agent;
            CurrentState = StateDictionary.First().Value;
            CurrentState?.Enter();
        }
        
        public virtual void ChangeState(Enum newState, bool forceMode = false)
        {
            if (_agentBase.CanStateChangeable == false && forceMode == false) return;
            if (_agentBase.isDead) return;

            if (StateDictionary.ContainsKey(newState) == false)
			{
                Debug.LogError("해당 Enum값에 지정된 State가 존재하지 않습니다. State 스크립트와 Enum을 확인해보세요.");
                return;
			}

            CurrentState.Exit();
            CurrentState = StateDictionary[newState];
            CurrentState.Enter();
        }

        public void AddState(Enum stateEnum, AgentState enemyState)
        {
            StateDictionary.Add(stateEnum, enemyState);
        }
    }
}