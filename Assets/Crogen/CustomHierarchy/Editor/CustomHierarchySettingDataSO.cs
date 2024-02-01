using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class CustomHierarchySettingDataSO : ScriptableObject
    {
        //Background
        public BackgroundType backgroundType;
        public List<Color> backgroundColor = new();
        
        //Icon
        public List<bool> activeIcons = new();
        
        //Line 
        public List<Color> lineColor = new();
        
        //Text
        public List<Color> textColor = new();

    }
}