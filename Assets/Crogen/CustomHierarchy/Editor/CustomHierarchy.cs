#if UNITY_EDITOR
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Crogen.CustomHierarchy.Editor
{
    [InitializeOnLoad]
    public class CustomHierarchy : UnityEditor.Editor
    {
        private static readonly float Offset = 4f;
        private static MethodInfo _loadIconMethodInfo;
        
        static CustomHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyOnGUI;
        }
    
        ~CustomHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HandleHierarchyOnGUI;
        }
    
        private static void HandleHierarchyOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj != null)
            {
                #region Init
    
                //MonoBehaviour
                var gameObject = (GameObject)obj;
                var parent = gameObject.transform.parent;
                var hierarchyInfo = gameObject.GetComponent<HierarchyInfo>();
                var components = gameObject.GetComponents<Component>();
              
                //Hierarchy Visual
                int hierarchyIndex = ((int)selectionRect.position.y / 16) % 2; //0 : 연한 거 / 1 : 진한 거
                
                EditorGUIUtility.hierarchyMode = false;
                #endregion
    
                if (hierarchyInfo != null)
                {
                    if (hierarchyInfo.showBackground)
                    {
                        #region Draw Background
                
                        //MainBackground
                        Rect backgroundPosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(1000, selectionRect.height));
                        
                        Color backgroundColor = hierarchyIndex == 0 ? new Color32(65, 65, 65, 100) : new Color32(56, 56, 56, 100);
                        
                        EditorGUI.DrawRect(backgroundPosition, backgroundColor);
                
                        Color color = hierarchyInfo != null ? hierarchyInfo.backgroundColor : Color.clear;
                        Rect gradientBackgroundPosition = new Rect(new Vector2(32, selectionRect.y), selectionRect.size + new Vector2(selectionRect.x, 0));
    
                        if (hierarchyInfo != null)
                        {
                            switch (hierarchyInfo.backgroundType)
                            {
                                case BackgroundType.Default:
                                    GUI.DrawTexture(gradientBackgroundPosition, new Texture2D(128, 128), ScaleMode.ScaleAndCrop, true, 0, color, Vector4.zero, 0);
                                    break;
                                case BackgroundType.Gradients:
                                    #region Draw Gradient
                                    Texture2D gradientTexture = (Resources.Load("GradientHorizontal") as Texture2D);
                                    GUI.DrawTexture(gradientBackgroundPosition, gradientTexture, ScaleMode.ScaleAndCrop, true, 0, color, Vector4.zero, 0);
                                    #endregion
                                    break;
                            }    
                        }
                        #endregion
                    }
    
                    if (hierarchyInfo.showIcon)
                    {
                        #region Draw Icon
    
                        Rect iconPosition = new Rect();
                        for (int i = 0; i < components.Length; i++)
                        {
                            try
                            {
                                hierarchyInfo.ComponentIcons[i].Component = components[i];
                            }
                            catch
                            {
                                hierarchyInfo.ComponentIcons = new ComponentIcon[128];
                            }
                        }
                        try
                        {
                            for (int i = 0; i < components.Length; ++i)
                            {
    
                                iconPosition = new Rect(
                                    new Vector2((selectionRect.width - selectionRect.height * (i + 1)) + (EditorGUIUtility.currentViewWidth - selectionRect.width) - Offset, selectionRect.y), 
                                    new Vector2(selectionRect.height, selectionRect.height));
    
                                //unity 기본 built-in 아이콘 가져오기
                                if(components[i]!=null)
                                {
                                    // Built in Icon
                                    _loadIconMethodInfo = typeof(EditorGUIUtility).GetMethod("LoadIcon", BindingFlags.Static | BindingFlags.NonPublic);
                                    Texture2D texture = _loadIconMethodInfo?.Invoke(null, new object[] { $"{components[i].GetType().Name} Icon" }) as Texture2D;
                                    
                                    //package 아이콘 가져오기
                                    if (texture == null)
                                    {
                                    
                                    }
                                
                                    //사용자 정의 컴포넌트
                                    if (texture == null)
                                    {
                                        string[] guid = AssetDatabase.FindAssets($"{components[i].GetType()}");

                                        foreach (var item in guid)
                                        {
                                            string path = AssetDatabase.GUIDToAssetPath(item);
                                            texture = AssetDatabase.GetCachedIcon(path) as Texture2D;
                                        }
                                    }
                                
                                    //실제로 그리기          
                                    if(texture != null)
                                        GUI.DrawTexture(iconPosition, texture);
                                }
                            }
                        }
                        catch
                        {
                            GUI.DrawTexture(iconPosition, new Texture2D(128, 128));
                        }
                
                        #endregion
                    }
                }
                
                if (HierarchyInfo.ShowLine)
                {
                    #region Draw Line
                
                    if (Mathf.Approximately(selectionRect.position.x, 60f) == false)
                    {
    
                        float count = (selectionRect.position.x - 60) / 14;
                        Transform parentTrm = parent;
                        
                        //HorizontalLine
                        Color parentSettingLineColor = GetLineColor(parent, new Color32(104, 104, 104, 255));

                        Vector2 positionOffset = new Vector2(-8.25f - 14, 8);
                        int lineSizeX = gameObject.transform.childCount != 0 ? 8 : 16;
                        
                        EditorGUI.DrawRect(new Rect(selectionRect.position + positionOffset, new Vector2(lineSizeX, 2)), parentSettingLineColor);
    
                        //VerticalLine
                        for (int i = 0; i < count; i++)
                        {
                            parentSettingLineColor = GetLineColor(parentTrm, new Color32(104, 104, 104, 255));
    
                            if (parent.GetChild(0) == gameObject.transform && i == 0)
                            {
                                EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),0), new Vector2(2, 8)), parentSettingLineColor);
                            }
                            else
                            {                                        
                                EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),-8), new Vector2(2, 16)), parentSettingLineColor);
                            }
                            try
                            {
                                parentTrm = parentTrm.parent;
                            }
                            catch
                            {
                                parentTrm = null;
                            }
                        }
                    }
    
                    #endregion
                }
    
                #region Draw Text
    
                Color textColor = hierarchyInfo != null ? hierarchyInfo.textColor : Color.white;
                GUIStyle textStyle = new GUIStyle() { normal = new GUIStyleState() { textColor = textColor } };
                
                if (gameObject.activeInHierarchy == false)
                {
                    textStyle.normal.textColor= Color.gray;
                }
                
                GUI.Box(new Rect(selectionRect.position + new Vector2(17.9f,0), selectionRect.size), obj.name, textStyle);
                
                #endregion
    
                #region Draw Toggle
    
                Rect togglePosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(selectionRect.height, selectionRect.height));
                gameObject.SetActive(GUI.Toggle(togglePosition, gameObject.activeSelf, ""));
    
                #endregion
            }
        }
        
        
        private static Color GetLineColor(Transform parentTrm, Color defaultColor)
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
#endif