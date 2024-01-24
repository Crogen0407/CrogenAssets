using Crogen.CustomHierarchy;
using UnityEngine;
using Color = UnityEngine.Color;

#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif

public class CustomHierarchy : MonoBehaviour
{
    private static Vector2 offset = new Vector2(16.8f, 0);
    private static bool _moreDetailed;

    static CustomHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyOnGUI;
    }

    private static void Init()
    {
        _moreDetailed = HierarchyExtensionSetting.MoreDetailed;
    }

    private static void HandleHierarchyOnGUI(int instanceID, Rect selectionRect)
    {
        Init();
        
        var obj = EditorUtility.InstanceIDToObject(instanceID);
        Color backgroundColor = Color.white;
        Color textColor = Color.white;
        Texture2D texture = null;
          
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

                if (_moreDetailed == true)
                {
                    float count = (selectionRect.position.x - 60) / 14;
                    for (int i = 0; i < count; i++)
                    {
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),-8), new Vector2(2, 16)), lineColor);
                        EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(16, 2)), lineColor);
                    }
                }
                else
                {
                    EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,-8), new Vector2(2, 16)), lineColor);
                    EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14,8), new Vector2(16, 2)), lineColor);
                }
            }
            

            #endregion
               
            if (backgroundColor != Color.white)
            {
                GUI.color = new Color32(104,104,104,255);
                EditorGUI.DrawRect(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)),new Color32(104,104,104,255) );
                GUI.color = Color.white;

                // Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                // Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50,
                //     selectionRect.height);
                // EditorGUI.DrawRect(bgRect, backgroundColor);
                
                texture = (Texture2D)EditorGUIUtility.IconContent("d_Grid.PickingTool").image;
                texture.alphaIsTransparency = false;
                if (texture != null)
                {

                    GUI.DrawTexture(new Rect(selectionRect.position, new Vector2(selectionRect.height, selectionRect.height)), texture);
                }
            }
        }
    }
}