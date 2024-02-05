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

                    DrawListButton(backgroundColor, Color.clear);
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

                    DrawListButton(activeIcons, true);

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

                    DrawListButton(lineColor, Color.clear);
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

                    DrawListButton(textColor, Color.clear);
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
        
        private void DrawListButton<T>(List<T> list, T item)
        {
            GUILayout.BeginHorizontal();
                    
            if (GUILayout.Button("+"))
            {
                if (list.Count == 0)
                {
                    list.Add(item);
                }
                else
                {
                    list.Add(list[list.Count - 1]);
                }
            }
            if (list.Count > 0)
            {
                if (GUILayout.Button("-"))
                {
                    list.RemoveAt(list.Count - 1);
                }    
            }
            GUILayout.EndHorizontal();
        }
    }
}