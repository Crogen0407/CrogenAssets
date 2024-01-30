using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class TextLogic
    {
        public void Draw(HierarchyInfo hierarchyInfo, GameObject gameObject, Rect selectionRect)
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