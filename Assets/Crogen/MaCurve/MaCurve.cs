using System;
using UnityEngine;

namespace Crogen.MaCurve
{
    public abstract class MaCurve<T0, T1> : MaCurveCore
        where T0 : Component 
        where T1 : struct
    {
        internal T0 target;
        internal T1 startPoint;
        internal T1 endPoint;
        
        internal float startTime;
        internal float endTime;
        
        internal float duration;
        internal float currentTime;

        internal EasingType easingType;
        
        internal virtual void Init(T0 target, float startTime, float duration, EasingType easingType, bool forcedinitializable)
        {
            this.target = target;
            this.easingType = easingType;
            Forcedinitializable = forcedinitializable;
            TargetTransformID = target.GetInstanceID();
   
            this.startTime = startTime;
            this.endTime = startTime + duration;
            this.duration = duration;
            this.currentTime = startTime;
            
            foreach (var activeMaCurve in MaCurveManager.activeMaCurves)
            {
                if (activeMaCurve.IsActive == true && activeMaCurve.TargetTransformID == TargetTransformID && activeMaCurve.GetType() == typeof(MaCurveForTransform))
                {
                    if (activeMaCurve.Forcedinitializable == true)
                    {
                    }
                    else
                    {
                        activeMaCurve.OnDie = null;
                    }
                    activeMaCurve.IsActive = false;
                    break;
                }
            }
            
            MaCurveManager.activeMaCurves.Add(this);
        }
    }
}