using Crogen.HealthSystem;
using UnityEngine;
using UnityEngine.Events;

public class DefaultHealth : Health
{
    [Header("Events")] 
    public UnityEvent hpChangeEvent;
    public UnityEvent hpUpEvent;
    public UnityEvent hpDownEvent;
    public UnityEvent dieEvent;
    
    protected override void OnHpChange() => hpChangeEvent?.Invoke();   
    protected override void OnHpUp() => hpUpEvent?.Invoke();
    protected override void OnHpDown() => hpDownEvent?.Invoke();
    protected override void OnDie() => dieEvent?.Invoke();
}
