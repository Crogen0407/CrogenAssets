using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.ObjectPooling
{
    public static class PopCore
    {
        private static PoolBase _poolBase;
        
        public static void Init(PoolBase poolBase)
        {
            _poolBase = poolBase;
        }
        
        public static GameObject Pop(this Transform parentTrm, string type, Vector3 vec, Quaternion rot, bool useParentSpacePosition = true, bool useParentSpaceRotation = true)
        {
            try
            {
                if (PoolManager.poolDic[type].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].prefabTypeName == type)
                        {
                            GameObject poolObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                            poolObject.Push(type);
                            break;
                        }
                    }
                }
                GameObject obj = PoolManager.poolDic[type].Dequeue();
            
                obj.SetActive(true);
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
        
        public static GameObject Pop(this GameObject targetGameObject, string type, bool followTargetObjectRotation = false)
        {
            try
            {
                if (PoolManager.poolDic[type].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].prefabTypeName == type)
                        {
                            GameObject poolObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero, Quaternion.identity);
                            poolObject.Push(type);
                            break;
                        }
                    }
                }
                GameObject obj = PoolManager.poolDic[type].Dequeue();
            
                obj.SetActive(true);
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
                Debug.LogError("You should make 'PoolManager'!");
                throw;
            }
        }

        public static GameObject Pop(this GameObject targetGameObject, string type, Vector3 vec, Quaternion rot,
            bool useParentSpacePosition = false, bool useParentSpaceRotation = false)
        {

            try
            {
                if (PoolManager.poolDic[type].Count == 0)
                {
                    for (int i = 0; i < _poolBase.pairs.Count; i++)
                    {
                        if (_poolBase.pairs[i].prefabTypeName == type)
                        {
                            GameObject poolObject = PoolManager.CreateObject(_poolBase.pairs[i], Vector3.zero,
                                Quaternion.identity);
                            poolObject.Push(type);
                            break;
                        }
                    }
                }

                GameObject obj = PoolManager.poolDic[type].Dequeue();

                obj.SetActive(true);

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