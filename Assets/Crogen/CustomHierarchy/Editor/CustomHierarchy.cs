#if UNITY_EDITOR
using Crogen.CustomHierarchy.Editor.HierarchyElement;
using UnityEngine;
using UnityEditor;

namespace Crogen.CustomHierarchy.Editor
{
    [InitializeOnLoad]
    public class CustomHierarchy : UnityEditor.Editor
    {
        private static readonly float Offset = 3f;

        private static IconLogic _iconLogic = new IconLogic();
        private static BackgroundLogic _backgroundLogic = new BackgroundLogic();
        private static LineLogic _lineLogic = new LineLogic();
        private static TextLogic _textLogic = new TextLogic();
        private static ToggleLogic _toggleLogic = new ToggleLogic();
        
        static CustomHierarchy()
        {
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
                EditorGUIUtility.hierarchyMode = false;
                
                if (hierarchyInfo != null)
                {
                    _backgroundLogic.Draw(hierarchyInfo, selectionRect);
                    _iconLogic.Draw(hierarchyInfo, components, selectionRect, Offset);
                    _textLogic.Draw(hierarchyInfo, gameObject, selectionRect);
                }
                
                _lineLogic.Draw(gameObject, parent, selectionRect, Offset);
                _toggleLogic.Draw(gameObject, selectionRect);
            }
        }
    }
}
#endif