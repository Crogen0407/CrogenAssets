#if UNITY_EDITOR
using UnityEngine;

public enum backgroundType
{
    Default,
    Gradients
}

public class HierarchyInfo : MonoBehaviour
{
    //Background
    public bool showBackground;
    public backgroundType backgroundType;
    public Color mainColor = Color.clear;
    
    //Line
    public static bool showLine;
    public Color lineColor = new Color32(104,104,104,255);
    
    //Icon
    public bool showIcon;
    
    //Text
    public Color textColor = Color.white;
    
}
#endif
