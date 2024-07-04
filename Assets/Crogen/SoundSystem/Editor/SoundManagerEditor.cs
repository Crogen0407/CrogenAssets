using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    private SoundManager _soundManager;

    private void OnEnable()
    {
        _soundManager = target as SoundManager;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("SoundBase");
        GUILayout.BeginHorizontal();

        _soundManager.soundBase = EditorGUILayout.ObjectField(_soundManager.soundBase, typeof(PoolBaseSO), false) as SoundBaseSO;
        
        if (GUILayout.Button("New"))
        {
            var soundBase = ScriptableObject.CreateInstance<SoundBaseSO>();

            CreatePoolBaseAsset(soundBase);
        }

        if (_soundManager.soundBase != null)
        {
            if (GUILayout.Button("Clone"))
            {
                var poolBase = Instantiate(_soundManager.soundBase);
                CreatePoolBaseAsset(poolBase);
            }
        }
    
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        //PoolBase Serialize
        if (_soundManager.soundBase != null)
        {
            var poolBaseArrayObject = serializedObject.FindProperty("poolingPairs");
            EditorGUILayout.PropertyField(poolBaseArrayObject, true);
            serializedObject.ApplyModifiedProperties();
            _soundManager.soundBase.pairs = _soundManager.pairs;
            _soundManager.soundBase.PairInit();

            serializedObject.Update();
        }

        if (GUILayout.Button("Generate Enum"))
        {
            GeneratePoolingEnumFile();
            Debug.Log("Success!");
        }
    }
    
    private void GeneratePoolingEnumFile()
    {
        StringBuilder codeBuilder = new StringBuilder();
    
        foreach(SoundPair item in _soundManager.soundBase.pairs)
        {
            codeBuilder.Append(item.audioType);
            codeBuilder.Append(", ");
        }

        string code = string.Format(CodeFormat.PoolingTypeFormat, codeBuilder.ToString());

        string path = $"{Application.dataPath}/Crogen/ObjectPooling";

        File.WriteAllText($"{path}/PoolType.cs", code);

        EditorUtility.SetDirty(_soundManager.soundBase);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    
    private void CreatePoolBaseAsset(SoundBaseSO clonePoolBaseSo)
    {
        var uniqueFileName = AssetDatabase.GenerateUniqueAssetPath($"Assets/New Pool Base.asset");
        AssetDatabase.CreateAsset(clonePoolBaseSo, uniqueFileName);
        _soundManager.soundBase = clonePoolBaseSo;
        EditorUtility.SetDirty(_soundManager.soundBase);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}