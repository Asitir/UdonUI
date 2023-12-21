
using UdonSharp;
using UnityEngine;
using Cinemachine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class PlayerCameThird : UdonSharpBehaviour
    {
        #region 申明变量
        [Header("鼠标右键放大视图")]
        [Space(20)]

        [Header("切换到第三人称按键设置")]
        public KeyCode thirdKey;
        [Header("切换左右视角按键设置")]
        public KeyCode switchThirdKey;
        Camera UIcame;
        //public KeyCode CameOn;
        //public Transform test;

        VRCPlayerApi LocalPlay;
        //VRCStation MainVRCStation;

        bool Third = false;
        MainUI_Script MainUI;
        Transform FpsCame;
        Camera MainCame;
        CinemachineVirtualCamera[] Vcam;
        Transform CameY, CameX, CameZ;
        int LastID = 0;
        float EnbleMianCame = 0;
        float deltatime = 0;
        float StartTime = 2;//5s后执行主函数
        Transform VcamMain;
        //bool AnimStation = false;

        bool Zoom = false, LastZoom = false;
        int LR = 4;
        #endregion

        #region Start函数
        void Start()
        {
#if UNITY_EDITOR
            gameObject.SetActive(false);
#else
        LocalPlay = Networking.LocalPlayer;
        //MainVRCStation = GetComponent<VRCStation>();
        //if (MainVRCStation.animatorController == true) {
        //    AnimStation = true;
        //}
        MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
        MainCame = transform.GetChild(0).GetChild(2).GetComponent<Camera>();
        FpsCame = transform.GetChild(0).GetChild(1);
        UIcame = FpsCame.GetChild(1).GetComponent<Camera>();
        CameY = transform.GetChild(0).GetChild(0);
        CameX = transform.GetChild(0).GetChild(0).GetChild(0);
        CameZ = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        VcamMain = transform.GetChild(0).GetChild(3);
        //Vcam[0] = transform.GetChild(0).GetChild(1).GetChild(0).gameObject;//回到第一人称 0
        //Vcam[1] = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;//前 1
        //Vcam[2] = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;//后 2
        //Vcam[3] = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;//左 3
        //Vcam[4] = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3).gameObject;//右 4


        Vcam = new CinemachineVirtualCamera[VcamMain.childCount];
        for(int i = 0; i < Vcam.Length; i++)
            Vcam[i] = VcamMain.GetChild(i).GetComponent<CinemachineVirtualCamera>();
#endif
        }
        #endregion

        #region 每帧执行的函数
        private void Update()
        {
            deltatime = Time.deltaTime;

            if (StartTime > 0) { StartTime -= deltatime; }
            else
            {
                MainVoid();
                InputKey();
                TimeAdd();
            }

            //test.Rotate(0, 90 * deltatime, 0);
        }
        #endregion

        #region 主要计算函数
        void MainVoid()
        {

            FpsCame.position = MainUI.Head.position;
            FpsCame.rotation = MainUI.Head.rotation;
            //CameX.position = MainUI.Head.position;
            //CameX.rotation = MainUI.Head.rotation;

            CameX.rotation = Quaternion.Lerp(CameX.rotation, MainUI.Head.rotation, 7 * Time.deltaTime);
            CameX.position = MainUI.Head.position;

        }
        #endregion

        #region 按键输入
        void InputKey()
        {
            if (Input.GetKeyDown(thirdKey))
            {
                Third = !Third;

                if (Third)
                {
                    MainCame.enabled = true;
                    UIcame.enabled = true;
                    SwitchCame(1);
                    EnbleMianCame = -1;//取消关闭
                }
                else
                {
                    SwitchCame(0);
                    EnbleMianCame = 0.7f;//2s内关闭came
                }
            }

            if (Third)
            {
                if (Input.GetMouseButton(1))
                {
                    Zoom = true;
                    //LR = Input.GetKeyDown(KeyCode.Tab) && LR == 4 ? 3 : 4;

                    if (Input.GetKeyDown(switchThirdKey))
                    {
                        LR = LR == 4 ? 3 : 4;
                        SwitchCame(LR);
                    }
                }
                else
                {
                    Zoom = false;
                }

                if (LastZoom != Zoom)
                {
                    if (Zoom) SwitchCame(LR);
                    else SwitchCame(1);

                    LastZoom = Zoom;
                }
            }

            //if (AnimStation)
            //{
            //    if (Input.GetKeyDown(KeyCode.M)) MainVRCStation.UseStation(LocalPlay);
            //}
            //if (Input.GetKeyDown(KeyCode.M)) ads();

        }
        #endregion

        #region 计时
        void TimeAdd()
        {
            if (EnbleMianCame > 0)
            {
                EnbleMianCame -= deltatime;
                if (EnbleMianCame <= 0)
                {
                    MainCame.enabled = false;
                    UIcame.enabled = false;

                }
            }

        }
        #endregion

        #region 自定义函数

        void SwitchCame(int ID)
        {
            if (ID != LastID)
            {
                if (ID >= Vcam.Length) return;

                //Vcam[LastID].SetActive(false);
                //Vcam[ID].SetActive(true);
                Vcam[LastID].enabled = false;
                Vcam[ID].enabled = true;
                LastID = ID;
            }
        }

        //void UseAttachedStation(VRCPlayerApi localp) { }


        #endregion

    }
}
