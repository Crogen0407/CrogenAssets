#if  UNITY_EDITOR
using System;
using Crogen.CrogenHierarchy.Editor;
using UnityEngine;

namespace Crogen.CrogenHierarchy
{
    [Serializable]
    public class ComponentIcon
    {
        [HideInInspector] public string name;
        [HideInInspector] public Component component;
        public bool enable = true;
    }
    
    [DisallowMultipleComponent]
    public class HierarchyInfo : MonoBehaviour
    {
        private CrogenHierarchySettingDataSO _hierarchySettingData;
    
        //Background
        public BackgroundType backgroundType = BackgroundType.Default;
        public Color backgroundColor = Color.clear;
    
        //Icon
        public ComponentIcon[] componentIcons;
    
        //Line
        public Color lineColor = new Color32(104,104,104,255);
    
        //Text
        public Color textColor = Color.white;
    
        public void Init()
        {
            int componentAmount = GetComponents<Component>().Length;
            for (int i = 0; i < componentAmount; ++i) UnityEditorInternal.ComponentUtility.MoveComponentDown(this);
        }
        
        private void Reset()
        {
            Init();
            if (_hierarchySettingData == null)
                _hierarchySettingData = Resources.Load<CrogenHierarchySettingDataSO>("HierarchySettingData");
            int parentIndex = GetParentIndex();

            #region Background


            backgroundType = _hierarchySettingData.backgroundType;
            
            try
            {
                backgroundColor = _hierarchySettingData.backgroundColor[parentIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                backgroundColor = _hierarchySettingData.backgroundColor.Count > 0 ? _hierarchySettingData.backgroundColor[^1] : Color.clear;
            }
    
            #endregion
    
            #region Line
    
            try
            {
                lineColor = _hierarchySettingData.lineColor[parentIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                lineColor = _hierarchySettingData.lineColor.Count > 0 ? _hierarchySettingData.lineColor[^1] : Color.clear;
            }
    
            #endregion
    
            #region Text
    
            try
            {
                textColor = _hierarchySettingData.textColor[parentIndex];
            }
            catch (ArgumentOutOfRangeException)
            {
                if (_hierarchySettingData.textColor.Count > 0)
                {
                    textColor = _hierarchySettingData.textColor[^1];
                }
                else
                {
                    textColor = Color.clear;
                }
            }
    
            #endregion
        }
    
        private int GetParentIndex()
        {
            Transform parent = transform.parent;
            int parentIndex = 0;
            while (parent != null)
            {
                parent = parent.parent;
                ++parentIndex;
            }
    
            return parentIndex;
        }
    }
}
#endif
