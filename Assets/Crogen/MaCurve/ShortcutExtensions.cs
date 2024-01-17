using UnityEngine;

namespace Crogen.MaCurve
{
    public static class ShortcutExtensions
    {
        public static void DOMove(this Transform target, Vector3 endPoint, float duration, EasingType easing)
        {
                
        }
        public static void DOMove(this RectTransform rectTransform, Vector3 endPoint, float duration, EasingType easing)
        {
        }
        public static void DOMove(this Rigidbody rigidbody, Vector3 endPoint, float duration, EasingType easing)
        {
        }
    }
}