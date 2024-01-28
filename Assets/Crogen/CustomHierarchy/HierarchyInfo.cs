#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Serialization;

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
    public Color backgroundColor = Color.white;
    
    //Icon
    public bool showIcon;
    
    //Line
    public static bool showLine = true;
    public Color lineColor = new Color32(104,104,104,255);
    
    //Text
    public Color textColor = Color.white;
    
}
#endif
