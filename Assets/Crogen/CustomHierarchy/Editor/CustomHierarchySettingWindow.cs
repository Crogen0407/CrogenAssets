using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    
    public class CustomHierarchySettingWindow : EditorWindow
    {
        [MenuItem("Crogen/CustomHierarchy/CustomHierarchyGlobalSetting")]
        public static void ShowWindow()
        {
            var window = GetWindow<CustomHierarchySettingWindow>();
            window.titleContent = new GUIContent("CustomHierarchySettingWindow");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Background", StyleEditor.BoldTitleStyle);
            GUILayout.Label("Icon", StyleEditor.BoldTitleStyle);
            GUILayout.Label("Line", StyleEditor.BoldTitleStyle);
            GUILayout.Label("Text", StyleEditor.BoldTitleStyle);
        }
    }
}