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
    
    private void OnEnable()
    {
        _hierarchyInfo = target as HierarchyInfo;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
