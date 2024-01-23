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
    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyOnGUI;
    }

    private static void HandleHierarchyOnGUI(int instanceID, Rect selectionRect)
    {
        FontStyle styleFont = FontStyle.Normal; //default font style

        var obj = EditorUtility.InstanceIDToObject(instanceID);
        if (obj != null)
        {
            if (obj.name.StartsWith("="))
            {
                GUI.color = Color.green;
            }
            else
            {
                GUI.color = Color.white;
            }    
        }
        
        // if (obj != null)
        // {
        //     var prefabType = PrefabUtility.GetPrefabAssetType(obj);
        //     
        //     Rect offsetRect = new Rect(selectionRect.position + new Vector2(17.2f, 0), selectionRect.size);
        //     EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
        //     {
        //         normal = new GUIStyleState() { textColor = Color.cyan },
        //         fontStyle = styleFont
        //     });
        // }
        
    }
}
