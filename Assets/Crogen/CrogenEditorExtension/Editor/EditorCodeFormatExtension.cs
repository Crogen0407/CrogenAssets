#if UNITY_EDITOR
using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenEditorExtension.Editor
{
    public class EditorCodeFormatExtension
    {
        private static string enumTypeFormat = 
        @"public enum {0}
{{
    {1}
}}";

        private static string soTypeFormat =
        @"using UnityEngine;

[CreateAssetMenu(menuName = ""SO/{0}"")]
public class {0} : {1}
{{
    {2}
}}";
        public static void ScriptEnumFileFormat(string enumName, string[] types, string path)
        {
            StringBuilder codeBuilder = new StringBuilder();

            for (int i = 0; i < types.Length; ++i)
            {
                codeBuilder.Append(types[i]);
                codeBuilder.Append(", ");
            }

            string code = string.Format(enumTypeFormat, enumName, codeBuilder);
            path = $"{Application.dataPath}{path.Replace("Assets", "")}";

            File.WriteAllText(path, code);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void ScriptSOFileFormat(string soName, Type parentType, string codeDescription, string path)
        {
            string code = string.Format(soTypeFormat, soName, parentType.ToString(), codeDescription);
            path = $"{Application.dataPath}{path.Replace("Assets", "")}";

            File.WriteAllText(path, code);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        public static void CustomScriptFileFormat(string scriptName, string parent, string codeDescription, string path)
        {
            string code = string.Format(soTypeFormat, scriptName, parent, codeDescription);
            path = $"{Application.dataPath}{path.Replace("Assets", "")}";

            File.WriteAllText(path, code);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
#endif