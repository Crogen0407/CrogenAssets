#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenHierarchy.Editor
{
    public class CrogenHierarchySettingWindow : EditorWindow
    {
        private Vector2 _scrollPosition = Vector2.zero;
        private static CrogenHierarchySettingDataSO _hierarchySettingData;
        private static string _editorPath = "Assets/Crogen/CrgoenHierarchy/Resources";
        private static Texture2D _bennerTexture;
        [MenuItem("Window/Crogen/CrogenHierarchy/GlobalSetting")]
        public static void ShowWindow()
        {
            var window = GetWindow<CrogenHierarchySettingWindow>();
            window.titleContent = new GUIContent("GlobalSetting");
            window.Show();

            _hierarchySettingData = Resources.Load<CrogenHierarchySettingDataSO>("HierarchySettingData");

            if (_hierarchySettingData == null)
            {
                ScriptableObject asset = ScriptableObject.CreateInstance(typeof(CrogenHierarchySettingDataSO));
                if (_bennerTexture == null)
                    _bennerTexture = (Texture2D)AssetDatabase.LoadAssetAtPath($"{_editorPath}/CustomHierarchyBenner.png", typeof(Texture2D));
                AssetDatabase.CreateAsset(asset, "Assets/Crogen/CrgoenHierarchy/Resources/HierarchySettingData.asset");
                _hierarchySettingData = Resources.Load<CrogenHierarchySettingDataSO>("HierarchySettingData");
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
                _hierarchySettingData = Resources.Load<CrogenHierarchySettingDataSO>("HierarchySettingData");
            }
            EditorGUILayout.EndScrollView();
        }
        
        private void DrawListButton<T>(List<T> list, T item)
        {
            GUILayout.BeginHorizontal();
                    
            if (GUILayout.Button("+"))
            {
                list.Add(list.Count == 0 ? item : list[^1]);
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
#endif