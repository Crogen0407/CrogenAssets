using System;
using UnityEngine;



namespace Crogen.CustomHierarchy
{
    using UnityEditor;
    
    #if UNITY_EDITOR
    using UnityEditor;
    [InitializeOnLoad]
    #endif
    public class HierarchyExtensionSetting : MonoBehaviour
    {
        private static bool _moreDetailed;

        internal static bool MoreDetailed
        {
            get => _moreDetailed;
            set
            {
                _moreDetailed = value;
                EditorApplication.RepaintHierarchyWindow();
            }
        }
        
        static HierarchyExtensionSetting()
        {
        }

        private static void HierarchySetting()
        {
            EditorGUI.DrawRect(new Rect(Vector2.zero, Vector2.one * 10000), Color.blue);        
        }
    }
}