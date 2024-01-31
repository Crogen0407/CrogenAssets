using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class BackgroundLogic
    {
        public void Draw(HierarchyInfo hierarchyInfo, Rect selectionRect)
        {
            if (hierarchyInfo.showBackground && hierarchyInfo.backgroundColor != Color.clear)
            {
                //Hierarchy Visual
                int hierarchyIndex = ((int)selectionRect.position.y / 16) % 2; //0 : 연한 거 / 1 : 진한 거
            
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
            }
        }
    }
}