﻿using UnityEditor;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor.HierarchyElement
{
    public class LineLogic : ILogic
    {
        public void Draw(Rect selectionRect = new Rect(), HierarchyInfo hierarchyInfo = null, GameObject gameObject = null,
            Transform parent = null, Component[] components = null, float offset = 0)
        {
            if (HierarchyInfo.ShowLine)
            {
                if (Mathf.Approximately(selectionRect.position.x, 60f) == false)
                {
                    float count = (selectionRect.position.x - 60) / 14;
                    
                    //HorizontalLine
                    Color parentSettingLineColor = GetLineColor(parent, StyleEditor.DefaultLineColor);

                    Vector2 positionOffset = new Vector2(-8.25f - 14, 8);
                    int lineSizeX = gameObject.transform.childCount != 0 ? 8 : 16;
                    
                    EditorGUI.DrawRect(new Rect(selectionRect.position + positionOffset, new Vector2(lineSizeX, 2)), parentSettingLineColor);
    
                    //VerticalLine
                    for (int i = 0; i < count; i++)
                    {
                        parentSettingLineColor = GetLineColor(parent, StyleEditor.DefaultLineColor);
    
                        if (parent.GetChild(0) == gameObject.transform && i == 0)
                        {
                            EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),0), new Vector2(2, 8)), parentSettingLineColor);
                        }
                        else
                        {                                        
                            EditorGUI.DrawRect(new Rect(selectionRect.position + new Vector2(-8.25f - 14 * (i + 1),-8), new Vector2(2, 16)), parentSettingLineColor);
                        }
                        try
                        {
                            parent = parent.parent;
                        }
                        catch
                        {
                            parent = null;
                        }
                    }
                }
            }
            
        }
        
        private Color GetLineColor(Transform parentTrm, Color defaultColor)
        {
            Color color;
            try
            {
                color = parentTrm != null ? parentTrm.GetComponent<HierarchyInfo>().lineColor : defaultColor;
            }
            catch
            {
                color = defaultColor;
            }
            return color;
        }
    }
}