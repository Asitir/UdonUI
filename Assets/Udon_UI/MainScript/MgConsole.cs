
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class MgConsole : UdonSharpBehaviour
    {
        [HideInInspector]
        public Text displayContent;
        [HideInInspector]
        public MainUI_Script mainUI;
        [HideInInspector]
        public Chat chatSystem;

        [HideInInspector]
        public int[] actionID = new int[0];
        [HideInInspector]
        public string[] actionName = new string[0];
        private int nowSelectAction = 0;

        private string[] nowCommand = null;
        private int command = 0;
        private string nullChar = " [...] ";//还未输入的字符
        private string selecteColor = "#FF0000";//当前准备输入的字符颜色
        private string fnishColor = "#FFC125";//当前准备好的字符颜色
        private string sendMessage = "";//发送的信息

        private Transform dynamicLis_Enble;
        private GameObject pageUp, pageDown;
        private VRCPlayerApi[] allPlayers;
        private Text[] allText;
        private Text disLog, disLogT;
        private int targetPlayerID = -1;

        [HideInInspector]
        public string[] ActionEvenName = new string[6]
        {
            "按下",
            "长按",
            "抬起",
            "进入悬停",
            "持续悬停",
            "离开悬停",
        };//按钮事件名字

        private string[] shutupValue = new string[9]
        {
            "解除禁言",
            "禁言1分钟",
            "禁言3分钟",
            "禁言5分钟",
            "禁言10分钟",
            "禁言30分钟",
            "禁言60分钟",
            "禁言120分钟",
            "永久禁言",
        };
        private string[] paValue = new string[6]
        {
            "解除扩音",
            "解除静音",
            "对所有人扩音",
            "对所有人静音",
            "只对我相互扩音",
            "只对我静音",
        };

        private int[] shutupValue_value = new int[9]
        {
            0,
            1,
            3,
            5,
            10,
            30,
            60,
            120,
            99999,
        };

        private int[] teleportValue_value = new int[8]
        {
            0,
            25,
            50,
            100,
            300,
            500,
            700,
            1000
        };

        private void Start()
        {
            SetBackGroundWeight_Start();
            dynamicLis_Enble    = transform.GetChild(3).GetChild(0);
            pageUp   = transform.GetChild(3).GetChild(1).gameObject;
            pageDown = transform.GetChild(3).GetChild(2).gameObject;
            pageUp.SetActive(false);
            pageDown.SetActive(false);

            disLog = transform.GetChild(6).GetChild(1).GetComponent<Text>();
            disLogT = transform.GetChild(6).GetChild(2).GetComponent<Text>();

            allText = new Text[10];
            for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                allText[i] = dynamicLis_Enble.GetChild(i).GetChild(0).GetComponent<Text>();
        }

        private void Update()
        {
            DisPlayListButton(disType);//显示动态列表
            SetBackGroundWeight_Update();
        }

        #region 设定背景板
        float targetWeight = 0;
        float nowTime = 0;
        Vector3 defaultScale;
        RectTransform mTrans;
        RectTransform mTrans2;

        GameObject buttonA1;
        GameObject buttonA2;
        private void SetBackGroundWeight_Start()
        {
            mTrans = transform.GetChild(0).GetComponent<RectTransform>();
            mTrans2 = transform.GetChild(4).GetComponent<RectTransform>();

            buttonA1 = transform.GetChild(2).gameObject;
            buttonA2 = transform.GetChild(3).gameObject;
            defaultScale = Vector3.one;
        }

        private void SetBackGroundWeight_Update() {
            if (nowTime > 0)
            {
                float deltatime = Time.deltaTime;
                nowTime -= deltatime;
                defaultScale.x = Mathf.Lerp(defaultScale.x, targetWeight, 12 * deltatime);
                mTrans.localScale = defaultScale;
                mTrans2.localScale = defaultScale;
                buttonA1.transform.localPosition = Vector3.Lerp(buttonA1.transform.localPosition, Vector3.zero, 7 * deltatime);
                buttonA2.transform.localPosition = Vector3.Lerp(buttonA2.transform.localPosition, Vector3.right * 0.0257f, 7 * deltatime);
            }
        }
        private void SetBackGroundWeight(int _target)
        {
            if (_target == 0)
            {//回到最开始的姿态
                nowTime = 1;//动画持续一秒
                targetWeight = 1;
                buttonA1.SetActive(true);
                buttonA2.SetActive(false);

                buttonA1.transform.localPosition += Vector3.down * 0.1f;
            }
            else if(_target == 1)
            {//展开
                nowTime = 1;//动画持续一秒
                targetWeight = 1.9f;
                buttonA1.SetActive(false);
                buttonA2.SetActive(true);

                buttonA2.transform.localPosition += Vector3.down * 0.01f;
            }
        }
        #endregion

        #region 输入流程
        int level = 0;//流程进度
        int page = 0;
        int pageConst = 10;//每页对象数量
        void RunEvent(int _ButtonID)
        {
            int nowID = page * pageConst + _ButtonID;//当前选择的对象
            if(level == 1)
            {//最终设定，设定好后直接发送
                switch (command)
                {
                    case 1://禁言--->选择禁言时间
                        //发送的文本
                        sendMessage += ("@" + shutupValue_value[nowID]);
                        //显示的文本
                        nowCommand[3] = string.Format("<color={0}>{1}</color>", fnishColor, shutupValue_value[nowID]);
                        break;

                    case 2://传送--->选择范围
                           //发送的文本
                        sendMessage += ("@" + teleportValue_value[nowID]);
                        //显示的文本
                        nowCommand[5] = string.Format("<color={0}>{1}</color>", fnishColor, ((float)teleportValue_value[nowID] / 100).ToString());
                        break;

                    case 4://Action列表-->选择具体事件
                        //发送的文本
                        sendMessage += ("@" + nowID);
                        //显示的文本
                        nowCommand[5] = string.Format("<color={0}>{1}</color>", fnishColor, ActionEvenName[nowID]);
                        break;

                    case 6://给予管理--->选择玩家
                    //case 7://玩家扩音--->选择玩家
                    //发送的文本
                        if (nowID == 0)
                        {
                            sendMessage += "@0";
                            //显示的文本
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, "全体玩家");
                        }
                        else
                        {
                            sendMessage += ("@" + allPlayers[nowID - 1].playerId);
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, allPlayers[nowID - 1].displayName);
                        }
                        break;
                    case 7://扩音 设定扩音功能类型
                        //sendMessage += ("@" + nowID);
                        sendMessage += ($"@{nowID}@{Networking.LocalPlayer.playerId}");
                        //显示的文本
                        nowCommand[2] = string.Format("<color={0}>{1}</color>", fnishColor, paValue[nowID]);
                        break;
                }
                SwitchCommand(-1);//结束流程 发送指令
                disType = 0;//关闭动态列表
            }
            else if(level == 2)
            {
                switch (command)
                {
                    case 1://禁言--->选择玩家

                        //发送的文本
                        if (nowID == 0)
                        {
                            sendMessage += "@0";
                            //显示的文本
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, "全体玩家");
                        }
                        else
                        {
                            mainUI.checkErrorPo = $"玩家数:{allPlayers.Length}   当前ID:{nowID-1}";
                            sendMessage += ("@" + allPlayers[nowID - 1].playerId);
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, allPlayers[nowID - 1].displayName);
                        }
                        nowCommand[3] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        disType = 3;//切换动态列表至 禁言分钟数
                        break;
                    case 2://传送--->选择玩家(无全体玩家)
                           //发送的文本
                        mainUI.checkErrorPo = $"玩家数:{allPlayers.Length}   当前ID:{nowID}";
                        sendMessage += ("@" + allPlayers[nowID].playerId);
                        //显示的文本
                        nowCommand[3] = string.Format("<color={0}>{1}</color>", fnishColor, allPlayers[nowID].displayName);
                        nowCommand[5] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        disType = 6;//切换动态列表至 传送范围
                        break;
                    case 4://Action列表--->选择按钮
                           //发送的文本
                        nowSelectAction = actionID[nowID];
                        sendMessage += ("@" + nowSelectAction);
                        //显示的文本
                        nowCommand[3] = string.Format("<color={0}>{1}</color>", fnishColor, actionName[nowID]);
                        nowCommand[5] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        disType = 4;//切换动态列表至 事件名
                        break;
                    case 7://扩音列表--选择玩家
                           //Debug.LogWarning($"当前选择的玩家：{nowID}\n当前玩家数量：{allPlayers.Length}");
                        if (nowID == 0)
                        {
                            sendMessage += "@0";
                            //显示的文本
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, "全体玩家");
                            nowCommand[2] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        }
                        else
                        {
                            sendMessage += ("@" + allPlayers[nowID - 1].playerId);
                            //显示的文本
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, allPlayers[nowID - 1].displayName);
                            nowCommand[2] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        }

                        disType = 7;//切换动态列表至 扩音功能选择
                        break;
                }
                SetBackGroundWeight(1);
            }
            else if(level == 3)
            {
                switch (command)
                {
                    case 2://传送玩家至--->选择玩家
                           //发送的文本
                        if (nowID == 0)
                        {
                            sendMessage += "@0";
                            //显示的文本
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, "全体");
                        }
                        else
                        {
                            mainUI.checkErrorPo = $"玩家数:{allPlayers.Length}   当前ID:{nowID - 1}";
                            sendMessage += ("@" + allPlayers[nowID - 1].playerId);
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, allPlayers[nowID - 1].displayName);
                        }
                        nowCommand[3] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        disType = 5;//切换动态列表至 玩家列表(无全体玩家)
                        break;

                    case 4://Action列表--->选择玩家
                        //发送的文本
                        targetPlayerID = nowID;
                        if (nowID == 0)
                        {
                            sendMessage += "@0";
                            //显示的文本
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, "全体");
                        }
                        else
                        {
                            mainUI.checkErrorPo = $"玩家数:{allPlayers.Length}   当前ID:{nowID - 1}";
                            sendMessage += ("@" + allPlayers[nowID - 1].playerId);
                            nowCommand[1] = string.Format("<color={0}>{1}</color>", fnishColor, allPlayers[nowID - 1].displayName);
                        }
                        nowCommand[3] = string.Format("<color={0}>{1}</color>", selecteColor, nullChar);
                        disType = 2;//切换动态列表至 Action列表
                        break;
                }
                SetBackGroundWeight(1);
            }
            string _display = "";
            foreach (var _text in nowCommand)
                _display += _text;
            displayContent.text = _display;

            level--;
            page = 0;//结束后回到第0页
            displayInit = false;//动态列表显示初始化
        }
        #endregion

        #region 定义动态列表的绘制
        private bool displayInit = false;
        private int disType = 0;
        void DisPlayListButton(int _DisType) {
            int _nowDisPlay = 0;
            int pageLastConst = page * pageConst;//第几页

            switch (_DisType)
            {
                case 1:
                    #region 显示玩家列表
                    allPlayers = GetAllplayer();//实时更新玩家列表

                    _nowDisPlay = allPlayers.Length - pageLastConst + 1;//当前应该显示的按钮数量  增加一个“全体玩家”按钮
                    if (_nowDisPlay <= 0 && page > 0) page--;//当前玩家变动导致比之前页面更少

                    if (page == 0)
                    {
                        dynamicLis_Enble.GetChild(0).gameObject.SetActive(true);
                        allText[0].text = string.Format("<color=#77FF00>{0}</color>", "全体玩家");
                        for (int i = 1; i < dynamicLis_Enble.childCount; i++)
                        {
                            Transform _nowObj = dynamicLis_Enble.GetChild(i);
                            if (i < _nowDisPlay)
                            {//当前应该显示的
                                _nowObj.gameObject.SetActive(true);
                                allText[i].text = allPlayers[pageLastConst + i - 1].displayName;
                            }
                            else
                            {
                                _nowObj.gameObject.SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                        {
                            Transform _nowObj = dynamicLis_Enble.GetChild(i);
                            if (i < _nowDisPlay)
                            {//当前应该显示的
                                _nowObj.gameObject.SetActive(true);
                                allText[i].text = allPlayers[pageLastConst + i - 1].displayName;
                            }
                            else
                            {
                                _nowObj.gameObject.SetActive(false);
                            }
                        }
                    }
                    break;
                #endregion

                case 2:
                    #region 显示Action列表
                    _nowDisPlay = actionID.Length - pageLastConst;
                    if (!displayInit)
                    {
                        _nowDisPlay = actionName.Length - pageLastConst;
                        for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                        {
                            Transform _nowObj = dynamicLis_Enble.GetChild(i);
                            if (i < _nowDisPlay)
                            {//当前应该显示的
                                _nowObj.gameObject.SetActive(true);
                                allText[i].text = actionName[pageLastConst + i];
                            }
                            else
                            {
                                _nowObj.gameObject.SetActive(false);
                            }
                        }
                    }
                    break;
                #endregion

                case 3:
                    #region 禁言分钟显示
                    if (!displayInit)
                    {
                        _nowDisPlay = shutupValue.Length - pageLastConst;
                        for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                        {
                            Transform _nowObj = dynamicLis_Enble.GetChild(i);
                            if (i < _nowDisPlay)
                            {//当前应该显示的
                                _nowObj.gameObject.SetActive(true);
                                allText[i].text = shutupValue[pageLastConst + i];
                            }
                            else
                            {
                                _nowObj.gameObject.SetActive(false);
                            }
                        }
                    }
                    break;
                #endregion

                case 4:
                    #region Action事件执行名
                    if (!displayInit)
                    {
                        //得到这个事件集的所有事件
                        Vector2 MainEvent_m = mainUI.MainEventNumber[nowSelectAction];
                        //Action;//行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
                        int _EventsLength = (int)MainEvent_m.y;

                        for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                            dynamicLis_Enble.GetChild(i).gameObject.SetActive(false);

                        int _a = 0, _b = 0, _c = 0, _d = 0, _e = 0, _f = 0;
                        //if (_EventsLength == 1)
                        //{//如果只设置了一个按钮
                        //    int _findID = (int)MainEvent_m.x;
                        //    RunEvent(mainUI.Action[_findID]);//直接执行事件
                        //}
                        //else
                        {
                            for (int i = 0; i < _EventsLength; i++)
                            {
                                int _findID = (int)(MainEvent_m.x) + i;
                                switch (mainUI.Action[_findID])
                                {
                                    case 0:
                                        if (_a == 0)
                                        {
                                            dynamicLis_Enble.GetChild(0).gameObject.SetActive(true);
                                            allText[0].text = ActionEvenName[0];
                                            _a = 1;
                                        }
                                        break;
                                    case 1:
                                        if (_b == 0)
                                        {
                                            dynamicLis_Enble.GetChild(1).gameObject.SetActive(true);
                                            allText[1].text = ActionEvenName[1];
                                            _b = 1;
                                        }
                                        break;
                                    case 2:
                                        if (_c == 0)
                                        {
                                            dynamicLis_Enble.GetChild(2).gameObject.SetActive(true);
                                            allText[2].text = ActionEvenName[2];
                                            _c = 1;
                                        }
                                        break;
                                    case 3:
                                        if (_d == 0)
                                        {
                                            dynamicLis_Enble.GetChild(3).gameObject.SetActive(true);
                                            allText[3].text = ActionEvenName[3];
                                            _d = 1;
                                        }
                                        break;
                                    case 4:
                                        if (_e == 0)
                                        {
                                            dynamicLis_Enble.GetChild(4).gameObject.SetActive(true);
                                            allText[4].text = ActionEvenName[4];
                                            _e = 1;
                                        }
                                        break;
                                    case 5:
                                        if (_f == 0)
                                        {
                                            dynamicLis_Enble.GetChild(5).gameObject.SetActive(true);
                                            allText[5].text = ActionEvenName[5];
                                            _f = 1;
                                        }
                                        break;
                                }
                            }
                            int _add = _a + _b + _c + _d + _e + _f;
                            if (_add == 1)
                            {//如果只设置了一个按钮
                                int _findID = (int)MainEvent_m.x;
                                RunEvent(mainUI.Action[_findID]);//直接执行事件
                            }
                        }
                    }
                    break;
                #endregion

                case 5:
                    #region 显示玩家列表(无全体玩家)
                    allPlayers = GetAllplayer();//实时更新玩家列表

                    _nowDisPlay = allPlayers.Length - pageLastConst ;//当前应该显示的按钮数量
                    if (_nowDisPlay <= 0 && page > 0) page--;//当前玩家变动导致比之前页面更少
                    for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                    {
                        Transform _nowObj = dynamicLis_Enble.GetChild(i);
                        if (i < _nowDisPlay)
                        {//当前应该显示的
                            _nowObj.gameObject.SetActive(true);
                            allText[i].text = allPlayers[pageLastConst + i].displayName;
                        }
                        else
                        {
                            _nowObj.gameObject.SetActive(false);
                        }
                    }
                    break;
                #endregion

                case 6:
                    #region 传送范围
                    if (!displayInit)
                    {
                        _nowDisPlay = teleportValue_value.Length - pageLastConst;
                        for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                        {
                            Transform _nowObj = dynamicLis_Enble.GetChild(i);
                            if (i < _nowDisPlay)
                            {//当前应该显示的
                                _nowObj.gameObject.SetActive(true);
                                allText[i].text = ((float)teleportValue_value[pageLastConst + i] / 100) + "米";
                            }
                            else
                            {
                                _nowObj.gameObject.SetActive(false);
                            }
                        }
                    }
                    break;
                #endregion

                case 7:
                    #region 玩家音频控制
                    if (!displayInit)
                    {
                        _nowDisPlay = paValue.Length - pageLastConst;
                        for (int i = 0; i < dynamicLis_Enble.childCount; i++)
                        {
                            Transform _nowObj = dynamicLis_Enble.GetChild(i);
                            if (i < _nowDisPlay)
                            {//当前应该显示的
                                _nowObj.gameObject.SetActive(true);
                                allText[i].text = paValue[pageLastConst + i];
                            }
                            else
                            {
                                _nowObj.gameObject.SetActive(false);
                            }
                        }
                    }

                    break;
                    #endregion
            }


            //上下翻页按钮
            if (page > 0)
                pageUp.SetActive(true);
            else
                pageUp.SetActive(false);

            if (_nowDisPlay > 10)
                pageDown.SetActive(true);
            else
                pageDown.SetActive(false);

            displayInit = true;
        }

        VRCPlayerApi[] GetAllplayer()
        {
            int playerConst = VRCPlayerApi.GetPlayerCount();
            VRCPlayerApi[] players = new VRCPlayerApi[playerConst];
            VRCPlayerApi.GetPlayers(players);
            return players;
        }
        #endregion

        #region 一级指令
        private void SwitchCommand(int _id)
        {
            displayInit = false;//允许初始化显示列表
            command = _id;
            switch (command)
            {
                case -1://完成指令
                    string _display2 = "";
                    foreach (var _text in nowCommand)
                        _display2 += _text;
                    SetDisPlayLogT(_display2);
                    SetDisPlayLog(sendMessage);
                    //chatSystem.isFindPlayer = false;
                    //chatSystem.playerTargetID = targetPlayerID;
                    chatSystem.SendMG(sendMessage,false);//发送指令
                    //Debug.Log("指令: " + sendMessage);
                    sendMessage = "";
                    level = 0;//流程节点数量
                    SetBackGroundWeight(0);//设置背景宽度
                    break;
                case 0://取消指令
                    nowCommand = new string[1] { 
                    "等待输入指令"
                    };
                    sendMessage = "";
                    level = 0;//流程节点数量
                    SetBackGroundWeight(0);//设置背景宽度
                    break;
                case 1://禁言
                    nowCommand = new string[5] {
                        "将",
                        string.Format("<color={0}>{1}</color>",selecteColor,nullChar),
                        "禁言",
                        string.Format("<color={0}>{1}</color>",fnishColor,nullChar),
                        "分钟"
                    };
                    sendMessage = "shutup";
                    disType = 1;//显示玩家列表
                    level = 2;//流程节点数量
                    SetBackGroundWeight(1);//设置背景宽度
                    break;
                case 2://传送
                    nowCommand = new string[7] {
                        "将",
                        string.Format("<color={0}>{1}</color>",selecteColor,nullChar),
                        "传送至",
                        string.Format("<color={0}>{1}</color>",fnishColor,nullChar),
                        "的",
                        string.Format("<color={0}>{1}</color>",fnishColor,nullChar),
                        "米范围内"
                    };
                    sendMessage = "teleport";
                    disType = 1;//显示玩家列表
                    level = 3;//流程节点数量
                    SetBackGroundWeight(1);//设置背景宽度
                    break;
                case 3://列表显示
                    nowCommand = new string[0];
                    break;
                case 4://按钮事件
                    nowCommand = new string[7] {
                        "执行",
                        string.Format("<color={0}>{1}</color>",selecteColor,nullChar),
                        "玩家",
                        string.Format("<color={0}>{1}</color>",fnishColor,nullChar),
                        "的",
                        string.Format("<color={0}>{1}</color>",fnishColor,nullChar),
                        "功能"
                    };
                    sendMessage = "action";
                    disType = 1;//显示玩家列表
                    level = 3;//流程节点数量
                    SetBackGroundWeight(1);//设置背景宽度
                    break;
                case 5:
                    nowCommand = new string[0];
                    break;
                case 6:
                    nowCommand = new string[3] {
                        "将",
                        string.Format("<color={0}>{1}</color>",selecteColor,nullChar),
                        "设为管理员"
                    };
                    sendMessage = "setmg";
                    disType = 1;//显示玩家列表
                    level = 1;//流程节点数量
                    SetBackGroundWeight(1);//设置背景宽度
                    break;
                case 7:
                    nowCommand = new string[3] {
                        "玩家音频设置,将",
                        string.Format("<color={0}>{1}</color>",selecteColor,nullChar),
                        string.Format("<color={0}>{1}</color>",selecteColor,nullChar)
                    };
                    sendMessage = "pa";
                    disType = 1;//显示玩家列表
                    level = 2;//流程节点数量
                    SetBackGroundWeight(1);//设置背景宽度
                    break;
            }
            string _display = "";
            foreach (var _text in nowCommand)
                _display += _text;
            displayContent.text = _display;
        }
        #endregion

        #region 设定玩家、Action
        private void SetActionID() 
        {
            displayInit = false;//允许初始化显示列表
            int _id = mainUI.Button[mainUI.ActiveButton].transform.GetSiblingIndex();
            RunEvent(_id);
        }//发送序号
        private void PageMove(bool _isNest)
        {
            if (_isNest) page++;
            else page--;
            displayInit = false;//允许初始化显示列表
        }//翻页

        private void SetDisPlayLog(string _log)
        {
            disLog.text = _log;
        }
        private void SetDisPlayLogT(string _log)
        {
            disLogT.text = _log;
        }
        #endregion

        public void ResetCommand() => SwitchCommand(0);//取消指令
        public void ShutUp() => SwitchCommand(1);//禁言
        public void TeleportTo() => SwitchCommand(2);//传送
        public void DisPlayList() => SwitchCommand(3);//列表显示
        public void OnAction() => SwitchCommand(4);//按钮事件
        public void OnActionC() => SwitchCommand(5);//触发器事件
        public void SetMG() => SwitchCommand(6);//给予权限
        public void OnPa() => SwitchCommand(7);//扩音
        public void SendActionID() => SetActionID();//发送ActionID
        public void PageNext() => PageMove(true);
        public void PageUp() => PageMove(false);
        public void OnCheck() { mainUI.checkID++; }//检查当前脚本
    }
}
