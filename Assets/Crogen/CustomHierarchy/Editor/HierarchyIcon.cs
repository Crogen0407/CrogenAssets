using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class HierarchyIcon
    {
        public static Texture2D LoadIcon(string iconName)
        {
            return (Texture2D)EditorGUIUtility.IconContent(iconName).image;
        }
    }

    public class HierarchyIconData
    {
        public static Dictionary<Component, string> IconData = new Dictionary<Component, string>();

        public static void Init()
        {
        }
    }
}