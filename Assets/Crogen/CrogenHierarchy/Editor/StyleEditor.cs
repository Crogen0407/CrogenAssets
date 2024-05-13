#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenHierarchy.Editor
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
            fontStyle = FontStyle.Bold,
            margin = new RectOffset(5, 0, 0, 0)
        };
        
        public static readonly GUIStyle NormalTitleStyle = new GUIStyle()
        {
            normal =
            {
                textColor = Color.white
            },
        };

        public static readonly Color DefaultLineColor = new Color32(104, 104, 104, 255);
        public static readonly int SpaceValue = 20;
    }
}
#endif