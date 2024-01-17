using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolingPair
{
    public string prefabTypeName;
    public GameObject prefab;
    public int poolCount;
}

[CreateAssetMenu (menuName = "Crogen/ObjectPooling/PoolingBase")]
public class PoolingBase : ScriptableObject
{
    public List<PoolingPair> pairs;
}
