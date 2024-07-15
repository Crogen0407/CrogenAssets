using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crogen.CrogenPooling;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PoolType cubePoolType;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
		{
            gameObject.Pop(cubePoolType, new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f),
                Random.Range(-10f, 10f)), Quaternion.identity);
		}
    }
}
