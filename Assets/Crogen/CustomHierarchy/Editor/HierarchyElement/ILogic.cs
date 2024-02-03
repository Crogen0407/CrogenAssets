using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public interface ILogic
    {
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null, Transform parent = null, Component[] components = null, float offset = 0);
    }
}