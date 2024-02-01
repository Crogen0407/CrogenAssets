using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    [Serializable]
    public class CustomHierarchySettingData
    {
        //Background
        public BackgroundType backgroundType;
        public List<Color> backgroundColor;
        
        //Icon
        public bool[] activeIcons;
        
        //Line 
        public List<Color> lineColor;
        
        //Text
        public List<Color> textColor;

    }
}