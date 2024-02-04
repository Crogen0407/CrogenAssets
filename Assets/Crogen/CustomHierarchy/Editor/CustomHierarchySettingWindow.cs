using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class CustomHierarchySettingWindow : EditorWindow
    {
        private Vector2 _scrollPosition = Vector2.zero;
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
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(position.height));
            if (_hierarchySettingData != null)
            {
                EditorGUI.BeginChangeCheck();
                
                #region Background
                
                GUILayout.Label("Background", StyleEditor.BoldTitleStyle);
                _hierarchySettingData.backgroundType = (BackgroundType)EditorGUILayout.EnumPopup(_hierarchySettingData.backgroundType);
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
                        backgroundColor.Add(backgroundColor[backgroundColor.Count - 1]);
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
                        activeIcons.Add(activeIcons[activeIcons.Count - 1]);
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

                GUILayout.Space(10);
                
                #region Line

                GUILayout.Label("Line", StyleEditor.BoldTitleStyle);

                List<Color> lineColor = _hierarchySettingData.lineColor;
                
                if (lineColor != null)
                {
                    for (int i = 0; i < lineColor.Count; i++)
                    {
                        GUILayout.BeginHorizontal();
                        
                        lineColor[i] =
                            EditorGUILayout.ColorField(lineColor[i]);
                        
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button("+"))
                    {
                        lineColor.Add(lineColor[lineColor.Count - 1]);
                    }
                    if (lineColor.Count > 0)
                    {
                        if (GUILayout.Button("-"))
                        {
                            lineColor.RemoveAt(lineColor.Count - 1);
                        }    
                    }
                    GUILayout.EndHorizontal();
                }

                #endregion

                GUILayout.Space(10);

                #region Text

                GUILayout.Label("Text", StyleEditor.BoldTitleStyle);

                List<Color> textColor = _hierarchySettingData.textColor;
                
                if (textColor != null)
                {
                    for (int i = 0; i < textColor.Count; i++)
                    {
                        GUILayout.BeginHorizontal();
                        
                        textColor[i] =
                            EditorGUILayout.ColorField(textColor[i]);
                        
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button("+"))
                    {
                        textColor.Add(textColor[textColor.Count - 1]);
                    }
                    if (textColor.Count > 0)
                    {
                        if (GUILayout.Button("-"))
                        {
                            textColor.RemoveAt(textColor.Count - 1);
                        }    
                    }
                    GUILayout.EndHorizontal();
                }

                #endregion
                

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(_hierarchySettingData, "Change HierarchySettings");
                    EditorUtility.SetDirty(_hierarchySettingData);
                }
            }
            else
            {
                _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
            }
            EditorGUILayout.EndScrollView();
        }
    }
}