using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class IconLogic : ILogic
    {
        private CustomHierarchySettingDataSO _hierarchySettingData;

        private static MethodInfo _loadIconMethodInfo;

        public IconLogic()
        {
            if (_hierarchySettingData == null)
                _hierarchySettingData = Resources.Load<CustomHierarchySettingDataSO>("HierarchySettingData");
            
            CustomHierarchy.LogicCallback += Draw;
        }
        
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float hierarchySibling = 0, int hierarchyIndex = 0, float offset = 0)
        {
            if (hierarchyInfo != null && components != null)
            {
                if (true)
                {
                    Rect iconPosition = new Rect();
                    if (hierarchyInfo.ComponentIcons != null && components.Length == hierarchyInfo.ComponentIcons.Length)
                    {
                        for (int i = 0; i < components.Length; ++i)
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
                        //OnReset
                        hierarchyInfo.ComponentIcons = new ComponentIcon[components.Length];
                        for (int i = 0; i < hierarchyInfo.ComponentIcons.Length; ++i)
                        {
                            hierarchyInfo.ComponentIcons[i] = new ComponentIcon();
                            hierarchyInfo.ComponentIcons[i].enable = _hierarchySettingData.activeIcons[i];
                            if(_hierarchySettingData.activeIcons.Count == i + 1) break;
                        }
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
                            if(components[i] != null && hierarchyInfo.ComponentIcons[i].enable &&  hierarchyInfo != hierarchyInfo.ComponentIcons[i].component)
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
                                    if(monoImporter != null)
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
}