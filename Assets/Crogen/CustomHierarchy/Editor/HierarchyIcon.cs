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
}