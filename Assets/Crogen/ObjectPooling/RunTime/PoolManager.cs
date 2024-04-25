using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Crogen.ObjectPooling
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;
        internal static Dictionary<string, Queue<MonoPoolingObject>> poolDic = new Dictionary<string, Queue<MonoPoolingObject>>();
        public PoolBase poolBase;
        public List<PoolPair> poolingPairs;

#if UNITY_EDITOR
        private void Reset()
        {
            transform.position = Vector3.zero;
        }  
#endif

        public void Awake()
        {
            Instance = this;
            PopCore.Init(poolBase, this);
            PushCore.Init(this);
            
            MakeObj();
        }
        
        private void MakeObj()
        {
            PoolPair[] poolingPairs = poolBase.pairs.ToArray();
            for (int i = 0; i < poolingPairs.Length; i++)
            {
                poolDic.Add(poolingPairs[i].poolType, new Queue<MonoPoolingObject>());
            }

	    	for (int i = 0; i < poolingPairs.Length; i++)
	    	{
                for (int j = 0; j < poolingPairs[i].poolCount; j++)
                {
                    MonoPoolingObject poolObject = CreateObject(poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                    poolObject.Push(poolingPairs[i].poolType, false);
	    		}
            }
        }

        public static MonoPoolingObject CreateObject(PoolPair poolPair, Vector3 vec, Quaternion rot)
        {
            MonoPoolingObject poolObject = Instantiate(poolPair.monoPoolingObjectPrefab);
            poolObject.transform.localPosition = vec;
            poolObject.transform.localRotation = rot;
            poolObject.name = poolObject.name.Replace("(Clone)","");

            return poolObject;
        }
    }
}
