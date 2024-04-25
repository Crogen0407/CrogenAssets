using System.Collections.Generic;
using UnityEngine;

namespace Crogen.ResourceManagementEditor
{
    [CreateAssetMenu(fileName = "ResourceListSO", menuName = "Crogen/ResourceListData")]
    public class ResourceListSO : ScriptableObject
    {
        private void OnEnable()
        {
            if (resourceList == null)
                resourceList = new List<ResourceSO>();

            if (resourceDictionary == null)
                resourceDictionary = new Dictionary<ResourceType, ResourceSO>();
        }

        public List<ResourceSO> resourceList;
        public Dictionary<ResourceType, ResourceSO> resourceDictionary;
    }
}