#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenHierarchy.Editor
{
    [CustomEditor(typeof(HierarchyInfo))]
    public class HierarchyInfoEditor : UnityEditor.Editor
    {
        private HierarchyInfo _hierarchyInfo;
        public CrogenHierarchySettingDataSO hierarchySettingData;
        private readonly int _spaceValue = 20;
        
        private void OnEnable()
        {
            _hierarchyInfo = target as HierarchyInfo;
            hierarchySettingData = Resources.Load<CrogenHierarchySettingDataSO>("HierarchySettingData");
        }

        public override void OnInspectorGUI()
        {
            if (_hierarchyInfo != null)
            {
                
                #region Style
                
                GUILayoutOption[] guiLayoutOption = StyleEditor.GUILayoutOption;
                GUIStyle titleStyle = StyleEditor.BoldTitleStyle;
                
                #endregion
                
                EditorGUI.BeginChangeCheck();
                
                #region Background
                
                GUILayout.BeginHorizontal();
                
                GUILayout.Label("Background", titleStyle);
                
                GUILayout.EndHorizontal();
                
                
                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Background Type");
                var backgroundType = (BackgroundType)EditorGUILayout.EnumPopup(_hierarchyInfo.backgroundType, guiLayoutOption);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Color");
                var backgroundColor = EditorGUILayout.ColorField(_hierarchyInfo.backgroundColor, guiLayoutOption);
                
                GUILayout.EndHorizontal();
                
                #endregion
                
                GUILayout.Space(_spaceValue);
                
                #region Icon
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Icon", titleStyle);
                GUILayout.EndHorizontal();
                
                _hierarchyInfo.componentIcons ??= new ComponentIcon[hierarchySettingData.activeIcons.Count];
                ComponentIcon[] componentIcons = _hierarchyInfo.componentIcons;
                for (int i = 0; i < _hierarchyInfo.componentIcons.Length; ++i)
                {
                    componentIcons[i] = _hierarchyInfo.componentIcons[i];
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
                }
                
                #endregion
                
                GUILayout.Space(_spaceValue);
                
                #region Line
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Line", titleStyle);
                
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Space(_spaceValue);
                GUILayout.Label("Color");
                var lineColor = EditorGUILayout.ColorField(_hierarchyInfo.lineColor, guiLayoutOption);
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
                
                    hierarchyInfo.componentIcons = componentIcons;
                
                    hierarchyInfo.lineColor = lineColor;
                        
                    hierarchyInfo.textColor = textColor;
                    serializedObject.ApplyModifiedProperties();
                }
                Repaint();
            }
        }
    }
}
#endif