using System;
using Crogen.CustomHierarchy;
using Crogen.CustomHierarchy.Editor;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif

public class CustomHierarchy : MonoBehaviour
{
    private static Vector2 offset = new Vector2(16.8f, 0);

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
            //MonoBehaviour
            var gameObject = (GameObject)obj;
            var parent = gameObject.transform.parent;
            var components = gameObject.GetComponents<Component>();
            
            //Hierarchy Visual
            int hierarchyIndex = ((int)selectionRect.position.y / 16) % 2; //0 : 연한 거 / 1 : 진한 거
            
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
            
            #region Draw Gradient

            Rect gradientBackgroundPosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(200, selectionRect.height));
            Texture2D gradientTexture = (Resources.Load("GradientHorizontal") as Texture2D);
            GUI.DrawTexture(gradientBackgroundPosition, gradientTexture, ScaleMode.ScaleAndCrop, true, 0, Color.red, Vector4.zero, 0);
            

            #endregion
            
            #endregion

            #region Draw Setting Icon

            
            

            #endregion
            
            #region Draw Icon

            Rect iconPosition = new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height));
            //
            // if (obj.name.StartsWith("="))
            // {
            //     Color iconBackgroundColor = Color.white;
            //     
            //     //Setting Icon
            //     Texture2D iconTexture = HierarchyIcon.LoadIcon("BuildSettings.Editor");
            //     
            //     //Icon Background
            //     if (Selection.activeGameObject == gameObject)
            //     {
            //         if (hierarchyIndex % 2 == 0)
            //         {
            //             iconBackgroundColor = new Color32(52,82,108,255);
            //
            //         }
            //         else
            //         {
            //             iconBackgroundColor = new Color32(49, 78, 104, 255);
            //         }
            //     }
            //     else
            //     {
            //         iconBackgroundColor = new Color32(56, 56, 56, 255);
            //         EditorGUI.DrawRect(iconPosition,new Color32(56,56,56,255));
            //     }
            //     EditorGUI.DrawRect(iconPosition, iconBackgroundColor);
            //     
            //     //Icon
            //     GUI.DrawTexture(iconPosition, iconTexture);
            // }
            // else
            // {
            Texture2D defaultIconTexture = HierarchyIcon.LoadIcon("d_GameObject Icon");
            GUI.DrawTexture(iconPosition, defaultIconTexture);
            // }

            #endregion
            
            #region Draw Line

            if (Mathf.Approximately(selectionRect.position.x, 60f) == false)
            {
                Color lineColor = new Color32(104,104,104,255);

                float count = (selectionRect.position.x - 60) / 14;
                for (int i = 0; i < count; i++)
                {
                    //HorizontalLine
                    if (gameObject.transform.childCount != 0)
                    {
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(8, 2)), lineColor);
                    }
                    else
                    {
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(16, 2)), lineColor);
                    }
                    
                    
                    //VerticalLine
                    if (parent.GetChild(0) == gameObject.transform && i == 0)
                    {
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),0), new Vector2(2, 8)), lineColor);
                    }
                    else
                    {                                        
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),-8), new Vector2(2, 16)), lineColor);
                    }
                }

            }

            #endregion

            #region Draw Text

            GUIStyle textStyle = null;
            textStyle = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = gameObject.activeSelf == true ? Color.white : Color.gray
                }
            };
            
            GUI.Box(new Rect(selectionRect.position + new Vector2(17.9f,0), selectionRect.size), obj.name, textStyle);
            
            #endregion

            #region Draw Toggle

            Rect togglePosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(selectionRect.height, selectionRect.height));
            gameObject.SetActive(GUI.Toggle(togglePosition, gameObject.activeSelf, ""));

            #endregion

            
        }
    }
}