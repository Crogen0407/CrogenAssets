#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Crogen.CustomHierarchy
{
    public enum BackgroundType
    {
        Default,
        Gradients
    }

    [Serializable]
    public class ComponentIcon
    {
        [HideInInspector] public string name;
        [HideInInspector] public Component component;
        public bool enable = true;
    }

    public class HierarchyInfo : MonoBehaviour
    {
        //Background
        public bool showBackground;
        public BackgroundType backgroundType = BackgroundType.Default;
        public Color backgroundColor = Color.clear;
    
        //Icon
        public bool showIcon;
        public ComponentIcon[] ComponentIcons = new ComponentIcon[128];
    
        //Line
        public static bool ShowLine = true;
        public Color lineColor = new Color32(104,104,104,255);
    
        //Text
        public Color textColor = Color.white;
    }
}
#endif
