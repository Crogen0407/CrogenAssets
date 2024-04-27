using System;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public class Agent : MonoBehaviour
    {
        public StateMachine<AgentStateEnum> StateMachine { get; private set; }
        public IMovement Movement { get; protected set; }
        public Animator Animator { get; private set; }
        public bool CanStateChangeable { get; protected set; } = true;
        public bool isDead;
    
        protected virtual void Awake()
        {
            Transform visualTrm = transform.Find("Visual");
            Animator = visualTrm.GetComponent<Animator>();
        
            StateMachine = new StateMachine<AgentStateEnum>();
        }

        protected void Start()
        {
            StateMachine.Initialize(this);
        }
    }
}