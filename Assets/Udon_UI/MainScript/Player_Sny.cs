
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonUI
{
    public class Player_Sny : UdonSharpBehaviour
    {
        MainUI_Script MainUI;
        //int.max = 2147483647
        bool truemain = false;//是否确定下最终的同步对象
        [UdonSynced]
        public bool IsChat = false;//是否为发言
        VRCPlayerApi Localplayer;
        [HideInInspector]
        [UdonSynced] public int InputID;
        //public int InputID
        //{
        //    get { return SeveInputID; }
        //    set { 
        //        SeveInputID = value;
        //        if (!IsChat) ChatText = "0";
        //        Synced();
        //        IsChat = false;
        //        Debug.Log("同步完成");
        //    }
        //}
        [HideInInspector]
        [UdonSynced] public string ChatText;
        [HideInInspector]
        [UdonSynced] public Vector3 MainPos;
        [HideInInspector]
        [UdonSynced] public bool LSword = false, RSword = false;
        bool SnyPlayerOne = false;//是否第一位序列
        bool Syninitialization = false;//是否初始化过
        public int InputID_late;
        //string thisname;
        Transform PlayerSny;
        Transform MainTrans;
        //public int MainID = -1;//我的同步对象ID

        float waiting = 0;
        float deltatime;
        int SynObjID = 0;
        void Start()
        {
            MainTrans = transform;

            //MainID = -1;
            MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            InputID_late = InputID;
            PlayerSny = transform.parent;
            Localplayer = Networking.LocalPlayer;

            SynObjID = transform.GetSiblingIndex();//得到当前对象所处的ID
            SnyPlayerOne = SynObjID == 0;

            //if (SnyPlayerOne)
            //    Debug.Log(gameObject.name + ":  " + SnyPlayerOne);
        }

        //override public void OnPlayerLeft(VRCPlayerApi player)
        //{
        //    if (Networking.IsOwner(gameObject))
        //    {
        //        if (player == Networking.GetOwner(gameObject))
        //        {
        //            SetInputID(0);
        //            Synced();
        //        }
        //    }
        //}

        //private void Update()
        //{
        //    //deltatime = Time.deltaTime;

        //    //if (waiting < 3)
        //    //{//3秒后触发同步初始化 
        //    //    waiting += deltatime;
        //    //    if (!Syninitialization && waiting > 2)
        //    //    {
        //    //        if (SnyPlayerOne) AllInitialization();//初始化所有同步
        //    //        Syninitialization = true;
        //    //    }
        //    //}
        //    //else
        //    {
        //        if (SnyPlayerOne)
        //        {//防止多脚本同时运行
        //            int MyID = 0;
        //            if (MainID > -1)
        //            {//当手上拥有同步对象父级后
        //                MyID++;

        //                if (Networking.GetOwner(PlayerSny.GetChild(MainID).gameObject) != Localplayer)
        //                {
        //                    MainID = -1;
        //                }
        //                else
        //                {
        //                    for (int i = 0; i < PlayerSny.childCount; i++)
        //                    {
        //                        //Player_Sny NowSny = PlayerSny.GetChild(i).GetComponent<Player_Sny>();
        //                        if (Networking.GetOwner(PlayerSny.GetChild(i).gameObject) == Networking.LocalPlayer)
        //                        {
        //                            Player_Sny NowSny = PlayerSny.GetChild(i).GetComponent<Player_Sny>();
        //                            if (i != MainID)
        //                            {
        //                                if (NowSny.InputID > 0)
        //                                {
        //                                    NowSny.InputID = 0;
        //                                    NowSny.Synced();
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                for (int i = 0; i < PlayerSny.childCount; i++)
        //                {
        //                    if (Networking.GetOwner(PlayerSny.GetChild(i).gameObject) == Localplayer)
        //                    {//如果有自己已经占用的同步对象
        //                        if (MyID == 0)
        //                        {
        //                            Networking.SetOwner(Localplayer, PlayerSny.GetChild(i).gameObject);
        //                            Player_Sny Main = PlayerSny.GetChild(i).GetComponent<Player_Sny>();
        //                            int pd = Main.InputID;
        //                            if ((int)(pd / 1000000000) <= 0)
        //                            {//未曾拥有过同步对象
        //                                int pd2 = pd;
        //                                pd2 = (int)(pd / 1000000000);
        //                                pd2 = (pd - (pd2 * 1000000000)) + 1000000000;//设置ID为1
        //                                Main.InputID = pd2;
        //                                Main.InputID_late = pd2;
        //                                Main.Synced();//发起同步
        //                                MainID = i;
        //                            }
        //                            MyID++;
        //                        }
        //                        else
        //                        {
        //                            //int pd = PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID;
        //                            //int pd2 = pd;
        //                            //pd2 = (int)(pd / 1000000000);
        //                            //pd2 = (pd - (pd2 * 1000000000));
        //                            //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID = pd2;
        //                            //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID_late = pd2;//清空
        //                            Networking.SetOwner(Localplayer, PlayerSny.GetChild(i).gameObject);
        //                            Player_Sny Main = PlayerSny.GetChild(i).GetComponent<Player_Sny>();

        //                            Main.InputID = 0;
        //                            Main.Synced();//发起同步
        //                        }
        //                    }
        //                }
        //            }

        //            if (MyID < 1)
        //            {//没有一个已经存在的同步对象是自己的
        //                for (int i = 0; i < PlayerSny.childCount; i++)
        //                {
        //                    int pd = PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID;

        //                    if ((int)(pd / 1000000000) <= 0)
        //                    {
        //                        Networking.SetOwner(Localplayer, PlayerSny.GetChild(i).gameObject);
        //                        //int pd2 = pd;
        //                        //pd2 = (int)(pd / 1000000000);
        //                        //pd2 = (pd - (pd2 * 1000000000)) + 1000000000;//设置ID为1
        //                        //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID = pd2;
        //                        //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID_late = pd2;
        //                        GameObject Targt = PlayerSny.GetChild(i).gameObject;
        //                        Networking.SetOwner(Localplayer, Targt);
        //                        Targt.GetComponent<Player_Sny>().InputID = 1000000000;
        //                        Targt.GetComponent<Player_Sny>().Synced();//发起同步
        //                        MainID = i;
        //                        i = 1000000;

        //                    }
        //                }
        //            }
        //        }


        //        //if (InputID_late != InputID)
        //        //{//拆解，执行
        //        //    if (Networking.IsOwner(gameObject))
        //        //    {
        //        //        if (!IsChat) ChatText = "0";
        //        //        Synced();
        //        //        IsChat = false;
        //        //    }
        //        //    else MainUI.snyEve(InputID, SynObjID, transform.parent, MainPos);
        //        //    InputID_late = InputID;
        //        //}
        //    }
        //}

        public void SetInputID(int _value) {
            InputID = _value;
            if (!IsChat) ChatText = "0";
            Synced();
            IsChat = false;
            //Debug.Log("同步完成");
        }

        //public bool IsValid() 
        //{
        //    return true;
        //}

        private bool isInit = false;
        public override void OnDeserialization()
        {
            if (isInit)
            {
                //if (Networking.IsOwner(gameObject))
                //{
                //    if (!IsChat) ChatText = "0";
                //    Synced();
                //    IsChat = false;
                //}

                if (!Networking.IsOwner(gameObject))
                {
                    MainUI.snyEve(InputID, SynObjID, transform.parent, MainPos);
                }
            }
            else
            {
                isInit = true;
            }

        }
        public void Synced()
        {
            RequestSerialization();
        }

        void AllInitialization()
        {
            for (int i = 0; i < PlayerSny.childCount; i++)
            {
                PlayerSny.GetChild(i).GetComponent<Player_Sny>().Synced();//全体初始化同步
            }
        }
    }
}