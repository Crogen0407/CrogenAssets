using System;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public class AgentState
    {
        protected StateMachine _stateMachine;
        protected Agent _agentBase;

        protected bool _endTriggerCalled;
        protected int _animBoolHash;
        
        public AgentState(Agent agentBase, StateMachine stateMachine, string animBoolName)
        {
            _agentBase = agentBase;
            _stateMachine = stateMachine;
            _animBoolHash = Animator.StringToHash(animBoolName);
        }

        public virtual void Enter()
        {
            _endTriggerCalled = false;
            if (_agentBase.Animator == null) return;
            _agentBase.Animator.SetBool(_animBoolHash, true);
        }

        public virtual void Exit()
        {
            if (_agentBase.Animator == null) return;
            _agentBase.Animator.SetBool(_animBoolHash, false);
        }

        public virtual void UpdateState() { }
        
        public void AnimationFinishTrigger()
        {
            _endTriggerCalled = true;
        }
    }
}