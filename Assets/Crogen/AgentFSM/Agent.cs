using System;
using System.Collections;
using UnityEngine;

namespace Crogen.AgentFSM
{
    public abstract class Agent<T> : MonoBehaviour where T : Enum
    {
        public StateMachine<T> StateMachine { get; private set; }
        public IMovement<T> Movement { get; protected set; }
        //public Animator Animator { get; private set; }
        public bool CanStateChangeable { get; protected set; } = true;
        public bool isDead;
        
        protected virtual void Awake()
        {
            Transform visualTrm = transform.Find("Visual");
            //Animator = visualTrm.GetComponent<Animator>();
        
            StateMachine = new StateMachine<T>();
            string name = GetType().Name;
            foreach(T stateEnum in Enum.GetValues(typeof(T)))
            {
                string typeName = stateEnum.ToString();
                try
                {
                    Type t = Type.GetType($"{name}{typeName}State");
                    AgentState<T> playerState = Activator.CreateInstance(
                        t, this, StateMachine, typeName) as AgentState<T>;
                    StateMachine.AddState(stateEnum, playerState);
                }catch(Exception ex)
                {
                    Debug.LogError($"{typeName} is loading error, check Message {ex.Message}");
                }
            }
        }

        protected virtual void Start()
        {
            StateMachine.Initialize(this);
        }

        protected virtual void Update()
        {
            StateMachine.CurrentState?.UpdateState();
        }

        #region Delay Callback coroutine 
        public Coroutine StartDelayCallback(float delayTime, Action callback)
        {
            return StartCoroutine(DelayCoroutine(delayTime, callback));
        }
        private IEnumerator DelayCoroutine(float delayTime, Action callback)
        {
            yield return new WaitForSeconds(delayTime);
            callback?.Invoke();
        }
        #endregion
        
        public abstract void SetDead();
    }
}