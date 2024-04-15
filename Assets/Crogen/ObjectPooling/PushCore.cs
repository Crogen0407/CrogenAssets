using System.Collections.Generic;
using UnityEngine;

namespace Crogen.ObjectPooling
{
    public static class PushCore
    {
        private static PoolManager _poolManager { get; set; }

        public static void Init(PoolManager poolManager)
        {
            _poolManager = poolManager;
        }
        
        public static void Push(this GameObject target, PoolType type)
        {
            target.transform.SetParent(_poolManager.transform);
            var trailRenderers = target.GetComponentsInChildren<TrailRenderer>();

            //Trail은 오브젝트를 끈 후에 무조건 Point들을 제거해야 함
            if (trailRenderers.Length != 0)
            {
                foreach (var trailRenderer in trailRenderers)
                {
                    trailRenderer.Clear();
                }
            }
        
            target.SetActive(false);
            PoolManager.poolDic[type.ToString()].Enqueue(target);
        }
        
        public static void Push(this GameObject target, string type)
        {
            target.transform.SetParent(_poolManager.transform);
            var trailRenderers = target.GetComponentsInChildren<TrailRenderer>();

            //Trail은 오브젝트를 끈 후에 무조건 Point들을 제거해야 함
            if (trailRenderers.Length != 0)
            {
                foreach (var trailRenderer in trailRenderers)
                {
                    trailRenderer.Clear();
                }
            }
        
            target.SetActive(false);
            PoolManager.poolDic[type].Enqueue(target);
        }
    }
}