using Crogen.ObjectPooling;
using UnityEngine.Events;

public class PoolableObject : MonoPoolingObject
{
    public UnityEvent PopEvent;
    public UnityEvent PushEvent;
    
    public override void OnPop()
    {
        PopEvent?.Invoke();
        
    }

    public override void OnPush()
    {
        PushEvent?.Invoke();
    }
}
