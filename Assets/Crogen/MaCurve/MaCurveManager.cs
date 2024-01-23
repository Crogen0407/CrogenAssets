using System;
using System.Collections.Generic;
using System.Linq;
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


        private void CheckActiveMaCurve()
        {
            for (int i = 0; i < activeMaCurves.Count; i++)
            {
                MaCurveCore activeMaCurveCore = activeMaCurves[i];
                if(activeMaCurveCore.IsActive == true)
                    activeMaCurveCore.Update();
            }
        }
        
        private void FixedUpdate()
        {
            CurrentRealTime = Time.unscaledTime;
            CheckActiveMaCurve();
        }
    }
}