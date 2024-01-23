using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crogen.MaCurve
{
    public class MaCurveForTransform : MaCurve<Transform, Vector3>
    {
        private EaseCollection _easeCollection;
        
        public MaCurveForTransform(Transform target, Vector3 endPosition, float startTime, float duration, EasingType easingType, bool forcedinitializable)
        {
            Init(target, startTime, duration, easingType, forcedinitializable);

            this.startPoint = target.position;
            this.endPoint = endPosition;
            
            Debug.Log(MaCurveManager.activeMaCurves.Count);
            OnDie = () =>
            {
                currentTime = endTime;
                this.target.position = endPoint;
            };
            IsActive = true;
            _easeCollection = new EaseCollection();
        }
        
        public override void Update()
        {
            if (IsActive == true)
            {
                currentTime = MaCurveManager.CurrentRealTime;

                float percent = (currentTime - startTime) / duration;
                target.position = Vector3.Lerp(startPoint, endPoint, _easeCollection.SetEase(easingType, percent));
    
                if (currentTime > endTime)
                {
                    IsActive = false;
                }
            }
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