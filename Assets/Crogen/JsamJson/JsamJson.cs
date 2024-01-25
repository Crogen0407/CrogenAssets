using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Crogen.JsamJson
{
    public class JsamJson     
    {
        private static object _saveData;
        private static List<string> _filePaths = new List<string>();
    
        public static string Save<T>(T saveData, bool convertToByte = true, bool showDebugMessage = true) where T : class
        {
            string currentFilePath = CreateFilePath(typeof(T), _filePaths);
            _saveData = saveData;
            string jsonData = JsonUtility.ToJson(_saveData, true);
            FileStream fs = File.Create(currentFilePath);
            StreamWriter sw = new StreamWriter(fs);
            if (convertToByte)
            {
                //암호화
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

                string encodedJson = System.Convert.ToBase64String(bytes);
                
                sw.Write(encodedJson);
            }
            else
            {
                sw.Write(jsonData);
            }
            sw.Close();

            if (showDebugMessage)
            {
                Debug.Log($"Save Complete!\n--> <color=red>{currentFilePath}</color> <--");
            }
            return jsonData;
        }
    
        public static T Load<T>(bool fileIsByte = true) where T : class
        {
            string currentFilePath = CreateFilePath(typeof(T), _filePaths);
            T saveData = null;
            FileStream fs = new FileStream(currentFilePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            
            if (fileIsByte)
            {
                string jsonFromFile = sr.ReadToEnd();

                byte[] bytes = System.Convert.FromBase64String(jsonFromFile);

                string decodedJson = System.Text.Encoding.UTF8.GetString(bytes);

                saveData = JsonUtility.FromJson<T>(decodedJson);
            }
            else
            {
                saveData = JsonUtility.FromJson<T>(sr.ReadToEnd());
                
            }
            sr.Close();
            _saveData = saveData;
            
            return saveData;
        }
    
        private static string CreateFilePath(Type target, List<string> filePathList)
        {
            string currentFilePath = string.Empty;
            foreach (var filePath in filePathList)
            {
                if (filePath == target.Name)
                {
                    currentFilePath = filePath;
                    return currentFilePath;
                }
            }
            
            if (currentFilePath.Equals(string.Empty))
            {
                currentFilePath = Application.persistentDataPath + $"/{target.Name}.json";
                filePathList.Add(currentFilePath);
            }
    
            return currentFilePath;
        }
    }
}