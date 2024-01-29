#if UNITY_EDITOR
using UnityEngine;

namespace Crogen.CustomHierarchy
{
    public enum BackgroundType
    {
        Default,
        Gradients
    }

    public class ComponentIcon
    {
        public Component Component;
        public bool Enable = true;
    }

    public class HierarchyInfo : MonoBehaviour
    {
        //Background
        public bool showBackground;
        public BackgroundType backgroundType;
        public Color backgroundColor = Color.white;
    
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
