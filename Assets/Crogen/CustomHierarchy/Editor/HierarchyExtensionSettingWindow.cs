using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class HierarchyExtensionSettingWindow : EditorWindow
    {
        [MenuItem ("Crogen/HierarchyExtensionSetting")]
        public static void ShowWindow () 
        {
            var window = GetWindow(typeof(HierarchyExtensionSettingWindow));
            window.Show();
        }

        void OnGUI ()
        {
            Type t = typeof(HierarchyExtensionSetting);
            var propertyInfos = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var propertyInfo in propertyInfos)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(propertyInfo.Name);
                object obj = null; 
                obj = propertyInfo.GetValue(propertyInfo);
                propertyInfo.SetValue(obj, GUILayout.Toggle((bool)obj, string.Empty));
                GUILayout.EndHorizontal();
                EditorApplication.RepaintHierarchyWindow();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
           
        }
    }
}