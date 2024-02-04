using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class DefaultBackgroundLogic : ILogic
    {
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0, float offset = 0)
        {
            if (hierarchyInfo == null)
            {
                int hierarchyOrderCount = hierarchyIndex % 2; //0 : 연한 거 / 1 : 진한 거
                Rect backgroundPosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(1000, selectionRect.height));                        
                Color backgroundColor = hierarchyOrderCount == 0 ? new Color32(65, 65, 65, 100) : new Color32(56, 56, 56, 100);
                EditorGUI.DrawRect(backgroundPosition, backgroundColor);
            }
        }
    }
}