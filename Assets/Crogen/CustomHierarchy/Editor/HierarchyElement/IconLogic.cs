using System.Reflection;
using Cinemachine.Editor;
using Cinemachine;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class IconLogic
    {
        private static MethodInfo _loadIconMethodInfo;

        public void Draw(HierarchyInfo hierarchyInfo, Component[] components, Rect selectionRect, float offset)
        {
            if (HierarchyInfo.showIcon)
            {
                Rect iconPosition = new Rect();
                if (hierarchyInfo.ComponentIcons != null && components.Length == hierarchyInfo.ComponentIcons.Length)
                {
                    for (int i = 0; i < components.Length; i++)
                    {
                        ComponentIcon componentIcon = hierarchyInfo.ComponentIcons[i];
                        
                        if(componentIcon == null)
                            componentIcon = new ComponentIcon();
                        
                        if(componentIcon.component != components[i])
                            componentIcon.name =components[i].GetType().Name;
                            
                        componentIcon.component = components[i];
                    }
                }
                else
                {
                    hierarchyInfo.ComponentIcons = new ComponentIcon[components.Length];
                }
                
                try
                {
                    int emptySpaceOffset = 0;
                    for (int i = 0; i < components.Length; ++i)
                    {
                        iconPosition = new Rect(
                            new Vector2((selectionRect.width - selectionRect.height * (i + 1 - emptySpaceOffset)) + (EditorGUIUtility.currentViewWidth - selectionRect.width) - offset, selectionRect.y), 
                            new Vector2(selectionRect.height, selectionRect.height));
                        
                        //unity 기본 built-in 아이콘 가져오기
                        if(components[i]!=null && hierarchyInfo.ComponentIcons[i].enable == true)
                        {
                            // Built in Icon
                            _loadIconMethodInfo = typeof(EditorGUIUtility).GetMethod("LoadIcon", BindingFlags.Static | BindingFlags.NonPublic);
                            Texture2D texture = _loadIconMethodInfo?.Invoke(null, new object[] { $"{components[i].GetType().Name} Icon" }) as Texture2D;
                            
                            //패키지 정의 컴포넌트
                            if (texture == null)
                            {
                                var item = MonoScript.FromMonoBehaviour(components[i] as MonoBehaviour);
                                string path = AssetDatabase.GetAssetPath(item);
                                MonoImporter monoImporter = AssetImporter.GetAtPath(path) as MonoImporter;
                                texture = monoImporter.GetIcon();
                                
                                if (texture == null)
                                {
                                    //사용자 정의 컴포넌트
                                    texture = AssetDatabase.GetCachedIcon(path) as Texture2D;
                                }
                            }
                        
                            //실제로 그리기          
                            if(texture != null)
                                GUI.DrawTexture(iconPosition, texture);
                        }
                        else
                        {
                            ++emptySpaceOffset;
                        }
                    }
                }
                catch
                {
                    GUI.DrawTexture(iconPosition, new Texture2D(128, 128));
                }
            }
        }
    }
}