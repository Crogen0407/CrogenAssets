#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Crogen.ResourceManagementEditor;
using UnityEngine;
using UnityEditor;
using Crogen.CrogenEditorExtension.Editor;

public class ResourceManagementWindow : EditorWindow
{
    private static string _path = $"Assets/Crogen/ResourceManagementEditor/Editor/Resources";
    private static string _runTimePath = $"Assets/Crogen/ResourceManagementEditor/RunTime";
    private static string _resourceListSOPath = $"Assets/Resources";
    private static string _newEnumName = "";
    private static Vector2 materialListScroll = Vector2.zero;
    private static ResourceManagementWindowSaveData _resourceManagementWindowSaveData;
    private ResourceSO _currentSelectedResourceSO;
    private Rect viewRect;
    private Editor _currentSelectedResourceSOEditor;
    
    [MenuItem ("Crogen/ResourceManagement")]
    public static void  ShowWindow () {
        var window = EditorWindow.GetWindow(typeof(ResourceManagementWindow));
        window.Show();
        window.minSize = new Vector2(600f, 360f);
    }

    private void OnEnable()
    {
        //데이터 로드
        _resourceManagementWindowSaveData = EditorDataExtension.LoadLiquidCreateSOData<ResourceManagementWindowSaveData>($"{_path}/WindowSaveData.asset");
        _resourceListSOPath = EditorDataExtension.LoadLiquidCreatePath(_resourceListSOPath);
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
            if (_resourceManagementWindowSaveData == null)
            {
                GUILayout.EndArea();
                return;
            }

            //ResourceListSO에 할당하기
            _resourceManagementWindowSaveData.ResourceListSO = EditorGUILayout.ObjectField(
                _resourceManagementWindowSaveData.ResourceListSO, 
                typeof(ResourceListSO)) as ResourceListSO;
            
            if (_resourceManagementWindowSaveData.ResourceListSO != null)
            {
                GUILayout.Space(10);
                GUI.color = Color.green; //Save
                {
                    if (GUILayout.Button("Save"))
                    {
                        for (int i = 0; i < _resourceManagementWindowSaveData.ResourceListSO.resourceList.Count; ++i)
                        {
                            string path = AssetDatabase.GetAssetPath(_resourceManagementWindowSaveData.ResourceListSO.resourceList[i].GetInstanceID());
                            EditorUtility.SetDirty(_resourceManagementWindowSaveData.ResourceListSO.resourceList[i]);
                            AssetDatabase.RenameAsset(path, _resourceManagementWindowSaveData.ResourceListSO.resourceList[i].name);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                        EditorUtility.SetDirty(_resourceManagementWindowSaveData.ResourceListSO);
                        EditorUtility.SetDirty(_resourceManagementWindowSaveData);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }    
                }
                GUI.color = Color.white;

                if (GUILayout.Button("Generate Enum"))
                {
                    string[] names = new string[_resourceManagementWindowSaveData.ResourceListSO.resourceList.Count];
                    for (int i = 0; i < names.Length; ++i)
                        names[i] = _resourceManagementWindowSaveData.ResourceListSO.resourceList[i].name;
                    string path = EditorDataExtension.LoadLiquidCreatePath(_runTimePath);
                    path += "/ResourceType.cs";
                    EditorCodeFormatExtension.ScriptEnumFileFormat("ResourceType", names, path);

                    for (int i = 0; i < _resourceManagementWindowSaveData.ResourceListSO.resourceList.Count; ++i)
                    {
                        _resourceManagementWindowSaveData.ResourceListSO.resourceDictionary.Add((ResourceType)i, 
                            _resourceManagementWindowSaveData.ResourceListSO.resourceList[i]);
                    }
                }

                GUILayout.Space(10);
            }
        }
        GUILayout.EndArea();
        if (_resourceManagementWindowSaveData.ResourceListSO)
        {
            GUILayout.BeginArea(mainSettingAreaRect, new GUIStyle("helpBox"));
            {
                if (_resourceManagementWindowSaveData.ResourceListSO == null)
                {
                    GUI.color = Color.red;
                    {
                        GUILayout.Label("NO RESOURCELISTSO IN PATH!");
                    }
                    GUI.color = Color.white;
                    return;
                }
                
                GUILayout.BeginHorizontal();
                {
                    viewRect = new Rect(0, 0, 0, 55f * (_resourceManagementWindowSaveData.ResourceListSO.resourceList.Count + 1));
                    materialListScroll = GUI.BeginScrollView(new Rect(0,0,mainSettingAreaRect.width, mainSettingAreaRect.height), materialListScroll, viewRect, false, true);
                    {
                        Rect btnRect = new Rect(0, 0, mainSettingAreaRect.width-60f, 50f);
                        for (int i = 0; i < _resourceManagementWindowSaveData.ResourceListSO.resourceList.Count; ++i)
                        {
                            btnRect.position = new Vector2(0f, 55f * i);
                            DrawElementButton(_resourceManagementWindowSaveData.ResourceListSO.resourceList[i], btnRect, mainSettingAreaRect, elementFont);
                        }
            
                        btnRect.y = 55f * _resourceManagementWindowSaveData.ResourceListSO.resourceList.Count;
                        btnRect.width = mainSettingAreaRect.width - 25f;
                        btnRect.height = 25f;
                        GUI.color = Color.yellow;
                        if (GUI.Button(btnRect, "Add"))
                        {
                            ListLastPointAddNewElement(_resourceManagementWindowSaveData.ResourceListSO.resourceList);    
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
                if (_currentSelectedResourceSO != null)
                {
                    Editor.CreateCachedEditor(_currentSelectedResourceSO, null, ref _currentSelectedResourceSOEditor);
                    _currentSelectedResourceSOEditor.OnInspectorGUI();
                    GUILayout.Space(mainSettingAreaRect.height * 0.5f);
                    //DeleteBtn
                    GUI.color = Color.red;
                    {
                        if (GUILayout.Button("Delete"))
                        {
                            DeleteElementInArray(_currentSelectedResourceSO);
                        }    
                    }
                    GUI.color = Color.white;
                }
            }
            GUILayout.EndArea();
        }
    }

    private void DrawElementButton(ResourceSO resource, Rect btnRect, Rect areaRect, GUIStyle style)
    {
        GUILayout.BeginHorizontal();
        {
            if(_currentSelectedResourceSO == resource)
                GUI.color = Color.cyan;
            
            if (GUI.Button(btnRect, "", GUI.skin.button))
            {
                _currentSelectedResourceSO = resource;
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
            GUI.Label(btnRect, resource.name);

            if(_currentSelectedResourceSO == resource)
                GUI.color = Color.white;
        }
        GUILayout.EndHorizontal();
    }
    
    private void ListLastPointAddNewElement(List<ResourceSO> array)
    {
        var data = EditorDataExtension.CreateUniqueSOData<ResourceSO>($"{_resourceListSOPath}/ResourceData.asset");
        array.Add(data);
    }

    private void DeleteElementInArray(ResourceSO targetElement)
    {
        List<ResourceSO> list = _resourceManagementWindowSaveData.ResourceListSO.resourceList.ToList();
        list.Remove(targetElement);

        _resourceManagementWindowSaveData.ResourceListSO.resourceList = list;
        EditorDataExtension.DeleteSOData(targetElement);
    }
}
#endif