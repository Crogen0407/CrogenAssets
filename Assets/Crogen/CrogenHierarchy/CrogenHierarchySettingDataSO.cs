#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenHierarchy.Editor
{
    public class CrogenHierarchySettingDataSO : ScriptableObject
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
#endif