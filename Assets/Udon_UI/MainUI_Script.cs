
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDKBase;
//using UdonSharp;
using VRC.Udon;

namespace UdonUI
{
    //[RequireComponent(typeof(VRCMirrorReflection))]

    public class MainUI_Script : UdonSharpBehaviour
    {
        #region 定义变量
        public Transform vrcWorld;
        [HideInInspector]
        public int ActiveButton = 0, ActiveButton_Dis = 0;//当前最新按下的按钮 当前最新离开的按钮
        [HideInInspector]
        public bool ActiveButtonLeft = true, ActiveButtonLeft_Dis = true;//当前按钮触发的左右手  离开按钮触发的左右手
        [HideInInspector]
        public int scriptID = 0;
        [HideInInspector]
        public string scriptString = string.Empty;
        [HideInInspector]
        public float InitTime = 0;


        bool FixedBool = true;
        bool FixedBoolLR = true;
        GameObject[] ButtonAnima;//4个按钮动画对象  实例化的新材质
        GameObject ButtonLast;
        [Tooltip("按钮开始播放动画的时长")]
        public float StartAnimTime = 7;
        [Tooltip("是否启用按钮开始时的平滑动画？如果为是，“StartAnimTime”值将会变成动画速度，否则就为播放时长")]
        public bool StartLerp = true;
        [Tooltip("按钮结束动画的时长")]
        public float EndAnimTime = 0.2f;
        int seq = 0;

        [HideInInspector]
        public int life = 0, pl = 0;
        MaterialPropertyBlock HoverAnim;
        MaterialPropertyBlock ResetBlock;
        //Material[] ButtonAnimaLast;//储存的旧材质
        //GameObject[] ButtonObjLast;//储存旧的对象
        [HideInInspector]
        public Chat MainChat;
        float[] MaterilAnim;
        float MaterilDownAnim = 0;
        bool[] MateAnimForward;
        //[Tooltip("手势识别的敏感度等级")]
        //public int HandMotionSpeed = 1;//手势识别敏感度
        [HideInInspector]
        public GameObject RhandObject, LhandObject, PcObject;//右手，左手，PC  接触到的按钮
        bool Sta = false, Sta2 = false;
        [HideInInspector]
        public Transform Head, Rhand, Lhand, ButtonInput, LButtonInput, RButtonInput, Lzt, Rzt;//头，右控制器，左控制器，PC用的按钮输入,左手用的按钮输入，右手用的按钮输入,左指头，右指头
        [HideInInspector]
        public Vector3    mHeadPos;
        [HideInInspector]
        public Quaternion mHeadRot;
                                                                                               //状态
        [HideInInspector]
        public bool RhandDown = false, LhandDown = false, PcDown = false;//右手，左手，PC 是否有按下按钮
        [HideInInspector]
        public bool Rhandhover = false, Lhandhover = false, Pchover = false;//右手，左手，PC 是否有停留在按钮上方
        [HideInInspector]
        public Vector3 HandInputPos;//获得手指的位置
        [HideInInspector]
        public Vector3 LhandMousePos, RhandMousePos;//左手和右手停留在UI时 指针的位置
        [HideInInspector]
        public Quaternion LhandMouseRot, RhandMouseRot;//左手和右手停留在UI时 指针的角度
        [Tooltip("触发长按事件的时长")]
        public float KeyInputHoldTime = 0.5f;
        [Tooltip("触发长时间停留事件的时长")]
        public float HoverHodlTime = 1;

        #region 系统信息
        int Group = 50;

        int Group_number = 0, Group_number_y = 0, Group_ID = 0;
        int Group_number_Collider = 0, Group_number_y_Collider = 0, Group_ID_Collider = 0;
        bool Group_b = false;
        //[HideInInspector]
        //public GameObject UdonUI_AGI;
        //[HideInInspector]
        [HideInInspector]
        public bool Mouse_switch = false, PlayerHight_switch = false;
        [HideInInspector]
        public float PlayerHight = 0;
        [HideInInspector]
        public GameObject[] MousePos = new GameObject[1];
        [HideInInspector]
        public Transform LhandInputPos, RhandInputPos;//左手和右手停留在UI时 指针的位置

        [HideInInspector]
        public bool UdonUI_AGI_bool = false;
        GameObject RhandObject_Late, LhandObject_Late, PcObject_Late;//右手，左手，PC  上次接触到的按钮
        bool RhandDown_late = false, LhandDown_late = false, PcDown_late = false;//右手，左手，PC 上次是否有按下按钮
        bool Rhandhover_late = false, Lhandhover_late = false, Pchover_late = false;//右手，左手，PC 上次是否有停留在按钮上方
        float RhandInputHold = 0, LhandInputHold = 0, RhandHoverHold = 0, LhandHoverHold = 0;
        GameObject RGMmb, LGMmb, RGMmb_late, LGMmb_late;
        GameObject falshobj;
        //int[] LAllEvent, RAllEvent;
        int LhandObjID = 0, RhandObjID = 0;
        int LhandObjID_last = 0, RhandObjID_last = 0;
        [HideInInspector]
        public Transform SAOUI, HANDUI, VRUI;
        [HideInInspector]
        public bool isSAOUI = false, isHANDUI = false, isVRUI = false;
        [HideInInspector]
        public HandMotion_UI _SAOUI;
        //[HideInInspector]
        //public Player_Sny nowSyn;

        //手势捕捉
        [HideInInspector]
        public Transform Lhand_ss, Rhand_ss;
        [HideInInspector]
        public Transform Lhand_sstart, Rhand_sstart;

        Vector3 Lhand_startpos, Rhand_startpos;
        bool Lhand_key, Rhand_key;
        bool LhandK_late, RhandK_late;
        float Lhandf = -1, Rhandf = -1;
        [HideInInspector]
        public float Lhandf_dj = 0, Rhandf_dj = 0;
        float mig = 0.5f, fd = 0.05f;//响应时间（秒）  响应幅度（米）
        [HideInInspector]
        public int LhandID = 1, RhandID = 1;//后，前，上，下，左，右
        int LhandID_late = 1, RhandID_late = 1;

        Transform LAudio, RAudio;
        //[HideInInspector]
        //public Transform eyesOffset;
        AudioSource LeftAudio, RightAudio;
        [HideInInspector]
        public AudioClip[] MainAudio = new AudioClip[20];
        #region 手势
        //public GameObject[] Finger = new GameObject[1];//按钮
        //public bool[] synFinger = new bool[1] { false };//是否同步
        //[HideInInspector]
        //public int[] Action_Finger;//行为、状态:  0为上滑  1为下滑  2为左滑  3为右滑  4为前推  5为后拉
        //public Vector2[] MainEventNumber_Finger = new Vector2[1] { new Vector2(0, 0) };//主要方法的编码
        //public int[] MainEvent_Finger;//事件、执行: 0为物体开关切换  1为开启物体   2为关闭物体  3为播放动画机动画  4为设置动画机触发  5为设置动画机bool为true  6为啥子动画机bool为false  7为设置音效  8为设置自定义事件
        //[HideInInspector]
        //public Animator[] MainAnimators_Finger;//由事件执行的动画机------------------------------------//
        //[HideInInspector]
        //public string[] AnimaName_Finger;//由事件执行的动画机里面的动画名-----------------------------------//
        //[HideInInspector]
        //public GameObject[] TargetGameObject_Finger;//被作为开关物体活动的物体目标---------------------------------------//
        //[HideInInspector]
        //public int[] MainEvents_Finger;//自定义 事件编号--->
        #endregion

        #region 碰撞
        //[HideInInspector]
        public GameObject[] BoxColliderUdon = new GameObject[1];//按钮
        //[HideInInspector]
        public bool[] synBoxCollider = new bool[1] { false };//是否同步
        [HideInInspector]
        public int[] Action_BoxCollider;//行为、状态:  0为进入  1为退出
        //[HideInInspector]
        public Vector2[] MainEventNumber_BoxCollider = new Vector2[1] { new Vector2(0, 0) };//主要方法的编码
        [HideInInspector]
        public int[] MainEvent_BoxCollider;//事件、执行: 0为物体开关切换  1为开启物体   2为关闭物体  3为播放动画机动画  4为设置动画机触发  5为设置动画机bool为true  6为啥子动画机bool为false  7为设置音效  8为设置自定义事件
        [HideInInspector]
        public Animator[] MainAnimators_BoxCollider;//由事件执行的动画机------------------------------------//
        [HideInInspector]
        public string[] AnimaName_BoxCollider;//由事件执行的动画机里面的动画名-----------------------------------//
        //[HideInInspector]
        public GameObject[] TargetGameObject_BoxCollider;//被作为开关物体活动的物体目标---------------------------------------//
        [HideInInspector]
        public int[] MainEvents_BoxCollider;//自定义 事件编号--->
        #endregion

        #region 按钮
        [HideInInspector]
        public bool[] synButton = new bool[1] {false};//按钮是否同步
        //[HideInInspector]
        public Vector2[] MainEventNumber = new Vector2[1] { new Vector2(0, 0) };//主要方法的编码
        [HideInInspector]
        public GameObject[] Button = new GameObject[1];//按钮
        [HideInInspector]
        public int[] ButtonType = new int[1] { 1 };//按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮
        [HideInInspector]
        public int[] Action;//行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
        [HideInInspector]
        public int[] MainEvent;//事件、执行: 0为物体开关切换  1为开启物体   2为关闭物体  3为播放动画机动画  4为设置动画机触发  5为设置动画机bool为true  6为啥子动画机bool为false  7为设置音效  8为设置自定义事件

        [HideInInspector]
        public Animator[] MainAnimators;//由事件执行的动画机------------------------------------//
        [HideInInspector]
        public string[] AnimaName;//由事件执行的动画机里面的动画名-----------------------------------//
        [HideInInspector]
        public GameObject[] TargetGameObject;//被作为开关物体活动的物体目标---------------------------------------//
        [HideInInspector]
        public int[] MainEvents;//自定义 事件编号--->
        VRCPlayerApi localplayer;

        float InputUI_thickness = 0.01f;//UdonUI厚度(米)
        [Tooltip("背景版长度，识别按钮时的深度极限")]
        public float Input_Dis = 0.05f;//手指输入的距离(米)
        [Tooltip("预测手指落点的距离范围")]
        public float Predict_Dis2 = 0.05f;//预测距离

        float rayhit_lenth = 1;//射线长度
        //[HideInInspector]
        public GameObject[] NullObject;
        PlayerCount MainplaySny = null;
        [HideInInspector]
        public Vector3 LztPos, RztPos;
        private float lSelfOut, rSelfOut;
        bool PCInput_b = false, LInput_b = false, RInput_b = false;//面板射线方向切换
        #endregion

        //MyScript MainScript;
        [HideInInspector]
        [Tooltip("是否为VR设备")]
        public bool VRgm = true;
        float deltatime;
        RaycastHit HitRhand, HitLhand, HitPc, playerhig;
        RaycastHit HitRhand_Input, HitLhand_Input, HitPc_Input;
        LayerMask HitDirLayer, HitInputHit, playermask;//面板，按钮
        [HideInInspector]
        public Vector3 RhandPos, RshoulderPos, RIndexIntermediate, RIndexDistalPos;//右手，右肩,右食指第二节，右食指第三节
        [HideInInspector]
        public Vector3 LhandPos, LshoulderPos, LIndexIntermediate, LIndexDistalPos;
        //bool LhandIs = false, RhandIs = false;
        bool isSyn = false;
        #endregion
        #endregion

        #region 运行时初始化
        void Start()
        {
            localplayer = Networking.LocalPlayer;
            InitTime = 3;
            //#if !UNITY_EDITOR
            VRgm = localplayer.IsUserInVR();
            //#endif
            HoverAnim = new MaterialPropertyBlock();
            ResetBlock = new MaterialPropertyBlock();
            ResetBlock.SetFloat("_Anim", 0);
            ButtonAnima = new GameObject[3];
            MaterilAnim = new float[3];
            MateAnimForward = new bool[3];
            ButtonLast = gameObject;
            ButtonAnima[0] = gameObject;
            for (int i = 1; i < ButtonAnima.Length; i++)
            {
                ButtonAnima[i] = ButtonAnima[0];
            }
            for (int i = 0; i < ButtonAnima.Length; i++)
            {
                MaterilAnim[i] = 0;
                MateAnimForward[i] = false;
            }

            if (GameObject.Find("/UdonUI_AGI"))
            {
                UdonUI_AGI_bool = true;
                isSyn = NullObject[5].transform.GetChild(0).childCount > 0;
                if (isSyn)
                    MainplaySny = NullObject[5].transform.GetChild(0).GetComponent<PlayerCount>();
                //if (GameObject.Find("/UdonUI_AGI/SAOUI"))
                //{//设置SAOUI初始值
                //    SAOUI = NullObject[5].transform.Find("SAOUI");
                //    _SAOUI = SAOUI.GetComponent<HandMotion_UI>();
                //}
                //if (GameObject.Find("/UdonUI_AGI/HandUI"))
                //    HANDUI = NullObject[5].transform.Find("HandUI");
                //if (GameObject.Find("/UdonUI_AGI/VRUI"))
                //    VRUI = NullObject[5].transform.Find("VRUI");
            }

            //Networking.LocalPlayer.PushAnimations(A.runtimeAnimatorController);
            #region 系统信息
            //MainScript = transform.GetComponent<MyScript>();

            Sta2 = Sta = false;
            //设置辅助
            Head = Instantiate(NullObject[0]).transform;
            Rhand = Instantiate(NullObject[0]).transform;
            Lhand = Instantiate(NullObject[0]).transform;
            ButtonInput = Instantiate(NullObject[0]).transform;
            LButtonInput = Instantiate(NullObject[0]).transform;
            RButtonInput = Instantiate(NullObject[0]).transform;
            Lzt = Instantiate(NullObject[0]).transform;
            Rzt = Instantiate(NullObject[0]).transform;
            falshobj = Instantiate(NullObject[0]);
            Lhand_ss = Instantiate(NullObject[0]).transform;
            Rhand_ss = Instantiate(NullObject[0]).transform;
            Lhand_sstart = Instantiate(NullObject[0]).transform;
            Rhand_sstart = Instantiate(NullObject[0]).transform;
            LAudio = Instantiate(NullObject[1]).transform;
            RAudio = Instantiate(NullObject[1]).transform;

            string _name = "UdonUI_Object";
            Head.name = _name;
            Rhand.name = _name;
            Lhand.name = _name;
            ButtonInput.name = _name;
            LButtonInput.name = _name;
            RButtonInput.name = _name;
            Lzt.name = _name;
            Rzt.name = _name;
            falshobj.name = _name;
            Lhand_ss.name = _name;
            Rhand_ss.name = _name;
            Lhand_sstart.name = _name;
            Rhand_sstart.name = _name;
            LAudio.name = "UdonUI_AudioL";
            RAudio.name = "UdonUI_AudioR";

            //eyesOffset = Instantiate(NullObject[4]).transform;
            Transform eyesOffset = NullObject[4].transform;
            eyesOffset.SetParent(Head);
            eyesOffset.localRotation = Quaternion.Euler(90, 0, 0);
            eyesOffset.localPosition = new Vector3(0, -100, 1);
            //eyesOffset.name = "EyeOffset";

            if (BoxColliderUdon.Length > 0)
            {
                Transform boxCo = Instantiate(NullObject[3]).transform;//碰撞触发
                boxCo.SetParent(Head);
            }
            if (Mouse_switch)
            {
                LhandInputPos = Instantiate(MousePos[0]).transform;
                RhandInputPos = Instantiate(MousePos[0]).transform;
            }

            //定义本地玩家
            rayhit_lenth = Input_Dis + Predict_Dis2;//射线长度

            //定义碰撞层
            HitDirLayer = 1 << 13;//开启第13层（面板）: Pickup
            HitInputHit = 1 << 4;  //开启第4层（按钮）: Water
            playermask = 1 << 10; //开启第10层 (本地玩家):localplayer

            //初始化自定参数
            LGMmb_late = falshobj;
            RGMmb_late = falshobj;
            LGMmb = falshobj;
            RGMmb = falshobj;

            Group_number = Button.Length / Group;//得到组数
            Group_number_y = Button.Length - Group * Group_number;//拿到余数

            Group_number_Collider = BoxColliderUdon.Length / Group;//得到组数
            Group_number_y_Collider = BoxColliderUdon.Length - Group * Group_number_Collider;//拿到余数

            CheckPlayerID_Start();
            #endregion
            //MainBotton[1].OnPointerDown();

            //MainBotton[1].onClick.Invoke();
        }
        #endregion

        #region 每帧执行
        private void FixedUpdate()
        {
            Sethit();//射线信息
        }

        private void Update()
        {
            deltatime = Time.deltaTime;
            Update_UdonUI();
            //CheckPlayerID_Update();
            OnMoveWindow_Update();//移动窗口
            if (InitTime >= 0) InitTime -= deltatime;
        }

        private void Update_UdonUI()
        {
            if (!Group_b)
            {//按组初始化

                //Group_ID = Group;
                //Group_number_y = 0;//不再修改按钮
                if (Group_ID < Group_number)
                {
                    for (int i = 0; i < Group; i++)
                    {
                        int id = i + (Group_ID * Group);
                        //BoxCollider SetBoxCollider = Button[id].GetComponent<BoxCollider>();
                        //SetBoxCollider.center = new Vector3(SetBoxCollider.center.x, SetBoxCollider.center.y, (Input_Dis / SetBoxCollider.transform.lossyScale.z + (InputUI_thickness * 0.5f)));
                        //SetBoxCollider.size = new Vector3(SetBoxCollider.size.x, SetBoxCollider.size.y, InputUI_thickness);
                        Button[id].name = "UUB_" + id;
                    }
                    Group_ID++;
                }
                else
                {
                    if (Group_number_y > 0)
                    {
                        for (int i = 0; i < Group_number_y; i++)
                        {
                            int id = i + (Group_ID * Group);
                            //BoxCollider SetBoxCollider = Button[id].GetComponent<BoxCollider>();
                            //SetBoxCollider.center = new Vector3(SetBoxCollider.center.x, SetBoxCollider.center.y, (Input_Dis / SetBoxCollider.transform.lossyScale.z + (InputUI_thickness * 0.5f)));
                            //SetBoxCollider.size = new Vector3(SetBoxCollider.size.x, SetBoxCollider.size.y, InputUI_thickness);
                            Button[id].name = "UUB_" + id;
                        }
                        Group_number_y = 0;
                    }

                    if (Group_ID_Collider < Group_number_Collider)
                    {
                        for (int i = 0; i < Group; i++)
                        {
                            int id = i + (Group_ID_Collider * Group);
                            //BoxColliderUdon[id].transform.GetChild(0).name = "UCo_" + id;
                            BoxColliderUdon[id].name = "UCo_" + id;
                        }
                        Group_ID_Collider++;
                    }
                    else
                    {
                        if (Group_number_y_Collider > 0)
                        {
                            for (int i = 0; i < Group_number_y_Collider; i++)
                            {
                                int id = i + (Group_ID_Collider * Group);
                                //BoxColliderUdon[id].transform.GetChild(0).name = "UCo_" + id;
                                BoxColliderUdon[id].name = "UCo_" + id;

                            }
                        }
                        Group_b = true;
                    }

                }
            }
            else
            {
                #region 系统信息
                //初始化音频组件
                if (!Sta2)
                {
                    if (Sta)
                    {
                        LeftAudio = LAudio.GetComponent<AudioSource>();
                        RightAudio = RAudio.GetComponent<AudioSource>();
                        Sta2 = true;
                    }
                    Sta = true;
                }
                #endregion

                ////UpdateEyesOffset();
                //if (lastFrameCount > 0)
                //{
                //    UpdateEyesOffset();
                //    lastFrameCount--;
                //    updateEyesOffsetTime -= deltatime;
                //    if (lastFrameCount <= 0)
                //    {
                //        if (updateEyesOffsetTime > 0)
                //            lastFrameCount = 1;//次数执行完毕后时间不到就继续执行
                //    }
                //}

                //#if !UNITY_EDITOR
                SetPos();//获取并设置骨骼位置
                         //#endif
                Hand_Motion();//手势判断
                ButtonAnimPlay(StartAnimTime);
                //if (!VRgm) Sethit();//射线信息
                //displayvalue();
                //ces();//测试用

            }
        }
        #endregion

        #region 射线判断
        void Sethit()
        {
            //初始化
            Lhandhover = false;//退出预测范围
            LhandDown = false; //已抬起按钮
            Rhandhover = false;//退出预测范围
            RhandDown = false; //已抬起按钮

            if (VRgm)
            {
                FixedBoolLR = !FixedBoolLR;

                //LztPos = Lzt.position + Lzt.TransformDirection(0, Vector3.Distance(LIndexIntermediate, LIndexDistalPos) * 0.85f, 0);//左食指末端
                //RztPos = Rzt.position + Rzt.TransformDirection(0, Vector3.Distance(RIndexIntermediate, RIndexDistalPos) * 0.85f, 0);//右食指末端
                //ASD.position = RztPos;
                #region 检查左右手的状态
                if (FixedBoolLR)
                {
                    RztPos = Rzt.TransformPoint(0, Vector3.Distance(RIndexIntermediate, RIndexDistalPos) * 0.85f, 0);//右食指末端

                    //右手的射线
                    if (RInput_b)
                    {
                        //确认面板
                        Vector3 _findPoint = RztPos + (RButtonInput.forward * 0.1f);
                        if (Physics.Raycast(_findPoint, RButtonInput.forward * -1, out HitRhand, 0.2f, HitDirLayer))
                        {
                            RButtonInput.rotation = Quaternion.LookRotation(HitRhand.normal);
                            if (HitRhand.transform.gameObject.name == "Udon_ControlPanel_Range")
                            {
                                RGMmb = HitRhand.transform.gameObject;
                                RhandMousePos = HitRhand.point;//手指落点位置
                                RhandMouseRot = Quaternion.LookRotation(HitRhand.normal, Vector3.up);

                                //面板在范围内  开始查找按钮
                                if (Physics.Raycast(_findPoint, RButtonInput.forward * -1, out HitRhand_Input, 0.2f, HitInputHit))//rayhit_lenth
                                {
                                    RhandObject = HitRhand_Input.transform.gameObject;//得到当前的按钮对象
                                    string[] objname = RhandObject.name.Split('_');
                                    if (objname[0] == "UUB")
                                    {
                                        RButtonInput.position = HitRhand_Input.point;

                                        RhandObjID = int.Parse(objname[1]);//得到按键ID
                                        if (RhandObjID_last != RhandObjID)
                                            RhandObjID_last = RhandObjID;
                                        else
                                            Rhandhover = true;//进入预测范围

                                        //if (Vector3.Distance(_findPoint, HitRhand_Input.point) < Input_Dis)
                                        if (RButtonInput.InverseTransformPoint(RztPos).z < rSelfOut)
                                        {
                                            RhandDown = true;//已检测到按下按钮
                                            if (ButtonType[RhandObjID] == 2)//滑条
                                                SliderEvents(RhandObjID, RhandMousePos);
                                            rSelfOut = 0.01f;
                                        }
                                        else
                                        {
                                            rSelfOut = 0;
                                        }
                                    }
                                }
                                if (Vector3.Distance(RztPos, HitRhand.point) > 0.1f) RhandMousePos = Vector3.zero;//重置手指落点位置
                            }
                            else
                            {
                                RInput_b = false;//面板离开范围或不在手指范围
                                RhandMousePos = Vector3.zero;//重置手指落点位置
                                RGMmb = falshobj;
                                //RhandIs = false;
                                RhandObjID_last = -1;
                                rSelfOut = 0;
                            }
                        }
                        else
                        {
                            RInput_b = false;//面板离开范围或不在手指范围
                            RhandMousePos = Vector3.zero;//重置手指落点位置
                            RGMmb = falshobj;
                            //RhandIs = false;
                            RhandObjID_last = -1;
                            rSelfOut = 0;
                        }
                    }
                    else
                    {
                        //寻找面板
                        if (Physics.Raycast(RztPos, RztPos - RshoulderPos, out HitRhand, 2, HitDirLayer))//从指头位置  肩膀到指头的方向发射射线
                        {
                            RButtonInput.rotation = Quaternion.LookRotation(HitRhand.normal);
                            RInput_b = true;//找到了面板
                        }
                    }
                    GMmb_R();//设置面板

                    #region 输入指令
                    //按下
                    RhandDown_late = InputKeyDown(RhandDown_late, RhandDown, RhandObjID, 0, false, RhandObject);
                    //长按
                    RhandInputHold = InputHold(RhandDown, RhandInputHold, KeyInputHoldTime, RhandObjID, 0, false);

                    //悬停
                    Rhandhover_late = InputKeyDown(Rhandhover_late, Rhandhover, RhandObjID, 3, false, RhandObject);
                    //持续停留
                    RhandHoverHold = InputHold(Rhandhover, RhandHoverHold, HoverHodlTime, RhandObjID, 3, false);
                    #endregion
                    if (Mouse_switch)
                    {
                        //在面板变化后更新位置
                        RhandInputPos.SetPositionAndRotation(RhandMousePos, RhandMouseRot);
                    }

                }
                else
                {
                    LztPos = Lzt.TransformPoint(0, Vector3.Distance(LIndexIntermediate, LIndexDistalPos) * 0.85f, 0);//左食指末端

                    //左手的射线
                    if (LInput_b)
                    {
                        //从面板方向确认面板范围
                        Vector3 _findPoint = LztPos + (LButtonInput.forward * 0.1f);
                        if (Physics.Raycast(_findPoint, LButtonInput.forward * -1, out HitLhand, 0.2f, HitDirLayer))
                        {
                            LButtonInput.rotation = Quaternion.LookRotation(HitLhand.normal);
                            if (HitLhand.transform.gameObject.name == "Udon_ControlPanel_Range")
                            {
                                LGMmb = HitLhand.transform.gameObject;
                                LhandMousePos = HitLhand.point + LButtonInput.TransformDirection(0, 0, Input_Dis);//手指落点位置
                                LhandMouseRot = Quaternion.LookRotation(HitLhand.normal, Vector3.up);
                                //找到了面板后开始确认按钮
                                if (Physics.Raycast(_findPoint, LButtonInput.forward * -1, out HitLhand_Input, 0.2f, HitInputHit))
                                {
                                    LhandObject = HitLhand_Input.transform.gameObject;//得到当前的按钮对象
                                    string[] objname = LhandObject.name.Split('_');
                                    if (objname[0] == "UUB")
                                    {
                                        LButtonInput.position = HitLhand_Input.point;
                                        LhandObjID = int.Parse(objname[1]);//得到按键ID
                                        if (LhandObjID_last != LhandObjID)
                                            LhandObjID_last = LhandObjID;
                                        else
                                            Lhandhover = true;//进入预测范围

                                        if (LButtonInput.InverseTransformPoint(LztPos).z < lSelfOut)
                                        {
                                            LhandDown = true;//已检测到按下按钮
                                            if (ButtonType[LhandObjID] == 2)//滑条
                                                SliderEvents(LhandObjID, LhandMousePos);
                                            lSelfOut = 0.01f;
                                        }
                                        else
                                        {
                                            lSelfOut = 0;
                                        }
                                    }
                                    //Debug.DrawLine(LztPos, HitLhand_Input.point, Color.red);
                                }

                                if (Vector3.Distance(LztPos, HitLhand.point) > 0.1f) LhandMousePos = Vector3.zero;//重置手指落点位置
                            }
                            else
                            {
                                LInput_b = false;//从面板方向没有找到面板
                                LhandMousePos = Vector3.zero;
                                LGMmb = falshobj;
                                //LhandIs = false;
                                LhandObjID_last = -1;
                                lSelfOut = 0;
                            }
                        }
                        else
                        {
                            LInput_b = false;//从面板方向没有找到面板
                            LhandMousePos = Vector3.zero;
                            LGMmb = falshobj;
                            //LhandIs = false;
                            LhandObjID_last = -1;
                            lSelfOut = 0;
                        }
                    }
                    else
                    {
                        //寻找面板
                        if (Physics.Raycast(LztPos, LztPos - LshoulderPos, out HitLhand, 2, HitDirLayer))//从指头位置  肩膀到指头的方向发射射线
                        {
                            LButtonInput.rotation = Quaternion.LookRotation(HitLhand.normal);
                            LInput_b = true;//找到了面板
                                            //Debug.DrawLine(HitLhand.point, LztPos, Color.red);
                        }
                    }
                    GMmb_L();//设置面板

                    #region 输入指令
                    //按下
                    LhandDown_late = InputKeyDown(LhandDown_late, LhandDown, LhandObjID, 0, true, LhandObject);
                    //长按
                    LhandInputHold = InputHold(LhandDown, LhandInputHold, KeyInputHoldTime, LhandObjID, 0, true);

                    //悬停
                    Lhandhover_late = InputKeyDown(Lhandhover_late, Lhandhover, LhandObjID, 3, true, LhandObject);
                    //持续停留
                    LhandHoverHold = InputHold(Lhandhover, LhandHoverHold, HoverHodlTime, LhandObjID, 3, true);
                    #endregion
                    if (Mouse_switch)
                    {
                        //在面板变化后更新位置
                        LhandInputPos.SetPositionAndRotation(LhandMousePos, LhandMouseRot);
                    }

                }
                #endregion



            }
            else
            {
                //寻找面板
                if (Physics.Raycast(Head.position, Head.forward, out HitPc, 2, HitDirLayer))
                {
                    if (HitPc.transform.gameObject.name == "Udon_ControlPanel_Range")
                    {
                        ButtonInput.SetPositionAndRotation(HitPc.point, Quaternion.LookRotation(HitPc.normal));//得到面板落脚位置
                        RhandMousePos = HitPc.point;
                        RhandMouseRot = Quaternion.LookRotation(HitPc.normal, Vector3.up);
                        Vector3 inputpos = Vector3.zero;
                        if (Input.GetMouseButton(0)) inputpos = ButtonInput.TransformPoint(0, 0, -0.05f);//输入
                        else inputpos = ButtonInput.position;//悬停

                        if (Physics.Raycast(inputpos + (ButtonInput.forward * 0.1f), ButtonInput.forward * -1, out HitRhand_Input, 0.2f, HitInputHit))
                        {
                            RhandObject = HitRhand_Input.transform.gameObject;//得到当前的按钮对象
                            string[] objname = RhandObject.name.Split('_');
                            if (objname[0] == "UUB")
                            {
                                RhandObjID = int.Parse(objname[1]);//得到按键ID
                                if (RhandObjID_last != RhandObjID)
                                    RhandObjID_last = RhandObjID;
                                else
                                    Rhandhover = true;//进入预测范围
                                if (ButtonInput.InverseTransformPoint(inputpos).z < 0)
                                {
                                    RhandDown = true;//已检测到按下按钮
                                    if (ButtonType[RhandObjID] == 2)//滑条
                                        SliderEvents(RhandObjID, RhandMousePos);
                                }
                            }
                        }
                    }
                    else
                    {
                        RhandMousePos = Vector3.zero;
                        RhandObjID_last = -1;
                    }
                }
                else
                {
                    RhandMousePos = Vector3.zero;
                    RhandObjID_last = -1;
                }
                //--------------------------------------------------//
                #region 输入指令
                //按下
                RhandDown_late = InputKeyDown(RhandDown_late, RhandDown, RhandObjID, 0, false, RhandObject);
                //长按
                RhandInputHold = InputHold(RhandDown, RhandInputHold, KeyInputHoldTime, RhandObjID, 0, false);

                //悬停
                Rhandhover_late = InputKeyDown(Rhandhover_late, Rhandhover, RhandObjID, 3, false, RhandObject);
                //持续停留
                RhandHoverHold = InputHold(Rhandhover, RhandHoverHold, HoverHodlTime, RhandObjID, 3, false);
                #endregion

                if (Mouse_switch) RhandInputPos.SetPositionAndRotation(RhandMousePos, RhandMouseRot);
            }
        }
        #endregion

        #region 脚本生命判断（检测脚本是否崩溃）
        //private void FixedUpdate()
        //{

        //}
        #endregion

        #region 获取玩家位置判断点
        void SetPos()
        {
            RhandPos = localplayer.GetBonePosition(HumanBodyBones.RightHand);//右手位置
            RshoulderPos = localplayer.GetBonePosition(HumanBodyBones.RightShoulder);//右肩膀
            RIndexIntermediate = localplayer.GetBonePosition(HumanBodyBones.RightIndexIntermediate);//右食指第二节
            RIndexDistalPos = localplayer.GetBonePosition(HumanBodyBones.RightIndexDistal);//右食指第三节

            LhandPos = localplayer.GetBonePosition(HumanBodyBones.LeftHand);
            LshoulderPos = localplayer.GetBonePosition(HumanBodyBones.LeftShoulder);
            LIndexIntermediate = localplayer.GetBonePosition(HumanBodyBones.LeftIndexIntermediate);
            LIndexDistalPos = localplayer.GetBonePosition(HumanBodyBones.LeftIndexDistal);

            Lzt.position = LIndexDistalPos;
            Lzt.rotation = localplayer.GetBoneRotation(HumanBodyBones.LeftIndexDistal);
            Rzt.position = RIndexDistalPos;
            Rzt.rotation = localplayer.GetBoneRotation(HumanBodyBones.RightIndexDistal);

            //localplayer.GetBoneTransform(HumanBodyBones.LeftIndexDistal).position = new Vector3(0, 0, 0);
            mHeadPos = localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
            mHeadRot = localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
            Head.SetPositionAndRotation(mHeadPos, mHeadRot);//头部控制器
//#if !UNITY_EDITOR
            Lhand.SetPositionAndRotation(localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position, localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).rotation);//左手控制器
            Rhand.SetPositionAndRotation(localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position, localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).rotation);//右手控制器
//#else
//            Rhand.SetPositionAndRotation(Head.position, Head.rotation);//右手控制器
//#endif
            Lhand_ss.position = LhandPos;
            Lhand_ss.rotation = Quaternion.LookRotation(LshoulderPos - LhandPos, Vector3.up);
            Rhand_ss.position = RhandPos;
            Rhand_ss.rotation = Quaternion.LookRotation(RshoulderPos - RhandPos, Vector3.up);

            //Chest.position = localplayer.GetBonePosition(HumanBodyBones.Chest);
            //Chest.rotation = Quaternion.LookRotation(RshoulderPos - Chest.position, Head.position - Chest.position);
            //Chest.SetPositionAndRotation(localplayer.GetBonePosition(HumanBodyBones.UpperChest), localplayer.GetBoneRotation(HumanBodyBones.LeftIndexDistal));
        }
        #endregion

        #region 按钮事件触发监视
        /// <summary>
        /// 按钮事件触发监视
        /// </summary>
        /// <param name="Key_Late">按钮上一帧状态</param>
        /// <param name="Key">按钮当前帧状态</param>
        /// <param name="InputID">获得的按钮ID</param>
        /// <param name="Numbered">步长 输入状态  按下  或者进入悬停</param>
        /// <returns></returns>
        bool InputKeyDown(bool Key_Late, bool Key, int InputID, int Numbered, bool LeftHand, GameObject NowButton)
        {
            bool A = Key_Late;
            if (Key_Late != Key)
            {
                if (Key)
                {//按下的一瞬间
                    But_RunEvents(0 + Numbered, InputID, LeftHand);
                    if (Numbered == 3) HoverButtonAnim(NowButton);
                    else InDownButtonAnim();

                    ActiveButton = InputID;
                    ActiveButtonLeft = LeftHand;
                }
                else
                {//抬起的一瞬间
                    But_RunEvents(2 + Numbered, InputID, LeftHand);
                    if (Numbered == 3) HoverButtonAnimEnd();

                    ActiveButton_Dis = InputID;
                    ActiveButtonLeft_Dis = LeftHand;
                }
                A = Key;
            }
            return A;
        }
        #endregion

        #region 按钮长按状态判断
        /// <summary>
        /// 按钮长按状态判断
        /// </summary>
        /// <param name="InputKey">按钮状态</param>
        /// <param name="TimeAdd">叠加的变量</param>
        /// <param name="HoldTime">触发时间</param>
        /// <param name="InputID">获得按钮ID</param>
        /// <param name="Numbered">步长</param>
        /// <returns></returns>
        float InputHold(bool InputKey, float TimeAdd, float HoldTime, int InputID, int Numbered, bool LeftHand)
        {
            float A = TimeAdd;
            if (InputKey)
            {
                if (A > -1) A += deltatime;
                if (A >= HoldTime)
                {
                    //长按条件达成
                    But_RunEvents(1 + Numbered, InputID, LeftHand);
                    A = -1;
                }
            }
            else
            {
                A = 0;
            }

            return A;
        }
        #endregion

        #region 执行事件

        #region 按钮事件
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="ButtonAction">事件ID</param>
        /// <param name="ButtonID">按钮ID</param>
        public void But_RunEvents(int ButtonAction, int ButtonID, bool LeftHand, bool isAction = false)
        {
            Vector2 MainEvent_m = MainEventNumber[ButtonID];//得到这个事件集的所有事件

            int switchbool = 3;//2同步物体状态为false  3同步物体为true
            int AllEvent = 0;//得到在指定状态下所有应该执行的事件
            int[] AllEventID = new int[(int)MainEvent_m.y];
            for (int i = 0; i < AllEventID.Length; i++)
            {
                if (Action[(int)MainEvent_m.x + i] == ButtonAction)//行为
                {
                    AllEventID[AllEvent] = (int)MainEvent_m.x + i;//把得到的事件储存起来
                    AllEvent++;//计数
                }
            }
            //Debug.Log($"执行了按钮  \n按钮ID: {ButtonID}  \n事件数量: {AllEvent}");

            //MyScript mainscript = 
            //执行事件
            for (int i = 0; i < AllEvent; i++)
            {
                int ID = AllEventID[i];
                switch (MainEvent[ID])
                {
                    case 0:
                        //if (TargetGameObject[ID] != null)
                        if (TargetGameObject[ID].activeInHierarchy) switchbool = 2;
                        TargetGameObject[ID].SetActive(!TargetGameObject[ID].activeInHierarchy);//切换物体Active
                        break;
                    case 1:
                        //if (TargetGameObject[ID] != null)
                        TargetGameObject[ID].SetActive(true);//打开物体Active
                        break;
                    case 2:
                        //if (TargetGameObject[ID] != null)
                        TargetGameObject[ID].SetActive(false);//关闭物体Active
                        break;
                    case 3:
                        //if (MainAnimators[ID] != null)
                        MainAnimators[ID].Play(AnimaName[ID]);//播放Animator指定动画
                        break;
                    case 4:
                        //if (MainAnimators[ID] != null)
                        MainAnimators[ID].SetTrigger(AnimaName[ID]);//设置Animator触发器
                        break;
                    case 5:
                        //if (MainAnimators[ID] != null)
                        MainAnimators[ID].SetBool(AnimaName[ID], true);//设置Animator布尔
                        break;
                    case 6:
                        //if (MainAnimators[ID] != null)
                        MainAnimators[ID].SetBool(AnimaName[ID], false);//设置Animator布尔
                        break;
                    case 7:
                        //7设置音效(1)
                        PlayAudio(LeftHand, MainEvent[ID] - 7);//0
                        break;
                    case 8:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 9:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 10:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 11:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 12:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 13:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 14:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 15:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 16:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 17:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 18:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 19:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 20:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 21:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 22:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 23:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 24:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 25:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 26:
                        PlayAudio(LeftHand, MainEvent[ID] - 7);
                        break;
                    case 27:
                        if (isAction) break;
                        //执行震动
                        if (LeftHand)
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.2f, 0.1f, 320);//持续时间  震动幅度  频率
                        else
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.2f, 0.1f, 320);
                        break;
                    case 28:
                        if (isAction) break;
                        //执行震动
                        if (LeftHand)
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.2f, 1f, 320);
                        else
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.2f, 1f, 320);
                        break;
                    case 29:
                        if (isAction) break;
                        //执行震动
                        if (LeftHand)
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.2f, 3, 320);
                        else
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.2f, 3, 320);
                        break;
                    case 30:
                        if (isAction) break;
                        //执行震动
                        if (LeftHand)
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.36f, 3f, 320);
                        else
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.36f, 3f, 320);
                        break;
                    case 31:
                        if (isAction) break;
                        //执行震动
                        if (LeftHand)
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 1f, 10f, 320);
                        else
                            localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 1f, 10f, 320);
                        break;
                    #region 自定义事件
                    case 32://自定义事件
                        //TargetGameObject[ID].SendMessage(AnimaName[ID]);\
                        RunEnvent(TargetGameObject[ID], AnimaName[ID]);
                        break;
                    case 33:
                        RunButtonEnvent(TargetGameObject[ID], AnimaName[ID]);
                        break;
                    case 34:
                        RunTriggerEnvent(TargetGameObject[ID], AnimaName[ID]);
                        break;

                        /*                    
                                            case 32://执行自定义事件
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 33:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 34:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 35:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 36:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 37:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 38:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 39:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 40:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 41:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 42:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 43:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 44:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 45:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 46:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 47:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 48:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 49:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 50:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 51:
                                                MainScript.RunScript(MainEvent[ID] - 31);//A组
                                                break;
                                            case 52:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 53:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 54:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 55:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 56:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 57:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 58:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 59:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 60:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 61:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 62:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 63:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 64:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 65:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 66:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 67:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 68:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 69:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 70:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 71:
                                                MainScript.RunScript(MainEvent[ID] - 31);//B组
                                                break;
                                            case 72:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 73:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 74:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 75:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 76:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 77:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 78:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 79:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 80:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 81:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 82:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 83:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 84:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 85:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 86:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 87:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 88:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 89:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 90:
                                                MainScript.RunScript(MainEvent[ID] - 31);
                                                break;
                                            case 91:
                                                MainScript.RunScript(MainEvent[ID] - 31);//C组
                                                break;
                        */
                        #endregion
                }
            }
            //NullObject[3].transform.GetChild(0).GetChild(0).GetComponent<Player_Sny>().InputID = (1000000000) + (9 * 100000000) + (switchbool * 1000000) + (ButtonAction * 100000) + (EventID);

            if (synButton[ButtonID] && !isAction)
            {//如果这个按钮开启了同步
                if (ButtonType[ButtonID] == 1)
                {
                    if (ButtonAction == 0 || ButtonAction == 1)
                    {//只同步按下和长按事件
                        if (UdonUI_AGI_bool)
                        {//如果场景中存在这个对象
                            if (isSyn)
                                if (MainplaySny.MainID > -1)
                                {
                                    //Debug.Log("尝试同步");
                                    Player_Sny nowSyn = NullObject[5].transform.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                                    int ID = nowSyn.InputID;
                                    if ((int)(ID / 1000000000) > 0)
                                    {
                                        //Debug.Log("尝试同步");
                                        int iptid = (ID - 1000000000) / 100000000;//操作ID
                                        if (iptid < 9) iptid++; else iptid = 0;
                                        nowSyn.SetInputID((1000000000) + (iptid * 100000000) + (switchbool * 1000000) + (ButtonAction * 100000) + (ButtonID));
                                    }
                                }
                                //else
                                //{
                                //    Debug.Log("未同步");
                                //}

                            //Transform snyobj = NullObject[3].transform.GetChild(0);
                            //for (int i = 0; i < snyobj.childCount; i++)
                            //{
                            //    if (Networking.GetOwner(snyobj.GetChild(i).gameObject) == localplayer)
                            //    {
                            //        int ID = snyobj.GetChild(i).GetComponent<Player_Sny>().InputID;
                            //        if ((int)(ID / 1000000000) > 0)
                            //        {
                            //            ////int.max = 2147483647
                            //            ////  2^14^74^83647
                            //            ////是否持有^操作ID^行为^事件ID
                            //            //int mast = 1;//是否持有（是
                            //            //int inputid = (int)((ID - 1000000000) / 10000000);//操作ID
                            //            ////int actin = (int)(ID - ((1000000000 + (inputid * 10000000))) / 10000);
                            //            //int actin = 1;//行为(执行按钮事件
                            //            ////int eve = ID - ((int)(ID / 100000) * 100000);//事件ID

                            //            //if (inputid < 99) inputid++; else inputid = 0;
                            //            ////eve = AllEvent;

                            //            //snyobj.GetChild(i).GetComponent<Player_Sny>().InputID = (mast * 1000000000) + (inputid * 10000000) + (actin * 100000) + (AllEvent);


                            //            //int.max = 2147483647
                            //            //  2^1^47^4^83647
                            //            //是否持有^操作ID^行为(按钮，攻击，等)^行为（按下 抬起）^事件ID

                            //            //int master = 1;//是否持有（是
                            //            int iptid = (ID - 1000000000) / 100000000;//操作ID
                            //            //int Acti = (ID - ((ID / 100000000) * 100000000)) / 1000000;//行为(按钮，攻击，等)
                            //            //int Actib = (ID - ((ID / 1000000) * 1000000)) / 100000;//行为（按下 抬起,等）
                            //            //int EveID = (ID - ((ID / 100000) * 100000));//事件ID
                            //            if (iptid < 9) iptid++; else iptid = 0;

                            //            //-----------------------------------------------------------------------------------------('按钮'，设置falsh和true)-------------------------------------
                            //            //-------------------------------------------------------(是否持有)----------(操作ID)-----------(行为‘按钮’等)-------(行为‘按下抬起’等)----(事件集ID)
                            //            snyobj.GetChild(i).GetComponent<Player_Sny>().InputID = (1000000000) + (iptid * 100000000) + (switchbool * 1000000) + (ButtonAction * 100000) + (EventID);
                            //            i = 99999;//退出循环
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
        }

        public void SliderEvents(int EventID, Vector3 inputPos)
        {
            Transform m_trans = Button[EventID].transform;
            Slider m_Slider = m_trans.GetChild(0).GetComponent<Slider>();
            float MaxDis = m_trans.GetChild(0).GetComponent<RectTransform>().rect.width;
            Transform targetTrans = m_trans.GetChild(0).GetChild(0);
            targetTrans.position = inputPos;

            float SliderValue = 0;//百分比
            int a = 0, c = 0;
            if (targetTrans.localPosition.x > 10)
            {
                //float now_Value = (((targetTrans.localPosition.x - 10) / (MaxDis - 20)) * (m_Slider.maxValue - m_Slider.minValue)) + m_Slider.minValue;
                //SliderValue = Mathf.Min(now_Value, m_Slider.maxValue);
                SliderValue = (targetTrans.localPosition.x - 10) / (MaxDis - 20);
                if (SliderValue >= 1)
                {
                    m_Slider.value = m_Slider.maxValue;
                    a = 9;
                    //b = 9;
                    c = 9;
                }
                else
                {
                    m_Slider.value = (SliderValue * (m_Slider.maxValue - m_Slider.minValue)) + m_Slider.minValue;
                    a = (int)(SliderValue * 10);
                    //b = (int)(SliderValue * 100) - a * 10;
                    c = (int)(SliderValue * 100) - a * 10;
                }

            }
            else
            {
                //SliderValue = m_Slider.minValue;
                SliderValue = 0;
                m_Slider.value = m_Slider.minValue;
            }
            //m_Slider.value = SliderValue;
            //float bfb = MaxDis

            //float SliderValue = m_Slider.value;
            if (synButton[EventID])
            {
                if (UdonUI_AGI_bool)
                {//如果场景中存在这个对象
                    if (isSyn)
                        if (MainplaySny.MainID > -1)
                        {
                            Player_Sny nowSyn = NullObject[5].transform.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                            //int ID = nowSyn.InputID;
                            //if ((int)(ID / 1000000000) > 0)
                            //{
                            //    nowSyn.MainPos.x = SliderValue;
                            //    int iptid = (ID - 1000000000) / 100000000;//操作ID
                            //    if (iptid < 9) iptid++; else iptid = 0;
                            nowSyn.SetInputID((1000000000) + (a * 100000000) + (9 * 1000000) + (c * 100000) + (EventID));
                            //}
                        }
                }
            }
        }
        #endregion

        #region 碰撞事件
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="ButtonAction">事件ID</param>
        /// <param name="EventID">按钮行为</param>
        public void BOXC_RunEvents(int ButtonAction, int EventID,bool isAction = false,bool refer=false)
        {
            Vector2 MainEvent_m = MainEventNumber_BoxCollider[EventID];//得到这个事件集的所有事件

            int switchbool = 6;
            int AllEvent = 0;//得到在指定状态下所有应该执行的事件
            int[] AllEventID = new int[(int)MainEvent_m.y];
            for (int i = 0; i < AllEventID.Length; i++)
            {
                if (Action_BoxCollider[(int)MainEvent_m.x + i] == ButtonAction)//行为
                {
                    AllEventID[AllEvent] = (int)MainEvent_m.x + i;//把得到的事件储存起来
                    AllEvent++;//计数
                }
            }
            for (int i = 0; i < AllEvent; i++)
            {
                int ID = AllEventID[i];
                switch (MainEvent_BoxCollider[ID])
                {
                    case 0:
                        if (TargetGameObject_BoxCollider[ID].activeInHierarchy) switchbool = 7; else switchbool = 8;
                        TargetGameObject_BoxCollider[ID].SetActive(!TargetGameObject_BoxCollider[ID].activeInHierarchy);//切换物体Active
                        break;
                    case 1:
                        TargetGameObject_BoxCollider[ID].SetActive(refer ? false : true);//打开物体Active
                        break;
                    case 2:
                        TargetGameObject_BoxCollider[ID].SetActive(refer ? true : false);//关闭物体Active
                        break;
                    case 3:
                        MainAnimators_BoxCollider[ID].Play(AnimaName_BoxCollider[ID]);//播放Animator指定动画
                        break;
                    case 4:
                        MainAnimators_BoxCollider[ID].SetTrigger(AnimaName_BoxCollider[ID]);//设置Animator触发器
                        break;
                    case 5:
                        MainAnimators_BoxCollider[ID].SetBool(AnimaName_BoxCollider[ID], true);//设置Animator布尔
                        break;
                    case 6:
                        MainAnimators_BoxCollider[ID].SetBool(AnimaName_BoxCollider[ID], false);//设置Animator布尔
                        break;
                    case 7:
                        //7设置音效(1)
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);//0
                        break;
                    case 8:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 9:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 10:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 11:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 12:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 13:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 14:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 15:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 16:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 17:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 18:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 19:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 20:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 21:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 22:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 23:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 24:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 25:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 26:
                        PlayAudio1(MainEvent_BoxCollider[ID] - 7);
                        break;
                    case 27:
                        //执行震动
                        //if (LeftHand)
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.2f, 0.1f, 320);//持续时间  震动幅度  频率
                                                                                                       //else
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.2f, 0.1f, 320);
                        break;
                    case 28:
                        //执行震动
                        //if (LeftHand)
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.2f, 1f, 320);
                        //else
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.2f, 1f, 320);
                        break;
                    case 29:
                        //执行震动
                        //if (LeftHand)
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.2f, 3, 320);
                        //else
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.2f, 3, 320);
                        break;
                    case 30:
                        //执行震动
                        //if (LeftHand)
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 0.36f, 3f, 320);
                        //else
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 0.36f, 3f, 320);
                        break;
                    case 31:
                        //执行震动
                        //if (LeftHand)
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 1f, 10f, 320);
                        //else
                        localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 1f, 10f, 320);
                        break;
                    #region 自定义事件
                    case 32:
                        //TargetGameObject[ID].SendMessage(AnimaName[ID]);
                        RunEnvent(TargetGameObject_BoxCollider[ID], AnimaName_BoxCollider[ID]);
                        break;
                    case 33:
                        RunButtonEnvent(TargetGameObject[ID], AnimaName[ID]);
                        break;
                    case 34:
                        RunTriggerEnvent(TargetGameObject[ID], AnimaName[ID]);
                        break;

                        #endregion
                }
            }
            if (synBoxCollider[EventID])
            {//如果这个按钮开启了同步
                if (ButtonAction == 0 || ButtonAction == 1)
                {//只同步按下和长按事件
                    if (UdonUI_AGI_bool)
                    {//如果场景中存在这个对象
                        if (isSyn)
                            if (MainplaySny.MainID > -1)
                            {
                                Player_Sny nowSyn = NullObject[5].transform.GetChild(0).GetChild(MainplaySny.MainID).GetComponent<Player_Sny>();
                                int ID = nowSyn.InputID;
                                if ((int)(ID / 1000000000) > 0)
                                {
                                    int iptid = (ID - 1000000000) / 100000000;//操作ID
                                    if (iptid < 9) iptid++; else iptid = 0;
                                    //----------------(是否持有)----------(操作ID)-----------(行为‘碰撞’等)-------(行为‘按下抬起’等)----(事件集ID)
                                    nowSyn.SetInputID((1000000000) + (iptid * 100000000) + (switchbool * 1000000) + (ButtonAction * 100000) + (EventID));
                                }
                            }

                        //Transform snyobj = NullObject[3].transform.GetChild(0);
                        //for (int i = 0; i < snyobj.childCount; i++)
                        //{
                        //    if (Networking.GetOwner(snyobj.GetChild(i).gameObject) == localplayer)
                        //    {
                        //        int ID = snyobj.GetChild(i).GetComponent<Player_Sny>().InputID;
                        //        if ((int)(ID / 1000000000) > 0)
                        //        {
                        //            int iptid = (ID - 1000000000) / 100000000;//操作ID
                        //            if (iptid < 9) iptid++; else iptid = 0;
                        //            //-------------------------------------------------------(是否持有)----------(操作ID)-----------(行为‘碰撞’等)-------(行为‘按下抬起’等)----(事件集ID)
                        //            snyobj.GetChild(i).GetComponent<Player_Sny>().InputID = (1000000000) + (iptid * 100000000) + (switchbool * 1000000) + (ButtonAction * 100000) + (EventID);
                        //            i = 99999;//退出循环
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }
        #endregion

        void RunEnvent(GameObject _obj,string _stringCon)
        {
            string[] _com = _stringCon.Split('@');
            if (_com.Length > 1)
            {
                //if(_com[0] == "c")
                //{
                //    var behaviours = _obj.GetComponents<UdonSharpBehaviour>();
                //    foreach (var behaviour in behaviours)
                //        behaviour.SendCustomEvent(_stringCon);

                //}
                if (int.TryParse(_com[1], out int _value)) scriptID = _value;
                else scriptString = _com[1];

                var behaviours = _obj.GetComponents<UdonSharpBehaviour>();
                foreach (var behaviour in behaviours)
                    behaviour.SendCustomEvent(_com[0]);

            }
            else
            {
                var behaviours = _obj.GetComponents<UdonSharpBehaviour>();
                foreach (var behaviour in behaviours)
                    behaviour.SendCustomEvent(_stringCon);
            }
            /*            var behaviours = TargetGameObject[ID].GetComponents<UdonSharpBehaviour>();
                        foreach (var behaviour in behaviours)
                            behaviour.SendCustomEvent(AnimaName[ID]);
            */
        }
        public void RunButtonEnvent(GameObject _obj, string _stringCon)
        {
            if (int.TryParse(_stringCon, out int _id))
                But_RunEvents(_id, int.Parse(_obj.name.Split('_')[1]), false, true);
            else
                But_RunEvents(0, int.Parse(_obj.name.Split('_')[1]), false, true);
        }
        public void RunTriggerEnvent(GameObject _obj, string _stringCon)
        {
            //string[] IsName = name.Split('_');
            //if (IsName[0] == "UCo")
            //{
            //    BOXC_RunEvents(Enter, int.Parse(IsName[1]));
            //}


            if (int.TryParse(_stringCon, out int _id))
                BOXC_RunEvents(_id, int.Parse(_obj.name.Split('_')[1]));
            else
                BOXC_RunEvents(0, int.Parse(_obj.name.Split('_')[1]));
        }
        #endregion

        #region 面板距离控制
        /// <summary>
        /// 面板距离控制
        /// </summary>
        void GMmb_R()
        {
            if (RGMmb_late != RGMmb)
            {
                if (RGMmb_late == falshobj)
                {//上一个状态为空值   从空值状态获取到面板
                    if (RGMmb != falshobj)
                    {
                        //BoxCollider GMmbCo = RGMmb.GetComponent<BoxCollider>();
                        //GMmbCo.center = new Vector3(GMmbCo.center.x, GMmbCo.center.y, (Input_Dis / GMmbCo.transform.lossyScale.z + (InputUI_thickness * 0.5f)));
                        //GMmbCo.size = new Vector3(GMmbCo.size.x, GMmbCo.size.y, InputUI_thickness);
                        RhandMousePos = HitRhand.point;//手指落点位置
                                                       //RhandIs = true;
                    }//和按钮的距离加上厚度  设定为工作状态
                }
                else
                {//上一个状态非空值
                    //RGMmb_late.GetComponent<BoxCollider>().center = Vector3.forward * (InputUI_thickness * 0.5f);//设置上一个对象为初始值
                    if (RGMmb != falshobj)
                    {
                        //BoxCollider GMmbCo = RGMmb.GetComponent<BoxCollider>();
                        //GMmbCo.center = new Vector3(GMmbCo.center.x, GMmbCo.center.y, (Input_Dis / GMmbCo.transform.lossyScale.z + (InputUI_thickness * 0.5f)));
                        //GMmbCo.size = new Vector3(GMmbCo.size.x, GMmbCo.size.y, InputUI_thickness);
                        RhandMousePos = HitRhand.point;//手指落点位置
                                                       //RhandIs = true;
                    }//和按钮的距离加上厚度  设定为工作状态 
                }
                RGMmb_late = RGMmb;
            }
        }

        /// <summary>
        /// 面板距离控制
        /// </summary>
        void GMmb_L()
        {
            if (LGMmb_late != LGMmb)
            {
                if (LGMmb_late == falshobj)
                {//上一个状态为空值   从空值状态获取到面板
                    if (LGMmb != falshobj)
                    {
                        //BoxCollider GMmbCo = LGMmb.GetComponent<BoxCollider>();
                        //GMmbCo.center = new Vector3(GMmbCo.center.x, GMmbCo.center.y, (Input_Dis / GMmbCo.transform.lossyScale.z + (InputUI_thickness * 0.5f)));
                        //GMmbCo.size = new Vector3(GMmbCo.size.x, GMmbCo.size.y, InputUI_thickness);
                        LhandMousePos = HitLhand.point;//手指落点位置
                                                       //LhandIs = true;
                    }//和按钮的距离加上厚度  设定为工作状态
                }
                else
                {//上一个状态非空值
                    //LGMmb_late.GetComponent<BoxCollider>().center = Vector3.forward * (InputUI_thickness * 0.5f);//设置上一个对象为初始值
                    if (LGMmb != falshobj)
                    {
                        //BoxCollider GMmbCo = LGMmb.GetComponent<BoxCollider>();
                        //GMmbCo.center = new Vector3(GMmbCo.center.x, GMmbCo.center.y, (Input_Dis / GMmbCo.transform.lossyScale.z + (InputUI_thickness * 0.5f)));
                        //GMmbCo.size = new Vector3(GMmbCo.size.x, GMmbCo.size.y, InputUI_thickness);
                        LhandMousePos = HitLhand.point;//手指落点位置
                                                       //LhandIs = true;
                    }//和按钮的距离加上厚度  设定为工作状态 
                }
                LGMmb_late = LGMmb;
            }

        }

        #endregion

        #region UI音效设置
        /// <summary>
        /// UI音效设置
        /// </summary>
        /// <param name="Left">是否为左手触发</param>
        /// <param name="ID">音效ID</param>
        void PlayAudio(bool Left, int ID)
        {
            if (Left)
            {
                LAudio.position = HitLhand_Input.point;
                LeftAudio.clip = MainAudio[ID];//0
                LeftAudio.Play();
            }
            else
            {
                RAudio.position = HitRhand_Input.point;
                RightAudio.clip = MainAudio[ID];//0
                RightAudio.Play();
            }

        }

        /// <summary>
        /// UI音效设置
        /// </summary>
        /// <param name="Left">是否为左手触发</param>
        /// <param name="ID">音效ID</param>
        void PlayAudio1(int ID)
        {
            LAudio.position = Head.position;
            LeftAudio.clip = MainAudio[ID];//0
            LeftAudio.Play();
        }

        #endregion

        #region 手势动作
        /// <summary>
        /// 手势动作
        /// </summary>
        void Hand_Motion()
        {
            if (Input.GetKey(KeyCode.JoystickButton4)) Lhand_key = true; else Lhand_key = false;
            if (Input.GetKey(KeyCode.JoystickButton5)) Rhand_key = true; else Rhand_key = false;

            if (LhandK_late != Lhand_key)
            {//左手按键变动
                if (Lhand_key)
                {
                    HandInputPos = LztPos;
                    Lhand_sstart.SetPositionAndRotation(Lhand_ss.position, Lhand_ss.rotation);
                    Lhand_sstart.Rotate(0, 180, 0);
                    if (Lhandf_dj <= 0) Lhandf = 0;
                    Lhand_startpos = LhandPos;
                }
                LhandK_late = Lhand_key;
            }

            if (RhandK_late != Rhand_key)
            {//右手按键变动
                if (Rhand_key)
                {
                    HandInputPos = RztPos;
                    Rhand_sstart.SetPositionAndRotation(Rhand_ss.position, Rhand_ss.rotation);
                    Rhand_sstart.Rotate(0, 180, 0);
                    if (Rhandf_dj <= 0) Rhandf = 0;
                    Rhand_startpos = RhandPos;
                }
                RhandK_late = Rhand_key;
            }

            if (Lhandf > -1)
            {
                Lhandf += deltatime;
                FXpd(true);
                if (Lhandf > mig)
                {
                    Lhandf = -1;
                }
            }
            if (Rhandf > -1)
            {
                Rhandf += deltatime;
                FXpd(false);
                if (Rhandf > mig)
                {
                    Rhandf = -1;
                }
            }

            if (Rhandf_dj > 0)
            {
                Rhandf_dj -= deltatime;
                //if (Rhandf_dj <= 0) Rhandf_dj = 0;
            }

            if (Lhandf_dj > 0)
            {
                Lhandf_dj -= deltatime;
                //if (Lhandf_dj <= 0) Lhandf_dj = 0;
            }
        }
        #endregion

        #region 滑动的方向判断
        /// <summary>
        /// 滑动的方向判断
        /// </summary>
        /// <param name="Leftt">是否为左手</param>
        void FXpd(bool Leftt)
        {
            if (Leftt)
            {
                float dis = Vector3.Distance(Lhand_startpos, Lhand_ss.position);

                if (dis > fd)
                {
                    Vector3 Fx = Vector3.Normalize(Lhand_ss.position - Lhand_startpos);

                    float UD = Mathf.Abs(Vector3.SignedAngle(Fx, Lhand_ss.up, Lhand_ss.forward));//上下判断
                    float LR = Vector3.Dot(Fx, Lhand_ss.right);//左右判断
                    float FB = Mathf.Abs(Vector3.Angle(Fx, Lhand_ss.forward));//前后判断

                    if (FB > 135)
                    {
                        //往前推进
                        int ID = LhandID - ((LhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        LhandID = ID + 50;
                    }
                    else if (FB < 45)
                    {
                        //往后拉扯
                        int ID = LhandID - ((LhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        LhandID = ID + 60;
                    }
                    else if (UD < 45)
                    {
                        //向上提
                        int ID = LhandID - ((LhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        LhandID = ID + 20;
                    }
                    else if (UD > 135)
                    {
                        //往下压
                        int ID = LhandID - ((LhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        LhandID = ID + 10;
                    }
                    else if (LR > 0)
                    {
                        //向左推
                        int ID = LhandID - ((LhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        LhandID = ID + 30;
                    }
                    else
                    {
                        //向右推
                        int ID = LhandID - ((LhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        LhandID = ID + 40;
                    }
                    Lhandf = -1;
                    //Lhandf_dj = 1;
                }
            }
            else
            {
                float dis = Vector3.Distance(Rhand_startpos, Rhand_ss.position);

                if (dis > fd)
                {
                    Vector3 Fx = Vector3.Normalize(Rhand_ss.position - Rhand_startpos);

                    float UD = Mathf.Abs(Vector3.SignedAngle(Fx, Rhand_ss.up, Rhand_ss.forward));//上下判断
                    float LR = Vector3.Dot(Fx, Rhand_ss.right);//左右判断
                    float FB = Mathf.Abs(Vector3.Angle(Fx, Rhand_ss.forward));//前后判断

                    if (FB > 135)
                    {
                        //往前推进
                        int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        RhandID = ID + 50;

                        //AI++;
                        //AT.text = "Forward " + AI;
                    }
                    else if (FB < 45)
                    {
                        //往后拉扯
                        int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        RhandID = ID + 60;

                        //AI++;
                        //AT.text = "Back " + AI;
                    }
                    else if (UD < 45)
                    {
                        //向上提
                        int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        RhandID = ID + 20;

                        //AI++;
                        //AT.text = "Up " + AI;
                    }
                    else if (UD > 135)
                    {
                        //往下压
                        int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        RhandID = ID + 10;

                        //AI++;
                        //AT.text = "Dwon " + AI;
                    }
                    else if (LR > 0)
                    {
                        //向左推
                        int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        RhandID = ID + 30;

                        //AI++;
                        //AT.text = "Left " + AI;
                    }
                    else
                    {
                        //向右推
                        int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                        if (ID < 9) ID++; else ID = 1;

                        RhandID = ID + 40;

                        //AI++;
                        //AT.text = "Right " + AI;
                    }
                    Rhandf = -1;
                    //Rhandf_dj = 1;
                }
            }
        }
        #endregion

        #region 同步所有客户端的操作
        /// <summary>
        /// 同步所有客户端的操作
        /// </summary>
        /// <param name="inputID">同步的主要ID</param>
        /// <param name="SnyObjID">要求执行同步的对象ID</param>
        /// <param name="MainSny">同步对象的总父级</param>
        public void snyEve(int inputID, int SnyObjID, Transform MainSny, Vector3 snypos)
        {
            if (InitTime > 0) return;

            int Acti = (inputID - ((inputID / 100000000) * 100000000)) / 1000000;// 1~3行为(按钮，攻击，等)   4同步手势
            int Actib = (inputID - ((inputID / 1000000) * 1000000)) / 100000;//行为（按下 抬起,等）
            int EveID = (inputID - ((inputID / 100000) * 100000));//事件ID
            Vector2 MainEvent_m;
            int AllEvent;
            int[] AllEventID;
            switch (Acti)
            {
                #region 当触发行为是按钮触发时
                case 1:
                case 2:
                case 3:
                    MainEvent_m = MainEventNumber[EveID];//得到这个事件集的所有事件
                    AllEvent = 0;//得到在指定状态下所有应该执行的事件
                    AllEventID = new int[(int)MainEvent_m.y];
                    for (int i = 0; i < AllEventID.Length; i++)
                    {
                        if (Action[(int)MainEvent_m.x + i] == Actib)//行为
                        {
                            AllEventID[AllEvent] = (int)MainEvent_m.x + i;//把得到的事件储存起来
                            AllEvent++;//计数
                        }
                    }

                    //执行事件
                    for (int i = 0; i < AllEvent; i++)
                    {
                        int ID = AllEventID[i];
                        switch (MainEvent[ID])
                        {
                            case 0:
                                //if (Acti == 2) TargetGameObject[ID].SetActive(false);
                                //else if (Acti == 3) TargetGameObject[ID].SetActive(true);
                                //else
                                //    TargetGameObject[ID].SetActive(!TargetGameObject[ID].activeInHierarchy);//切换物体Active
                                TargetGameObject[ID].SetActive(Acti != 2);
                                break;
                            case 1:
                                TargetGameObject[ID].SetActive(true);//打开物体Active
                                break;
                            case 2:
                                TargetGameObject[ID].SetActive(false);//关闭物体Active
                                break;
                            case 3:
                                MainAnimators[ID].Play(AnimaName[ID]);//播放Animator指定动画
                                break;
                            case 4:
                                MainAnimators[ID].SetTrigger(AnimaName[ID]);//设置Animator触发器
                                break;
                            case 5:
                                MainAnimators[ID].SetBool(AnimaName[ID], true);//设置Animator布尔
                                break;
                            case 6:
                                MainAnimators[ID].SetBool(AnimaName[ID], false);//设置Animator布尔
                                break;
                            #region 自定义事件
                            case 32:
                                //TargetGameObject[ID].SendMessage(AnimaName[ID]);
                                RunEnvent(TargetGameObject[ID], AnimaName[ID]);
                                break;
                            case 33:
                                RunButtonEnvent(TargetGameObject[ID], AnimaName[ID]);
                                break;
                            case 34:
                                RunTriggerEnvent(TargetGameObject[ID], AnimaName[ID]);
                                break;
                                #endregion
                        }
                    }
                    break;
                #endregion
                #region Acti 4,同步手势相关的操作
                case 4:
                    //EveID 事件ID  1,打开手势菜单  2，关闭手势菜单
                    switch (EveID)
                    {
                        case 1:
                            if (!isSAOUI) break;
                            //打开手势菜单
                            Transform Target = MainSny.GetChild(SnyObjID);
                            VRCPlayerApi player = Networking.GetOwner(Target.gameObject);
                            Transform MainUI = SAOUI.GetChild(SnyObjID);
                            MainUI.position = snypos;
                            MainUI.GetChild(0).GetComponent<Animator>().Play(_SAOUI.startname);
                            MainUI.rotation = Quaternion.LookRotation(player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position - snypos, Vector3.up);
                            break;
                        case 2:
                            if (!isSAOUI) break;
                            //关闭手势菜单
                            SAOUI.GetChild(SnyObjID).GetChild(0).GetComponent<Animator>().Play(_SAOUI.stopName);
                            break;
                    }
                    break;
                #endregion
                #region 5,玩家文本输入
                case 5:
                    Player_Sny MainSnyobj = MainSny.GetChild(SnyObjID).GetComponent<Player_Sny>();
                    int AvatarID_ = (int)EveID / 10000;
                    int AvatarID = (int)((EveID - AvatarID_ * 10000) / 100);
                    int AvatarID_1 = (int)((((float)EveID / 100) - (EveID / 100)) * 100);
                    //string outStr = MainSnyobj.ChatText.Replace(" ", "\u00A0");
                    //outStr = outStr.Replace("   ", "\u3000");
                    MainChat.InputChatText(SnyObjID, AvatarID_, AvatarID, AvatarID_1, MainSnyobj.ChatText, pl);
                    break;
                #endregion
                #region 6,7,8碰撞触发
                case 6:
                case 7:
                case 8:
                    MainEvent_m = MainEventNumber_BoxCollider[EveID];//得到这个事件集的所有事件
                    AllEvent = 0;//得到在指定状态下所有应该执行的事件
                    AllEventID = new int[(int)MainEvent_m.y];
                    for (int i = 0; i < AllEventID.Length; i++)
                    {
                        if (Action_BoxCollider[(int)MainEvent_m.x + i] == Actib)//行为
                        {
                            AllEventID[AllEvent] = (int)MainEvent_m.x + i;//把得到的事件储存起来
                            AllEvent++;//计数
                        }
                    }

                    //执行事件
                    for (int i = 0; i < AllEvent; i++)
                    {
                        int ID = AllEventID[i];
                        switch (MainEvent_BoxCollider[ID])
                        {
                            case 0:
                                if (Acti == 7) TargetGameObject_BoxCollider[ID].SetActive(false);
                                else if (Acti == 8) TargetGameObject_BoxCollider[ID].SetActive(true);
                                else
                                    TargetGameObject_BoxCollider[ID].SetActive(!TargetGameObject_BoxCollider[ID].activeInHierarchy);//切换物体Active
                                break;
                            case 1:
                                TargetGameObject_BoxCollider[ID].SetActive(true);//打开物体Active
                                break;
                            case 2:
                                TargetGameObject_BoxCollider[ID].SetActive(false);//关闭物体Active
                                break;
                            case 3:
                                MainAnimators_BoxCollider[ID].Play(AnimaName_BoxCollider[ID]);//播放Animator指定动画
                                break;
                            case 4:
                                MainAnimators_BoxCollider[ID].SetTrigger(AnimaName_BoxCollider[ID]);//设置Animator触发器
                                break;
                            case 5:
                                MainAnimators_BoxCollider[ID].SetBool(AnimaName_BoxCollider[ID], true);//设置Animator布尔
                                break;
                            case 6:
                                MainAnimators_BoxCollider[ID].SetBool(AnimaName_BoxCollider[ID], false);//设置Animator布尔
                                break;
                            #region 自定义事件
                            case 32:
                                //TargetGameObject[ID].SendMessage(AnimaName[ID]);
                                RunEnvent(TargetGameObject[ID], AnimaName[ID]);
                                break;
                                /*                            case 32:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 33:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 34:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 35:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 36:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 37:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 38:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 39:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 40:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 41:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 42:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 43:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 44:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 45:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 46:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 47:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 48:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 49:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 50:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 51:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);//A组
                                                                break;
                                                            case 52:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 53:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 54:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 55:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 56:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 57:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 58:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 59:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 60:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 61:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 62:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 63:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 64:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 65:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 66:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 67:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 68:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 69:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 70:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 71:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);//B组
                                                                break;
                                                            case 72:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 73:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 74:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 75:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 76:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 77:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 78:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 79:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 80:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 81:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 82:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 83:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 84:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 85:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 86:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 87:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 88:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 89:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 90:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);
                                                                break;
                                                            case 91:
                                                                MainScript.RunScript(MainEvent_BoxCollider[ID] - 31);//C组
                                                                break;
                                */
                                #endregion
                        }
                    }
                    break;
                #endregion
                #region 9滑条
                case 9:
                    int a = (inputID - 1000000000) / 100000000;
                    //int b = Acti / 10;
                    int c = Actib;
                    Slider m_Slider = Button[EveID].transform.GetChild(0).GetComponent<Slider>();
                    if (a + c == 18)
                        m_Slider.value = m_Slider.maxValue;
                    else if (a + c == 0)
                        m_Slider.value = m_Slider.minValue;
                    else
                        m_Slider.value = ((float)a / 10 + (float)c / 100) * (m_Slider.maxValue - m_Slider.minValue) + m_Slider.minValue;
                    break;
                    #endregion

            }

        }
        #endregion

        #region 测试用
        void ces()
        {
            //ASD.Rotate(0, 360 * deltatime, 0);

            if (Input.GetKeyDown(KeyCode.O))
            {
                //往下压
                int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                if (ID < 9) ID++; else ID = 1;

                RhandID = ID + 10;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                //向左推
                int ID = RhandID - ((RhandID / 10) * 10);//拿到个位数
                if (ID < 9) ID++; else ID = 1;

                RhandID = ID + 30;
            }
            //AL.transform.SetPositionAndRotation(Lhand_sstart.position, Lhand_sstart.rotation);
            //AR.transform.SetPositionAndRotation(Rhand_sstart.position, Rhand_sstart.rotation);
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    //MainButton[0].GetComponent<Button>().onClick.Invoke();
            //    //MainButton[0].GetComponent<Button>().FindSelectableOnDown();

            //}

            //if (Input.GetKey(KeyCode.UpArrow))
            //    ADS.GetChild(0).Translate(0, 0, 1 * deltatime);
            //if (Input.GetKey(KeyCode.DownArrow))
            //    ADS.GetChild(0).Translate(0, 0, -1 * deltatime);
            //if (Input.GetKey(KeyCode.LeftArrow))
            //    ADS.GetChild(0).Translate(0, 0, 1 * deltatime);
            //if (Input.GetKey(KeyCode.RightArrow))
            //    ADS.GetChild(0).localScale += Vector3.left * deltatime;
            //string ASD = "123";
            //GetComponent<UdonBehaviour>().send


        }
        #endregion

        #region 设置手势识别的敏感度
        /// <summary>
        /// 设置手势识别的敏感度
        /// </summary>
        /// <param name="Leve">敏感度等级(越小越敏感，最小1最大5)</param>
        public void SetHandMotionSpeedLeve(int Leve)
        {
            int HandMotionSpeed = Mathf.Clamp(Leve, 1, 5);
            switch (HandMotionSpeed)
            {
                case 1:
                    mig = 0.5f;//响应时间（秒）
                    fd = 0.05f;//响应幅度（米）
                    break;
                case 2:
                    mig = 0.5f;//响应时间（秒）
                    fd = 0.1f;//响应幅度（米）
                    break;
                case 3:
                    mig = 0.3f;//响应时间（秒）
                    fd = 0.1f;//响应幅度（米）
                    break;
                case 4:
                    mig = 0.2f;//响应时间（秒）
                    fd = 0.1f;//响应幅度（米）
                    break;
                case 5:
                    mig = 0.1f;//响应时间（秒）
                    fd = 0.1f;//响应幅度（米）
                    break;
            }
        }
        #endregion

        #region 按钮动画状态
        //接触到新按钮时
        void HoverButtonAnim(GameObject NowButton)
        {
            //ButtonAnima[3].SetFloat("_Anim", 0);//吊车尾直接把数值清零
            if (NowButton != ButtonLast)
            {
                ButtonAnima[0].GetComponent<MeshRenderer>().SetPropertyBlock(ResetBlock);
                ButtonAnima[0] = ButtonAnima[1];
                ButtonAnima[1] = ButtonAnima[2];
                ButtonAnima[2] = NowButton;

                MateAnimForward[0] = false;
                MateAnimForward[1] = false;
                MateAnimForward[2] = true;//正序播放

                MaterilAnim[0] = MaterilAnim[1];
                MaterilAnim[1] = MaterilAnim[2];
                MaterilAnim[2] = 0;

                MaterilDownAnim = 0;

                ButtonLast = NowButton;
            }
            else
            {
                MateAnimForward[2] = true;//正序播放
                MateAnimForward[1] = false;
                MateAnimForward[0] = false;

                //int tid = seq;
                //tid--;
                //if (tid <= 0) tid = 3;
                //MateAnimForward[tid] = true;
            }

        }

        void HoverButtonAnimEnd()
        {
            MateAnimForward[2] = false;
            MateAnimForward[1] = false;
            MateAnimForward[0] = false;
        }

        void displayvalue()
        {
            //Atext[0].text = ButtonAnima[0].transform.GetChild(0).GetComponent<Text>().text;
            //Atext[1].text = ButtonAnima[1].transform.GetChild(0).GetComponent<Text>().text;
            //Atext[2].text = ButtonAnima[2].transform.GetChild(0).GetComponent<Text>().text;

            //Atext[4].text = MaterilAnim[0] + "";
            //Atext[5].text = MaterilAnim[1] + "";
            //Atext[6].text = MaterilAnim[2] + "";

            //Atext[8].text = MateAnimForward[0] + "";
            //Atext[9].text = MateAnimForward[1] + "";
            //Atext[10].text = MateAnimForward[2] + "";

        }

        void InDownButtonAnim()
        {
            MaterilDownAnim = 1;
        }

        void ButtonAnimPlay(float time)
        {
            for (int i = 0; i < MateAnimForward.Length; i++)
            {
                if (MateAnimForward[i])
                {
                    if (StartLerp) MaterilAnim[i] = Mathf.Lerp(MaterilAnim[i], 1, time * deltatime);
                    else MaterilAnim[i] += deltatime / time;
                    if (MaterilAnim[i] > 1) MaterilAnim[i] = 1;

                    if (MaterilDownAnim > 0)
                    {
                        MaterilDownAnim = Mathf.Lerp(MaterilDownAnim, 0, 3 * deltatime);
                        HoverAnim.SetFloat("_AnimDown", MaterilDownAnim);
                    }
                }
                else
                {
                    MaterilAnim[i] -= deltatime / EndAnimTime;
                    if (MaterilAnim[i] < 0) MaterilAnim[i] = 0;
                    HoverAnim.SetFloat("_AnimDown", 0);
                }


                //ButtonAnima[i].SetFloat("_Anim", MaterilAnim[i]);
                HoverAnim.SetFloat("_Anim", MaterilAnim[i]);
                //foreach (var item in ButtonAnima[i].GetComponents<MeshRenderer>())
                //{
                //    item.SetPropertyBlock(HoverAnim);
                //}
                ButtonAnima[i].GetComponent<MeshRenderer>().SetPropertyBlock(HoverAnim);

                //MaterialPropertyBlock ABC = new MaterialPropertyBlock();
                //ABC.SetFloat("_Anim", MaterilAnim[i]);
                //ButtonAnima[i].GetComponent<MeshRenderer>().SetPropertyBlock(ABC);
            }

            //if (Input.GetKey(KeyCode.U))
            //{
            //    MaterilAnim[0] = 1;
            //    HoverAnim.SetFloat("_Anim", MaterilAnim[0]);
            //    ButtonAnima[0].GetComponent<MeshRenderer>().SetPropertyBlock(HoverAnim);
            //}
            //if (Input.GetKey(KeyCode.I))
            //{
            //    MaterilAnim[1] = 1;
            //    HoverAnim.SetFloat("_Anim", MaterilAnim[1]);
            //    ButtonAnima[1].GetComponent<MeshRenderer>().SetPropertyBlock(HoverAnim);
            //}
            //if (Input.GetKey(KeyCode.O))
            //{
            //    MaterilAnim[2] = 1;
            //    HoverAnim.SetFloat("_Anim", MaterilAnim[2]);
            //    ButtonAnima[2].GetComponent<MeshRenderer>().SetPropertyBlock(HoverAnim);
            //}
        }
        #endregion

        #region 按键事件
        //void InputJump(bool b, VRC.Udon.Common.UdonInputEventArgs args)
        //{
        //    InputCS.Rotate(0, 90 * deltatime, 0);
        //}

        public void RunScript(int sctiptID)
        {
            switch (sctiptID)
            {
                case 1000001:
                    break;
                case 1000002:
                    break;
            }
        }
        #endregion

        #region UdonUI按钮交互
        private bool moveWondowLeft = false;
        private bool moveWondowRight = false;

        Transform leftHandRefer, rightHandRefer;
        Transform leftMoveTarget, rightMoveTarget;
        Vector3 lHandInitPos, rHandInitPos;
        Vector3 lConInitPos, rConInitPos;
        float moveSelfDisL, moveSelfDisR;
        float moveSelfDis = 0.01f;
        bool instanl = false;

        public void OnMoveWindow() {
            if (!instanl)
            {
                leftHandRefer = Instantiate(NullObject[0],transform).transform;
                rightHandRefer = Instantiate(NullObject[0],transform).transform;
                instanl = true;
            }

            Transform _targetButton = Button[ActiveButton].transform;
            if (VRgm)
            {
                if (ActiveButtonLeft)
                {
                    moveWondowLeft = true;
                    leftMoveTarget = _targetButton.parent;
                    leftHandRefer.position = _targetButton.position;
                    leftHandRefer.rotation = Quaternion.LookRotation(HitLhand_Input.normal);
                    lConInitPos = leftHandRefer.InverseTransformPoint(leftMoveTarget.position);//面板相对位置
                    lHandInitPos = leftHandRefer.InverseTransformPoint(LztPos);//手指相对位置
                    Networking.SetOwner(Networking.LocalPlayer, leftMoveTarget.gameObject);
                }
                else
                {
                    moveWondowRight = true;
                    rightMoveTarget = _targetButton.parent;
                    rightHandRefer.position = _targetButton.position;
                    rightHandRefer.rotation = Quaternion.LookRotation(HitRhand_Input.normal);
                    rConInitPos = rightHandRefer.InverseTransformPoint(rightMoveTarget.position);
                    rHandInitPos = rightHandRefer.InverseTransformPoint(RztPos);//手指相对位置
                    Networking.SetOwner(Networking.LocalPlayer, rightMoveTarget.gameObject);
                }
            }
            else
            {
                moveWondowRight = true;
                rightMoveTarget = _targetButton.parent;
                rightHandRefer.position = _targetButton.position;
                rightHandRefer.rotation = Quaternion.LookRotation(HitRhand_Input.normal);
                rConInitPos = rightHandRefer.InverseTransformPoint(rightMoveTarget.position);
                rHandInitPos = rightHandRefer.InverseTransformPoint(RhandMousePos);//鼠标相对位置
                Networking.SetOwner(Networking.LocalPlayer, rightMoveTarget.gameObject);
            }
        }
        void OnMoveWindow_Update()
        {
            if (VRgm)
            {
                if (moveWondowLeft)
                {
                    Vector3 _fingerPos = leftHandRefer.InverseTransformPoint(LztPos);
                    if (_fingerPos.z > moveSelfDis)
                    {
                        moveWondowLeft = false;
                        return;
                    }

                    Vector3 _fingerPosOffset = _fingerPos - lHandInitPos;
                    _fingerPosOffset.z = 0;
                    leftMoveTarget.position = leftHandRefer.TransformPoint(lConInitPos + _fingerPosOffset);

                }
                if (moveWondowRight)
                {
                    Vector3 _fingerPos = rightHandRefer.InverseTransformPoint(RztPos);
                    if (_fingerPos.z > moveSelfDis)
                    {
                        moveWondowRight = false;
                        return;
                    }

                    Vector3 _fingerPosOffset = _fingerPos - rHandInitPos;
                    _fingerPosOffset.z = 0;
                    rightMoveTarget.position = rightHandRefer.TransformPoint(rConInitPos + _fingerPosOffset);

                }
            }
            else
            {
                if (moveWondowRight)
                {
                    if (Input.GetMouseButtonUp(0) || RhandMousePos == Vector3.zero)
                    {
                        moveWondowRight = false;
                        return;
                    }

                    Vector3 _fingerPos = rightHandRefer.InverseTransformPoint(RhandMousePos);
                    Vector3 _fingerPosOffset = _fingerPos - rHandInitPos;
                    _fingerPosOffset.z = 0;
                    rightMoveTarget.position = rightHandRefer.TransformPoint(rConInitPos + _fingerPosOffset);

                }
            }


        }
        #endregion

        #region 返回特殊变量
        const int bidL = 6;

        [HideInInspector] public int[] selfAvatar;
        [HideInInspector]//切换模型时的默认代码
        public int[] defaltAvatar;

        private Camera mirrorCam;
        private float eyeOffset = 0.6f;
        Transform mirrorTrans;

        int CheckPlayer_ID = 0;
        int[] bid;

        public int changeNuber = 0;
        float updateEyesOffsetTime = -1;//最少持续时间
        private float lastFrameCount;//最少刷新次数

        float checkChange = 0;
        void CheckPlayerID_Start()
        {
            bid = new int[bidL];
            defaltAvatar = new int[bidL]
            {
                8089,
                8681,
                2473,
                4244,
                1388,
                16728
            };
            updateEyesOffsetTime = 5f;
            lastFrameCount = 300;
        }
        /// <summary>
        /// 检查玩家是否更换了角色
        /// </summary>
        void CheckPlayerID_Update()
        {
            if (checkChange > -1)
            {
                checkChange += deltatime;
                if (checkChange > 2)
                    checkChange = -2;
                return;
            }

            int _bid = 0;
            switch (CheckPlayer_ID)
            {
                case 0:
                    _bid = (int)((localplayer.GetBonePosition(HumanBodyBones.RightHand) - localplayer.GetBonePosition(HumanBodyBones.RightLowerArm)).sqrMagnitude * 100000);
                    break;
                case 1:
                    _bid = (int)((localplayer.GetBonePosition(HumanBodyBones.RightUpperArm) - localplayer.GetBonePosition(HumanBodyBones.RightLowerArm)).sqrMagnitude * 100000);
                    break;
                case 2:
                    _bid = (int)((localplayer.GetBonePosition(HumanBodyBones.LeftUpperLeg) - localplayer.GetBonePosition(HumanBodyBones.RightUpperLeg)).sqrMagnitude * 100000);
                    break;
                case 3:
                    _bid = (int)((localplayer.GetBonePosition(HumanBodyBones.LeftShoulder) - localplayer.GetBonePosition(HumanBodyBones.RightShoulder)).sqrMagnitude * 100000);
                    break;
                case 4:
                    _bid = (int)((localplayer.GetBonePosition(HumanBodyBones.LeftUpperLeg) - localplayer.GetBonePosition(HumanBodyBones.Hips)).sqrMagnitude * 100000);
                    break;
                case 5:
                    _bid = (int)((localplayer.GetBonePosition(HumanBodyBones.LeftUpperLeg) - localplayer.GetBonePosition(HumanBodyBones.LeftLowerLeg)).sqrMagnitude * 100000);
                    break;
            }

            if (bid[CheckPlayer_ID] == 0)
            {//未初始化
                bid[CheckPlayer_ID] = _bid;
            }
            else if(_bid != defaltAvatar[CheckPlayer_ID])
            {//当前的骨骼不是vrc标准骨骼时
                if (bid[CheckPlayer_ID] != _bid)
                {
                    PlayerIsChange();
                    return;
                }
            }
            //else
            //{//切换中
            //}


            if (CheckPlayer_ID >= (bidL - 1))
            {
                CheckPlayer_ID = 0;
            }
            else
            {
                CheckPlayer_ID++;
            }
        }
        void PlayerIsChange()
        {
            CheckPlayer_ID = 0;
            bid = new int[bidL];
            //debugString = new string[bidL];
            updateEyesOffsetTime = 1f;//修改持续时间
            lastFrameCount = 100;
            //lastFrameCount = 1;
            //udonUIOffset.OnUpdate();
            changeNuber++;
        }
        public void UpdateEyesOffset()
        {
            if (!Networking.LocalPlayer.IsUserInVR())
            {
                eyeOffset = 1;
                return;
            }
            if (!mirrorCam)
            {
                GameObject a = GameObject.Find("/MirrorCam" + NullObject[4].gameObject.name);
                if (a)
                {
                    mirrorCam = a.GetComponent<Camera>();
                    if (!mirrorCam) return;
                    mirrorTrans = NullObject[4].transform;
                }
                else
                {
                    return;
                }
            }


            Transform headTracker = Head;
            Vector3 mirrorCenter = mirrorTrans.position;
            Vector3 mirrorNormal = mirrorTrans.forward;
            Vector3 mirrorHeadPos = Vector3.Reflect(headTracker.position - mirrorCenter, mirrorNormal) + mirrorCenter;//将主相机位置沿镜子组件位置镜像
            float eyeOffsetVecMag = (mirrorCam.transform.position - mirrorHeadPos).magnitude;//求主相机和镜像相机的距离  以得到宽度

            eyeOffset = eyeOffsetVecMag / 0.032f;//0.032f为参考相机常量
        }
        /// <summary>
        /// 获取双眼间距
        /// </summary>
        /// <returns></returns>
        public float GetEyesOffset()
        {
            return eyeOffset;
        }
        //public float GetEyesScale()
        //{
        //    return eyeOffset / 0.032f;
        //}

        #endregion

        #region 特殊事件
        //public void OnPlayerPa() 
        //{ 

        //}

        //public void SetPaOn(VRCPlayerApi _player)
        //{
        //    _player.SetVoiceDistanceNear(1000.0f);
        //    _player.SetVoiceDistanceFar(1000000.0f);
        //    //_player.SetVoiceVolumetricRadius(1000.0f);

        //}
        //public void SetPaOff(VRCPlayerApi _player)
        //{
        //    _player.SetVoiceDistanceNear(0);
        //    _player.SetVoiceDistanceFar(0);
        //    //_player.SetVoiceVolumetricRadius(0);
        //}
        //public void SetPaReset(VRCPlayerApi _player)
        //{
        //    _player.SetVoiceDistanceNear(0.0f);
        //    _player.SetVoiceDistanceFar(25.0f);
        //    //_player.SetVoiceVolumetricRadius(25.0f);
        //}

        public int checkID = 0;//检查修改
        public string checkErrorPo = "";//报错前的最后信息 
        private int lastCheckID = 0;
        public bool OnCheck()
        {
            if (checkID != lastCheckID)
            {
                lastCheckID = checkID;
                return true;
            }
            lastCheckID = checkID;
            return false;
        }
        #endregion
    }
}
