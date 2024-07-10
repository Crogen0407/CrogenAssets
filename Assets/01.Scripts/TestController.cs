using UnityEngine;
using Crogen.ObjectPooling;

public class TestController : MonoBehaviour
{
    private void Update()
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