using System;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public class AgentState<T> where T : Enum
    {
        protected StateMachine<T> _stateMachine;
        protected Agent<T> _agentBase;

        protected bool _endTriggerCalled;
        protected int _animBoolHash;
        
        public AgentState(Agent<T> agentBase, StateMachine<T> stateMachine, string animBoolName)
        {
            _agentBase = agentBase;
            _stateMachine = stateMachine;
            _animBoolHash = Animator.StringToHash(animBoolName);
        }

        public virtual void Enter()
        {
            _endTriggerCalled = false;
            _agentBase.Animator.SetBool(_animBoolHash, true);
        }

        public virtual void Exit()
        {
            _agentBase.Animator.SetBool(_animBoolHash, false);
        }

        public virtual void UpdateState() { }
        
        public void AnimationFinishTrigger()
        {
            _endTriggerCalled = true;
        }
    }
}