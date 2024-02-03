using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class CustomHierarchySettingDataSO : ScriptableObject
    {
        //Background
        public BackgroundType backgroundType;
        public List<Color> backgroundColor;
        
        //Icon
        public List<bool> activeIcons;
        
        //Line 
        public List<Color> lineColor;
        
        //Text
        public List<Color> textColor;
    }
}