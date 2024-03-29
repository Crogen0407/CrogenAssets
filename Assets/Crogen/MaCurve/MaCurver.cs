﻿using UnityEngine;

namespace Crogen.MaCurve
{
    public static class MaCurver
    {
        public static MaCurveForTransform MaCurve(this Transform target, Vector3 endPoint, float duration, EasingType easing, bool forcedinitializable = false)
        {
            return new MaCurveForTransform(target, endPoint, MaCurveManager.CurrentRealTime, duration, easing, forcedinitializable);
        }
    }
}