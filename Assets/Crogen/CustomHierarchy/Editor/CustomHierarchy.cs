using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
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
        var obj = EditorUtility.InstanceIDToObject(instanceID);

        Color backgroundColor = Color.white;
        Color textColor = Color.white;
        Texture2D texture = null;
        if (obj != null)
        {
            if (obj.name.StartsWith("="))
            {
                backgroundColor = new Color(0,1,1,0.5f);
                textColor = new Color(0,1,0.5f,1);
            }

            if (backgroundColor != Color.white)
            {
                Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50,
                    selectionRect.height);
                
                EditorGUI.DrawRect(bgRect, backgroundColor);
                EditorGUI.LabelField(new Rect(selectionRect.position + offset, selectionRect.size), obj.name, new GUIStyle()
                {
                    normal = new GUIStyleState()
                    {
                        textColor = textColor
                    },
                });
                if(texture != null)
                    EditorGUI.DrawPreviewTexture(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)), texture);
            }
            
        }
    }
}
