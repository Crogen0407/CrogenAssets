using System;
using System.Collections;
using UnityEngine;
using Crogen.AgentFSM.Movement;
using System.Collections.Generic;

namespace Crogen.AgentFSM
{
    public abstract class Agent : MonoBehaviour
    {
        public StateMachine StateMachine { get; private set; }
        public IMovement Movement { get; private set; }
        public Animator Animator { get; private set; }
        public bool CanStateChangeable { get; protected set; } = true;
        public bool isDead;
        
        protected void Initialize<T>() where T : Enum
        {
            Transform visualTrm = transform.Find("Visual");
            Animator = visualTrm.GetComponent<Animator>();
            Movement = GetComponent<IMovement>();
            StateMachine = new StateMachine();
            string name = GetType().Name;
            foreach(Enum stateEnum in Enum.GetValues(typeof(T)))
            {
                string typeName = stateEnum.ToString();
                try
                {
                    Type t = Type.GetType($"{name}{typeName}State");
                    AgentState playerState = Activator.CreateInstance(
                        t, this, StateMachine, typeName) as AgentState;
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

#if UNITY_EDITOR
		private void OnValidate()
		{
			if (transform.Find("Visual") == null)
			{
                GameObject visualObject =  new GameObject("Visual");
                visualObject.transform.SetParent(transform);
                visualObject.transform.localPosition = Vector3.zero;
			}
		}
#endif
	}
}