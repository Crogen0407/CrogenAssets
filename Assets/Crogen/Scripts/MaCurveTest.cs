using System;
using Crogen.MaCurve;
using Crogen.MaCurve.Sequence;
using Crogen.ObjectPooling;
using UnityEngine;

public class MaCurveTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.MaCurve( Vector3.back * 10, 2, EasingType.EaseOutBounce);
            
            transform.MaCurve( Vector3.back * 10, 2, EasingType.EaseOutBounce);
        }
    }
}
