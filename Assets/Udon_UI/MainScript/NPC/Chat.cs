
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace UdonUI
{
    public class Chat : UdonSharpBehaviour
    {
        //public int ListConston = 0;
        #region
        Transform ALLplayer;
        PlayerCount MMyPlaerID;
        MainUI_Script MainUI;
        #endregion

        //[HideInInspector]
        public Transform chat_VRroot, chat_PCroot;
        public bool MasterIsMG = false;
        public bool pcUISwitch = true;

        public Sprite[] ChatBobble;//气泡类型
        public Sprite[] ChatAvatar;//头像
        public Sprite[] AvatarHead;//头像相框
        [HideInInspector] public string[] Managers = new string[0];//管理者们
        int AvatarHeadID = 0, BobbleID = 0, ChatAvatarID = 0;
        public Transform Main3DUI;
        //int Main3DUI_ID;
        //Vector3 pos;
        int MainUILength;
        //Transform Main;
        InputField MainInputField;
        GameObject MainInputFieldObj;
        Vector3 AvatarOffset_Right, AvatarOffset_Left, BobbleOffset_Right, BobbleOffset_Left, MainText_Right, MainText_Left;
        Vector3 TextRootPos, TextRootPos_Sub;
        Transform FrendChat;
        GameObject EnterPrompt;
        float ChatDelay = 0, deltatime;
        VRCPlayerApi LocalPlayer;
        Vector3 LockPos = Vector3.zero;
        bool IsChat = true, IsVR = false;
        Animator ChatAnim;
        public Transform AvatarChir;
        bool IsChir = false;
        int img = 0;
        //Text[] TextList;//所有的Text
        int MessageNumber = 0, MaxMessageNumber = 0;//当前信息数量   最高信息数量
        RectTransform TextRootParent;
        RectTransform TextRoot;//TextRoot 代替Text方法
        float MoveTextRoot = 0;
        float shutupTime = -1;
        //int MessegeNumber = 0;
        //List块属性
        float Hight = 40;//块高度
        [HideInInspector]
        public int ShotID = 0, AGIGM = 0;//权限,操作类型
        //float AvatarPosX = -65;//头像X坐标位置
        ////装饰
        ////名字
        //float BobblePosX = -50;//气泡X坐标位置 50
        //float TextLeft = 40, TextRight = 9;//Text内容
        bool Esc = false;
        bool isScript = false;//是否跳过权限判断


        public string[] HistorySpeak, LateHistorySpeak;
        bool IsInputPart = false;
        int MaxHistorySpeak = 0;
        int NowHistorySpeak = 0;
        const int Max = 10;
        public GameObject jyan;
        //bool isFindPlayer = false;
        //[HideInInspector]
        //public int playerTargetID = -1;
        //[HideInInspector]
        //public bool isFindPlayer = false;

        int LateInputID = 0;
        int InputID = 0;

        //[UdonSynced] string ChatName;
        string MainText, MainText2;

        //GameObject playerList,gmComList;
        bool playerListState = false, gmComListState = false;
        Text playerListContent;
        void Start()
        {
            //Main = transform.parent;

            ChatAnim = transform.GetChild(2).GetComponent<Animator>();
            //playerList = transform.GetChild(4).gameObject;
            //gmComList = transform.GetChild(5).gameObject;
            //playerListContent = playerList.transform.GetChild(1).GetComponent<Text>();
            //AvatarChir = transform.parent.parent.parent.GetChild(2);

            AvatarOffset_Right = AvatarOffset_Left = BobbleOffset_Right = BobbleOffset_Left = MainText_Right = MainText_Left = Vector3.zero;
            TextRootPos_Sub = Vector3.zero;
            TextRootPos_Sub.y = Hight;
            AvatarOffset_Right.x = 65;
            AvatarOffset_Right.y = 6.5f;
            ShotID = 0;

            AvatarOffset_Left.x = -65;
            AvatarOffset_Left.y = 6.5f;

            BobbleOffset_Right.x = 50;
            BobbleOffset_Right.y = 8;

            BobbleOffset_Left.x = -50;
            BobbleOffset_Left.y = 8;

            MainText_Right.x = -15.5f;
            MainText_Right.y = -6f;

            MainText_Left.x = 15.5f;
            MainText_Left.y = -6f;

            TextRootParent = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            TextRoot = TextRootParent.GetChild(0).GetComponent<RectTransform>();
            //TextRoot.anchoredPosition3D = new Vector3(10,9,8);

            MainUILength = Main3DUI.childCount;
            FrendChat = transform.GetChild(0);
            EnterPrompt = transform.GetChild(3).gameObject;
            MainInputFieldObj = transform.GetChild(1).gameObject;
            MainInputField = MainInputFieldObj.GetComponent<InputField>();
            //IsVR = Networking.LocalPlayer.IsUserInVR();
            MainInputFieldObj.SetActive(false);
            if (Networking.LocalPlayer.IsUserInVR())
            {
                FrendChat.gameObject.SetActive(true); 
                EnterPrompt.SetActive(false);
            }
            else
            {
                FrendChat.gameObject.SetActive(false);
                EnterPrompt.SetActive(true);
            }
            LocalPlayer = Networking.LocalPlayer;

            ALLplayer = GameObject.Find("/UdonUI_AGI/PlayerCount").transform;
            MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            MainUI.MainChat = transform.GetComponent<Chat>();
            MMyPlaerID = ALLplayer.GetComponent<PlayerCount>();

            InitializationTextList(20);//初始化最大聊天记录数量

//#if !UNITY_EDITOR
        GmFor();
            if (pcUISwitch)
            {
                if (!LocalPlayer.IsUserInVR())
                    SwitchRootToPC(true);
            }
//#endif
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.H)) Debug.Log(TextRootParent.localPosition);

            deltatime = Time.deltaTime;
            InputChat();
            UiAnima();
            Update_PlayerList();
            if (shutupTime > 0)
            {
                shutupTime -= deltatime;
                if (shutupTime <= 0)
                {
                    IsChat = true;
                    jyan.SetActive(false);
                    shutupTime = -1;
                }
            }
        }

        #region 旧方法，直接按键驱动
        void InputChat()
        {
            if (Networking.LocalPlayer.IsPlayerGrounded())
            {
                //Input.GetKeyDown(KeyCode.Return
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (!MainInputFieldObj.activeSelf)
                    {
                        //if (!Esc)
                        {
                            MainInputFieldObj.SetActive(true);
                            FrendChat.gameObject.SetActive(true);
                            EnterPrompt.SetActive(false);
                            MainInputField.ActivateInputField();
                        }
                    }
                    else
                    {
                        //if (IsChat)//是否禁言
                        {

                            if (!string.IsNullOrEmpty(MainInputField.text))
                            {
                                {
                                    SendData(MainInputField.text);
                                    MainInputField.ActivateInputField();
                                }
                            }
                            else
                            {
                                MainInputFieldObj.SetActive(false);
                                //输入为空
                            }
                        }
                        //else
                        //{
                        //    ChatAnim.Play("JY");
                        //    MainInputField.text = null;
                        //    ChatDelay = 1;
                        //    MainInputField.ActivateInputField();
                        //    MainInputFieldObj.SetActive(false);
                        //    //禁止
                        //}
                    }
                }

                if (MainInputFieldObj.activeSelf)
                {
                    if (Input.anyKeyDown)
                    {
                        IsInputPart = false;

                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (MaxHistorySpeak > 0)
                            {
                                IsInputPart = true;
                                NowHistorySpeak++;
                                NowHistorySpeak = Mathf.Min(NowHistorySpeak, MaxHistorySpeak - 1, NowHistorySpeak);
                                MainInputField.text = HistorySpeak[NowHistorySpeak];
                                MainInputField.ActivateInputField();
                            }
                        }
                        else if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (MaxHistorySpeak > 0)
                            {
                                IsInputPart = true;
                                NowHistorySpeak--;
                                NowHistorySpeak = Mathf.Max(NowHistorySpeak, -1, NowHistorySpeak);
                                if (NowHistorySpeak == -1) { MainInputField.text = null; }
                                else MainInputField.text = HistorySpeak[NowHistorySpeak];
                                MainInputField.ActivateInputField();
                            }
                        }
                    }


                    //LocalPlayer.TeleportTo(pos, LocalPlayer.GetRotation());
                    if (!IsChir)
                    {
                        if (!Networking.LocalPlayer.IsUserInVR())
                        {
//#if !UNITY_EDITOR
                            AvatarChir.position = Networking.LocalPlayer.GetPosition();
                            AvatarChir.rotation = Networking.LocalPlayer.GetRotation();
                            AvatarChir.gameObject.SetActive(true);
//#endif
                        }
                        IsChir = true;
                    }
                }
                else
                {
                    //pos = LocalPlayer.GetPosition();

                    if (IsChir)
                    {
                        if (!Networking.LocalPlayer.IsUserInVR())
                        {
//#if !UNITY_EDITOR

                            Networking.LocalPlayer.TeleportTo(AvatarChir.position, AvatarChir.rotation);
                            AvatarChir.gameObject.SetActive(false);
//#endif
                        }
                        IsChir = false;
                    }
                }

                if (FrendChat.gameObject.activeSelf)
                {
                    Vector3 OffsetP = Vector3.zero;
                    OffsetP.y = Input.GetAxis("Mouse ScrollWheel") * -100;
                    if (OffsetP != Vector3.zero) TextRootParent.localPosition += OffsetP;

                    if (Input.GetKeyDown(KeyCode.Tab)) { FrendChat.gameObject.SetActive(false); EnterPrompt.SetActive(true); }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Tab)) { FrendChat.gameObject.SetActive(true); EnterPrompt.SetActive(false); }
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    MainInputFieldObj.SetActive(false);
                    FrendChat.gameObject.SetActive(false);
                    EnterPrompt.SetActive(true);
                }

                if (ChatDelay > 0)
                {
                    ChatDelay -= deltatime;
                    if (ChatDelay < 0) ChatDelay = 0;
                }
            }
            else
            {
                //先生请不要尝试在半空中发送消息
            }

            //if (Input.GetKeyDown(KeyCode.Escape)) {
            //    Esc = !Esc;
            //    if (Esc) {
            //        MainInputFieldObj.SetActive(false);
            //        FrendChat.gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        MainInputFieldObj.SetActive(true);
            //        FrendChat.gameObject.SetActive(true);
            //    }
            //}
        }

        public void SendData(string _conste,bool _isEvent = false)
        {//执行数据
            MainUI.checkErrorPo = "发送数据_0";
            if (ChatDelay == 0)
            {
                //Networking.SetOwner(Networking.LocalPlayer, gameObject);
                MainText = _conste + "@" + ShotID;
                MainText2 = "";
                string[] newStr = MainText.Split('@');
                for (int i = 0; i < newStr.Length - 1; i++)
                {
                    if (i == newStr.Length - 2)
                        MainText2 += newStr[i];
                    else
                        MainText2 += newStr[i] + "@";
                }
                MainUI.checkErrorPo = "发送数据_输入检测";


                if (!IsInputPart)
                {
                    if (MaxHistorySpeak < Max)
                    {
                        MaxHistorySpeak++;
                        LateHistorySpeak = HistorySpeak;
                        HistorySpeak = new string[MaxHistorySpeak];
                        if (MaxHistorySpeak == 1)
                        {
                            HistorySpeak[0] = MainText2;
                        }
                        else
                        {
                            for (int i = 0; i < LateHistorySpeak.Length; i++)
                                HistorySpeak[i + 1] = LateHistorySpeak[i];
                            HistorySpeak[0] = MainText2;
                        }
                    }
                    else
                    {
                        for (int i = HistorySpeak.Length - 1; i > 0; i--)
                            HistorySpeak[i] = HistorySpeak[i - 1];
                        HistorySpeak[0] = MainText2;
                    }
                }
                NowHistorySpeak = -1;

                if (!IsChat)
                {
                    ChatAnim.Play("JY");
                    _conste = null;
                    ChatDelay = 1;
                    if (!_isEvent)
                    {
                        MainInputField.ActivateInputField();
                        MainInputFieldObj.SetActive(false);
                    }

                }
                else
                {
                    MainUI.checkErrorPo = "发送数据_获取网络ID";

                    int MyID = MMyPlaerID.MainID;

                    if (MyID > -1)
                    {//如果拥有我的同步对象
                        Player_Sny MyObj = ALLplayer.GetChild(MyID).GetComponent<Player_Sny>();
                        MyObj.IsChat = true;
                        MyObj.ChatText = MainText;
                        int ID = MyObj.InputID;
                        int iptid = (ID - 1000000000) / 100000000;//操作ID
                        if (iptid < 9) iptid++; else iptid = 0;
                        //---------------(是否持有)--------(操作ID:迭代)---(行为‘文本’等)---(行为:操作类型)----------(事件集ID‘头像相框、头像气泡类型’)
                        MyObj.SetInputID((1000000000) + (iptid * 100000000) + (5 * 1000000) + (AGIGM * 100000) + (AvatarHeadID * 10000 + ChatAvatarID * 100 + BobbleID));//修改我的同步信息发送到所有客户端  
                        if (ChatV(true)) return;
                        DisplayText(true);
                    }
                    //RequestSerialization();
                    if (!_isEvent)
                        MainInputField.text = null;
                    ChatDelay = 1;
                }

            }
            else
            {
                //ChatAnim.Play("Null");
                //null
                ChatAnim.Play("PF");
                //频繁
            }
            MainUI.checkErrorPo = "发送数据_1";

        }
        void SendMessge(string str) {
            int MyID = MMyPlaerID.MainID;

            if (MyID > -1)
            {//如果拥有我的同步对象
                Player_Sny MyObj = ALLplayer.GetChild(MyID).GetComponent<Player_Sny>();
                MyObj.IsChat = true;
                MyObj.ChatText = str;
                int ID = MyObj.InputID;
                int iptid = (ID - 1000000000) / 100000000;//操作ID
                if (iptid < 9) iptid++; else iptid = 0;

                //---------------(是否持有)--------(操作ID:迭代)---(行为‘文本’等)---(行为:操作类型)----------(事件集ID‘头像相框、头像气泡类型’)
                MyObj.SetInputID((1000000000) + (iptid * 100000000) + (5 * 1000000) + (AGIGM * 100000) + (AvatarHeadID * 10000 + ChatAvatarID * 100 + BobbleID));//修改我的同步信息发送到所有客户端  
                //DisplayText(true);
                //MyObj.Synced();//推送
            }

        }
        void DisplayText(bool Mine)
        {
            MessageNumber++;//迭代信息数量
            if (MessageNumber < MaxMessageNumber)
                TextRootParent.sizeDelta = new Vector2(TextRootParent.rect.width, MessageNumber * Hight);

            VRCPlayerApi Player = Networking.GetOwner(gameObject);

            TextRootPos -= TextRootPos_Sub;
            MoveTextRoot = 1;

            Transform ListParent = TextRoot.GetChild(0);
            ListParent.GetChild(0).localPosition = AvatarOffset_Right;
            //ListParent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AvatarHead[AvatarHeadID];//头像边框
            ListParent.GetChild(0).GetComponent<Image>().sprite = ChatAvatar[ChatAvatarID]; //头像
                                                                                            //ListParent.GetChild(0).GetComponent<Image>().material.SetTexture("_Detail", AvatarHead[MMyPlaerID.MainID]);
            ListParent.GetChild(1).GetComponent<Text>().text = Networking.LocalPlayer.displayName;
            ListParent.GetChild(1).GetComponent<Text>().alignment = TextAnchor.LowerRight;
            ListParent.GetChild(2).localRotation = Quaternion.Euler(0, 180, 0);
            ListParent.GetChild(2).localPosition = BobbleOffset_Right;
            ListParent.GetChild(2).GetComponent<Image>().sprite = ChatBobble[BobbleID];
            ListParent.GetChild(3).localPosition = MainText_Right;
            ListParent.GetChild(3).GetComponent<Text>().text = MainText;//信息内容
            Vector3 Offset = Vector3.zero;
            Offset.y = Hight;//最后消除的高度

            for (int i = 1; i < TextRoot.childCount; i++)
                TextRoot.GetChild(i).localPosition += Offset;//将所有text上推

            //介入信息
            TextRoot.GetChild(0).localPosition = new Vector3(0, Hight / 2, 0);
            TextRoot.GetChild(0).gameObject.SetActive(true);
            TextRoot.GetChild(0).SetSiblingIndex(TextRoot.childCount - 1);

        }

        #endregion
        #region ste
        string ActionEventName(int _id)
        {
            switch (_id)
            {
                case 0:
                    return "按下";
                case 1:
                    return "长按";
                case 2:
                    return "抬起";
                case 3:
                    return "进入悬停";
                case 4:
                    return "持续悬停";
                case 5:
                    return "离开悬停";
                default:
                    return "错误";
            }
        }
        string ActioncEventName(int _id)
        {
            if(_id == 0)
                return "进入";
            else
                return "退出";
        }

        void SetPaOn(VRCPlayerApi _player)
        {
            _player.SetVoiceDistanceNear(1000.0f);
            _player.SetVoiceDistanceFar(1000000.0f);
            //_player.SetVoiceVolumetricRadius(1000.0f);

        }
        void SetPaOff(VRCPlayerApi _player)
        {
            _player.SetVoiceDistanceNear(0);
            _player.SetVoiceDistanceFar(0);
            //_player.SetVoiceVolumetricRadius(0);
        }
        void SetPaReset(VRCPlayerApi _player)
        {
            _player.SetVoiceDistanceNear(0.0f);
            _player.SetVoiceDistanceFar(25.0f);
            //_player.SetVoiceVolumetricRadius(25.0f);
        }
        bool ChatV(bool isMine) {
            bool ab = false;//是否隐藏消息

            bool _isMG = ShotID > 0 || isScript;

            string[] newStr = MainText.Split('@');
            if (newStr.Length > 2)
            {
                //isFindPlayer = false;
                switch (newStr[0].ToLower())
                {
                    #region 禁言
                    case "shutup":
                        ab = true;

                        if (isMine)
                        {
                            if (!_isMG)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }
                        else
                        {
                            bool mJY = false;
                            if (int.TryParse(newStr[1], out int c))
                            {
                                if (c == Networking.LocalPlayer.playerId || c <= 0)
                                    mJY = true;
                            }
                            else if (newStr[1] == Networking.LocalPlayer.displayName)
                            {
                                    mJY = true;
                            }

                            if (mJY)
                            {
                                int pl = int.Parse(newStr[newStr.Length - 1]);
                                if (pl > ShotID)
                                {
                                    if (newStr.Length > 3)
                                    {
                                        float.TryParse(newStr[2], out float b);//禁言时间
                                        if (b <= 0)
                                        {
                                            if (!IsChat) {
                                                SendMessge(Networking.LocalPlayer.displayName + "已解除禁言" + "@"+ ShotID);
                                            }
                                            shutupTime = -1;
                                            IsChat = true;
                                            jyan.SetActive(false);
                                        }
                                        else
                                        {
                                            IsChat = false;
                                            shutupTime = b * 60;
                                            SendMessge(Networking.LocalPlayer.displayName + "已被禁言" + b + "分钟" + "@" + ShotID);
                                            jyan.SetActive(true);
                                        }
                                    }
                                    else
                                    {
                                        IsChat = false;
                                        SendMessge(Networking.LocalPlayer.displayName + "已被禁言" + "@" + ShotID);
                                        jyan.SetActive(true);
                                    }
                                    //禁言
                                }
                                else
                                {
                                    SendMessge(Networking.LocalPlayer.displayName + "无法被禁言" + "@" + ShotID);
                                    //对方权限同等或者更高
                                }
                            }
                            else
                            {
                                //操作无效
                            }
                        }
                        
                        break;
                    #endregion
                    #region 强制传送玩家至另一个玩家
                    case "teleport":
                        ab = true;
                        if (isMine)
                        {
                            if (!_isMG)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }

                        if (int.TryParse(newStr[1], out int _playerID))
                        {
                            if (_playerID == Networking.LocalPlayer.playerId)
                            {
                                if (isMine)
                                {
                                    MainInputFieldObj.SetActive(false);
                                    IsChir = false;
                                }
                                if (int.TryParse(newStr[2], out int _playerID2))
                                {
                                    int playerConst = VRCPlayerApi.GetPlayerCount();
                                    VRCPlayerApi[] players = new VRCPlayerApi[playerConst];
                                    VRCPlayerApi.GetPlayers(players);
                                    foreach (var item in players)
                                    {
                                        if (item.playerId == _playerID2)
                                        {
                                            SendMessge(string.Format("已将{0}传送至{1}@{2}", Networking.LocalPlayer.displayName, item.displayName, ShotID));
                                            if (newStr.Length > 3 && int.TryParse(newStr[3], out int _rangen))
                                            {
                                                //Networking.LocalPlayer.TeleportTo(item.GetPosition(), item.GetRotation());
                                                Quaternion _randomAngle = Quaternion.Euler(0, Random.Range(0, 360), 0);
                                                Vector3 _point = item.GetPosition() + (_randomAngle * (Vector3.forward * ((float)_rangen / 100)));
                                                //Networking.LocalPlayer.TeleportTo(_point + (Vector3.up * 0.5f), Quaternion.Euler(0, _randomAngle.eulerAngles.y + 180, 0), 0);
                                                PlayerTeleport(_point + (Vector3.up * 0.5f), Quaternion.Euler(0, _randomAngle.eulerAngles.y + 180, 0));
                                            }
                                            else
                                            {
                                                //Networking.LocalPlayer.TeleportTo(item.GetPosition() + (Vector3.up * 0.5f), item.GetRotation());
                                                PlayerTeleport(item.GetPosition() + (Vector3.up * 0.5f), item.GetRotation());
                                            }
                                            break;
                                        }
                                    }
                                }
                            }else if(_playerID == 0)
                            {//集合所有玩家
                                if (int.TryParse(newStr[2], out int _playerID2))
                                {
                                    if (_playerID2 != Networking.LocalPlayer.playerId)
                                    {
                                        int playerConst = VRCPlayerApi.GetPlayerCount();
                                        VRCPlayerApi[] players = new VRCPlayerApi[playerConst];
                                        VRCPlayerApi.GetPlayers(players);
                                        foreach (var item in players)
                                        {
                                            if (item.playerId == _playerID2)
                                            {
                                                if (isMine)
                                                {
                                                    MainInputFieldObj.SetActive(false);
                                                    IsChir = false;
                                                    //    SendMessge(string.Format("已将所有玩家传送至{0}@{1}", item.displayName, ShotID));
                                                }
                                                if (newStr.Length > 3 && int.TryParse(newStr[3], out int _rangen))
                                                {
                                                    //Networking.LocalPlayer.TeleportTo(item.GetPosition(), item.GetRotation());
                                                    Quaternion _randomAngle = Quaternion.Euler(0, Random.Range(0, 360), 0);
                                                    Vector3 _point = item.GetPosition() + (_randomAngle * (Vector3.forward * ((float)_rangen / 100)));
                                                    //Networking.LocalPlayer.TeleportTo(_point + (Vector3.up * 0.5f), Quaternion.Euler(0, _randomAngle.eulerAngles.y + 180, 0), 0);
                                                    PlayerTeleport(_point + (Vector3.up * 0.5f), Quaternion.Euler(0, _randomAngle.eulerAngles.y + 180, 0));
                                                }
                                                else
                                                {
                                                    //Networking.LocalPlayer.TeleportTo(item.GetPosition() + (Vector3.up * 0.5f), item.GetRotation());
                                                    PlayerTeleport(item.GetPosition() + (Vector3.up * 0.5f), item.GetRotation());
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                
                            }
                        }
                        break;
                    #endregion
                    #region 列表显示
                    case "display":
                        ab = true;
                        if (isMine)
                        {
                            if (!_isMG)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }

                        Debug.Log("进入了Display");
                        if (isMine)
                        {
                            if (newStr[1].ToLower() == "playerlist")
                            {
                                //Debug.Log("进入了DisplayPlayerlist");
                                //playerListState = !playerListState;
                                //playerList.SetActive(playerListState);
                            }
                            else if (newStr[1].ToLower() == "com")
                            {
                                //gmComListState = !gmComListState;
                                //gmComList.SetActive(gmComListState);
                            }
                        }
                        break;
                    #endregion
                    #region 强制执行按钮事件
                    case "action":
                        ab = true;
                        if (isMine)
                        {
                            if (!_isMG)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }

                        if (int.TryParse(newStr[1], out int _mPlayerID))
                        {
                            //playerTargetID = _mPlayerID;
                            if (_mPlayerID <= 0)
                            {
                                if (int.TryParse(newStr[2], out int _mPlayerID2))
                                {
                                    if (_mPlayerID2 < MainUI.MainEventNumber.Length)
                                    {
                                        if (newStr.Length > 3 && int.TryParse(newStr[3], out int _eventsID) && _eventsID < 6)
                                        {
                                            //if (isMine)
                                            //    SendMessge(string.Format("所有玩家已被强制执行序号{0}按钮的{1}功能@{2}", _mPlayerID2, ActionEventName(_eventsID), ShotID));
                                            MainUI.But_RunEvents(_eventsID, _mPlayerID2, false, true);//执行按下

                                        }
                                        else
                                        {
                                            if (isMine)
                                                ChatAnim.Play("ER");
                                            //SendMessge(string.Format("已将所有玩家执行序号{0}的按钮功能@{1}", _mPlayerID2, ShotID));
                                            MainUI.But_RunEvents(0, _mPlayerID2, false, true);//执行按下 
                                        }
                                    }
                                }
                            }
                            else if (_mPlayerID == Networking.LocalPlayer.playerId)
                            {//指定玩家

                                if (int.TryParse(newStr[2], out int _mPlayerID2))
                                {
                                    if (_mPlayerID2 < MainUI.MainEventNumber.Length)
                                    {
                                        SendMessge(string.Format("{0}已执行事件,序号{1}@{2}", Networking.LocalPlayer.displayName, _mPlayerID2, ShotID));
                                        if (newStr.Length > 3 && int.TryParse(newStr[3], out int _eventsID) && _eventsID < 6)
                                        {
                                            //SendMessge(string.Format("{0}已被强制执行序号{1}按钮的{2}功能@{3}", Networking.LocalPlayer.displayName, _mPlayerID2, ActionEventName(_eventsID), ShotID));
                                            //test.text = string.Format("已被强制执行序号{0}按钮的{1}功能\n我的序号为:{2}", _mPlayerID2, ActionEventName(_eventsID), Networking.LocalPlayer.playerId);
                                            MainUI.But_RunEvents(_eventsID, _mPlayerID2, false, true);//执行按下
                                            //test.text += $"\n执行码: {MainText}";
                                        }
                                        else
                                        {
                                            SendMessge(string.Format("序号为{0}的按钮事件失效@{1}", _mPlayerID2, ShotID));
                                        }
                                        //else
                                        //{
                                        //    //SendMessge(string.Format("{0}已被强制执行序号{1}按钮的按下功能@{2}", Networking.LocalPlayer.displayName, _mPlayerID2, ShotID));
                                        //    MainUI.But_RunEvents(0, _mPlayerID2, false, true);//执行按下
                                        //}
                                    }
                                    else
                                    {
                                        SendMessge(string.Format("不存在序号为{0}的按钮@{1}", _mPlayerID2, ShotID));
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region 强制执行触发器事件
                    case "actionc":
                        ab = true;
                        if (isMine)
                        {
                            if (!_isMG)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }

                        if (int.TryParse(newStr[1], out int _mPlayerIDc))
                        {
                            //playerTargetID = _mPlayerIDc;
                            if (_mPlayerIDc <= 0)
                            {
                                if (int.TryParse(newStr[2], out int _mPlayerID2))
                                {
                                    if (_mPlayerID2 < MainUI.BoxColliderUdon.Length)
                                    {

                                        if (newStr.Length > 3 && int.TryParse(newStr[3], out int _eventsID) && _eventsID < 2)
                                        {
                                            //if (isMine)
                                            //    SendMessge(string.Format("所有玩家已被强制执行序号{0}按钮的{1}功能@{2}", _mPlayerID2, ActionEventName(_eventsID), ShotID));
                                            MainUI.BOXC_RunEvents(_eventsID, _mPlayerID2, true);//执行按下

                                        }
                                        else
                                        {
                                            //if (isMine)
                                            //    SendMessge(string.Format("已将所有玩家执行序号{0}的按钮功能@{1}", _mPlayerID2, ShotID));
                                            MainUI.BOXC_RunEvents(0, _mPlayerID2, true);//执行按下 
                                        }
                                    }
                                    else
                                    {
                                        if (isMine)
                                            ChatAnim.Play("ER");
                                        //SendMessge(string.Format("不存在序号为{0}的触发器@{1}", _mPlayerID2, ShotID));
                                    }

                                }
                            }
                            else if (_mPlayerIDc == Networking.LocalPlayer.playerId)
                            {//指定玩家

                                if (int.TryParse(newStr[2], out int _mPlayerID2))
                                {
                                    if (_mPlayerID2 < MainUI.BoxColliderUdon.Length)
                                    {
                                        if (newStr.Length > 3 && int.TryParse(newStr[3], out int _eventsID) && _eventsID < 2)
                                        {
                                            SendMessge(string.Format("{0}已被强制执行序号{1}触发器的{2}功能@{3}", Networking.LocalPlayer.displayName, _mPlayerID2, ActioncEventName(_eventsID), ShotID));
                                            MainUI.BOXC_RunEvents(_eventsID, _mPlayerID2, true);//执行按下
                                        }
                                        else
                                        {
                                            SendMessge(string.Format("{0}已被强制执行序号{1}触发器的进入功能@{2}", Networking.LocalPlayer.displayName, _mPlayerID2, ShotID));
                                            MainUI.BOXC_RunEvents(0, _mPlayerID2, true);//执行按下
                                        }
                                    }
                                    else
                                    {
                                        SendMessge(string.Format("不存在序号为{0}的触发器@{1}", _mPlayerID2, ShotID));
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region 给予权限
                    case "setmg":
                        ab = true;
                        if (isMine)
                        {
                            if (img < 1)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }

                        if (int.TryParse(newStr[1], out int _playerIDg))
                        {
                            if (_playerIDg == Networking.LocalPlayer.playerId)
                            {
                                if (int.TryParse(newStr[2], out int _state))
                                {
                                    if(_state == 0)
                                    {
                                        if (img > 1)
                                        {
                                            SendMessge(string.Format("{0}无法被取消管理权限@{1}", Networking.LocalPlayer.displayName, ShotID));
                                        }
                                        else
                                        {
                                            SendMessge(string.Format("{0}已被取消管理权限@{1}", Networking.LocalPlayer.displayName, ShotID));
                                            ShotID = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (ShotID > 0)
                                        {
                                            SendMessge(string.Format("{0}已为管理者,无需设定@{1}", Networking.LocalPlayer.displayName, ShotID));
                                        }
                                        else
                                        {
                                            ShotID = 5;
                                            SendMessge(string.Format("已经成功设定{0}为管理者@{1}", Networking.LocalPlayer.displayName, ShotID));
                                        }
                                    }
                                }
                            }
                        }

                        break;
                    #endregion
                    #region 扩音
                    case "pa":
                        ab = true;
                        if (isMine)
                        {
                            if (!_isMG)
                            {
                                //无权者
                                ChatAnim.Play("NG");
                                break;
                            }
                        }

                        if (int.TryParse(newStr[1], out int _playerIDp))
                        {
                            if (_playerIDp > 0)
                            {//争对的玩家
                                if (int.TryParse(newStr[2], out int _eventID))
                                {
                                    if (_eventID == 0 || _eventID == 1)
                                    {//解除静音
                                        foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                        {
                                            if (item.playerId == _playerIDp)
                                            {
                                                SetPaReset(item);
                                                break;
                                            }
                                        }
                                    }
                                    else if (_eventID == 2)
                                    {//扩音
                                        foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                        {
                                            if (item.playerId == _playerIDp)
                                            {
                                                SetPaOn(item);
                                                break;
                                            }
                                        }
                                    }
                                    else if (_eventID == 3)
                                    {//静音
                                        foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                        {
                                            if (item.playerId == _playerIDp)
                                            {
                                                SetPaOff(item);
                                                break;
                                            }
                                        }
                                    }
                                    else if (_eventID == 4)
                                    {//相互扩音
                                        if (isMine)
                                        {//本地  只对指定目标扩音
                                            foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                            {
                                                if (item.playerId == _playerIDp)
                                                {
                                                    SetPaOn(item);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (Networking.LocalPlayer.playerId == _playerIDp)
                                        {//接受端  只对发送对象扩音
                                            if (int.TryParse(newStr[3], out int _eventID2))
                                            {
                                                foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                                {
                                                    if (item.playerId == _eventID2)
                                                    {
                                                        SetPaOn(item);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {//只对我静音
                                        if (isMine)
                                        {
                                            foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                            {
                                                if (item.playerId == _playerIDp)
                                                {
                                                    SetPaOff(item);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }

                                //if (Networking.LocalPlayer.playerId == _playerIDp)
                                //{
                                //    if(int.TryParse(newStr[2], out int _playerIDpp) && _playerIDpp == 0)
                                //    {
                                //        SetPaReset(Networking.LocalPlayer);
                                //    }
                                //    else
                                //    {
                                //        SetPaOn(Networking.LocalPlayer);
                                //    }
                                //}
                            }
                            else
                            {//全体
                                if (int.TryParse(newStr[2], out int _eventID))
                                {
                                    if (_eventID == 0 || _eventID == 1)
                                    {//解除禁言
                                        foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                            SetPaReset(item);
                                    }
                                    else if (_eventID == 2)
                                    {//扩音
                                        foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                            SetPaOn(item);
                                    }
                                    else if (_eventID == 3)
                                    {//静音
                                        foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                            SetPaOff(item);
                                    }
                                    else if (_eventID == 4)
                                    {//相互扩音
                                        if (isMine)
                                        {//本地  只对指定目标扩音
                                            foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                            {
                                                SetPaOn(item);
                                            }
                                        }
                                        //else if (Networking.LocalPlayer.playerId == _playerIDp)
                                        {//接受端  只对发送对象扩音
                                            if (int.TryParse(newStr[3], out int _eventID2))
                                            {
                                                foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                                {
                                                    if (item.playerId == _eventID2)
                                                    {
                                                        SetPaOn(item);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {//只对我静音
                                        if (isMine)
                                        {
                                            foreach (VRCPlayerApi item in GetInstanceAllPlayer())
                                                SetPaOff(item);
                                        }
                                    }
                                    //if (int.TryParse(newStr[2], out int _playerIDpp) && _playerIDpp == 0)
                                    //{
                                    //    SetPaReset(Networking.LocalPlayer);
                                    //}
                                    //else
                                    //{
                                    //    SetPaOn(Networking.LocalPlayer);
                                    //}
                                }
                            }
                        }
                        break;
                    #endregion
                    #region 指令错误
                    default:
                        ab = true;
                        if (isMine)
                            ChatAnim.Play("ER");
                        break;
                        #endregion
                }
            }
            if (isMine) 
            {
                if (ab)
                {
                    MainInputField.text = null;
                    ChatDelay = 1;
                    MainInputField.ActivateInputField();
                } 
            }

            if (!ab)
            {
                if (isMine)
                {
                    MainText = MainText2.Replace(" ", "\u00A0");
                }
                else
                {
                    MainText = "";
                    for (int i = 0; i < newStr.Length - 1; i++)
                    {
                        if (i == newStr.Length - 2)
                            MainText += newStr[i];
                        else
                            MainText += newStr[i] + "@";
                    }
                    MainText = MainText.Replace(" ", "\u00A0");

                    ////测试
                    ////MainText += "    :" + c;
                    //MainText ="shutup:" + Networking.LocalPlayer.displayName +"   --"+ (c == Networking.LocalPlayer.playerId).ToString() + "\n" + "被执行ID：" + c + "  我的ID：" + Networking.LocalPlayer.playerId;
                    //MainText += "\n" + "被执行ID：" + c + "  我的ID：" + Networking.LocalPlayer.playerId;
                }
            }
            return ab;
        }

        void Update_PlayerList()
        {
            if (playerListState)
            {
                int playerConst = VRCPlayerApi.GetPlayerCount();
                string content = "玩家数量: " + playerConst + "\n";
                VRCPlayerApi[] players = new VRCPlayerApi[playerConst];
                VRCPlayerApi.GetPlayers(players);
                foreach (var item in players)
                {
                    content += string.Format("{0}(ID:{1})", item.displayName, item.playerId);//
                }
                playerListContent.text = content;
            }
        }
#endregion
        public void InputChatText(int PlayerID, int AvatarHeadID_, int AvatarChatID_, int BobbleChatID_, string MainText3,int pl)
        {
            MainText = MainText3;
            if (ChatV(false))return;
               //MainText = "";
               //switch()
               MessageNumber++;//迭代信息数量
            if (MessageNumber < MaxMessageNumber)
            {
                //TextRootParent
                //TextRootParent.rect.height = MessageNumber * Hight;
                //TextRootParent.rect.height = 1;
                TextRootParent.sizeDelta = new Vector2(TextRootParent.rect.width, MessageNumber * Hight);
            }

            TextRootPos -= TextRootPos_Sub;
            MoveTextRoot = 1;

            VRCPlayerApi Player = Networking.GetOwner(ALLplayer.GetChild(PlayerID).gameObject);

            Transform ListParent = TextRoot.GetChild(0);
            ListParent.GetChild(0).localPosition = AvatarOffset_Left;
            //ListParent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = ChatAvatar[AvatarChatID];
            //ListParent.GetChild(0).GetComponent<Image>().material.SetTexture  AvatarHead[PlayerID] SetPropertyBlock
            //ListParent.GetChild(0).GetComponent<Image>().material.SetTexture("_Detail", AvatarHead[PlayerID]);
            //ListParent.GetChild(0).GetChild(0).GetComponent<Image>().sprite = AvatarHead[AvatarHeadID_];//头像边框
            ListParent.GetChild(0).GetComponent<Image>().sprite = ChatAvatar[AvatarChatID_]; //头像
            ListParent.GetChild(1).GetComponent<Text>().text = Player.displayName;
            ListParent.GetChild(1).GetComponent<Text>().alignment = TextAnchor.LowerLeft;
            ListParent.GetChild(2).localRotation = Quaternion.Euler(0, 0, 0);
            ListParent.GetChild(2).localPosition = BobbleOffset_Left;
            ListParent.GetChild(2).GetComponent<Image>().sprite = ChatBobble[BobbleChatID_];
            ListParent.GetChild(3).localPosition = MainText_Left;
            ListParent.GetChild(3).GetComponent<Text>().text = MainText;//信息内容
            //3d气泡
            if (Vector3.Distance(Player.GetPosition(), Networking.LocalPlayer.GetPosition()) < 20)
            {
                Transform MainSet = Main3DUI.GetChild(0);
                Chat_3DUI TestChat = MainSet.GetComponent<Chat_3DUI>();
                if (TestChat.isInit)
                {
                    TestChat.LocalPlayer = Player;
                    TestChat.MainText.text = MainText;
                    TestChat.Life = 4;
                    MainSet.gameObject.SetActive(true);
                    MainSet.SetSiblingIndex(MainUILength - 1);
                }
            }
            

            Vector3 Offset = Vector3.zero;
            Offset.y = Hight;//最后消除的高度

            for (int i = 1; i < TextRoot.childCount; i++)
                TextRoot.GetChild(i).localPosition += Offset;//将所有text上推

            //介入信息
            TextRoot.GetChild(0).localPosition = new Vector3(0, Hight / 2, 0);
            TextRoot.GetChild(0).gameObject.SetActive(true);
            TextRoot.GetChild(0).SetSiblingIndex(TextRoot.childCount - 1);
        }

        void InitializationTextList(int MaxListConst1)
        {
            MaxListConst1++;
            int MaxListConst = Mathf.Clamp(MaxListConst1, 5, 21);//最少4条聊天记录，最多20条
            if (TextRoot.childCount < MaxListConst)
            {
                //当设置更多的聊天记录时
                int AddLIstConston = MaxListConst - TextRoot.childCount;
                for (int i = 0; i < AddLIstConston; i++)//添加至尾部
                {
                    TextRootParent.GetChild(1).SetParent(TextRoot);
                    //Debug.Log(i);
                }
            }
            else if (TextRoot.childCount > MaxListConst)
            {
                int MinusLIstConston = TextRoot.childCount - MaxListConst;
                for (int i = 0; i < MinusLIstConston; i++)
                {//从头部删除
                    TextRoot.GetChild(0).gameObject.SetActive(false);
                    TextRoot.GetChild(0).SetParent(TextRootParent);
                }
            }
            MaxMessageNumber = MaxListConst;
            MessageNumber = Mathf.Min(MessageNumber, MaxMessageNumber - 1);
            //重新排序
        }

        void UiAnima()
        {
            //if (Vector3.Distance(LocalPlayer.GetVelocity(), Vector3.zero) > 0.5f)
            //{
            //    TextRootParent.localPosition = LockPos;
            //}
            //else
            //{
            //    if (MoveTextRoot > 0)
            //    {
            //        MoveTextRoot -= deltatime;
            //        TextRootPos = Vector3.Lerp(TextRootPos, Vector3.zero, 7 * deltatime);
            //        if (MoveTextRoot < 0) MoveTextRoot = 0;
            //        TextRoot.localPosition = TextRootPos;
            //    }
            //    LockPos = TextRootParent.localPosition;
            //}

            if (MoveTextRoot > 0)
            {
                MoveTextRoot -= deltatime;
                TextRootPos = Vector3.Lerp(TextRootPos, Vector3.zero, 7 * deltatime);
                if (MoveTextRoot < 0) MoveTextRoot = 0;
                //TextRootPos.x = 90;
                TextRoot.localPosition = TextRootPos;
                //TextRoot.anchoredPosition3D = Vector3.zero;
            }
            //LockPos = TextRootParent.localPosition;

        }

        void GmFor()
        {
            string MyName = Networking.LocalPlayer.displayName;


            switch (MyName)
            {
#region GM
                case "Asitir":
                    ShotID = 8;
                    ChatAvatarID = 0;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "Iven遠洋":
                    ShotID = 7;
                    ChatAvatarID = 5;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "shine trick":
                    ShotID = 7;
                    ChatAvatarID = 4;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "StackOverflow":
                    ShotID = 7;
                    ChatAvatarID = 3;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "TiritoTR":
                    ShotID = 7;
                    ChatAvatarID = 2;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "柠檬味的海苔":
                    ShotID = 7;
                    ChatAvatarID = 1;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "xiaokongbai":
                    ShotID = 7;
                    ChatAvatarID = 8;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "白色有角千倍速":
                    ShotID = 7;
                    ChatAvatarID = 6;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "犬者カト":
                    ShotID = 7;
                    ChatAvatarID = 7;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "luoyvzhuxiong":
                    ShotID = 7;
                    ChatAvatarID = 9;
                    BobbleID = 0;
                    img = 2;
                    break;
                case "Dolo_H":
                    ShotID = 7;
                    ChatAvatarID = 10;
                    BobbleID = Random.Range(1, 4);
                    img = 2;
                    break;
                default:
                    ChatAvatarID = Random.Range(20, 30);
                    BobbleID = Random.Range(1, 4);
                    bool _isGm = false;
                    foreach (var item in Managers)
                    {
                        if(item == MyName)
                        {
                            _isGm = true;
                            ShotID = 6;
                            img = 1;
                            break;
                        }
                    }
                    if (_isGm) break;
                    if (Networking.IsMaster)
                    {
                        if (MasterIsMG)
                        {
                            ShotID = 5;
                            img = 1;
                        }
                    }
                    else
                    {
                        ShotID = 0;
                    }
                    break;
#endregion
            }
            MainUI.pl = ShotID;

        }


        /// <summary>
        /// 酱父级切换到PC吗？
        /// </summary>
        /// <param name="pc"></param>
        public void SwitchRootToPC(bool pc)
        {
            if (pc)
            {
                transform.SetParent(chat_PCroot);
                transform.localRotation = Quaternion.identity;
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.SetParent(chat_VRroot);
                transform.localRotation = Quaternion.identity;
                transform.localPosition = Vector3.zero;
                transform.localScale = Vector3.one;

            }
        }

        #region Events
        private VRCPlayerApi[] GetInstanceAllPlayer()
        {
            VRCPlayerApi[] IPlayer = new VRCPlayerApi[VRCPlayerApi.GetPlayerCount()];
            VRCPlayerApi.GetPlayers(IPlayer);
            return IPlayer;
        }
        private void PlayerTeleport(Vector3 _pos,Quaternion _rot) 
        {
            MainUI.vrcWorld.SetPositionAndRotation(_pos, _rot);
            Networking.LocalPlayer.Respawn();
        }
        #endregion

        #region 开放功能
        public void OnPa()
        {
            SendMG($"pa@{Networking.LocalPlayer.playerId}@2@1");//发送指令
        }
        public void OnPaReset()
        {
            SendMG($"pa@{Networking.LocalPlayer.playerId}@0@1");//发送指令
        }

        public void SendMG(string _sendMessage,bool _isScript = true)
        {
            isScript = _isScript;
#if UNITY_EDITOR
            Debug.Log($"已发送指令：{_sendMessage}");
#endif
            SendData(_sendMessage, true);//发送指令
        }

        VRCPlayerApi targetPlayer = null;
        //public VRCPlayerApi GetTargetPlayer() {
        //    if (!isFindPlayer)
        //    {
        //        targetPlayer = null;
        //        isFindPlayer = true;
        //        if (playerTargetID > 0)
        //        {
        //            int playerConst = VRCPlayerApi.GetPlayerCount();
        //            VRCPlayerApi[] players = new VRCPlayerApi[playerConst];
        //            foreach (var item in VRCPlayerApi.GetPlayers(players))
        //            {
        //                if (item.playerId == playerConst)
        //                {
        //                    targetPlayer = item;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return targetPlayer;
        //}

        public void OnCheck() { MainUI.checkID++; }//检查当前脚本
        #endregion
    }
}