using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public delegate void LogicCallback(Rect selectionRect, HierarchyInfo hierarchyInfo, GameObject gameObject,
        Transform parent, Component[] components, float hierarchySibling, int hierarchyIndex, float offset);
}