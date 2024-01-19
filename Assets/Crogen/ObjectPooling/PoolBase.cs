using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolPair
{
    public string prefabTypeName;
    public GameObject prefab;
    public int poolCount;
}

[CreateAssetMenu (menuName = "Crogen/ObjectPooling/PoolBase")]
public class PoolBase : ScriptableObject
{
    public List<PoolPair> pairs;
}
