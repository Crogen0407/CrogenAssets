using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class ToggleLogic
    {
        public void Draw(GameObject gameObject, Rect selectionRect)
        {
            Rect togglePosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(selectionRect.height, selectionRect.height));
            gameObject.SetActive(GUI.Toggle(togglePosition, gameObject.activeSelf, ""));
        }
    }
}