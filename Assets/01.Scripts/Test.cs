using System.Collections;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.Pop(PoolType.pf_Cube, new Vector3(
                Random.Range(-10, 10),
                Random.Range(-10, 10),
                Random.Range(-10, 10)), Quaternion.identity);
        }
    }
}
