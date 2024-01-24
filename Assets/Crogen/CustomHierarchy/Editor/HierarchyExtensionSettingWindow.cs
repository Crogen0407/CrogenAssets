using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class HierarchyExtensionSettingWindow : EditorWindow
    {
        [MenuItem ("Crogen/HierarchyExtensionSetting")]

        public static void  ShowWindow () 
        {
            var window = GetWindow(typeof(HierarchyExtensionSettingWindow));
            window.Show();
        }
    
        void OnGUI ()
        {
            GUILayout.Space(25);
            GUILayout.Label("More Detailed");
            HierarchyExtensionSetting.MoreDetailed = GUILayout.Toggle(
                    HierarchyExtensionSetting.MoreDetailed, "Line");
        }
    }
}