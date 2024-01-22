using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Crogen.MaCurve
{
    public class MaCurveManager : MonoBehaviour
    {
        internal static List<MaCurveCore> activeMaCurves = new List<MaCurveCore>();
        
        private static float _currentRealTime;

        internal static float CurrentRealTime
        {
            get => _currentRealTime;
            private set
            {
                _currentRealTime = value;
            }
        }
        
        private void FixedUpdate()
        {
            CurrentRealTime = Time.unscaledTime;
            foreach (MaCurveCore activeMaCurve in activeMaCurves)
            {
                if (activeMaCurve.IsActive == true)
                    activeMaCurve.Move();
                else
                {
                    activeMaCurves.Remove(activeMaCurve);
                    break;
                }
            }    
        }
    }
}