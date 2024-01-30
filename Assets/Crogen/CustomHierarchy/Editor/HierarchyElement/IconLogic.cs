﻿using System.Reflection;
using PlasticPipe.Server;
using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class IconLogic
    {
        private static MethodInfo _loadIconMethodInfo;

        public void Draw(HierarchyInfo hierarchyInfo, Component[] components, Rect selectionRect, float offset)
        {
            if (hierarchyInfo.showIcon)
            {
                Rect iconPosition = new Rect();
                for (int i = 0; i < components.Length; i++)
                {
                    try
                    {
                        hierarchyInfo.ComponentIcons[i].Component = components[i];
                    }
                    catch
                    {
                        hierarchyInfo.ComponentIcons = new ComponentIcon[128];
                    }
                }
                try
                {
                    for (int i = 0; i < components.Length; ++i)
                    {
                        iconPosition = new Rect(
                            new Vector2((selectionRect.width - selectionRect.height * (i + 1)) + (EditorGUIUtility.currentViewWidth - selectionRect.width) - offset, selectionRect.y), 
                            new Vector2(selectionRect.height, selectionRect.height));
                
                        //unity 기본 built-in 아이콘 가져오기
                        if(components[i]!=null)
                        {
                            // Built in Icon
                            _loadIconMethodInfo = typeof(EditorGUIUtility).GetMethod("LoadIcon", BindingFlags.Static | BindingFlags.NonPublic);
                            Texture2D texture = _loadIconMethodInfo?.Invoke(null, new object[] { $"{components[i].GetType().Name} Icon" }) as Texture2D;
                            
                            //package 아이콘 가져오기
                            if (texture == null)
                            {
                            
                            }
                        
                            //사용자 정의 컴포넌트
                            if (texture == null)
                            {
                                string path = AssetDatabase.GetAssetPath(components[i]);
                                Debug.Log(path);
                                MonoImporter monoImporter = AssetImporter.GetAtPath(path) as MonoImporter;

                                texture = monoImporter.GetIcon();
                            }
                        
                            //실제로 그리기          
                            if(texture != null)
                                GUI.DrawTexture(iconPosition, texture);
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