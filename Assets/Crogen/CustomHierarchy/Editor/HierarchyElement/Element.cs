using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class Element
    {
        private Texture2D _gradientTexture;

        private CustomHierarchySettingDataSO _hierarchySettingData;

        private static MethodInfo _loadIconMethodInfo;

        
        public Element()
        {
            _gradientTexture = (Resources.Load("GradientHorizontal") as Texture2D);
            
            if (_hierarchySettingData == null)
                _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
            _loadIconMethodInfo = typeof(EditorGUIUtility).GetMethod("LoadIcon", BindingFlags.Static | BindingFlags.NonPublic);
        }

        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null,
            GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0,
            float offset = 0)
        {
            
            if (hierarchyInfo != null)
            {
                #region DrawBackground

                if (hierarchyInfo.backgroundColor.a > 0)
                {
                    Color color = hierarchyInfo != null ? hierarchyInfo.backgroundColor : Color.clear;
                    Rect gradientBackgroundPosition = new Rect(new Vector2(32, selectionRect.y),
                        selectionRect.size + new Vector2(selectionRect.x, 0));

                    if (hierarchyInfo != null)
                    {
                        switch (hierarchyInfo.backgroundType)
                        {
                            case BackgroundType.Default:
                                GUI.DrawTexture(gradientBackgroundPosition, new Texture2D(128, 128),
                                    ScaleMode.ScaleAndCrop, true, 0, color, Vector4.zero, 0);
                                break;
                            case BackgroundType.Gradients:

                                #region Draw Gradient

                                GUI.DrawTexture(gradientBackgroundPosition, _gradientTexture, ScaleMode.ScaleAndCrop,
                                    true, 0, color, Vector4.zero, 0);

                                #endregion

                                break;
                        }
                    }
                }

                #endregion

                #region DrawIcon

                if (components != null)
                {
                    if (true)
                    {
                        Rect iconPosition = new Rect();
                        if (hierarchyInfo.ComponentIcons != null &&
                            components.Length == hierarchyInfo.ComponentIcons.Length)
                        {
                            for (int i = 0; i < components.Length; ++i)
                            {
                                ComponentIcon componentIcon = hierarchyInfo.ComponentIcons[i];

                                if (componentIcon == null)
                                    componentIcon = new ComponentIcon();

                                if (componentIcon.component != components[i])
                                    componentIcon.name = components[i].GetType().Name;

                                componentIcon.component = components[i];
                            }
                        }
                        else
                        {
                            //OnReset
                            hierarchyInfo.ComponentIcons = new ComponentIcon[components.Length];
                            for (int i = 0; i < hierarchyInfo.ComponentIcons.Length; ++i)
                            {
                                hierarchyInfo.ComponentIcons[i] = new ComponentIcon();
                                hierarchyInfo.ComponentIcons[i].enable = _hierarchySettingData.activeIcons[i];
                                if (_hierarchySettingData.activeIcons.Count == i + 1) break;
                            }
                        }

                        try
                        {
                            int emptySpaceOffset = 0;
                            for (int i = 0; i < components.Length; ++i)
                            {
                                iconPosition = new Rect(
                                    new Vector2(
                                        (selectionRect.width - selectionRect.height * (i + 1 - emptySpaceOffset)) +
                                        (EditorGUIUtility.currentViewWidth - selectionRect.width) - offset,
                                        selectionRect.y),
                                    new Vector2(selectionRect.height, selectionRect.height));

                                //unity 기본 built-in 아이콘 가져오기
                                if (components[i] != null && hierarchyInfo.ComponentIcons[i].enable &&
                                    hierarchyInfo != hierarchyInfo.ComponentIcons[i].component)
                                {
                                    // Built in Icon
                                    Texture2D texture = _loadIconMethodInfo?.Invoke(null,
                                        new object[] { $"{components[i].GetType().Name} Icon" }) as Texture2D;

                                    //패키지 정의 컴포넌트
                                    if (texture == null)
                                    {
                                        var item = MonoScript.FromMonoBehaviour(components[i] as MonoBehaviour);
                                        string path = AssetDatabase.GetAssetPath(item);
                                        MonoImporter monoImporter = AssetImporter.GetAtPath(path) as MonoImporter;
                                        if (monoImporter != null)
                                            texture = monoImporter.GetIcon();

                                        if (texture == null)
                                        {
                                            //사용자 정의 컴포넌트
                                            texture = AssetDatabase.GetCachedIcon(path) as Texture2D;
                                        }
                                    }

                                    //실제로 그리기          
                                    if (texture != null)
                                        GUI.DrawTexture(iconPosition, texture);
                                }
                                else
                                {
                                    ++emptySpaceOffset;
                                }
                            }
                        }
                        catch
                        {
                            GUI.DrawTexture(iconPosition, new Texture2D(128, 128));
                        }
                    }
                }

                #endregion

                #region DrawText

                if (hierarchyInfo.textColor.a > 0)
                {
                    Color textColor = hierarchyInfo != null ? hierarchyInfo.textColor : Color.white;
                    GUIStyle textStyle = new GUIStyle() { normal = new GUIStyleState() { textColor = textColor } };

                    if (gameObject.activeInHierarchy == false)
                    {
                        textStyle.normal.textColor = Color.gray;
                    }

                    GUI.Box(new Rect(selectionRect.position + new Vector2(17.9f, 0), selectionRect.size),
                        gameObject.name, textStyle);
                }

                #endregion
            }

            #region DrawLine

            if (Mathf.Approximately(selectionRect.position.x, 60f) == false)
            {
                if (hierarchyInfo != null && Mathf.Approximately(hierarchyInfo.lineColor.a, 0))
                    return;

                //HorizontalLine
                Color parentSettingLineColor = GetLineColor(parent, StyleEditor.DefaultLineColor);

                Vector2 positionOffset = new Vector2(-8.25f - 14, 8);
                int lineSizeX = gameObject.transform.childCount != 0 ? 8 : 16;

                EditorGUI.DrawRect(new Rect(selectionRect.position + positionOffset, new Vector2(lineSizeX, 2)),
                    parentSettingLineColor);

                //VerticalLine
                for (int i = 0; i < hierarchySibling; i++)
                {
                    parentSettingLineColor = GetLineColor(parent, StyleEditor.DefaultLineColor);

                    if (parent.GetChild(0) == gameObject.transform && i == 0)
                    {
                        EditorGUI.DrawRect(
                            new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1), 0), new Vector2(2, 8)),
                            parentSettingLineColor);
                    }
                    else
                    {
                        EditorGUI.DrawRect(
                            new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1), -8),
                                new Vector2(2, 16)), parentSettingLineColor);
                    }

                    try
                    {
                        parent = parent.parent;
                    }
                    catch
                    {
                        parent = null;
                    }
                }
            }

            #endregion
            #region DrawToggle

            Rect togglePosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(selectionRect.height, selectionRect.height));
            gameObject.SetActive(GUI.Toggle(togglePosition, gameObject.activeSelf, ""));            

            #endregion
        }
        
        private Color GetLineColor(Transform parentTrm, Color defaultColor)
        {
            Color color;
            try
            {
                color = parentTrm != null ? parentTrm.GetComponent<HierarchyInfo>().lineColor : defaultColor;
            }
            catch
            {
                color = defaultColor;
            }
            return color;
        }
    }
}