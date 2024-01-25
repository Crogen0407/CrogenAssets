using System;
using Crogen.CustomHierarchy;
using Crogen.CustomHierarchy.Editor;
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

    private static void HandleHierarchyOnGUI(int instanceID, Rect selectionRect)
    {
        Texture2D texture = null;
        
        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (((GameObject)obj) != null)
        {
            var components = ((GameObject)obj).GetComponents<Component>();
            if (components != null)
            {
                Type t = null;
                string guid;
                long file;
                // foreach (var component in components)
                // {
                //     t = component.GetType();
                //     if (UnityEditor.AssetPreview.IsLoadingAssetPreview(component.GetInstanceID()))
                //     {
                //     }
                //
                //     AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out file);
                //     var path = AssetDatabase.GetAssetPath(components[0].GetInstanceID());
                //
                //     Debug.Log(texture);
                // }
            }

            texture = HierarchyIcon.LoadIcon("BuildSettings.Editor");
        }


        Color backgroundColor = Color.white;
        Color textColor = Color.white;
          
        if (obj != null)
        {
            
            if (obj.name.StartsWith("="))
            {
                backgroundColor = new Color(0,1,1,0.35f);
            }
            
            #region DrawLine

            if (Mathf.Approximately(selectionRect.position.x, 60f) == false)
            {
                Color lineColor = new Color32(104,104,104,255);

                float count = (selectionRect.position.x - 60) / 14;
                for (int i = 0; i < count; i++)
                {
                    EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),-8), new Vector2(2, 16)), lineColor);
                    EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(16, 2)), lineColor);
                }
            }

            #endregion
            
            if (backgroundColor != Color.white)
            {
                EditorGUI.DrawRect(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)),new Color32(56,56,56,255) );

                // Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                // Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50,
                //     selectionRect.height);
                // EditorGUI.DrawRect(bgRect, backgroundColor);
                
                if (texture != null)
                {
                    GUI.DrawTexture(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)), texture);
                }
            }
        }
    }
}