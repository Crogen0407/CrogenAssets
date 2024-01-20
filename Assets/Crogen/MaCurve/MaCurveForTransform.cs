using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crogen.MaCurve
{
    public class MaCurveForTransform : MaCurve<Transform, Vector3>
    {
        //Position
        public MaCurveForTransform(Transform target, Vector3 endPoint, float startTime, float duration)
        {
            this.target = target;
            this.startPoint = target.position;
            this.endPoint = endPoint;
            
            this.startTime = startTime;
            this.endTime = startTime + duration;
            this.duration = duration;
            this.currentTime = startTime;
            
            MaCurveManager.MaCurveEvent += Move;
        }
        
        public override void Move()
        {
            currentTime = MaCurveManager.CurrentRealTime;

            float percent = (currentTime - startTime) / duration;
            target.position = Vector3.Lerp(startPoint, endPoint, percent);
            
            Debug.Log(currentTime - startTime);
            
            if (currentTime > endTime) MaCurveManager.MaCurveEvent -= Move;
        }
        
        //Rotation
        
        //Scale
        
        
        // private static IEnumerator Move(RectTransform rectTransform, Vector3 endPoint, float duration, EasingType easing, IEnumerator lateCoroutine = null, Action lateAction = null)
        // {
        //     float currentTime = 0;
        //     float percentTime = 0;
        //     
        //     Vector3 startPoint = rectTransform.anchoredPosition;
        //     
        //     while (currentTime < duration)
        //     {
        //         currentTime += Time.deltaTime;
        //         percentTime = currentTime / duration;
        //         rectTransform.anchoredPosition = Vector2.Lerp(startPoint, endPoint, new EaseCollection().SetEase(easing, percentTime));
        //         yield return null;
        //     }
        //     rectTransform.anchoredPosition = endPoint;
        //     yield return new WaitForSeconds(duration);
        //     lateAction?.Invoke();
        //
        //     if (lateCoroutine != null)
        //     {
        //         yield return StartCoroutine(lateCoroutine);
        //     }
        // }
        

        // //for Renderers
        // public static IEnumerator DOColor(Renderer target, Color endPoint, float duration, EasingType easing)
        // {
        //     float currentTime = 0;
        //     float percentTime = 0;
        //         
        //     Color startPoint = target.material.color;
        //         
        //     while (currentTime < duration)
        //     {
        //         currentTime += Time.deltaTime;
        //         percentTime = currentTime / duration;
        //         target.material.color = Color.Lerp(startPoint, endPoint, new EaseCollection().SetEase(easing, percentTime));
        //         yield return null;
        //     }        
        //     target.material.color = endPoint;
        // }
        //
        // public static void DOColor(Image image, Color endPoint, float duration, EasingType easing)
        // {
        //     StartCoroutine(ChangeColor(image, endPoint, duration, easing));
        // }
        //
        // private static IEnumerator ChangeColor(Image image, Color endPoint, float duration, EasingType easing)
        // {
        //     float currentTime = 0;
        //     float percentTime = 0;
        //
        //     Color startColor = image.color;
        //     
        //     while (currentTime < duration)
        //     {
        //         currentTime += Time.deltaTime;
        //         percentTime = currentTime / duration;
        //         image.color = Color.Lerp(startColor, endPoint, new EaseCollection().SetEase(easing, percentTime));
        //         yield return null;
        //     }        
        //     image.color = endPoint;
        // }
        //
        // public static void DOColor(Image image, Color endPoint, float duration, IEnumerator lateCoroutine, EasingType easing)
        // {
        //     StartCoroutine(ChangeColor(image, endPoint, duration, lateCoroutine, easing));
        // }
        //
        // private static IEnumerator ChangeColor(Image image, Color endPoint, float duration, IEnumerator lateCoroutine, EasingType easing)
        // {
        //     float currentTime = 0;
        //     float percentTime = 0;
        //
        //     Color startColor = image.color;
        //     
        //     while (currentTime < duration)
        //     {
        //         currentTime += Time.deltaTime;
        //         percentTime = currentTime / duration;
        //         image.color = Color.Lerp(startColor, endPoint, new EaseCollection().SetEase(easing, percentTime));
        //         yield return null;
        //     }        
        //     image.color = endPoint;
        //
        //     yield return new WaitForSeconds(duration);
        //     yield return StartCoroutine(lateCoroutine);
        // }
    }
}