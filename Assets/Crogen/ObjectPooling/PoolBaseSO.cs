using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PoolPair
{
    public string poolType;
    public MonoPoolingObject monoPoolingObjectPrefab;
    public int poolCount;
}

public class PoolBaseSO : ScriptableObject
{
    public List<PoolPair> pairs;

    private void OnValidate()
    {
        PairInit();
    }

    public void PairInit()
    {
        if (pairs == null) return;
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
