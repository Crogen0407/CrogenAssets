using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Text : MonoBehaviour
{
    public GameObject target;
    public Texture2D texture2D;
    
    private void Awake()
    {
         texture2D = (Texture2D)EditorGUIUtility.IconContent("BuildSettings.Android").image;     
    }

    
}
