#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Crogen.CrogenEditorExtension.Editor;

public class AgentCustomEditorWindow : EditorWindow
{
    private static string _path = $"Assets/Crogen/AgentFSM/Resources";
    private static string _agentPath = $"Assets/Crogen/AgentFSM/Agent";
    private static string _resourceListSOPath = $"Assets/AgentFSM/Resources";
    private static string _newEnumName = "";
    private static Vector2 _materialListScroll = Vector2.zero;
    private static AgentCustomEditorWindowSaveData _agentCustomEditorWindowSaveData;
    private AgentScriptData _curSelectedAgentScriptData;
    private Rect _viewRect;
    
    [MenuItem("Crogen/AgentCustomEditorWindow")]
    private static void ShowWindow()
    {
        var window = GetWindow<AgentCustomEditorWindow>();
        window.titleContent = new GUIContent("AgentCustomEditorWindows");
        window.minSize = new Vector2(800f, 600f);
        window.Show();
    }
    
    private void OnEnable()
    {
        //데이터 로드
        _path = EditorDataExtension.LoadLiquidCreatePath(_path);
        _agentCustomEditorWindowSaveData = EditorDataExtension.LoadLiquidCreateSOData<AgentCustomEditorWindowSaveData>($"{_path}/WindowSaveData.asset");
        _resourceListSOPath = EditorDataExtension.LoadLiquidCreatePath(_resourceListSOPath);

        if (_agentCustomEditorWindowSaveData.agentScriptList == null)
        {
            _agentCustomEditorWindowSaveData.agentScriptList = new List<AgentScriptData>();
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        #region Init
    
        Rect settingSaveAndReturnAreaRect = new Rect(0, 0, position.size.x, 120f);
        
        Rect mainSettingAreaRect = new Rect(
            0, 
            settingSaveAndReturnAreaRect.height, 
              settingSaveAndReturnAreaRect.width/2, 
        position.height - settingSaveAndReturnAreaRect.height);
        
        Rect mainSettingAreaRectSecond = new Rect()
        {
            size = mainSettingAreaRect.size,
            position = new Vector2(mainSettingAreaRect.width, mainSettingAreaRect.y)
        };
        
        GUIStyle elementFont = new GUIStyle()
        {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };
        #endregion
        
        GUILayout.BeginArea(settingSaveAndReturnAreaRect, new GUIStyle("helpBox"));
        {
            //window 저장 SO가 없으면 아예 그리지 말것!
            if (_agentCustomEditorWindowSaveData == null)
            {
                GUILayout.EndArea();
                return;
            }
    
            if (_agentCustomEditorWindowSaveData.agentScriptList != null)
            {
                GUILayout.Space(10);
                GUI.color = Color.green; //Save
                {
                    if (GUILayout.Button("Save"))
                    {
                        for (int i = 0; i < _agentCustomEditorWindowSaveData.agentScriptList.Count; ++i)
                        {
                            string path = _agentCustomEditorWindowSaveData.agentScriptList[i].scriptPath;
                            
                            //Enum 파일 저자했고 스크립트 생성해서 만드는 거 해야 함
                            EditorCodeFormatExtension.ScriptEnumFileFormat(
                                _agentCustomEditorWindowSaveData.agentScriptList[i].enumScriptName, 
                                _agentCustomEditorWindowSaveData.agentScriptList[i].enumElements,
                                $"{path}{_agentCustomEditorWindowSaveData.agentScriptList[i].enumScriptName}.cs");
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                        EditorUtility.SetDirty(_agentCustomEditorWindowSaveData);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }    
                }
                GUI.color = Color.white;
    
                if (GUILayout.Button("Generate Enum"))
                {
                    string[] names = new string[_curSelectedAgentScriptData.enumElements.Length];
                    for (int i = 0; i < names.Length; ++i)
                        names[i] = _curSelectedAgentScriptData.enumElements[i];
                    string path = EditorDataExtension.LoadLiquidCreatePath(_agentPath);
                    path += $"/{_curSelectedAgentScriptData.enumScriptName}.cs";
                    EditorCodeFormatExtension.ScriptEnumFileFormat(_curSelectedAgentScriptData.enumScriptName, names, path);
                }
    
                GUILayout.Space(10);
            }
        }
        GUILayout.EndArea();
        if (_agentCustomEditorWindowSaveData.agentScriptList != null)
        {
            GUILayout.BeginArea(mainSettingAreaRect, new GUIStyle("helpBox"));
            {
                GUILayout.BeginHorizontal();
                {
                    _viewRect = new Rect(0, 0, 0, 55f * (_agentCustomEditorWindowSaveData.agentScriptList.Count + 1));
                    _materialListScroll = GUI.BeginScrollView(new Rect(0,0,mainSettingAreaRect.width, mainSettingAreaRect.height), _materialListScroll, _viewRect, false, true);
                    {
                        Rect btnRect = new Rect(0, 0, mainSettingAreaRect.width-60f, 50f);
                        for (int i = 0; i < _agentCustomEditorWindowSaveData.agentScriptList.Count; ++i)
                        {
                            btnRect.position = new Vector2(0f, 55f * i);
                            DrawElementButton(_agentCustomEditorWindowSaveData.agentScriptList[i], btnRect, mainSettingAreaRect, elementFont);
                        }
            
                        btnRect.y = 55f * _agentCustomEditorWindowSaveData.agentScriptList.Count;
                        btnRect.width = mainSettingAreaRect.width - 25f;
                        btnRect.height = 25f;
                        GUI.color = Color.yellow;
                        if (GUI.Button(btnRect, "Add"))
                        {
                            ListLastPointAddNewElement(_agentCustomEditorWindowSaveData.agentScriptList);    
                        }
            
                        GUI.color = Color.white;
                    }
                    GUI.EndScrollView();    
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
            
            GUILayout.BeginArea(mainSettingAreaRectSecond, new GUIStyle("helpBox"));
            {
                if (_curSelectedAgentScriptData != null)
                {
                    var so = new SerializedObject(_curSelectedAgentScriptData);
                    SerializedProperty arrProperty = so.FindProperty("enumElements");
                    EditorGUILayout.PropertyField(arrProperty);
                
                    GUILayout.Space(mainSettingAreaRect.height * 0.5f);
                    //DeleteBtn
                    GUI.color = Color.red;
                    {
                        if (GUILayout.Button("Delete"))
                        {
                            DeleteElementInArray(_curSelectedAgentScriptData);
                        }    
                    }
                    GUI.color = Color.white;    
                }
            }
            GUILayout.EndArea();
        }
    }
    
    private void DrawElementButton(AgentScriptData resource, Rect btnRect, Rect areaRect, GUIStyle style)
    {
        GUILayout.BeginHorizontal();
        {
            if (_curSelectedAgentScriptData != null)
            {
                if(_curSelectedAgentScriptData.mainScriptName == resource.mainScriptName)
                    GUI.color = Color.cyan;
        
                if (GUI.Button(btnRect, "", GUI.skin.button))
                {
                    _curSelectedAgentScriptData = resource;
                }

                GUI.color = Color.red;
                {
                    Rect deleteBtnRect = new Rect(btnRect.x + areaRect.width - 50f, btnRect.y + 15f, 20f, 20f);
                    if (GUI.Button(deleteBtnRect, EditorGUIUtility.IconContent("CrossIcon")))
                    {
                        DeleteElementInArray(resource);
                    }    
                }
                GUI.color = Color.white;
        
                btnRect.x += 100f;
                btnRect.width = 1000f;
                GUI.Label(btnRect, resource.enumScriptName);

                if(_curSelectedAgentScriptData.mainScriptName == resource.mainScriptName)
                    GUI.color = Color.white;    
            }
        }
        GUILayout.EndHorizontal();
    }
    
    private void ListLastPointAddNewElement(List<AgentScriptData> list)
    {
        var data = EditorDataExtension.CreateSOData<AgentScriptData>($"{_path}/NewAgentData.asset");

        var scriptPath = EditorDataExtension.LoadLiquidCreatePath("Assets/Crogen/AgentFSM/Code/{}");
        list.Add(data);
    }

    private void DeleteElementInArray(AgentScriptData targetData)
    {
        _agentCustomEditorWindowSaveData.agentScriptList.Remove(targetData);
    }
}
#endif