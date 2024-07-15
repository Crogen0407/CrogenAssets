using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenPooling
{
    public static class PopCore
    {
        private static PoolManager _poolManager { get; set; }
        private static PoolBaseSO _poolBase;
        
        public static void Init(PoolManager poolManager, PoolBaseSO poolBaseSo)
        {
            _poolManager = poolManager;
            _poolBase = poolBaseSo;
        }

        public static IPoolingObject Pop(this GameObject target, PoolType type, Transform parent)
        {

            try
            {
                IPoolingObject poolingObject;

                if (PoolManager.poolDic[type].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].poolType.Equals(type.ToString()))
                        {
                            poolingObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                            PoolManager.PoolingObjectInit(poolingObject, type, PoolManager.Transform);
                            break;
                        }
                    }
                }
                poolingObject = PoolManager.poolDic[type].Pop();
                GameObject obj = poolingObject.gameObject;

                obj.SetActive(true);

                obj.transform.SetParent(parent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                poolingObject.OnPop();

                return poolingObject;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
        }

        public static IPoolingObject Pop(this GameObject target, PoolType type, Vector3 pos, Quaternion rot)
        {

            try
            {
                IPoolingObject poolingObject;

                if (PoolManager.poolDic[type].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].poolType.Equals(type.ToString()))
                        {
                            poolingObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                            PoolManager.PoolingObjectInit(poolingObject, type, PoolManager.Transform);
                            break;
                        }
                    }
                }
                poolingObject = PoolManager.poolDic[type].Pop();
                GameObject obj = poolingObject.gameObject;

                obj.SetActive(true);

                obj.transform.SetParent(null);
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                poolingObject.OnPop();

                return poolingObject;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
        }
    }
}