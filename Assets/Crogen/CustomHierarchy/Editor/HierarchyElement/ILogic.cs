using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public interface ILogic
    {
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null, Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0, float offset = 0);
    }
}