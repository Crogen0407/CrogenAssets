using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    [CustomEditor(typeof(HierarchyInfo))]
    public class HierarchyInfoEditor : UnityEditor.Editor
    {
        private HierarchyInfo _hierarchyInfo;
        public CustomHierarchySettingDataSO hierarchySettingData;
        private readonly int _spaceValue = 20;
        
        private void OnEnable()
        {
            _hierarchyInfo = target as HierarchyInfo;
            hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
        }

        public override void OnInspectorGUI()
        {
            #region Style

            GUILayoutOption[] guiLayoutOption = StyleEditor.GUILayoutOption;
            GUIStyle titleStyle = StyleEditor.BoldTitleStyle;

            #endregion
            
            EditorGUI.BeginChangeCheck();
            
            #region Background

            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Show Background", titleStyle);
            
            GUILayout.EndHorizontal();
            BackgroundType backgroundType = BackgroundType.Default;
            
            Color backgroundColor = new Color();
            backgroundColor = _hierarchyInfo.backgroundColor;
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Background Type");
            backgroundType = (BackgroundType)EditorGUILayout.EnumPopup(_hierarchyInfo.backgroundType, guiLayoutOption);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            backgroundColor = EditorGUILayout.ColorField(_hierarchyInfo.backgroundColor, guiLayoutOption);
            
            GUILayout.EndHorizontal();

            #endregion

            GUILayout.Space(_spaceValue);

            #region Icon

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Icon", titleStyle);
            GUILayout.EndHorizontal();

            ComponentIcon[] componentIcons = _hierarchyInfo.ComponentIcons == null ? new ComponentIcon[_hierarchyInfo.ComponentIcons.Length] : _hierarchyInfo.ComponentIcons;
            for (int i = 0; i < _hierarchyInfo.ComponentIcons.Length; ++i)
            {
                componentIcons[i] = _hierarchyInfo.ComponentIcons[i];
                if (componentIcons[i] != null && componentIcons[i].component != _hierarchyInfo)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(_spaceValue);

                    GUIStyle textStyle = GUI.skin.toggle;
                    textStyle.normal = new GUIStyleState() { textColor = componentIcons[i].enable ? Color.white : Color.gray };
                    GUILayoutOption[] toggleOption = new[]
                    {
                        GUILayout.Width(EditorGUIUtility.currentViewWidth),
                        GUILayout.Height(20),
                        GUILayout.ExpandWidth(false),
                    };
                    
                    componentIcons[i].enable = GUILayout.Toggle(componentIcons[i].enable, $"  {componentIcons[i].name}", toggleOption);
                
                    GUILayout.EndHorizontal();
                
                
                    if (componentIcons[i].component == null)
                        break;    
                }
                else
                {
                    continue;
                }
            }

            #endregion

            GUILayout.Space(_spaceValue);

            #region Line

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Line", titleStyle);
            Color lineColor = Color.white;
            GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Color");
                lineColor = EditorGUILayout.ColorField(_hierarchyInfo.lineColor, guiLayoutOption);
                GUILayout.EndHorizontal();

            #endregion

            GUILayout.Space(_spaceValue);

            #region Text

            GUILayout.Label("Text", titleStyle);
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            var textColor = EditorGUILayout.ColorField(_hierarchyInfo.textColor, guiLayoutOption);
            GUILayout.EndHorizontal();

            #endregion

            
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_hierarchyInfo, "Change HierarchyInfo");
                
                HierarchyInfo hierarchyInfo = _hierarchyInfo;
                hierarchyInfo.backgroundType = backgroundType;
                hierarchyInfo.backgroundColor = backgroundColor;

                hierarchyInfo.ComponentIcons = componentIcons;

                hierarchyInfo.lineColor = lineColor;
                    
                hierarchyInfo.textColor = textColor;
                serializedObject.ApplyModifiedProperties();
            }
            Repaint();
        }
    }
}
