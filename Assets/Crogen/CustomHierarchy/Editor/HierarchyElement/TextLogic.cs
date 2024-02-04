using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class TextLogic : ILogic
    {
        public TextLogic()
        {
            CustomHierarchy.LogicCallback += Draw;
        }
        
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0, float offset = 0)
        {
            if (hierarchyInfo != null && hierarchyInfo.textColor.a > 0)
            {
                Color textColor = hierarchyInfo != null ? hierarchyInfo.textColor : Color.white;
                GUIStyle textStyle = new GUIStyle() { normal = new GUIStyleState() { textColor = textColor } };
                
                if (gameObject.activeInHierarchy == false)
                {
                    textStyle.normal.textColor= Color.gray;
                }
                
                GUI.Box(new Rect(selectionRect.position + new Vector2(17.9f,0), selectionRect.size), gameObject.name, textStyle);      
            }
        }
    }
}