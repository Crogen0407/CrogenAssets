using System;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PoolPair
{
    public string poolType;
    [FormerlySerializedAs("poolablePrefab")] public MonoPoolingObject monoPoolingObjectPrefab;
    public int poolCount;
}

[CreateAssetMenu (menuName = "Crogen/ObjectPooling/PoolBase")]
public class PoolBase : ScriptableObject
{
    public List<PoolPair> pairs;

    private void OnValidate()
    {
        PairInit();
    }

    public void PairInit()
    {
        if (pairs != null)
        {
            foreach (var pair in pairs)
            {
                if (pair.poolType.Equals(string.Empty) && pair.monoPoolingObjectPrefab != null)
                {
                    pair.poolType = pair.monoPoolingObjectPrefab.name;
                    break;
                }
            }
        }
    }
}
