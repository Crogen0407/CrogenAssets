#if UNITY_EDITOR
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

        private static IconLogic _iconLogic = new IconLogic();
        private static BackgroundLogic _backgroundLogic = new BackgroundLogic();
        private static LineLogic _lineLogic = new LineLogic();
        private static TextLogic _textLogic = new TextLogic();
        private static ToggleLogic _toggleLogic = new ToggleLogic();
        private static HierarchyInfoSettingLogic _hierarchyInfoSettingLogic = new HierarchyInfoSettingLogic();
        private static DefaultBackgroundLogic _defaultBackgroundLogic = new DefaultBackgroundLogic();

        public static ILogic[] Logics = new ILogic[128];
        
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
                float hierarchySibling = (selectionRect.position.x - 60) / 14;
                int hierarchyIndex = (int)selectionRect.position.y/16;
                
                foreach (var logic in Logics)
                {
                    if(logic == null)
                        break;
                    logic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
                }
                _backgroundLogic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
                _iconLogic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
                _textLogic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
                _defaultBackgroundLogic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
                
                _lineLogic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
                _toggleLogic.Draw(selectionRect, hierarchyInfo, gameObject, parent, components, hierarchySibling, hierarchyIndex, Offset);
            }
        }
    }
}
#endif