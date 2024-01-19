using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Crogen.ObjectPooling
{
    public class PoolManager : MonoBehaviour
    {
        internal static Dictionary<string, Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
        public PoolBase poolBase;
        public List<PoolPair> poolingPairs;
        
        public void Awake()
        {
            MakeObj(); 
        }
        
        private void MakeObj()
        {
            PopCore.Init(poolBase);
            PoolPair[] poolingPairs = poolBase.pairs.ToArray();
            for (int i = 0; i < poolingPairs.Length; i++)
            {
                poolDic.Add(poolingPairs[i].prefabTypeName, new Queue<GameObject>());
            }

	    	for (int i = 0; i < poolingPairs.Length; i++)
	    	{
                for (int j = 0; j < poolingPairs[i].poolCount; j++)
                {
                    GameObject poolObject = CreateObject(poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                    poolObject.Push(poolingPairs[i].prefabTypeName);
	    		}
            }
        }

        public static GameObject CreateObject(PoolPair poolPair, Vector3 vec, Quaternion rot)
        {
            GameObject poolObject = Instantiate(poolPair.prefab);
            poolObject.transform.localPosition = vec;
            poolObject.transform.localRotation = rot;
            poolObject.name = poolObject.name.Replace("(Clone)","");

            return poolObject;
        }
    
    }
}
