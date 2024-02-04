using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    [CustomEditor(typeof(HierarchyInfo))]
    [CanEditMultipleObjects]
    public class HierarchyInfoEditor : UnityEditor.Editor
    {
        private HierarchyInfo[] _hierarchyInfo;
        public CustomHierarchySettingDataSO hierarchySettingData;
        private readonly int _spaceValue = 20;
        
        private void OnEnable()
        {
            _hierarchyInfo = targets.Cast<HierarchyInfo>().ToArray();
            hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
        }

        public override void OnInspectorGUI()
        {
            #region Style

            GUILayoutOption[] guiLayoutOption = StyleEditor.GUILayoutOption;
            GUIStyle titleStyle = StyleEditor.BoldTitleStyle;

            #endregion
            
            EditorGUI.BeginChangeCheck();
            
            //다른 수치가 2개 이상 있으면 true
            EditorGUI.showMixedValue =
                _hierarchyInfo.Select (x => x.backgroundColor).Distinct ().Count () > 1;
            
            #region Background

            GUILayout.BeginHorizontal();
            
            GUILayout.Label("Show Background", titleStyle);
            
            GUILayout.EndHorizontal();
            BackgroundType backgroundType = BackgroundType.Default;
            
            Color[] backgroundColor = new Color[_hierarchyInfo.Length];
            for (int i = 0; i < backgroundColor.Length; ++i) backgroundColor[i] = _hierarchyInfo[i].backgroundColor;
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Background Type");
            backgroundType = (BackgroundType)EditorGUILayout.EnumPopup(_hierarchyInfo[0].backgroundType, guiLayoutOption);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            backgroundColor[0] = EditorGUILayout.ColorField(_hierarchyInfo[0].backgroundColor, guiLayoutOption);
            
            for (int i = 1; i < backgroundColor.Length; ++i) backgroundColor[i] = backgroundColor[0];
            
            GUILayout.EndHorizontal();

            #endregion

            GUILayout.Space(_spaceValue);

            #region Icon

            GUILayout.BeginHorizontal();
            GUILayout.Label("Show Icon", titleStyle);
            GUILayout.EndHorizontal();

            ComponentIcon componentIcon = null;
            for (int i = 0; i < _hierarchyInfo[0].ComponentIcons.Length; ++i)
            {
                componentIcon = _hierarchyInfo[0].ComponentIcons[i];
                if (componentIcon != null && componentIcon.component != _hierarchyInfo[0])
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
                lineColor = EditorGUILayout.ColorField(_hierarchyInfo[0].lineColor, guiLayoutOption);
                GUILayout.EndHorizontal();

            #endregion

            GUILayout.Space(_spaceValue);

            #region Text

            GUILayout.Label("Text", titleStyle);
            GUILayout.BeginHorizontal();
            GUILayout.Space(_spaceValue);
            GUILayout.Label("Color");
            var textColor = EditorGUILayout.ColorField(_hierarchyInfo[0].textColor, guiLayoutOption);
            GUILayout.EndHorizontal();

            #endregion

            
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(_hierarchyInfo, "Change HierarchyInfo");
                
                //모든 컴포넌트에 수치 대입 및 갱신

                for (int i = 0; i < _hierarchyInfo.Length; ++i)
                {
                    HierarchyInfo hierarchyInfo = _hierarchyInfo[i];
                    hierarchyInfo.backgroundType = backgroundType;
                    hierarchyInfo.backgroundColor = backgroundColor[i];

                    for (int j = 0; j < hierarchyInfo.ComponentIcons.Length; ++j)
                    {
                        hierarchyInfo.ComponentIcons[i] = componentIcon;
                    }

                    hierarchyInfo.lineColor = lineColor;
                    
                    hierarchyInfo.textColor = textColor;
                }
                serializedObject.ApplyModifiedProperties();
            }
            Repaint();
        }
    }
}
