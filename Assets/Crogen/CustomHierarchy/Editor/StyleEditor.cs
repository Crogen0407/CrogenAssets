using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class StyleEditor
    {
        public static readonly GUILayoutOption[] GUILayoutOption = new[]
        {
            GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.55f),
            GUILayout.Height(20),
            GUILayout.ExpandWidth(false),
        };

        public static readonly GUIStyle BoldTitleStyle = new GUIStyle()
        {
            normal =
            {
                textColor = Color.white
            },
            fontStyle = FontStyle.Bold
        };
        
        public static readonly GUIStyle NormalTitleStyle = new GUIStyle()
        {
            normal =
            {
                textColor = Color.white
            },
            fontStyle = FontStyle.Bold
        };
    }
}