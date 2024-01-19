using System.Collections.Generic;
using UnityEngine;

namespace Crogen.ObjectPooling
{
    public static class PushCore
    {
        public static void Push(this GameObject target, string type)
        {
            target.transform.SetParent(null);
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