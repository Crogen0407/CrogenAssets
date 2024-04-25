using UnityEngine;

namespace Crogen.ObjectPooling
{
    public abstract class MonoPoolingObject : MonoBehaviour
    {
        protected MonoPoolingObject poolObject => this;
        public abstract void OnPop();
        public abstract void OnPush();

        private PoolManager _poolManager = PoolManager.Instance;
        
        public void Push(PoolType type, bool useEvent = true)
        {
            transform.SetParent(_poolManager.transform);

            //Trail은 오브젝트를 끈 후에 무조건 Point들을 제거해야 함
            if (transform.childCount > 0)
            {
                var trailRenderers = GetComponentsInChildren<TrailRenderer>();

                //Trail은 오브젝트를 끈 후에 무조건 Point들을 제거해야 함
                if (trailRenderers.Length != 0)
                {
                    foreach (var trailRenderer in trailRenderers)
                    {
                        trailRenderer.Clear();
                    }
                }
            }
        
            gameObject.SetActive(false);
            if(useEvent)
                OnPush();
            PoolManager.poolDic[type.ToString()].Enqueue(this);
        }
    }
}