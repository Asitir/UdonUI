using System.Collections;
using System.Collections.Generic;
using UdonUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
// _instance = UdonUI_Editor.UdonUI_EditorManager; 

namespace UdonUI_Editor
{
    public partial class UdonUI_Manager
    {
        private static UdonUI_Manager _instance;
        public static UdonUI_Manager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UdonUI_Manager();
                return _instance;
            }
        }
        public string udonUIver = "Ver0.23.1221";

        #region 常规变量
        private string PeivatePath
        {
            get
            {
                string[] guid = AssetDatabase.FindAssets(this.GetType().Name);
                string[] nowPathA = AssetDatabase.GUIDToAssetPath(guid[0]).Split('/');
                string nowPath = "";
                for (int i = 0; i < nowPathA.Length - 2; i++)
                    nowPath += (nowPathA[i] + "/");
                //Debug.Log("路径: " + AssetDatabase.GUIDToAssetPath(aaa[0]));
                return nowPath;
            }
        }
        public string Path_SamplePrefad { get { return PeivatePath + "SampleUI/SamplePrefad/"; } }
        public string Path_SampleObj { get { return PeivatePath + "SampleObject/"; } }
        public string Path_HandMotion { get { return PeivatePath + "SampleUI/HandMotion/"; } }
        public string Path_Audio { get { return PeivatePath + "Audio/"; } }
        public string Path_Prefad { get { return PeivatePath + "MainUI/Prefad/"; } }

        private GameObject _mainUIobj;
        private MainUI_Script _mainUI;
        private Chat _udonChat;
        private HandMotion_UI _saoUI;
        private Transform _vrcWorld;
        private MgConsole _mMgConsole;

        public GameObject mainUIobj { get { if (!_mainUIobj) {if(mainUI!=null) _mainUIobj = mainUI.gameObject; } return _mainUIobj; } }
        public MainUI_Script mainUI { get { if (!_mainUI) _mainUI = FindObjects<MainUI_Script>();  return _mainUI; } }
        public Chat udonChat { get { if (!_udonChat) _udonChat = FindObjects<Chat>(); return _udonChat; } }
        public HandMotion_UI saoUI { get { if (!_saoUI) _saoUI = FindObjects<HandMotion_UI>(); return _saoUI; } }
        public Transform vrcWorld { get { if (!_vrcWorld) _vrcWorld = FindObjects<VRCSceneDescriptor>().transform; return _vrcWorld; } }
        public MgConsole mgConsole { get { if (_mMgConsole == null) _mMgConsole = UdonUI_Manager.Instance.FindObjects<MgConsole>(); return _mMgConsole; }set { _mMgConsole = value; } }

        public T FindObjects<T>() where T: MonoBehaviour
        {
            var _mainUI1 = Resources.FindObjectsOfTypeAll<T>();
            foreach (var item in _mainUI1)
            {
                if (!EditorUtility.IsPersistent(item))
                    return item;
            }
            return null;
        }
        #endregion

        #region 事件
        public void Update(float _deltaTime)
        {
            if (_mainUI != null)
            {
                if (mainUI.vrcWorld == null)
                {//设定vrcworld
                    mainUI.vrcWorld = vrcWorld;
                    EditorUtility.SetDirty(mainUI);
                }
            }
        }

        public T LoadAssetAtPath<T>(string _path) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(_path);
        }

        //申请添加UdonUI按钮
        public void AddUdonUIButton(GameObject Target, EButtonType _type = EButtonType.Default)
        {
            M_AddUdonUIButton(Target);//添加UdonUI按钮
            ChackUdonUIButton_Update(false);

            if (_type == EButtonType.Slid)
                M_SetButtonToSlid(Target);
            else if(_type == EButtonType.Drag)
                SetUdonUIButtonEvent(mainUI.Button.Length - 1, 0, 32, mainUI.gameObject, "OnMoveWindow");
        }

        /// <summary>
        /// 为UdonUI按钮添加事件
        /// </summary>
        /// <param name="GameObjID">UdonUI按钮的ID，默认为最后一个</param>
        /// <param name="sj">ActionID（按下行为等）</param>
        /// <param name="Event">执行的事件类型 32为自定义事件</param>
        /// <param name="target">事件目标</param>
        /// <param name="eventName">事件内容</param>
        public void SetUdonUIButtonEvent(int GameObjID, int sj, int Event, Object target, string eventName = "")
            => M_AddWindowsMenu(GameObjID, mainUI, sj, Event, target, eventName);

        /// <summary>
        /// 增减UdonUI_Button后刷新
        /// </summary>
        /// <param name="_window">是否对增减数量弹窗提示</param>
        public void ChackUdonUIButton_Update(bool _window = true,bool debugRemove = true) => OnChackUdonUIButton_Update(_window, debugRemove);
        /// <summary>
        ///  增减UdonUI_触发器后刷新
        /// </summary>
        /// <param name="_window">是否对增减数量弹窗提示</param>
        public void UdonBoxColliderUpdate(bool _window = true) => OnUdonBoxColliderUpdate(_window);
        //其它公开函数
        public void PingObj(UnityEngine.Object _target)
        {
            Selection.activeObject = _target;
            EditorGUIUtility.PingObject(_target);
            SceneView.FrameLastActiveSceneView();
        }
        #endregion

        #region 本地函数
        private void M_AddUdonUIButton(GameObject _target)
        {
            GameObject[] Button_late, Button;
            MainUI_Script UdonUiButton = mainUI;
            Button_late = UdonUiButton.Button;
            Button = new GameObject[Button_late.Length + 1];
            for (int i = 0; i < Button.Length; i++)
            {
                if (i < Button_late.Length)
                    Button[i] = Button_late[i];
                else
                    Button[i] = _target;
            }
            UdonUiButton.Button = Button;
            Undo.RegisterCompleteObjectUndo(UdonUiButton, "按钮修改");
        }
        private void M_SetButtonToSlid(GameObject TargetObj)
        {
            //UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
            MainUI_Script UdonUiButton = UdonUI_Manager.Instance.mainUI;
            GameObject[] Buttons = new GameObject[0];
            //UdonUiButton.publicVariables.TryGetVariableValue("Button", out Buttons);
            Buttons = UdonUiButton.Button;
            int id = -1;
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i] == TargetObj)
                {
                    id = i;
                    break;
                }
            }
            if (id > -1)
            {//主逻辑
                Transform MainObj = null;
                ScrollRect m_ScrollRect = null;
                if (Buttons[id].transform.parent.GetComponent<ScrollRect>())
                {
                    m_ScrollRect = Buttons[id].transform.parent.GetComponent<ScrollRect>();
                    if (Buttons[id].transform.parent.Find("Viewport"))
                        MainObj = Buttons[id].transform.parent.Find("Viewport");
                }
                else if (Buttons[id].transform.parent.parent.GetComponent<ScrollRect>())
                {
                    m_ScrollRect = Buttons[id].transform.parent.parent.GetComponent<ScrollRect>();
                    if (Buttons[id].transform.parent.parent.Find("Viewport"))
                        MainObj = Buttons[id].transform.parent.parent.Find("Viewport");
                }

                if (MainObj == null)
                {
                    if (EditorUtility.DisplayDialog("警告", "        请选择‘Viewport’子级下的UdonUI按钮再创建滑动功能('Scroll View'子级)，如果‘Viewport’子级下没有UdonUI按钮则请手动创建", "我知道了", "关闭")) { }
                    return;
                }
                else
                {
                    for (int i = 0; i < MainObj.childCount; i++)
                    {
                        if (MainObj.GetChild(i).name == "SlidingWindows")
                        {
                            if (EditorUtility.DisplayDialog("警告", "        当前按钮已存在滑动功能组件，请勿重复添加", "我知道了", "关闭")) { }
                            return;
                        }
                    }
                }

                if (MainObj.Find("Content"))
                {
                    Transform MainObj2 = MainObj.Find("Content");
                    RectTransform Rect = MainObj2.GetComponent<RectTransform>();
                    Rect.pivot = new Vector2(0.5f, 0);
                    Rect.anchorMax = new Vector2(1, 0);
                    Rect.anchorMin = new Vector2(0, 0);

                    m_ScrollRect.horizontal = false;
                    if (m_ScrollRect.horizontalScrollbar)
                    {
                        Scrollbar s = m_ScrollRect.horizontalScrollbar;
                        m_ScrollRect.horizontalScrollbar = null;
                        s.gameObject.SetActive(false);
                    }
                    if (m_ScrollRect.verticalScrollbar)
                    {
                        RectTransform s = m_ScrollRect.verticalScrollbar.GetComponent<RectTransform>();
                        s.offsetMin = new Vector2(s.offsetMin.x, 0);

                    }
                    //m_ScrollRect.horizontalScrollbarSpacing = -20;
                    //m_ScrollRect.transform.GetChild(1).gameObject.SetActive(false);
                    //Rect.
                    //Rect.a
                    GameObject SlidingWindows = Object.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SlidingWindows.prefab", typeof(GameObject)));//创建UdonUI环境
                    Undo.RegisterCreatedObjectUndo(SlidingWindows, "创建Windows组件");
                    SlidingWindows.transform.SetParent(MainObj.transform);
                    SlidingWindows.transform.position = MainObj.transform.position;
                    SlidingWindows.name = "SlidingWindows";
                    SlidingWindows.SetActive(false);
                    //M_AddWindowsMenu(id, SlidingWindows, UdonUiButton);//配置该窗口
                    M_AddWindowsMenu(id, UdonUiButton, 0, 1, SlidingWindows);//配置该窗口

                    GameObject SlidingWindows_Out = Object.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SlidingWindows_Out.prefab", typeof(GameObject)));//创建UdonUI环境
                    Undo.RegisterCreatedObjectUndo(SlidingWindows_Out, "创建Windows组件");
                    SlidingWindows_Out.transform.SetParent(MainObj.transform);
                    SlidingWindows_Out.transform.position = MainObj.transform.position;
                    SlidingWindows_Out.name = "SlidingWindows_Out";
                    SlidingWindows_Out.SetActive(false);
                    M_AddWindowsMenu(id, UdonUiButton, 2, 2, SlidingWindows);//配置该窗口

                    //if (EditorUtility.DisplayDialog("通知", "        设定成功，当滑动此按钮时，滑动该组件画面\n\n      已自动向该按钮配置相关事件，请勿删除或谨慎修改此事件，否则可能会使‘滑动窗口’功能失效！！！", "我知道了", "关闭")) { }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("通知", "        未找到Content", "我知道了", "关闭")) { }
                    return;
                }


            }
            else
            {
                if (EditorUtility.DisplayDialog("警告", "        选中UdonUI下的Button", "我知道了", "关闭")) { }
            }
        }

        #region 检查UdonUI按钮新增状况
        static GameObject[] Button, Button_late;
        static Vector2[] MainEventNumber;//主要事件编号
        static Vector2[] MainEventNumber_late;
        static int[] ButtonType;//按钮类型
        static int[] ButtonType_late;
        static bool[] synbutton;//同步事件
        static bool[] synbutton_late;
        static int[] ButtonAction;//按钮行为
        static int[] ButtonEvent;//按钮事件
        static Animator[] MainAnimators;//动画机
        static string[] AnimaName;//动画 或 控制器名
        static GameObject[] TargetGameObject;//被控制的目标游戏对象
        static int[] MainEvents;//事件编号

        public void OnChackUdonUIButton_Start() {
            Button = mainUI.Button;
            MainEventNumber = mainUI.MainEventNumber;
            ButtonType = mainUI.ButtonType;
            synbutton = mainUI.synButton;
            Button_late = Button;
            MainEventNumber_late = MainEventNumber;
            ButtonType_late = ButtonType;
            synbutton_late = synbutton;
        }
        private void OnChackUdonUIButton_Update(bool _window,bool debugRemove) {
            //MainUIUdon.publicVariables.TryGetVariableValue("Button", out Button);//所有的按钮
            MainUI_Script MainUIUdon_E = mainUI;
            Button = MainUIUdon_E.Button;

            if (Button_late.Length < Button.Length)
            {
                MainEventNumber = MainUIUdon_E.MainEventNumber;
                ButtonType = MainUIUdon_E.ButtonType;
                synbutton = MainUIUdon_E.synButton;


                int ADDNumber = (int)(Button.Length - Button_late.Length);//新增按钮的总数
                                                                          //Debug.Log("增加了" + ADDNumber + "个按钮");
                                                                          //buttonta = ButtonType_late;

                #region 第一阶段,在新增按钮里面找到所有 合规，不合格，重复的按钮
                int reprtition = 0, qualified = 0, notqualified = 0;//重复，合格，不合格总量
                int[] _reprtition = new int[ADDNumber], _qualified = new int[ADDNumber], _notqualified = new int[ADDNumber];
                int[] _ButtonTyp = new int[ADDNumber];
                //遍历增加的按钮
                for (int i = 0; i < ADDNumber; i++)
                {
                    int ObjectID = Button_late.Length + i;//当前新增按钮的ID
                    if (CheckRepetition(ObjectID))
                    { //第一遍清洗  检查是否有重复
                        int ButtonID = ButtonInput(ObjectID); //第二遍清洗  检查是否合规  并且在检查出合规的同时输出合规对象ID
                        if (ButtonID > 0)
                        {
                            _qualified[qualified] = ObjectID;//新增按钮中所有合规的按钮ID
                            _ButtonTyp[qualified] = ButtonID;//将合格ID装载
                            qualified++;//合格的总数
                        }
                        else
                        {
                            _notqualified[notqualified] = ObjectID;//新增按钮中所有不合规的按钮ID
                            notqualified++;//不合格总数
                        }
                    }
                    else
                    {
                        _reprtition[reprtition] = ObjectID;//新增按钮中所有重复的按钮ID
                        reprtition++;//重复对象的总数
                    }
                }
                #endregion

                #region 第二阶段,制作弹窗提示以及应用合规按钮
                string DebugS = null;
                if (reprtition > 0)
                {
                    Debug.Log("重复按钮有" + reprtition + "个");
                    DebugS = "\n 重复按钮有" + reprtition + "个";
                }

                if (notqualified > 0)
                {
                    Debug.Log("不合格的按钮有" + notqualified + "个");
                    DebugS += "\n 不合格的按钮有" + notqualified + "个";
                }

                if (qualified > 0 && _window)//
                {
                    Debug.Log("已成功添加" + qualified + "个按钮");
                    DebugS += "\n 已成功添加" + qualified + "个按钮";
                }
                if (_window)
                    if (EditorUtility.DisplayDialog("通知", DebugS, "我知道了", "关闭")) { }
                //将所有合规按钮应用上
                GameObject[] qualified_obj = Button_late;
                Vector2[] Events = MainEventNumber;
                int[] ButtonTyp = ButtonType;
                bool[] sbutton = synbutton;
                if (qualified > 0)
                {
                    qualified_obj = new GameObject[qualified + Button_late.Length];
                    Events = new Vector2[qualified + Button_late.Length];
                    ButtonTyp = new int[qualified + Button_late.Length];
                    sbutton = new bool[qualified + Button_late.Length];
                    for (int i = 0; i < qualified_obj.Length; i++)
                    {
                        if (i < Button_late.Length)
                        {
                            qualified_obj[i] = Button_late[i];//保留旧的按钮
                            Events[i] = MainEventNumber[i];
                            ButtonTyp[i] = ButtonType[i];
                            sbutton[i] = synbutton[i];
                        }
                        else
                        {
                            int NewID = i - Button_late.Length;
                            qualified_obj[i] = Button[_qualified[NewID]];//添上新的按钮
                            if (i == 0) Events[i] = Vector2.zero;
                            else Events[i] = new Vector2(Events[i - 1].x + Events[i - 1].y, 0);//给予初始值
                            ButtonTyp[i] = _ButtonTyp[NewID];//添加上新ID
                            sbutton[i] = false;
                        }
                    }
                }
                MainUIUdon_E.Button = qualified_obj;
                MainUIUdon_E.MainEventNumber = Events;
                MainUIUdon_E.ButtonType = ButtonTyp;
                MainUIUdon_E.synButton = sbutton;


                Undo.RegisterCompleteObjectUndo(MainUIUdon_E, "按钮修改");
                Button_late = qualified_obj;
                MainEventNumber_late = Events;
                ButtonType_late = ButtonTyp;
                synbutton_late = synbutton;
                #endregion
            }//按钮增加时初始化缓存的记号

            //遍历按钮组 查找是否有按钮被删除
            for (int i = 0; i < Button.Length; i++)
            {
                if (Button[i] == null)
                {
                    //按钮减少 不定数量
                    MainEventNumber = MainUIUdon_E.MainEventNumber;
                    ButtonType = MainUIUdon_E.ButtonType;
                    synbutton = MainUIUdon_E.synButton;

                    int[] ID = new int[Button.Length];
                    int number = 0;

                    int[] RemoveID = new int[Button.Length];
                    int RemoveNumber = 0;

                    for (int it = 0; it < Button.Length; it++)
                    {
                        //确认空对象数量  检查用户当前保留了多少按钮
                        if (Button[it] != null)
                        {
                            ID[number] = it;//将保留下来的ID储存到数组里面
                            number++;//剩余按钮总量
                        }
                        else
                        {
                            RemoveID[RemoveNumber] = it;//将删除的ID储存到数组里面 
                            RemoveNumber++;//删除的按钮总量
                        }
                    }

                    if (debugRemove)
                        Debug.Log("删除了" + RemoveNumber + "个按钮");

                    GameObject[] Button2 = new GameObject[number];
                    Vector2[] MainEvent = new Vector2[number];
                    int[] buttontyp = new int[number];
                    bool[] sbutton = new bool[number];

                    if (number > 0)
                    {
                        Button2[0] = Button[ID[0]];
                        Vector2 _v2 = MainEventNumber[ID[0]];
                        MainEvent[0] = new Vector2(0, _v2.y);
                        buttontyp[0] = ButtonType[ID[0]];
                        sbutton[0] = synbutton[ID[0]];
                        for (int ic = 1; ic < number; ic++)
                        {
                            Button2[ic] = Button[ID[ic]];
                            _v2 = MainEventNumber[ID[ic]];
                            Vector2 _v2c = MainEvent[ic - 1];
                            MainEvent[ic] = new Vector2(_v2c.x + _v2c.y, _v2.y);
                            buttontyp[ic] = ButtonType[ID[ic]];
                            sbutton[ic] = synbutton[ID[ic]];
                        }

                        //开始整理事件
                        List<int> mAction = new List<int>();
                        List<int> mMainEvent = new List<int>();
                        List<Animator> mMainAnimators = new List<Animator>();
                        List<string> mAnimaName = new List<string>();
                        List<GameObject> mTargetGameObject = new List<GameObject>();
                        List<int> mMainEvents = new List<int>();

                        ButtonAction = MainUIUdon_E.Action;
                        ButtonEvent = MainUIUdon_E.MainEvent;
                        MainAnimators = MainUIUdon_E.MainAnimators;
                        AnimaName = MainUIUdon_E.AnimaName;
                        TargetGameObject = MainUIUdon_E.TargetGameObject;
                        MainEvents = MainUIUdon_E.MainEvents;

                        for (int ic = 0; ic < MainEvent.Length; ic++)
                        {
                            int _tLength = (int)MainEventNumber[ID[ic]].y;
                            int _sPos = (int)MainEventNumber[ID[ic]].x;
                            if (_tLength > 0)
                            {
                                for (int icc = 0; icc < _tLength; icc++)
                                {
                                    int _tID = _sPos + icc;//要获取的目标ID
                                    mAction.Add(ButtonAction[_tID]);
                                    mMainEvent.Add(ButtonEvent[_tID]);
                                    mMainAnimators.Add(MainAnimators[_tID]);
                                    mAnimaName.Add(AnimaName[_tID]);
                                    mTargetGameObject.Add(TargetGameObject[_tID]);
                                    mMainEvents.Add(MainEvents[_tID]);
                                }
                            }
                        }
                        MainUIUdon_E.Action = mAction.ToArray();
                        MainUIUdon_E.MainEvent = mMainEvent.ToArray();
                        MainUIUdon_E.MainAnimators = mMainAnimators.ToArray();
                        MainUIUdon_E.AnimaName = mAnimaName.ToArray();
                        MainUIUdon_E.TargetGameObject = mTargetGameObject.ToArray();
                        MainUIUdon_E.MainEvents = mMainEvents.ToArray();
                    }
                    else
                    {
                        MainUIUdon_E.Action = new int[0];
                        MainUIUdon_E.MainEvent = new int[0];
                        MainUIUdon_E.MainAnimators = new Animator[0];
                        MainUIUdon_E.AnimaName = new string[0];
                        MainUIUdon_E.TargetGameObject = new GameObject[0];
                        MainUIUdon_E.MainEvents = new int[0];
                    }

                    MainUIUdon_E.Button = Button2;
                    MainUIUdon_E.MainEventNumber = MainEvent;
                    MainUIUdon_E.ButtonType = buttontyp;
                    MainUIUdon_E.synButton = sbutton;


                    Button_late = Button2;
                    MainEventNumber_late = MainEvent;
                    ButtonType_late = buttontyp;
                    synbutton_late = sbutton;
                    Undo.RegisterCompleteObjectUndo(MainUIUdon_E, "按钮修改");
                    return;
                }
            }

        }

        #region 按钮
        /// <summary>
        /// 返回falsh就是有重复对象
        /// </summary>
        /// <param name="ObjID">输入按钮ID</param>
        static bool CheckRepetition(int ObjID)
        {
            bool pand = true;
            for (int i = 0; i < ObjID; i++)
            {
                if (Button[i] == Button[ObjID])
                {
                    i = ObjID + 1;
                    pand = false;
                }
            }
            return pand;
        }

        /// <summary>
        /// 判断追加物体的格式是否合法 true为合法
        /// </summary>
        /// <param name="number"> 追加对象的ID </param>
        static int ButtonInput(int number)
        {
            //Debug.Log(ButtonA.transform.GetChild(0).name);
            if (Button[number].transform.childCount >= 1)
            {
                if (Button[number].transform.GetChild(0).childCount > 3)
                {
                    if (Button[number].transform.GetChild(0).GetComponent<Slider>())
                        return 2;//判定为滑条
                }
                //else if (Button[number].transform.GetComponent<BoxCollider>())
                //{
                //    if (Button[number].transform.GetComponent<BoxCollider>().isTrigger)
                //        pand = 1;//判定为普通按钮
                //}
                //else
                //{
                //    pand = 1;//判定为普通按钮
                //}
            }

            return 1;
        }
        #endregion

        #endregion
        #region 检查UdonUI触发器增减
        static GameObject[] Button_BoxCollider, Button_late_BoxCollider;
        static Vector2[] MainEventNumber_BoxCollider;//主要事件编号
        static Vector2[] MainEventNumber_late_BoxCollider;
        //static int[] ButtonType_BoxCollider;//按钮类型
        //static int[] ButtonType_late_BoxCollider;
        static bool[] synbutton_BoxCollider;//同步事件
        static bool[] synbutton_late_BoxCollider;
        static int[] ButtonAction_BoxCollider;//按钮行为
        static int[] ButtonEvent_BoxCollider;//按钮事件
        static Animator[] MainAnimators_BoxCollider;//动画机
        static string[] AnimaName_BoxCollider;//动画 或 控制器名
        static GameObject[] TargetGameObject_BoxCollider;//被控制的目标游戏对象
        static int[] MainEvents_BoxCollider;//事件编号

        public void OnStartBoxCollider() {
            MainUI_Script MainUIUdon_E = mainUI;

            Button_BoxCollider = MainUIUdon_E.BoxColliderUdon;
            MainEventNumber_BoxCollider = MainUIUdon_E.MainEventNumber_BoxCollider;
            synbutton_BoxCollider = MainUIUdon_E.synBoxCollider;
            Button_late_BoxCollider = Button_BoxCollider;
            MainEventNumber_late_BoxCollider = MainEventNumber_BoxCollider;
            synbutton_late_BoxCollider = synbutton_BoxCollider;
        }
        private void OnUdonBoxColliderUpdate(bool _window)
        {
            MainUI_Script MainUIUdon_E = mainUI;
            Button_BoxCollider = MainUIUdon_E.BoxColliderUdon;

            if (Button_late_BoxCollider.Length < Button_BoxCollider.Length)
            {
                //MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber_BoxCollider", out MainEventNumber_BoxCollider);//主要事件编号
                //MainUIUdon.publicVariables.TryGetVariableValue("synBoxCollider", out synbutton_BoxCollider);//是否同步
                MainEventNumber_BoxCollider = MainUIUdon_E.MainEventNumber_BoxCollider;
                synbutton_BoxCollider = MainUIUdon_E.synBoxCollider;

                int ADDNumber = (int)(Button_BoxCollider.Length - Button_late_BoxCollider.Length);//新增按钮的总数
                                                                                                  //Debug.Log("增加了" + ADDNumber + "个按钮");
                                                                                                  //buttonta = ButtonType_late;

                #region 第一阶段,在新增按钮里面找到所有 合规，不合格，重复的按钮
                int reprtition = 0, qualified = 0, notqualified = 0;//重复，合格，不合格总量
                int[] _reprtition = new int[ADDNumber], _qualified = new int[ADDNumber], _notqualified = new int[ADDNumber];
                int[] _ButtonTyp = new int[ADDNumber];
                //遍历增加的按钮
                for (int i = 0; i < ADDNumber; i++)
                {
                    int ObjectID = Button_late_BoxCollider.Length + i;//当前新增按钮的ID
                    if (CheckRepetition_BoxCollider(ObjectID))
                    { //第一遍清洗  检查是否有重复
                        int ButtonID = ButtonInput_BoxCollider(ObjectID); //第二遍清洗  检查是否合规  并且在检查出合规的同时输出合规对象ID
                        if (ButtonID > 0)
                        {
                            _qualified[qualified] = ObjectID;//新增按钮中所有合规的按钮ID
                            _ButtonTyp[qualified] = ButtonID;//将合格ID装载
                            qualified++;//合格的总数
                        }
                        else
                        {
                            _notqualified[notqualified] = ObjectID;//新增按钮中所有不合规的按钮ID
                            notqualified++;//不合格总数
                        }
                    }
                    else
                    {
                        _reprtition[reprtition] = ObjectID;//新增按钮中所有重复的按钮ID
                        reprtition++;//重复对象的总数
                    }
                }
                #endregion

                #region 第二阶段,制作弹窗提示以及应用合规按钮
                if (reprtition > 0)
                {
                    Debug.Log("重复碰撞触发器有" + reprtition + "个");
                }

                if (notqualified > 0)
                {
                    Debug.Log("不合格的碰撞触发器有" + notqualified + "个");
                }

                if (qualified > 0)
                {
                    Debug.Log("已成功添加" + qualified + "个碰撞触发器");
                }
                //将所有合规按钮应用上
                GameObject[] qualified_obj = Button_late_BoxCollider;
                Vector2[] Events = MainEventNumber_BoxCollider;
                bool[] sbutton = synbutton_BoxCollider;
                if (qualified > 0)
                {
                    qualified_obj = new GameObject[qualified + Button_late_BoxCollider.Length];
                    Events = new Vector2[qualified + Button_late_BoxCollider.Length];
                    sbutton = new bool[qualified + Button_late_BoxCollider.Length];
                    for (int i = 0; i < qualified_obj.Length; i++)
                    {
                        if (i < Button_late_BoxCollider.Length)
                        {
                            qualified_obj[i] = Button_late_BoxCollider[i];//保留旧的按钮
                            Events[i] = MainEventNumber_BoxCollider[i];
                            sbutton[i] = synbutton_BoxCollider[i];
                        }
                        else
                        {
                            int NewID = i - Button_late_BoxCollider.Length;
                            qualified_obj[i] = Button_BoxCollider[_qualified[NewID]];//添上新的按钮
                            if (i == 0) Events[i] = Vector2.zero;
                            else Events[i] = new Vector2(Events[i - 1].x + Events[i - 1].y, 0);//给予初始值
                            sbutton[i] = false;
                        }
                    }
                }
                //MainUIUdon.publicVariables.TrySetVariableValue("BoxColliderUdon", qualified_obj);//应用到UdonUI环境
                //MainUIUdon.publicVariables.TrySetVariableValue("MainEventNumber_BoxCollider", Events);//
                ////MainUIUdon.publicVariables.TrySetVariableValue("ButtonType_x", ButtonTyp);//
                //MainUIUdon.publicVariables.TrySetVariableValue("synBoxCollider", sbutton);//
                MainUIUdon_E.BoxColliderUdon = qualified_obj;
                MainUIUdon_E.MainEventNumber_BoxCollider = Events;
                MainUIUdon_E.synBoxCollider = sbutton;


                Undo.RegisterCompleteObjectUndo(MainUIUdon_E, "按钮修改");
                Button_late_BoxCollider = qualified_obj;
                MainEventNumber_late_BoxCollider = Events;
                //ButtonType_late = ButtonTyp;
                synbutton_late_BoxCollider = synbutton_BoxCollider;
                #endregion
            }//按钮增加时初始化缓存的记号

            //遍历按钮组 查找是否有按钮被删除
            for (int i = 0; i < Button_BoxCollider.Length; i++)
            {
                if (Button_BoxCollider[i] == null)
                {
                    //MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber_BoxCollider", out MainEventNumber_BoxCollider);//主要事件编号
                    //MainUIUdon.publicVariables.TryGetVariableValue("synBoxCollider", out synbutton_BoxCollider);//是否同步
                    MainEventNumber_BoxCollider = MainUIUdon_E.MainEventNumber_BoxCollider;
                    synbutton_BoxCollider = MainUIUdon_E.synBoxCollider;

                    int[] ID = new int[Button_BoxCollider.Length];
                    int number = 0;

                    int[] RemoveID = new int[Button_BoxCollider.Length];
                    int RemoveNumber = 0;

                    for (int it = 0; it < Button_BoxCollider.Length; it++)
                    {
                        //确认空对象数量  检查用户当前保留了多少按钮
                        if (Button_BoxCollider[it] != null)
                        {
                            ID[number] = it;//将保留下来的ID储存到数组里面
                            number++;//剩余按钮总量
                        }
                        else
                        {
                            RemoveID[RemoveNumber] = it;//将删除的ID储存到数组里面
                            RemoveNumber++;//删除的按钮总量
                        }
                    }
                    Debug.Log("删除了" + RemoveNumber + "个碰撞触发器");

                    GameObject[] Button2 = new GameObject[number];
                    Vector2[] MainEvent = new Vector2[number];
                    bool[] sbutton = new bool[number];

                    if (number > 0)
                    {
                        Button2[0] = Button_BoxCollider[ID[0]];
                        Vector2 _v2 = MainEventNumber_BoxCollider[ID[0]];
                        //Debug.Log($"第一个范围:{_v2}\n当前ID：{ID[0]}");
                        MainEvent[0] = new Vector2(0, _v2.y);
                        sbutton[0] = synbutton_BoxCollider[ID[0]];
                        for (int ic = 1; ic < number; ic++)
                        {
                            Button2[ic] = Button_BoxCollider[ID[ic]];
                            _v2 = MainEventNumber_BoxCollider[ID[ic]];
                            Vector2 _v2c = MainEvent[ic - 1];
                            MainEvent[ic] = new Vector2(_v2c.x + _v2c.y, _v2.y);
                            sbutton[ic] = synbutton_BoxCollider[ID[ic]];
                        }

                        //开始整理事件
                        List<int> mAction = new List<int>();
                        List<int> mMainEvent = new List<int>();
                        List<Animator> mMainAnimators = new List<Animator>();
                        List<string> mAnimaName = new List<string>();
                        List<GameObject> mTargetGameObject = new List<GameObject>();
                        List<int> mMainEvents = new List<int>();

                        ButtonAction_BoxCollider = MainUIUdon_E.Action_BoxCollider;
                        ButtonEvent_BoxCollider = MainUIUdon_E.MainEvent_BoxCollider;
                        MainAnimators_BoxCollider = MainUIUdon_E.MainAnimators_BoxCollider;
                        AnimaName_BoxCollider = MainUIUdon_E.AnimaName_BoxCollider;
                        TargetGameObject_BoxCollider = MainUIUdon_E.TargetGameObject_BoxCollider;
                        MainEvents_BoxCollider = MainUIUdon_E.MainEvents_BoxCollider;

                        for (int ic = 0; ic < MainEvent.Length; ic++)
                        {
                            int _tLength = (int)MainEventNumber_BoxCollider[ID[ic]].y;
                            int _sPos = (int)MainEventNumber_BoxCollider[ID[ic]].x;
                            if (_tLength > 0)
                            {
                                for (int icc = 0; icc < _tLength; icc++)
                                {
                                    int _tID = _sPos + icc;//要获取的目标ID
                                    mAction.Add(ButtonAction_BoxCollider[_tID]);
                                    mMainEvent.Add(ButtonEvent_BoxCollider[_tID]);
                                    mMainAnimators.Add(MainAnimators_BoxCollider[_tID]);
                                    mAnimaName.Add(AnimaName_BoxCollider[_tID]);
                                    mTargetGameObject.Add(TargetGameObject_BoxCollider[_tID]);
                                    mMainEvents.Add(MainEvents_BoxCollider[_tID]);
                                }
                            }
                        }
                        MainUIUdon_E.Action_BoxCollider = mAction.ToArray();
                        MainUIUdon_E.MainEvent_BoxCollider = mMainEvent.ToArray();
                        MainUIUdon_E.MainAnimators_BoxCollider = mMainAnimators.ToArray();
                        MainUIUdon_E.AnimaName_BoxCollider = mAnimaName.ToArray();
                        MainUIUdon_E.TargetGameObject_BoxCollider = mTargetGameObject.ToArray();
                        MainUIUdon_E.MainEvents_BoxCollider = mMainEvents.ToArray();
                    }
                    else
                    {
                        MainUIUdon_E.Action_BoxCollider = new int[0];
                        MainUIUdon_E.MainEvent_BoxCollider = new int[0];
                        MainUIUdon_E.MainAnimators_BoxCollider = new Animator[0];
                        MainUIUdon_E.AnimaName_BoxCollider = new string[0];
                        MainUIUdon_E.TargetGameObject_BoxCollider = new GameObject[0];
                        MainUIUdon_E.MainEvents_BoxCollider = new int[0];
                    }

                    MainUIUdon_E.BoxColliderUdon = Button2;
                    MainUIUdon_E.MainEventNumber_BoxCollider = MainEvent;
                    MainUIUdon_E.synBoxCollider = sbutton;


                    Button_late_BoxCollider = Button2;
                    MainEventNumber_late_BoxCollider = MainEvent;
                    synbutton_late_BoxCollider = sbutton;
                    Undo.RegisterCompleteObjectUndo(MainUIUdon_E, "按钮修改");
                    return;
                }
            }
        }
        #region 碰撞触发器
        static bool CheckRepetition_BoxCollider(int ObjID)
        {
            bool pand = true;
            for (int i = 0; i < ObjID; i++)
            {
                if (Button_BoxCollider[i] == Button_BoxCollider[ObjID])
                {
                    i = ObjID + 1;
                    pand = false;
                }
            }
            return pand;
        }
        static int ButtonInput_BoxCollider(int number)
        {
            return 1;//始终合格
            int pand = 0;

            if (Button_BoxCollider[number].transform.childCount >= 1)
            {
                if (Button_BoxCollider[number].transform.GetChild(0).name == "UdonUI_Collider")
                    pand = 1;//判定为普通触发器
            }
            else
            {
                if (Button_BoxCollider[number].GetComponent<Collider>())
                {
                    Button_BoxCollider[number].GetComponent<Collider>().isTrigger = true;
                    Button_BoxCollider[number].layer = 13;
                    pand = 1;//判定为普通触发器
                }
            }

            return pand;
        }
        #endregion

        #endregion
        private void M_AddWindowsMenu(int GameObjID, MainUI_Script MainUI, int sj, int Event, Object target, string eventName = "")
        {
            if (GameObjID < 0)
            {
                GameObjID = MainUI.Button.Length - 1;
            }

            Vector2[] MainEventNumber = new Vector2[1];
            int[] Action = new int[1];
            int[] MainEvent = new int[1];
            Animator[] MainAnimators = new Animator[1];
            string[] AnimaName = new string[1];
            GameObject[] ALLObject = new GameObject[1];
            int[] MainEvents = new int[1];
            MainEventNumber = MainUI.MainEventNumber;
            Action = MainUI.Action;
            MainEvent = MainUI.MainEvent;
            MainAnimators = MainUI.MainAnimators;
            AnimaName = MainUI.AnimaName;
            ALLObject = MainUI.TargetGameObject;
            MainEvents = MainUI.MainEvents;


            MainEventNumber[GameObjID].y += 1;//增加事件量
            #region 修改事件量后保留事件内的变量
            int[] Action_Late = new int[Action.Length + 1];
            int[] MainEvent_Late = new int[Action_Late.Length];
            //----------------------------//
            Animator[] MainAnimators_late = new Animator[Action_Late.Length];//动画机
            string[] AnimaName_late = new string[Action_Late.Length];//动画机名
            GameObject[] ALLObject_late = new GameObject[Action_Late.Length];//物体对象
            int[] MainEvents_Late = new int[Action_Late.Length];//编号

            int SetID = MainEvent.Length;
            bool SteIDb = false;
            for (int i = 0; i < MainEvent.Length; i++)
            {
                if (i < (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y - 1)
                {//当前事件集末端ID以下
                    Action_Late[i] = Action[i];
                    MainEvent_Late[i] = MainEvent[i];
                    //----------------------------//
                    MainAnimators_late[i] = MainAnimators[i];
                    AnimaName_late[i] = AnimaName[i];
                    ALLObject_late[i] = ALLObject[i];
                    MainEvents_Late[i] = MainEvents[i];
                    //----------------------------//
                }
                else
                {//当前事件集末端ID以上
                    if (!SteIDb)
                    {
                        SetID = i;
                        SteIDb = true;
                    }
                    Action_Late[i + 1] = Action[i];
                    MainEvent_Late[i + 1] = MainEvent[i];
                    //----------------------------//
                    MainAnimators_late[i + 1] = MainAnimators[i];
                    AnimaName_late[i + 1] = AnimaName[i];
                    ALLObject_late[i + 1] = ALLObject[i];
                    MainEvents_Late[i + 1] = MainEvents[i];
                    //----------------------------//
                }
            }

            if (target is Animator _anim)
                MainAnimators_late[SetID] = _anim;
            if (target is GameObject _eventTarget)
                ALLObject_late[SetID] = _eventTarget;
            else if (target is Transform _eventTarget2)
                ALLObject_late[SetID] = _eventTarget2.gameObject;
            if (!string.IsNullOrEmpty(eventName))
                AnimaName_late[SetID] = eventName;
            Action_Late[SetID] = sj;
            MainEvent_Late[SetID] = Event;
            MainUI.Action = Action_Late;
            MainUI.MainEvent = MainEvent_Late;
            MainUI.MainAnimators = MainAnimators_late;
            MainUI.AnimaName = AnimaName_late;
            MainUI.TargetGameObject = ALLObject_late;
            MainUI.MainEvents = MainEvents_Late;

            #endregion
            for (int i = 0; i < MainEventNumber.Length; i++)
            {
                //将该变量后的所有变量向前推一位
                if (i > GameObjID)
                {
                    MainEventNumber[i].x += 1;
                }
            }
            MainUI.MainEventNumber = MainEventNumber;
            Undo.RegisterCompleteObjectUndo(MainUI, "事件数量修改");
        }


        #endregion
    }
    public enum EButtonType
    {
        Default,
        Slid,
        Drag
    }
}
