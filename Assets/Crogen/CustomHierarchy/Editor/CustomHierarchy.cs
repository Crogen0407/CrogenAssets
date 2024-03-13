#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Crogen.CustomHierarchy.Editor.HierarchyElement;
using UnityEngine;
using UnityEditor;

namespace Crogen.CustomHierarchy.Editor
{
    [InitializeOnLoad]
    public class CustomHierarchy
    {
        private static readonly float Offset = 3f;
        public static Element element;
        
        static CustomHierarchy()
        {
            //ElementDraw
            element = new Element();
            
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyOnGUI;
        }
    
        ~CustomHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HandleHierarchyOnGUI;
        }
    
        private static void HandleHierarchyOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj != null)
            {
                //MonoBehaviour
                var gameObject = (GameObject)obj;
                var parent = gameObject.transform.parent;
                var hierarchyInfo = gameObject.GetComponent<HierarchyInfo>();
                var components = gameObject.GetComponents<Component>();
                float hierarchySibling = (selectionRect.position.x - 60) / 14;
                int hierarchyIndex = (int)selectionRect.position.y/16;
                if(hierarchyInfo) hierarchyInfo.Init();
                element.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
            }
        }
    }
}
#endif