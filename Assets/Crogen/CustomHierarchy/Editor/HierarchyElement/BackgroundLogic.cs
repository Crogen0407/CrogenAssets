using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class BackgroundLogic : ILogic
    {
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0, float offset = 0)
        {
            if (hierarchyInfo != null)
            {
                if (hierarchyInfo.showBackground && hierarchyInfo.backgroundColor != Color.clear)
                {
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
                }
            }
        }
    }
}