#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum backgroundType
{
    Default,
    Gradients
}

public class ComponentIcon
{
    public Component component;
    public bool enable = true;
}

public class HierarchyInfo : MonoBehaviour
{
    //Background
    public bool showBackground;
    public backgroundType backgroundType;
    public Color backgroundColor = Color.white;
    
    //Icon
    public bool showIcon;
    public ComponentIcon[] componentIcons = new ComponentIcon[128];
    
    //Line
    public static bool showLine = true;
    public Color lineColor = new Color32(104,104,104,255);
    
    //Text
    public Color textColor = Color.white;
    
}
#endif
