#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FolderNamingEditor : Editor
{
    [ContextMenu("Folder/CreateBeforeFolder")]
    public static void CreateSaveFolder()
    {
        
    }
}
#endif