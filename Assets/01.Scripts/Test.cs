using System;
using System.Collections;
using Crogen.ObjectPooling;
using UnityEngine;

public class Test : MonoPoolingObject
{
    void Update()
    {
        StartCoroutine(PushCoroutine());
    }

    private void OnEnable()
    {
        Debug.Log("죽었따");
    }

    IEnumerator PushCoroutine()
    {
        yield return new WaitForSeconds(1);
    }

    public override void OnPop()
    {
        
    }

    public override void OnPush()
    {
        
    }
}
