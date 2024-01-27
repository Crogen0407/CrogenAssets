using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class HierarchyExtensionSettingWindow : EditorWindow
    {
        public static void ShowWindow (Vector2 position) 
        {
            var window = GetWindow(typeof(HierarchyExtensionSettingWindow));
            window.maxSize = new Vector2(200, 200);
            window.minSize = new Vector2(200, 200);
            window.position = new Rect(position, Vector2.one * 200);
            window.wantsMouseEnterLeaveWindow = false;
            window.Show();
        }

        void OnGUI ()
        {
            Type t = typeof(HierarchyExtensionSettingData);
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