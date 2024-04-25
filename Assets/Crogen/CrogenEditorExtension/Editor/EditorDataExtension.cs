using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenEditorExtension.Editor
{
    public class EditorDataExtension
    {
        public static T LoadLiquidCreateSOData<T>(string path) where T : ScriptableObject
        {
            var dataSO = AssetDatabase.LoadAssetAtPath<T>(path);
            if (dataSO == null)
            {
                dataSO = CreateSOData<T>(path);
            }
            return dataSO;
        } 
        public static T CreateSOData<T>(string path) where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance(typeof(T));
            AssetDatabase.CreateAsset(instance, path);
            
            var dataSO = AssetDatabase.LoadAssetAtPath<T>(path);
            
            EditorUtility.SetDirty(dataSO);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return dataSO;
        }
        public static void DeleteSOData<T>(T data) where T : ScriptableObject
        {
            string path = AssetDatabase.GetAssetPath(data);
            AssetDatabase.RemoveObjectFromAsset(data);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        public static string LoadLiquidCreatePath(string path)
        {
            string[] folders = path.Split('/');
            string pathStack = $"{folders[0]}";

            for (int i = 1; i < folders.Length; ++i)
            {
                if (!AssetDatabase.IsValidFolder($"{pathStack}/{folders[i]}"))
                {
                    AssetDatabase.CreateFolder(pathStack, folders[i]);
                }
                pathStack += $"/{folders[i]}";
            }
            return pathStack;
        }
        public static T CreateUniqueSOData<T>(string path) where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance(typeof(T));
            int count = 1;
            while (true)
            {
                var dataSO = AssetDatabase.LoadAssetAtPath<T>(path);
                if (dataSO != null)
                {
                    path = path.Replace($"_{count-1}", "");
                    path = path.Replace(".asset", $"_{count}.asset");
                    ++count;
                }
                else
                {
                    break;
                }
            }
            AssetDatabase.CreateAsset(instance, path);
            var finalData = AssetDatabase.LoadAssetAtPath<T>(path);

            EditorUtility.SetDirty(finalData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return finalData;
        }
    }
}