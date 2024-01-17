using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Crogen.MaCurve
{
    public class MaCurveManager : MonoBehaviour
    {
        private static MaCurveManager _instance;
        internal static MaCurveManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MaCurveManager>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("MaCurveManager");
                        _instance = obj.AddComponent<MaCurveManager>();
                    }
                    DontDestroyOnLoad(_instance);
                }
                
                return _instance;
            }
        }
        internal static float CurrentRealTime { get; private set; }

        
        private void Awake()
        {
            
                
        }

        private void FixedUpdate()
        {
            CurrentRealTime = Time.unscaledTime;
            Debug.Log(CurrentRealTime);
        }
    }
}