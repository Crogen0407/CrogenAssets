using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class ToggleLogic : ILogic
    {
        public ToggleLogic()
        {
            CustomHierarchy.LogicCallback += Draw;
        }
        
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0, float offset = 0)
        {
            //if (Selection.gameObjects.Contains(gameObject))
            {
                Rect togglePosition = new Rect(new Vector2(32, selectionRect.y), new Vector2(selectionRect.height, selectionRect.height));
                gameObject.SetActive(GUI.Toggle(togglePosition, gameObject.activeSelf, ""));
            }
        }
    }
}