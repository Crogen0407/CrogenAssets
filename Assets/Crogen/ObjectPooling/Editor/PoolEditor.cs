using System.IO;
using Crogen.ObjectPooling;
using UnityEditor;
using UnityEngine;

namespace Crogen.ObjectPooling
{
    [CustomEditor(typeof(PoolManager))]
    public class PoolEditor : Editor
    {
        private PoolManager _poolManager;
    
        private void OnEnable()
        {
            _poolManager = target as PoolManager;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Label("PoolBase");
            GUILayout.BeginHorizontal();

            _poolManager.poolBase = EditorGUILayout.ObjectField(_poolManager.poolBase, typeof(PoolBase)) as PoolBase;
            if (GUILayout.Button("New"))
            {
                var poolBase = ScriptableObject.CreateInstance<PoolBase>();

                CreatePoolBaseAsset(poolBase);
            }

            if (_poolManager.poolBase != null)
            {
                if (GUILayout.Button("Clone"))
                {
                    var poolBase = Instantiate(_poolManager.poolBase);
                    CreatePoolBaseAsset(poolBase);
                }
            }
        
            GUILayout.EndHorizontal();

            GUILayout.Space(20);

            if (_poolManager.poolBase != null)
            {
                _poolManager.poolingPairs = _poolManager.poolBase.pairs;
                var poolBaseArrayObject = serializedObject.FindProperty("poolingPairs");
                EditorGUILayout.PropertyField(poolBaseArrayObject, true);
                serializedObject.ApplyModifiedProperties();
                _poolManager.poolBase.pairs = _poolManager.poolingPairs;
                
                serializedObject.Update();
            }
        }

        private void CreatePoolBaseAsset(PoolBase clonePoolBase)
        {
            var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath($"Assets/New Pool Base.asset");
            AssetDatabase.CreateAsset(clonePoolBase, uniqueFileName);
            _poolManager.poolBase = clonePoolBase;
            AssetDatabase.SaveAssets();
        }
    }
}

