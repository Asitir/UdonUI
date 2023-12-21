
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class HandMotion_UI : UdonSharpBehaviour
    {
        [Header("打开菜单的按键设置")]
        [Tooltip("设置打开和关闭菜单的按键")]
        public KeyCode OpenEnumKey = KeyCode.X;
        [Header("关闭菜单的距离设置")]
        [Tooltip("离开当前范围关闭菜单")]
        public float maxDistance = 0.2f;
        [HideInInspector]
        public bool synChatIsOn = true;
        Vector3 nowPos;
        //public Transform ADS;
        [HideInInspector]
        public bool UIposToHead = false;
        MainUI_Script MainScript;
        //[HideInInspector]
        public GameObject[] MainGameobject = new GameObject[2];
        [HideInInspector]
        public string startname, stopName;
        [HideInInspector]
        public bool Audio_on;
        [HideInInspector]
        public AudioClip MainAudio1, MainAudio2;
        [HideInInspector]
        public int OpenUI = 0, OffUI = 0;//手势命令
        bool Lopne = false, Ropne = false;
        bool Loff = false, Roff = false;
        Animator playeranima;

        int LhandID = 1, RhandID = 1;
        int LhandID_late = 1, RhandID_late = 1;
        //GameObject nullobj;
        //Transform Rhand_1, Rhand_2, Lhand_1, Lhand_2;
        int StartID = 18, EndID = 18;
        bool animaplay, animaplay_late;
        bool star = false;

        float deltatime;
        VRCPlayerApi localplayer;
        [HideInInspector]
        public AudioSource MainAudio;
        PlayerCount MainplaySny;
        Player_Sny NowPlayerSny;
        bool isOn = false;
        void Start()
        {
            synChatIsOn = transform.parent.GetChild(0).childCount > 0 && transform.childCount > 0;
            if (synChatIsOn)
                MainplaySny = transform.parent.GetChild(0).GetComponent<PlayerCount>();
            localplayer = Networking.LocalPlayer;
            MainScript = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();

            MainScript.isSAOUI = true;
            MainScript.SAOUI = transform;
            MainScript._SAOUI = this;

            playeranima = MainGameobject[0].transform.GetChild(0).GetComponent<Animator>();
            MainGameobject[0].transform.position = Vector3.up * -1999;
            if (Audio_on)
            {
                MainAudio = Instantiate(MainScript.NullObject[1]).GetComponent<AudioSource>();
            }

            if (OpenUI < 6)
            {//选择了右手
                Ropne = true;
                StartID = OpenUI;
            }
            else if (OpenUI < 12)
            {//选择了左手
                Lopne = true;
                StartID = OpenUI - 6;
            }
            else if (OpenUI < 18)
            {//选择了双手
                Ropne = true;
                Lopne = true;
                StartID = OpenUI - 12;
            }

            if (OffUI < 6)
            {//选择了右手
                Roff = true;
                EndID = OffUI;
            }
            else if (OffUI < 12)
            {//选择了左手
                Loff = true;
                EndID = OffUI - 6;
            }
            else if (OffUI < 18)
            {//选择了双手
                Roff = true;
                Loff = true;
                EndID = OffUI - 12;
            }

        }

        private void Update()
        {
            //Debug.Log(MainplaySny.MainID);
            deltatime = Time.deltaTime;
            //ADS.Rotate(180 * deltatime, 0, deltatime);
            ///if (Input.GetKey(KeyCode.JoystickButton4)) Lhand = true; else Lhand = false;
            ///if (Input.GetKey(KeyCode.JoystickButton5)) Rhand = true; else Rhand = false;
            ///if (Input.GetKey(KeyCode.JoystickButton8))
            ///{
            ///    box.Rotate(0, 360 * deltatime, 0);
            ///    boxt.text = "JoystickButton8";
            ///    //左摇杆按下
            ///}
            ///if (Input.GetKey(KeyCode.JoystickButton9))
            ///{
            ///    box.Rotate(0, 360 * deltatime, 0);
            ///    boxt.text = "JoystickButton9";
            ///    //右摇杆按下
            ///}
            if (synChatIsOn)
            {
                if (MainplaySny.MainID > -1)
                {//
                    SaoMainScript(synChatIsOn);
                    if (!star) { playeranima.Play(stopName); star = true; }
                }
                else
                {
                    Debug.Log("按下了");
                }
            }
            else {
                SaoMainScript(synChatIsOn);
            }
        }

        void SaoMainScript(bool isSyn = true)
        {

            if (!MainGameobject[0].activeSelf) return;
            LhandID = MainScript.LhandID;
            RhandID = MainScript.RhandID;


            if (LhandID_late != LhandID)
            {
                int ID = LhandID / 10;
                ID--;
                LhandID_late = LhandID;
                if (Lopne)
                {
                    if (ID == StartID)
                    {
                        //if (UIposToHead) {
                        //    Vector3 pos = MainScript.Head.position + MainScript.Head.TransformDirection(-0.05f, 0.02f, 0.3f);
                        //    playeranima.SetBool(startname, true);
                        //    playeranima.transform.parent.position = pos;
                        //    playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.TransformDirection(0, 0, -1));
                        //    transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>().MainPos = pos;//同步UI位置
                        //}
                        //else
                        //{
                        //    playeranima.SetBool(startname, true);
                        //    playeranima.transform.parent.position = MainScript.HandInputPos;
                        //    playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.position - playeranima.transform.position, Vector3.up);
                        //    transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>().MainPos = MainScript.HandInputPos;
                        //}
                        isOn = true;
                        if (isSyn)
                            NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                        playeranima.Play(startname);

                        if (UIposToHead)
                        {
                            SynSaoEnum();
                            //Vector3 pos = MainScript.Head.position + MainScript.Head.TransformDirection(-0.05f, 0.02f, 0.3f);
                            //playeranima.transform.parent.position = pos;
                            //playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.TransformDirection(0, 0, -1));
                            //NowPlayerSny.MainPos = pos;//同步UI位置
                        }
                        else
                        {
                            SynSaoEnum2();
                            //playeranima.transform.parent.position = MainScript.HandInputPos;
                            //playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.position - playeranima.transform.position, Vector3.up);
                            //NowPlayerSny.MainPos = MainScript.HandInputPos;
                        }
                        SnyLastAnimator(true);
                    }
                }
                if (Loff)
                {
                    if (ID == EndID)
                    {
                        if (isOn)
                        {
                            playeranima.Play(stopName);
                            if (isSyn)
                                NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                            SnyLastAnimator(false);
                        }

                        //playeranima.SetBool(startname, false);
                        isOn = false;
                    }
                }

            }

            if (RhandID_late != RhandID)
            {
                int ID = RhandID / 10;
                ID--;
                RhandID_late = RhandID;
                if (Ropne)
                {
                    if (ID == StartID)
                    {//播放SAO启动动画
                     //if (UIposToHead)
                     //{
                     //    Vector3 pos = MainScript.Head.position + MainScript.Head.TransformDirection(-0.05f, 0.02f, 0.3f);
                     //    playeranima.SetBool(startname, true);
                     //    playeranima.transform.parent.position = pos;
                     //    playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.TransformDirection(0, 0, -1));
                     //    transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>().MainPos = pos;//同步UI位置
                     //}
                     //else
                     //{
                     //    playeranima.SetBool(startname, true);
                     //    playeranima.transform.parent.position = MainScript.HandInputPos;
                     //    playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.position - playeranima.transform.position, Vector3.up);
                     //    transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>().MainPos = MainScript.HandInputPos;//同步UI位置
                     //}
                        isOn = true;
                        if (isSyn)
                            NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                        playeranima.Play(startname);

                        if (UIposToHead)
                        {
                            SynSaoEnum();
                            //Vector3 pos = MainScript.Head.position + MainScript.Head.TransformDirection(-0.05f, 0.02f, 0.3f);
                            //playeranima.transform.parent.position = pos;
                            //playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.TransformDirection(0, 0, -1));
                            //NowPlayerSny.MainPos = pos;//同步UI位置
                        }
                        else
                        {
                            SynSaoEnum2();
                            //playeranima.transform.parent.position = MainScript.HandInputPos;
                            //playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.position - playeranima.transform.position, Vector3.up);
                            //NowPlayerSny.MainPos = MainScript.HandInputPos;//同步UI位置
                        }
                        SnyLastAnimator(true);

                    }
                }
                if (Roff)
                {
                    if (ID == EndID)
                    {//播放SAO关闭动画
                     //playeranima.SetBool(startname, false);
                        if (isOn)
                        {
                            playeranima.Play(stopName);
                            if (isSyn)
                                NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                            SnyLastAnimator(false);
                        }

                        isOn = false;
                    }
                }
            }

            if (Input.GetKeyDown(OpenEnumKey))
            {
                if (isOn)
                {
                    isOn = false;
                    playeranima.Play(stopName);
                    if (isSyn)
                        NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                    SnyLastAnimator(false);
                }
                else
                {
                    isOn = true;

                    if (isSyn)
                        NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                    playeranima.Play(startname);

                    Vector3 pos = MainScript.Head.position + MainScript.Head.TransformDirection(-0.05f, 0.02f, 0.2f);
                    playeranima.transform.parent.position = pos;
                    playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.TransformDirection(0, 0, -1));

                    if (isSyn)
                        NowPlayerSny.MainPos = pos;//同步UI位置

                    SnyLastAnimator(true);
                }
            }

            //PC端控制
            if (isOn)
            {//如果菜单是打开状态
                if (Vector3.Distance(localplayer.GetPosition(), nowPos) > maxDistance)
                {
                    if (isSyn)
                        NowPlayerSny = transform.parent.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                    playeranima.Play(stopName);
                    SnyLastAnimator(false);
                    isOn = false;
                }
            }
        }

        void SynSaoEnum()
        {
            Transform parentMain = playeranima.transform.parent;
            Transform Head = MainScript.Head;

            Vector3 pos = Head.TransformPoint(-0.05f, 0.02f, 0.3f);
            parentMain.position = pos;
            if (Vector3.Dot(Head.forward, Vector3.up) > 0.5f)
            {
                parentMain.rotation = Head.rotation;
                parentMain.Rotate(0, 180, 0);
            }
            else
            {
                parentMain.rotation = Quaternion.LookRotation(Head.TransformDirection(0, 0, -1));
            }
            if (synChatIsOn)
                NowPlayerSny.MainPos = pos;//同步UI位置
        }
        void SynSaoEnum2()
        {
            Transform parentMain = playeranima.transform.parent;
            Vector3 HandInputPos = MainScript.HandInputPos;
            Transform Head = MainScript.Head;
            parentMain.position = HandInputPos;

            if (Vector3.Dot(Head.forward, Vector3.up) > 0.5f)
            {
                parentMain.rotation = Head.rotation;
                parentMain.Rotate(0, 180, 0);
            }
            else
            {
                if (Vector3.Dot(HandInputPos - Head.position, Head.forward) > 0.5f)
                    parentMain.rotation = Quaternion.LookRotation(Head.TransformDirection(0, 0, -1));
                else
                    parentMain.rotation = Quaternion.LookRotation(Head.position - HandInputPos, Vector3.up);
            }

            //playeranima.transform.parent.position = MainScript.HandInputPos;
            //playeranima.transform.parent.rotation = Quaternion.LookRotation(MainScript.Head.position - playeranima.transform.position, Vector3.up);
            if (synChatIsOn)
                NowPlayerSny.MainPos = parentMain.position;//同步UI位置
        }

        /// <summary>
        /// 同步菜单状态为开启？
        /// </summary>
        /// <param name="On">开启？</param>
        void SnyLastAnimator(bool On)
        {

            if (On)
            {
                nowPos = localplayer.GetPosition();
                //打开菜单
                if (Audio_on)
                {
                    MainAudio.clip = MainAudio1;
                    MainAudio.Play();
                    MainAudio.transform.position = playeranima.transform.parent.position;

                    if (synChatIsOn)
                        if (MainplaySny.MainID > -1)
                        {
                            int ID = NowPlayerSny.InputID;
                            int iptid = (ID - 1000000000) / 100000000;//操作ID
                            if (iptid < 9) iptid++; else iptid = 0;

                            //----------------------------------------------------------------------------------------------(是否持有)----------(操作ID)------(行为‘手势’等)----(行为‘按下抬起’等)----(事件集ID)
                            NowPlayerSny.SetInputID((1000000000) + (iptid * 100000000) + (4 * 1000000) + (0 * 100000) + (1));//修改我的同步信息发送到所有客户端  
                                                                                                                           //播放启动UI动画
                        }
                }
            }
            else
            {//关闭菜单
                if (Audio_on)
                {
                    MainAudio.clip = MainAudio2;
                    MainAudio.Play();
                    //MainAudio.transform.position = MainScript.HandInputPos;

                    if (synChatIsOn)
                        if (MainplaySny.MainID > -1)
                        {
                            int ID = NowPlayerSny.InputID;
                            int iptid = (ID - 1000000000) / 100000000;//操作ID
                            if (iptid < 9) iptid++; else iptid = 0;

                            //----------------------------------------------------------------------------------------------(是否持有)----------(操作ID)------(行为‘手势’等)----(行为‘按下抬起’等)----(事件集ID)
                            NowPlayerSny.SetInputID((1000000000) + (iptid * 100000000) + (4 * 1000000) + (0 * 100000) + (2));//修改我的同步信息发送到所有客户端
                                                                                                                           //播放关闭UI动画
                        }
                }
            }

        }

    }
}