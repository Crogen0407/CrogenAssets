using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
    [FormerlySerializedAs("poolingBase")] public PoolingBase PoolingBase;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        MakeObj();
    }

    void MakeObj()
    {
        PoolingPair[] poolingPairs = PoolingBase.pairs.ToArray();
        for (int i = 0; i < poolingPairs.Length; i++)
        {
            poolDic.Add(poolingPairs[i].prefabTypeName, new Queue<GameObject>());
        }
        
		for (int i = 0; i < poolingPairs.Length; i++)
		{
            for (int j = 0; j < poolingPairs[i].poolCount; j++)
			{
                GameObject poolObject = Instantiate(poolingPairs[i].prefab, Vector3.zero, Quaternion.identity);
                poolObject.name = poolObject.name.Replace("(Clone)","");
                Push(poolingPairs[i].prefabTypeName, poolObject);
			}
        }
    }

    public GameObject Pop(string type, Vector3 vec, Quaternion rot)
    {
        if (poolDic[type].Count == 0)
        {
            for (int i = 0; i < PoolingBase.pairs.Count; i++)
            {
                if (PoolingBase.pairs[i].prefabTypeName == type)
                {
                    GameObject poolObject = Instantiate(PoolingBase.pairs[i].prefab, Vector3.zero, Quaternion.identity);
                    poolObject.name = poolObject.name.Replace("(Clone)","");
                    Push(type, poolObject);
                    break;
                }
            }
        }
        GameObject obj = poolDic[type].Dequeue();

        obj.SetActive(true);
        obj.transform.position = vec;
        obj.transform.rotation = rot;

        return obj;
    }
    public GameObject Pop(string type, Transform parentTrm)
    {

        if (poolDic[type].Count == 0)
        {
            for (int i = 0; i < PoolingBase.pairs.Count; i++)
            {
                if (PoolingBase.pairs[i].prefabTypeName == type)
                {
                    GameObject poolObject = Instantiate(PoolingBase.pairs[i].prefab);
                    poolObject.transform.localPosition = Vector3.zero;
                    poolObject.transform.localRotation = Quaternion.identity;
                    poolObject.name = poolObject.name.Replace("(Clone)","");
                    Push(type, poolObject);
                    break;
                }
            }
        }
        GameObject obj = poolDic[type].Dequeue();

        obj.SetActive(true);
        obj.transform.SetParent(parentTrm);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        return obj;
    }
    public GameObject Pop(string type, Transform parentTrm, Vector3 vec, Quaternion rot)
    {

        if (poolDic[type].Count == 0)
        {
            for (int i = 0; i < PoolingBase.pairs.Count; i++)
            {
                if (PoolingBase.pairs[i].prefabTypeName == type)
                {
                    GameObject poolObject = Instantiate(PoolingBase.pairs[i].prefab);
                    poolObject.transform.localPosition = vec;
                    poolObject.transform.localRotation = rot;
                    poolObject.name = poolObject.name.Replace("(Clone)","");
                    Push(type, poolObject);
                    break;
                }
            }
        }
        GameObject obj = poolDic[type].Dequeue();
        
        obj.SetActive(true);
        obj.transform.SetParent(parentTrm);
        obj.transform.localPosition = vec;
        obj.transform.localRotation = rot;
        return obj;
    }
    public void Push(string type, GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
        gameObject.SetActive(false);
        poolDic[type].Enqueue(gameObject);
    }
}