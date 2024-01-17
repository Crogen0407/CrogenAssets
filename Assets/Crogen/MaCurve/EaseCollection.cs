using System;
using UnityEngine;

namespace Crogen.MaCurve
{
    public class EaseCollection
    {
        public float SetEase(EasingType easingType, float x)
        {
            switch (easingType)
            {
                // Sine
                case EasingType.EaseInSine:
                    return 1 - Mathf.Cos((x * Mathf.PI) / 2);
                case EasingType.EaseOutSine:
                    return Mathf.Sin((x * Mathf.PI) / 2);
                case EasingType.EaseInOutSine:
                    return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
                
                // Cubic
                case EasingType.EaseInCubic:
                    return x * x * x;
                case EasingType.EaseOutCubic:
                    return 1 - Mathf.Pow(1 - x, 3);
                case EasingType.EaseInOutCubic:
                    return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
                
                // Quint
                case EasingType.EaseInQuint:
                    return x * x * x * x * x;
                case EasingType.EaseOutQuint:
                    return 1 - Mathf.Pow(1 - x, 5);
                case EasingType.EaseInOutQuint:
                    return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
                
                // Circ
                case EasingType.EaseInCirc:
                    return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
                case EasingType.EaseOutCirc:
                    return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
                case EasingType.EaseInOutCirc:
                    return x < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
                
                // Elastic
                case EasingType.EaseInElastic:
                {
                    float c4 = (2 * Mathf.PI) / 3;
                    return x == 0 ? 0 : x == 1 ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
                }
                case EasingType.EaseOutElastic:
                {
                    float c4 = (2 * Mathf.PI) / 3;
                    return x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;                
                }
                case EasingType.EaseInOutElastic:
                {
                    float c5 = (2 * Mathf.PI) / 4.5f;
                    return x == 0 ? 0 : x == 1 ? 1 : x < 0.5f ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
                }
                    
                // Quad
                case EasingType.EaseInQuad:
                    return x * x;
                case EasingType.EaseOutQuad:
                    return 1 - (1 - x) * (1 - x);
                case EasingType.EaseInOutQuad:
                    return x < 0.5f ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
                
                // Quart
                case EasingType.EaseInQuart:
                    return x * x * x * x;
                case EasingType.EaseOutQuart:
                    return 1 - Mathf.Pow(1 - x, 4);
                case EasingType.EaseInOutQuart:
                    return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
                
                // Expo
                case EasingType.EaseInExpo:
                    return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
                case EasingType.EaseOutExpo:
                    return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
                case EasingType.EaseInOutExpo:
                    return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
                
                // Back
                case EasingType.EaseInBack:
                {
                    float c1 = 1.70158f;
                    float c3 = c1 + 1;

                    return c3 * x * x * x - c1 * x * x;
                }
                case EasingType.EaseOutBack:
                {
                    float c1 = 1.70158f;
                    float c3 = c1 + 1;

                    return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
                }
                case EasingType.EaseInOutBack:
                {
                    float c1 = 1.70158f;
                    float c2 = c1 * 1.525f;

                    return x < 0.5 ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2 : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
                }                
                
                // Bounce
                case EasingType.EaseInBounce:
                    return 1 - EaseOutBounce(1 - x);
                case EasingType.EaseOutBounce:
                    return EaseOutBounce(x);
                case EasingType.EaseInOutBounce:
                    return x < 0.5 ? (1 - EaseOutBounce(1 - 2 * x)) / 2 : (1 + EaseOutBounce(2 * x - 1)) / 2;
            }

            return 0;
        }
        
        private float EaseOutBounce(float x)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;

            if (x < 1 / d1) {
                return n1 * x * x;
            } else if (x < 2 / d1) {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            } else if (x < 2.5f / d1) {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            } else {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }
    }
}