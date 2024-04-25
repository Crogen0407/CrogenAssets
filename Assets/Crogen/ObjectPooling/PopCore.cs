using System.Collections.Generic;
using UnityEngine;

namespace Crogen.ObjectPooling
{
    public static class PopCore
    {
        private static PoolManager _poolManager { get; set; }
        private static PoolBase _poolBase;
        
        public static void Init(PoolBase poolBase, PoolManager poolManager)
        {
            _poolManager = poolManager;
            _poolBase = poolBase;
        }
        
        public static MonoPoolingObject Pop(this Transform parentTrm, string type, Vector3 vec, Quaternion rot, bool useParentSpacePosition = true, bool useParentSpaceRotation = true)
        {
            try
            {
                if (PoolManager.poolDic[type].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].poolType == type)
                        {
                            MonoPoolingObject poolObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                            poolObject.Push(type, false);
                            break;
                        }
                    }
                }
                MonoPoolingObject obj = PoolManager.poolDic[type].Dequeue();
            
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(parentTrm);
            
                if (useParentSpacePosition) obj.transform.localPosition = vec; else obj.transform.position = vec; 
            
                if(useParentSpaceRotation) obj.transform.localRotation = rot; else obj.transform.rotation = rot;
            
                return obj;
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
            
            
        }
        
        public static MonoPoolingObject Pop(this MonoBehaviour targetGameObject, PoolType type, bool followTargetObjectRotation = false, bool useEvent = true)
        {
            try
            {
                if (PoolManager.poolDic[type.ToString()].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].poolType.Equals(type.ToString()))
                        {
                            MonoPoolingObject poolObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                            poolObject.Push(type.ToString(), false);
                            break;
                        }
                    }
                }
                MonoPoolingObject obj = PoolManager.poolDic[type.ToString()].Dequeue();
                obj.gameObject.SetActive(true);
                if(useEvent)
                    obj.OnPop();

                obj.transform.SetParent(_poolManager.transform);
                obj.transform.position = targetGameObject.transform.position;
                if (followTargetObjectRotation)
                {
                    obj.transform.rotation = targetGameObject.transform.rotation;
                }
                else
                {
                    obj.transform.rotation = Quaternion.identity;
                }
                return obj;
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError($"You should make 'PoolManager'!");
                throw;
            }
        }

        public static MonoPoolingObject Pop(this MonoBehaviour targetGameObject, PoolType type, Vector3 vec, Quaternion rot,
            bool useParentSpacePosition = false, bool useParentSpaceRotation = false, bool useEvent = true)
        {

            try
            {
                if (PoolManager.poolDic[type.ToString()].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].poolType.Equals(type.ToString()))
                        {
                            MonoPoolingObject poolObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero,
                                Quaternion.identity);
                            poolObject.Push(type.ToString(), false);
                            break;
                        }
                    }
                }

                MonoPoolingObject obj = PoolManager.poolDic[type.ToString()].Dequeue();
                obj.gameObject.SetActive(true);
                if(useEvent)
                    obj.OnPop();
                obj.transform.SetParent(_poolManager.transform);
                if (useParentSpacePosition)
                    obj.transform.position = targetGameObject.transform.position + vec;
                else
                    obj.transform.position = vec;

                if (useParentSpaceRotation)
                    obj.transform.eulerAngles = targetGameObject.transform.eulerAngles + rot.eulerAngles;
                else
                    obj.transform.rotation = rot;


                return obj;
            }
            catch (KeyNotFoundException e)
            {
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
        }
    }
}