using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class LevelEditorWindow : EditorWindow
{
    #region 编辑器固有的变量 Transform
    private const string CONFIG_FOLDER_PATH = "/Resources/Data/Config/Level/";
    private const string ITEM_GROUP_CONFIG_PATN = "ItemGroupConfig/ItemGroupConfig.csv.txt";
    private const string ROAD_GROUP_CONFIG_PATH = "RoadGroupConfig/RoadGroupConfig.csv.txt";
    private const string SECTION_GROUP_CONFIG_PATH = "SectionConfig/SectionConfig.csv.txt";
    private const string LEVEL_GROUP_CONFIG_PATH = "LevelConfig/LevelConfig.csv.txt";
    private const string SCENERY_GROUP_CONFIG_PATH = "SceneryGroupConfig/SceneryGroupConfig.csv.txt";
    #endregion 路径结束

    #region 编辑器固有的变量 Transform

    static Transform _itemGroup;
    static Transform _roadGruop;
    static Transform _section;
    static Transform _level;
    static Transform _scenery;

    static Transform itemGroup
    {
        get
        {
            if (_itemGroup == null)
            {
                if (GameObject.Find("ItemGroup") != null)
                {
                    _itemGroup = GameObject.Find("ItemGroup").transform;
                }
                else
                {
                    GameObject GO = new GameObject();
                    GO.name = "ItemGroup";
                    _itemGroup = GO.transform;
                }
            }

            return _itemGroup;
        }

        set { _itemGroup = value; }
    }
    static Transform roadGruop
    {
        get
        {
            if (_roadGruop == null)
            {
                if (GameObject.Find("RoadGroup") != null)
                {
                    _roadGruop = GameObject.Find("RoadGroup").transform;
                }
                else
                {
                    GameObject GO = new GameObject();
                    GO.name = "RoadGroup";
                    _roadGruop = GO.transform;
                }
            }

            return _roadGruop;
        }

        set { _roadGruop = value; }
    }
    static Transform section
    {
        get
        {
            if (_section == null)
            {
                if (GameObject.Find("Section") != null)
                {
                    _section = GameObject.Find("Section").transform;
                }
                else
                {
                    GameObject GO = new GameObject();
                    GO.name = "Section";
                    _section = GO.transform;
                }
            }

            return _section;
        }

        set { _section = value; }
    }
    static Transform level
    {
        get
        {
            if (_level == null)
            {
                if (GameObject.Find("Level") != null)
                {
                    _level = GameObject.Find("Level").transform;
                }
                else
                {
                    GameObject GO = new GameObject();
                    GO.name = "Level";
                    _level = GO.transform;
                }
            }

            return _level;
        }

        set { _level = value; }
    }

    static Transform sceneryGroup
    {
        get
        {
            if (null == _scenery)
            {
                GameObject go = GameObject.Find("Scenery");
                if (null == go)
                {
                    go = new GameObject("Scenery");
                }

                _scenery = go.transform;
            }

            return _scenery;
        }
        set
        {
            _scenery = value;
        }
    }

    public static LevelEditorWindow window;


    #endregion


    //头部选项卡的数据
    enum EditorTabEnum { ItemEditor, RoadEditor, SectionEditor, SceneryEditor, LevelEditor };
    string[] editorTabStr = {"关卡编辑" };
	EditorTabEnum currentTab;

	//GUI  Scroll View 
	Vector2 itemEditorScrollPos;
	Vector2 roadEditorScrollPos;

    //LevelEditResLoad resLoader = null;

	[MenuItem ("Window/[赛尔号]关卡编辑器 #%e")]
	static void Init () {
		window = (LevelEditorWindow)EditorWindow.GetWindow (typeof (LevelEditorWindow), true,"关卡编辑");
	}

    #region 画界面:begin
    public void OnGUI()
    {
        DrawTab();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

    }
    //绘制头部选项卡
    void DrawTab()
    {
        //EditorGUILayout.BeginHorizontal();

        //if (GUILayout.Button("关卡编辑"))
        //{
        //    currentTab = EditorTabEnum.LevelEditor;
        //    InitLevelUIData();
        //}

        //EditorGUILayout.EndHorizontal();

        //string titleStr = editorTabStr[(int)currentTab];
        //GUILayout.Button(titleStr);

        //EditorGUILayout.Space();

        DrawLevelEditorGUI();
    }

    #region 关卡编辑器的GUI功能代码
    string levelID = "";
    string levelContent = "";
    string levelBackGround = "";
    List<string> levelIDList = new List<string>();

    string levelEnvID = string.Empty;
    //string[] levelEnvType = {"dark","green"};

    Vector2 levelScrollViewPos;

    void InitLevelUIData()
	{
        

	}

    #endregion 关卡编辑器

    /// <summary>
    /// 绘制关卡编辑的GUI
    /// </summary>
    void DrawLevelEditorGUI()
    {

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        levelID = EditorGUILayout.TextField("关卡ID", levelID);
        if (GUILayout.Button("加载"))
        {
            //PreViewLevel();
        }

        EditorGUILayout.Space();

        levelID = EditorGUILayout.TextField("关卡ID", levelID);
        levelEnvID = EditorGUILayout.TextField("背景ID", levelEnvID);
        levelBackGround = EditorGUILayout.TextField("场景背景", levelBackGround);
        levelContent = EditorGUILayout.TextField("前景配置", levelContent);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("保存"))
        {
            //CreateLevel(levelContent, levelBackGround);
            //WriteLevelData();
        }

        

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        levelScrollViewPos = EditorGUILayout.BeginScrollView(levelScrollViewPos);
        for (int k = 0; k < 6; ++k)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(k.ToString() ))
            {
                //PreViewLevel(levelIDList[k]);
            }
            if (GUILayout.Button("加载"))
            {
                //PreViewLevel();
            }
            if (GUILayout.Button("保存"))
            {
                //PreViewLevel();
            }
            if (GUILayout.Button("删除"))
            {
                //DeleteLevel(levelIDList[k]);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }
    #endregion 画界面:end
}
