using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Crogen.ObjectPooling
{
    public class PoolDeath : MonoBehaviour
    {
        [SerializeField] private string _prefabTypeName;
        [SerializeField] private float _duration = 1;
        [SerializeField] private UnityEvent _dieEvent;
    
        private void OnEnable()
        {
            StartCoroutine(EffectDeathCoroutine());
        }

        private IEnumerator EffectDeathCoroutine()
        {
            yield return new WaitForSeconds(_duration);
            _dieEvent?.Invoke();
            gameObject.Push(_prefabTypeName);
        }
    }
}

