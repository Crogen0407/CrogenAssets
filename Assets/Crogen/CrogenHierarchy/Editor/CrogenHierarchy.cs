#if UNITY_EDITOR
using Crogen.CrogenHierarchy.Editor.HierarchyElement;
using UnityEngine;
using UnityEditor;

namespace Crogen.CrogenHierarchy.Editor
{
    [InitializeOnLoad]
    public class CrogenHierarchy
    {
        private static readonly float Offset = 3f;
        private static readonly DrawManager DrawManager;
        
        static CrogenHierarchy()
        {
            //ElementDraw
            DrawManager = new DrawManager();

            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyOnGUI;
        }
    
        ~CrogenHierarchy()
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
                
                DrawManager.Draw(
                    selectionRect, 
                    hierarchyInfo, 
                    gameObject, 
                    parent, 
                    components, 
                    hierarchySibling, 
                    hierarchyIndex, 
                    Offset);
            }
        }
    }
}
#endif