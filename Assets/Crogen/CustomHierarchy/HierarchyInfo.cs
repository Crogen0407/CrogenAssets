#if UNITY_EDITOR
using UnityEngine;

public enum backgroundType
{
    Default,
    Gradients
}

public class HierarchyInfo : MonoBehaviour
{
    public backgroundType backgroundType;
    public Color mainColor = Color.black;
    public Color lineColor = new Color32(104,104,104,255);
    public Color textColor = Color.white;
    public bool showIcon;
}
#endif
