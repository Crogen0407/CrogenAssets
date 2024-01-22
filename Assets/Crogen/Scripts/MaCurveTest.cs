using System;
using Crogen.MaCurve;
using Crogen.ObjectPooling;
using UnityEngine;

public class MaCurveTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.MaCurve(transform.position + Vector3.back * 10, 2, EasingType.EaseOutBounce, true);
        }
    }
}
