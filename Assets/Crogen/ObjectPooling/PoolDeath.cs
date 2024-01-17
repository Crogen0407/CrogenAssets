using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolDeath : MonoBehaviour
{
    [SerializeField] private string _prefabTypeName;
    [SerializeField] private float _duration = 1;
    [SerializeField] private UnityEvent _dieEvent;
    
    //Managements
    private PoolManager _poolManager;
    private void OnEnable()
    {
        if(_poolManager == null)
            _poolManager = PoolManager.Instance;
        StartCoroutine(EffectDeathCoroutine());
    }

    private IEnumerator EffectDeathCoroutine()
    {
        yield return new WaitForSeconds(_duration);
        _dieEvent?.Invoke();
        _poolManager.Push(_prefabTypeName, gameObject);
    }
}
