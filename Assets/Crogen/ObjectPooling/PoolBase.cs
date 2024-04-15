using System;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

[System.Serializable]
public class PoolPair
{
    public string poolType;
    public GameObject prefab;
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
                if (pair.poolType.Equals(string.Empty) && pair.prefab != null)
                {
                    pair.poolType = pair.prefab.name;
                    break;
                }
            }
        }
    }
}
