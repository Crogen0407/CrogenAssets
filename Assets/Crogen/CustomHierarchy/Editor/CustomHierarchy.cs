#if UNITY_EDITOR
using System;
using System.Reflection;
using PlasticPipe.Server;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[InitializeOnLoad]
public class CustomHierarchy : Editor
{
    private static Vector2 offset = new Vector2(4, 0);
    //private static int _visibleObjectCount;
    //private static bool _showSettingWindow = true;
    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyOnGUI;
        //EditorApplication.hierarchyChanged += HandleHierarchyInit;
    }

    private static MethodInfo loadIconMethodInfo;


    ~CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= HandleHierarchyOnGUI;
        //EditorApplication.hierarchyChanged -= HandleHierarchyInit;
    }

    private static Color GetLineColor(Transform parentTrm, Color defaultColor)
    {
        Color color = defaultColor;
        try
        {
            color = parentTrm != null ? parentTrm.GetComponent<HierarchyInfo>().lineColor : defaultColor;
        }
        catch (NullReferenceException e)
        {
            color = defaultColor;
        }

        return color;
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
            var objectIndex = gameObject.transform.GetSiblingIndex();
            var hierarchyInfo = gameObject.GetComponent<HierarchyInfo>();
            var parentHierarchyInfo = parent != null ? parent.GetComponent<HierarchyInfo>() : null;
            var components = gameObject.GetComponents<Component>();
            var currentItemPositionY = (int)(selectionRect.y / 16f);
          
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
                    Color backgroundColor = Color.white;
                    Rect backgroundPosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(1000, selectionRect.height));
        
                    if (hierarchyIndex == 0)
                    {
                        backgroundColor = new Color32(65, 65, 65, 100);
                    }
                    else
                    {
                        backgroundColor = new Color32(56, 56, 56, 100);
                    }
                    EditorGUI.DrawRect(backgroundPosition, backgroundColor);
            
                    Color color = hierarchyInfo != null ? hierarchyInfo.backgroundColor : Color.clear;
                    Rect gradientBackgroundPosition = new Rect(new Vector2(32, selectionRect.y), selectionRect.size + new Vector2(selectionRect.x, 0));

                    if (hierarchyInfo != null)
                    {
                        switch (hierarchyInfo.backgroundType)
                        {
                            case backgroundType.Default:
                                GUI.DrawTexture(gradientBackgroundPosition, new Texture2D(128, 128), ScaleMode.ScaleAndCrop, true, 0, color, Vector4.zero, 0);
                                break;
                            case backgroundType.Gradients:
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

                    int disableCount = 0;
                    try
                    {
                        for (int i = 0; i < components.Length; ++i)
                        {
                            Texture2D texture = null;

                            iconPosition = new Rect(
                                new Vector2((selectionRect.width - selectionRect.height * (i + 1 - disableCount)) + (EditorGUIUtility.currentViewWidth - selectionRect.width) - offset.x, selectionRect.y), 
                                new Vector2(selectionRect.height, selectionRect.height));

                            // 아이콘 가져오기
                            if (components[i].GetType() == typeof(HierarchyInfo))
                            {
                                texture = Resources.Load<Texture2D>("CustomHierarchy Icon");
                            }
                            else
                            {
                                // Built in Icon
                                loadIconMethodInfo = typeof(EditorGUIUtility).GetMethod("LoadIcon", BindingFlags.Static | BindingFlags.NonPublic);
                                texture = loadIconMethodInfo.Invoke(null, new object[] { $"{components[i].GetType().Name} Icon" }) as Texture2D;
                            }
                            
                            if(texture == null)
                                texture = EditorGUIUtility.FindTexture("cs Script Icon");
                    
                            //실제로 그리기                            
                            GUI.DrawTexture(iconPosition, texture);
                        }
                    }
                    catch
                    {
                        GUI.DrawTexture(iconPosition, new Texture2D(128, 128));
                    }
            
                    #endregion
                }
            }
            
            if (HierarchyInfo.showLine)
            {
                #region Draw Line
            
                if (Mathf.Approximately(selectionRect.position.x, 60f) == false)
                {

                    float count = (selectionRect.position.x - 60) / 14;
                    Transform parentTrm = parent;
                    Color parentSettingLineColor = Color.gray;
                
                    //HorizontalLine
                    parentSettingLineColor = GetLineColor(parent, new Color32(104, 104, 104, 255));
                    if (gameObject.transform.childCount != 0)
                    {
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(8, 2)), parentSettingLineColor);
                    }
                    else
                    {
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(16, 2)), parentSettingLineColor);
                    }

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
                        catch (NullReferenceException e)
                        {
                            parentTrm = null;
                        }
                    }
                }

                #endregion
            }

            #region Draw Text

            GUIStyle textStyle = null;
            Color textColor = hierarchyInfo != null ? hierarchyInfo.textColor : Color.white;
            
            textStyle = new GUIStyle() { normal = new GUIStyleState() { textColor = textColor } };

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
}
#endif