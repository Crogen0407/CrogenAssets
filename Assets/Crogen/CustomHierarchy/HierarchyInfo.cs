#if UNITY_EDITOR
using System;
using Crogen.CustomHierarchy.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ComponentIcon
{
    [HideInInspector] public string name;
    [HideInInspector] public Component component;
    public bool enable = true;
}

[DisallowMultipleComponent]
public class HierarchyInfo : MonoBehaviour
{
    private CustomHierarchySettingDataSO _hierarchySettingData;
    
    //Background
    public bool showBackground;
    public BackgroundType backgroundType = BackgroundType.Default;
    public Color backgroundColor = Color.clear;

    //Icon
    public bool showIcon;
    public ComponentIcon[] ComponentIcons;

    //Line
    public static bool ShowLine = true;
    public Color lineColor = new Color32(104,104,104,255);

    //Text
    public Color textColor = Color.white;


    private void Reset()
    {
        if (_hierarchySettingData == null)
            _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");


        backgroundColor = _hierarchySettingData.backgroundColor[GetParentIndex()];
    }

    public int GetParentIndex()
    {
        Transform parent = transform.parent;
        int parentIndex = 0;
        while (parent != null)
        {
            parent = parent.parent;
            ++parentIndex;
        }

        return parentIndex;
    }
}
#endif
