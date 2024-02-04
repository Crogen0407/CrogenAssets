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
    public BackgroundType backgroundType = BackgroundType.Default;
    public Color backgroundColor = Color.clear;

    //Icon
    public ComponentIcon[] ComponentIcons = new ComponentIcon[128];

    //Line
    public Color lineColor = new Color32(104,104,104,255);

    //Text
    public Color textColor = Color.white;

    private void Reset()
    {
        if (_hierarchySettingData == null)
            _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");

        try
        {
            backgroundColor = _hierarchySettingData.backgroundColor[GetParentIndex()];
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.Log("설정한 값이 존재하지 않습니다. 기본색인 (0, 0, 0, 0)으로 설정합니다.");
            backgroundColor = Color.clear;
        }
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
