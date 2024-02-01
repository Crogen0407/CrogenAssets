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
            GUILayout.Label("Icon");
            GUILayout.Label("Line");
            GUILayout.Label("Text");
        }
    }
}