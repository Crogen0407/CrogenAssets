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
        //Background
        GUILayout.Label("Show Background");
        _hierarchyInfo.showBackground = GUILayout.Toggle(_hierarchyInfo.showBackground, "");
        if (_hierarchyInfo.showBackground)
        {
            _hierarchyInfo.backgroundColor = EditorGUILayout.ColorField(_hierarchyInfo.backgroundColor);
        }
            
        GUILayout.Space(_spaceValue);
        
        //Icon            
        GUILayout.Label("Show Icon");
        _hierarchyInfo.showIcon = GUILayout.Toggle(_hierarchyInfo.showIcon, "");
        
        GUILayout.Space(_spaceValue);
        
        //Line
        GUILayout.Label("Show Line");
        HierarchyInfo.showLine = GUILayout.Toggle(HierarchyInfo.showLine, "");
        if (HierarchyInfo.showLine)
        {
            _hierarchyInfo.lineColor = EditorGUILayout.ColorField(_hierarchyInfo.lineColor);
        }
        
    }
}
