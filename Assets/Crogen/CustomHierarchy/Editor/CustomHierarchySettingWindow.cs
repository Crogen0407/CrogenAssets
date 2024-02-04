using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class CustomHierarchySettingWindow : EditorWindow
    {
        private static CustomHierarchySettingDataSO _hierarchySettingData;

        [MenuItem("Crogen/CustomHierarchy/CustomHierarchyGlobalSetting")]
        public static void ShowWindow()
        {
            var window = GetWindow<CustomHierarchySettingWindow>();
            window.titleContent = new GUIContent("CustomHierarchySettingWindow");
            window.Show();

            _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");

            if (_hierarchySettingData == null)
            {
                ScriptableObject asset = ScriptableObject.CreateInstance(typeof(CustomHierarchySettingDataSO));
                AssetDatabase.CreateAsset(asset, "Assets/Crogen/CustomHierarchy/Resources/HierarchySettingData.asset");
                _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
            }
            AssetDatabase.SaveAssets();
        }

        private void OnGUI()
        {
            if (_hierarchySettingData != null)
            {
                EditorGUI.BeginChangeCheck();
                
                #region Background
                GUILayout.Label("Background", StyleEditor.BoldTitleStyle);

                List<Color> backgroundColor = _hierarchySettingData.backgroundColor;
                
                if (backgroundColor != null)
                {
                    for (int i = 0; i < backgroundColor.Count; i++)
                    {
                        GUILayout.BeginHorizontal();
                        
                        backgroundColor[i] =
                            EditorGUILayout.ColorField(backgroundColor[i]);
                        
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button("+"))
                    {
                        backgroundColor.Add(Color.clear);
                    }
                    if (backgroundColor.Count > 0)
                    {
                        if (GUILayout.Button("-"))
                        {
                            backgroundColor.RemoveAt(backgroundColor.Count - 1);
                        }    
                    }
                    GUILayout.EndHorizontal();
                }
                #endregion

                GUILayout.Space(10);
                
                #region Icon
                List<bool> activeIcons = _hierarchySettingData.activeIcons;

                GUILayout.Label("Icon", StyleEditor.BoldTitleStyle);
                
                if (activeIcons != null)
                {
                    for (int i = 0; i < activeIcons.Count; i++)
                    {
                        GUILayout.BeginHorizontal();
                        
                        activeIcons[i] =
                            EditorGUILayout.Toggle(activeIcons[i]);
                        
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button("+"))
                    {
                        activeIcons.Add(false);
                    }
                    if (activeIcons.Count > 0)
                    {
                        if (GUILayout.Button("-"))
                        {
                            activeIcons.RemoveAt(activeIcons.Count - 1);
                        }    
                    }
                    GUILayout.EndHorizontal();
                }
                #endregion
                GUILayout.Label("Line", StyleEditor.BoldTitleStyle);
                GUILayout.Label("Text", StyleEditor.BoldTitleStyle);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_hierarchySettingData, "Change HierarchySettings");
                    _hierarchySettingData.backgroundColor = backgroundColor;
                    EditorUtility.SetDirty(_hierarchySettingData);
                }
            }
            else
            {
                _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
            }
        }
    }
}