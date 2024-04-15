using UnityEngine;

namespace Crogen.ObjectPooling
{
    public class CodeFormat : MonoBehaviour
    {
        public static string PoolingTypeFormat = 
        @"namespace Crogen.ObjectPooling
        {{
            public enum PoolType
            {{
                {0}
            }}
        }}";
    }

}