// using System;
// using System.Collections;
// using System.Reflection;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Crogen.MaCurve
// {
//     public abstract class MaCurve
//     {
//         public float startTime;
//         public float endTime;
//         
//         public float duration;
//         public float currentTime;
//
//         
//         private static IEnumerator Move(Transform transform, Vector3 endPoint, float duration, EasingType easing, IEnumerator lateCoroutine = null)
//         {
//             float currentTime = 0;
//             float percentTime = 0;
//             
//             Vector3 startPoint = transform.position;
//             
//             while (currentTime < duration)
//             {
//                 currentTime += Time.deltaTime;
//                 percentTime = currentTime / duration;
//                 transform.localPosition = Vector3.Lerp(startPoint, endPoint, new EaseCollection().SetEase(easing, percentTime));
//                 yield return null;
//             }
//             transform.localPosition = endPoint;
//             yield return new WaitForSeconds(duration);
//         
//             if (lateCoroutine != null)
//             {
//                 yield return StartCoroutine(lateCoroutine);
//             }
//         }
//         private static IEnumerator Move(RectTransform rectTransform, Vector3 endPoint, float duration, EasingType easing, IEnumerator lateCoroutine = null, Action lateAction = null)
//         {
//             float currentTime = 0;
//             float percentTime = 0;
//             
//             Vector3 startPoint = rectTransform.anchoredPosition;
//             
//             while (currentTime < duration)
//             {
//                 currentTime += Time.deltaTime;
//                 percentTime = currentTime / duration;
//                 rectTransform.anchoredPosition = Vector2.Lerp(startPoint, endPoint, new EaseCollection().SetEase(easing, percentTime));
//                 yield return null;
//             }
//             rectTransform.anchoredPosition = endPoint;
//             yield return new WaitForSeconds(duration);
//             lateAction?.Invoke();
//         
//             if (lateCoroutine != null)
//             {
//                 yield return StartCoroutine(lateCoroutine);
//             }
//         }
//         
//         private static IEnumerator Move(Rigidbody rigidbody, Vector3 endPoint, float duration, EasingType easing, IEnumerator lateCoroutine = null)
//         {
//             float currentTime = 0;
//             float percentTime = 0;
//             
//             Vector3 startPoint = rigidbody.position;
//             
//             while (currentTime < duration)
//             {
//                 currentTime += Time.deltaTime;
//                 percentTime = currentTime / duration;
//                 transform.localPosition = Vector3.Lerp(startPoint, endPoint, new EaseCollection().SetEase(easing, percentTime));
//                 yield return null;
//             }
//             rigidbody.position = endPoint;
//             yield return new WaitForSeconds(duration);
//             if (lateCoroutine != null)
//             {
//                 yield return StartCoroutine(lateCoroutine);
//             }
//         }
//
//         //for Renderers
//         public static IEnumerator DOColor(Renderer target, Color endPoint, float duration, EasingType easing)
//         {
//             float currentTime = 0;
//             float percentTime = 0;
//                 
//             Color startPoint = target.material.color;
//                 
//             while (currentTime < duration)
//             {
//                 currentTime += Time.deltaTime;
//                 percentTime = currentTime / duration;
//                 target.material.color = Color.Lerp(startPoint, endPoint, new EaseCollection().SetEase(easing, percentTime));
//                 yield return null;
//             }        
//             target.material.color = endPoint;
//         }
//
//         public static void DOColor(Image image, Color endPoint, float duration, EasingType easing)
//         {
//             StartCoroutine(ChangeColor(image, endPoint, duration, easing));
//         }
//         
//         private static IEnumerator ChangeColor(Image image, Color endPoint, float duration, EasingType easing)
//         {
//             float currentTime = 0;
//             float percentTime = 0;
//
//             Color startColor = image.color;
//             
//             while (currentTime < duration)
//             {
//                 currentTime += Time.deltaTime;
//                 percentTime = currentTime / duration;
//                 image.color = Color.Lerp(startColor, endPoint, new EaseCollection().SetEase(easing, percentTime));
//                 yield return null;
//             }        
//             image.color = endPoint;
//         }
//         
//         public static void DOColor(Image image, Color endPoint, float duration, IEnumerator lateCoroutine, EasingType easing)
//         {
//             StartCoroutine(ChangeColor(image, endPoint, duration, lateCoroutine, easing));
//         }
//         
//         private static IEnumerator ChangeColor(Image image, Color endPoint, float duration, IEnumerator lateCoroutine, EasingType easing)
//         {
//             float currentTime = 0;
//             float percentTime = 0;
//
//             Color startColor = image.color;
//             
//             while (currentTime < duration)
//             {
//                 currentTime += Time.deltaTime;
//                 percentTime = currentTime / duration;
//                 image.color = Color.Lerp(startColor, endPoint, new EaseCollection().SetEase(easing, percentTime));
//                 yield return null;
//             }        
//             image.color = endPoint;
//
//             yield return new WaitForSeconds(duration);
//             yield return StartCoroutine(lateCoroutine);
//         }
//     }
// }