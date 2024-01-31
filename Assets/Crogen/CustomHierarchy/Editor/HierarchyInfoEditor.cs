using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(HierarchyInfo))]
    public class HierarchyInfoEditor : UnityEditor.Editor
    {
        private HierarchyInfo _hierarchyInfo;
        private readonly int _spaceValue = 20;

        private void OnEnable()
        {
            _hierarchyInfo = target as HierarchyInfo;
        }

        public override void OnInspectorGUI()
        {
            #region Style

            GUILayoutOption[] guiLayoutOption = new[]
            {
                GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.55f),
                GUILayout.Height(20),
                GUILayout.ExpandWidth(false),
            };

            GUIStyle titleStyle = new GUIStyle()
            {
                normal =
                {
                    textColor = Color.white
                },
                fontStyle = FontStyle.Bold
            };

            #endregion

            #region Background

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Background", titleStyle);
            _hierarchyInfo.showBackground = GUILayout.Toggle(_hierarchyInfo.showBackground, "", guiLayoutOption);
            GUILayout.EndHorizontal();

            if (_hierarchyInfo.showBackground)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Background Type");
                _hierarchyInfo.backgroundType =
                    (BackgroundType)EditorGUILayout.EnumPopup(_hierarchyInfo.backgroundType, guiLayoutOption);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Color");
                _hierarchyInfo.backgroundColor =
                    EditorGUILayout.ColorField(_hierarchyInfo.backgroundColor, guiLayoutOption);
                GUILayout.EndHorizontal();
            }

            #endregion

            GUILayout.Space(_spaceValue);

            #region Icon

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Icon", titleStyle);
            _hierarchyInfo.showIcon = GUILayout.Toggle(_hierarchyInfo.showIcon, "", guiLayoutOption);
            GUILayout.EndHorizontal();

            if (_hierarchyInfo.showIcon)
            {
                for (int i = 0; i < _hierarchyInfo.ComponentIcons.Length; i++)
                {
                    ComponentIcon componentIcon = _hierarchyInfo.ComponentIcons[i];
                
                    if (componentIcon != null && componentIcon.component != _hierarchyInfo)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(_spaceValue);

                        GUIStyle textStyle = GUI.skin.toggle;
                        textStyle.normal = new GUIStyleState() { textColor = componentIcon.enable ? Color.white : Color.gray };
                        GUILayoutOption[] toggleOption = new[]
                        {
                            GUILayout.Width(EditorGUIUtility.currentViewWidth),
                            GUILayout.Height(20),
                            GUILayout.ExpandWidth(false),
                        };
                        
                        componentIcon.enable = GUILayout.Toggle(componentIcon.enable, $"  {componentIcon.name}", toggleOption);
                    
                        GUILayout.EndHorizontal();
                    
                    
                        if (componentIcon.component == null)
                            break;    
                    }
                }
            }

            #endregion

            GUILayout.Space(_spaceValue);

            #region Line

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Line", titleStyle);
            HierarchyInfo.ShowLine = GUILayout.Toggle(HierarchyInfo.ShowLine, "", guiLayoutOption);
            GUILayout.EndHorizontal();
            if (HierarchyInfo.ShowLine)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Color");
                _hierarchyInfo.lineColor = EditorGUILayout.ColorField(_hierarchyInfo.lineColor, guiLayoutOption);
                GUILayout.EndHorizontal();
            }

            #endregion

            GUILayout.Space(_spaceValue);

            #region Text

            GUILayout.Label("Text", titleStyle);
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            _hierarchyInfo.textColor = EditorGUILayout.ColorField(_hierarchyInfo.textColor, guiLayoutOption);
            GUILayout.EndHorizontal();

            #endregion
        }
    }
}
