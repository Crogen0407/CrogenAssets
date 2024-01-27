using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HierarchyInfo))]
public class HierarchyInfoEditor : Editor
{
    private HierarchyInfo _hierarchyInfo;
    private readonly int _spaceValue = 20;
    private void OnEnable()
    {
        _hierarchyInfo = target as HierarchyInfo;
    }

    public override void OnInspectorGUI()
    {
        GUILayoutOption[] guiLayoutOption = new[]
        {
            GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.55f),
            GUILayout.Height(20),
            GUILayout.ExpandWidth(false),
        };

        #region Background
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Show Background");
        _hierarchyInfo.showBackground = GUILayout.Toggle(_hierarchyInfo.showBackground, "", guiLayoutOption);
        GUILayout.EndHorizontal();

        if (_hierarchyInfo.showBackground)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Background Type");
            _hierarchyInfo.backgroundType = (backgroundType)EditorGUILayout.EnumPopup(_hierarchyInfo.backgroundType, guiLayoutOption);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            _hierarchyInfo.backgroundColor = EditorGUILayout.ColorField(_hierarchyInfo.backgroundColor, guiLayoutOption);
            GUILayout.EndHorizontal();
        }
        
        #endregion
            
        GUILayout.Space(_spaceValue);

        #region Icon

        GUILayout.BeginHorizontal();
        GUILayout.Label("Show Icon");
        _hierarchyInfo.showIcon = GUILayout.Toggle(_hierarchyInfo.showIcon, "", guiLayoutOption);
        GUILayout.EndHorizontal();
        
        #endregion
        
        GUILayout.Space(_spaceValue);

        #region Line

        GUILayout.BeginHorizontal();
        GUILayout.Label("Show Line");
        HierarchyInfo.showLine = GUILayout.Toggle(HierarchyInfo.showLine, "", guiLayoutOption);
        GUILayout.EndHorizontal();
        if (HierarchyInfo.showLine)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            _hierarchyInfo.lineColor = EditorGUILayout.ColorField(_hierarchyInfo.lineColor, guiLayoutOption);
            GUILayout.EndHorizontal();
        }
        
        #endregion
    }
}
