
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class PlayerCount : UdonSharpBehaviour
    {
        float deltatime;
        float StartTime = 5;//进入地图五秒后刷新
        float AddStartime = -1;
        //int[] PlayerListID;
        //[HideInInspector]
        //public int[] PlayerID;//房间内所有玩家的ID
        [HideInInspector]
        public int MainID = -1;//我的同步ID
        [HideInInspector]
        //public int Operation = 0;//操作ID
        //[HideInInspector]
        //public GameObject[] SynObject;//已经在线的玩家列表
        //MainUI_Script MainUI;
        Transform PlayerSny;
        VRCPlayerApi Localplayer;
        //[HideInInspector]
        //public bool PlayerEnter = false;
        bool isRun = false;

        void Start()
        {
            MainID = -1;
            //PlayerListID = new int[transform.childCount];
            //PlayerID = new int[0];
            isRun = transform.childCount > 0;
            if(!isRun) gameObject.SetActive(false);
            Localplayer = Networking.LocalPlayer;
            PlayerSny = transform;
            //MyID = transform.GetChild(0).GetChild(0).GetComponent<Player_Sny>().MainID;
        }

        private void Update()
        {
            if (!isRun)
            {
                gameObject.SetActive(false);
                return;
            }
            FindMySynID();

            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    Debug.Log($"我的ID序号：{MainID}");

            //    for (int i = 0; i < PlayerSny.childCount; i++)
            //    {
            //        //PlayerSny.GetChild(i).GetComponent<Player_Sny>().Synced();//全体初始化同步
            //        Debug.Log($"同步ID：{PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID}\n序号: {i}");
            //    }

            //}
            //deltatime = Time.deltaTime;

            //if (AddStartime > -1)
            //{
            //    AddStartime += deltatime;
            //    if (AddStartime > StartTime)
            //    {
            //        ForAllPlayer();
            //        AddStartime = -1;
            //    }
            //}

        }

        //public override void OnPlayerJoined(VRCPlayerApi player)
        //{
        //    PlayerEnter = true;
        //    AddStartime = 0;
        //}

        //public override void OnPlayerLeft(VRCPlayerApi player)
        //{
        //    PlayerEnter = false;
        //    if (AddStartime == -1)
        //    {
        //        //ForAllPlayer();
        //        AddStartime = 4.8f;
        //    }
        //}

        /// <summary>
        /// 更新玩家列表
        /// </summary>
        //void ForAllPlayer()
        //{
        //    int IdNumber = 0;
        //    //bool findid = false;
        //    for (int i = 0; i < transform.childCount; i++)
        //    {
        //        //Debug.Log("总数：" + transform.childCount + "   当前ID：" + i);
        //        int id = transform.GetChild(i).GetComponent<Player_Sny>().InputID;
        //        if ((int)(id / 1000000000) > 0)
        //        {
        //            PlayerListID[IdNumber] = i;//得到被注册的对象
        //            IdNumber++;
        //            //if (!findid) {
        //            //    //if(if(Networking.GetOwner(transform.GetChild(i).gameObject) == )
        //            //    if (Networking.GetOwner(transform.GetChild(i).gameObject) == Networking.LocalPlayer)
        //            //    {
        //            //        findid = true;
        //            //        MyID = i;//得到本地玩家的同步对象
        //            //    }
        //            //}
        //        }
        //    }

        //    MyID = transform.GetChild(0).GetComponent<Player_Sny>().MainID;
        //    PlayerID = new int[IdNumber];
        //    SynObject = new GameObject[IdNumber];
        //    for (int i = 0; i < PlayerID.Length; i++)
        //    {
        //        PlayerID[i] = PlayerListID[i];
        //        SynObject[i] = transform.GetChild(PlayerID[i]).gameObject;
        //    }

        //    if (Operation < 999999) Operation++; else Operation = 0;//操作ID
        //}

        private void FindMySynID()
        {

            int MyID_0 = 0;
            if (MainID > -1)
            {//当手上拥有同步对象父级后
                MyID_0++;

                if (Networking.GetOwner(PlayerSny.GetChild(MainID).gameObject) != Localplayer)
                {
                    MainID = -1;
                }
                else
                {
                    for (int i = 0; i < PlayerSny.childCount; i++)
                    {
                        //Player_Sny NowSny = PlayerSny.GetChild(i).GetComponent<Player_Sny>();
                        if (Networking.GetOwner(PlayerSny.GetChild(i).gameObject) == Networking.LocalPlayer)
                        {
                            Player_Sny NowSny = PlayerSny.GetChild(i).GetComponent<Player_Sny>();
                            if (i != MainID)
                            {
                                if (NowSny.InputID > 0)
                                {
                                    NowSny.SetInputID(0);
                                    NowSny.Synced();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < PlayerSny.childCount; i++)
                {
                    if (Networking.GetOwner(PlayerSny.GetChild(i).gameObject) == Localplayer)
                    {//如果有自己已经占用的同步对象
                        if (MyID_0 == 0)
                        {
                            Networking.SetOwner(Localplayer, PlayerSny.GetChild(i).gameObject);
                            Player_Sny Main = PlayerSny.GetChild(i).GetComponent<Player_Sny>();
                            int pd = Main.InputID;
                            if ((int)(pd / 1000000000) <= 0)
                            {//未曾拥有过同步对象
                                int pd2 = pd;
                                pd2 = (int)(pd / 1000000000);
                                pd2 = (pd - (pd2 * 1000000000)) + 1000000000;//设置ID为1
                                Main.SetInputID(pd2);
                                Main.InputID_late = pd2;
                                Main.Synced();//发起同步
                                MainID = i;
                            }
                            MyID_0++;
                        }
                        else
                        {
                            //int pd = PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID;
                            //int pd2 = pd;
                            //pd2 = (int)(pd / 1000000000);
                            //pd2 = (pd - (pd2 * 1000000000));
                            //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID = pd2;
                            //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID_late = pd2;//清空
                            Networking.SetOwner(Localplayer, PlayerSny.GetChild(i).gameObject);
                            Player_Sny Main = PlayerSny.GetChild(i).GetComponent<Player_Sny>();

                            Main.SetInputID( 0);
                            Main.Synced();//发起同步
                        }
                    }
                }
            }

            if (MyID_0 < 1)
            {//没有一个已经存在的同步对象是自己的
                for (int i = 0; i < PlayerSny.childCount; i++)
                {
                    int pd = PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID;

                    if ((int)(pd / 1000000000) <= 0)
                    {
                        Networking.SetOwner(Localplayer, PlayerSny.GetChild(i).gameObject);
                        //int pd2 = pd;
                        //pd2 = (int)(pd / 1000000000);
                        //pd2 = (pd - (pd2 * 1000000000)) + 1000000000;//设置ID为1
                        //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID = pd2;
                        //PlayerSny.GetChild(i).GetComponent<Player_Sny>().InputID_late = pd2;
                        GameObject Targt = PlayerSny.GetChild(i).gameObject;
                        Networking.SetOwner(Localplayer, Targt);
                        Targt.GetComponent<Player_Sny>().SetInputID(1000000000);
                        Targt.GetComponent<Player_Sny>().Synced();//发起同步
                        MainID = i;
                        i = 1000000;

                    }
                }
            }
        }
    }
}