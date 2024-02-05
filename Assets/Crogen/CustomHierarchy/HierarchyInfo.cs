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
    public ComponentIcon[] ComponentIcons;

    //Line
    public Color lineColor = new Color32(104,104,104,255);

    //Text
    public Color textColor = Color.white;

    public void Init()
    {
        int componentAmount = GetComponents<Component>().Length;
        for (int i = 0; i < componentAmount; ++i) UnityEditorInternal.ComponentUtility.MoveComponentDown(this);
    }
    
    private void Reset()
    {
        Init();
        if (_hierarchySettingData == null)
            _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
        
        #region Background

        backgroundType = _hierarchySettingData.backgroundType;
        
        try
        {
            backgroundColor = _hierarchySettingData.backgroundColor[GetParentIndex()];
        }
        catch (ArgumentOutOfRangeException e)
        {
            if (_hierarchySettingData.backgroundColor.Count > 0)
            {
                backgroundColor = _hierarchySettingData.backgroundColor[_hierarchySettingData.backgroundColor.Count - 1];
            }
            else
            {
                backgroundColor = Color.clear;
            }
        }

        #endregion

        #region Line

        try
        {
            lineColor = _hierarchySettingData.lineColor[GetParentIndex()];
        }
        catch (ArgumentOutOfRangeException e)
        {
            if (_hierarchySettingData.lineColor.Count > 0)
            {
                lineColor = _hierarchySettingData.lineColor[_hierarchySettingData.lineColor.Count - 1];
            }
            else
            {
                lineColor = Color.clear;
            }
        }

        #endregion

        #region Text

        try
        {
            textColor = _hierarchySettingData.textColor[GetParentIndex()];
        }
        catch (ArgumentOutOfRangeException e)
        {
            if (_hierarchySettingData.textColor.Count > 0)
            {
                lineColor = _hierarchySettingData.textColor[_hierarchySettingData.textColor.Count - 1];
            }
            else
            {
                textColor = Color.clear;
            }
        }

        #endregion
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
