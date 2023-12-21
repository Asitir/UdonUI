using System.Collections.Generic;
using UdonUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace UdonUI_Editor
{
    [InitializeOnLoad]
    public class Udon_Check
    {
        static GameObject[] nullobj;
        static GameObject UdonUI_Offset = null;

        static MainUI_Script MainUIUdon_E_m = null;

        static MainUI_Script MainUIUdon_E {
            set { 
                MainUIUdon_E_m = value; 
                if(value == null)
                {
                    //Debug.LogError("尝试赋值，但是失败");
                }
                else
                {
                    //Debug.LogError("赋值成功");
                }
            }
            get { return MainUIUdon_E_m; }
        }
        static UdonBehaviour m_MainUIUdon = null;
        static UdonBehaviour MainUIUdon 
        {
            set 
            {
                m_MainUIUdon = value;
                MainUIUdon_E = value.GetComponent<MainUI_Script>();
            }
            get { return m_MainUIUdon; }
        }
        //[SerializeField]

        //static bool isMainUdonUI = false;
        //static bool isMainUdonUI_late = false;
        static int DelayFps = 0;

        static int FindID = 0;
        #region 按钮下的属性
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
                                //static AudioClip[] AudioMain;
                                //static int[] buttonta;
        #endregion

        #region 触发器下的属性
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
        #endregion

        #region 手势触发下的属性
        static GameObject[] Button_Finger, Button_late_Finger;
        static Vector2[] MainEventNumber_Finger;//主要事件编号
        static Vector2[] MainEventNumber_late_Finger;
        //static int[] ButtonType_Finger;//按钮类型
        //static int[] ButtonType_late_Finger;
        static bool[] synbutton_Finger;//同步事件
        static bool[] synbutton_late_Finger;
        static int[] ButtonAction_Finger;//按钮行为
        static int[] ButtonEvent_Finger;//按钮事件
        static Animator[] MainAnimators_Finger;//动画机
        static string[] AnimaName_Finger;//动画 或 控制器名
        static GameObject[] TargetGameObject_Finger;//被控制的目标游戏对象
        static int[] MainEvents_Finger;//事件编号
        #endregion
        static Udon_Check()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            UdonUI_Manager.Instance.Update(Time.deltaTime);
            //Debug.Log(1 / Time.deltaTime);
            if (!MainUIUdon_E)
            {
                GameObject text_A = UdonUI_Manager.Instance.mainUIobj;
                //初始化
                if (text_A)
                {
                    MainUIUdon_E = UdonUI_Manager.Instance.mainUI;

                    if (MainUIUdon_E)
                    {
                        StartButton();
                        StartBoxCollider();
                        StartFinger();
                    }
                }
                else
                {
                    DelayFps = 0;
                }
            }
            else
            {

                if (DelayFps > -1)
                {
                    DelayFps++;
                    GameObject[] adas;
                    //MainUIUdon.publicVariables.TryGetVariableValue("BoxColliderUdon", out adas);
                    adas = MainUIUdon_E.BoxColliderUdon;
                    if (adas.Length == 1 && adas[0] == null)
                    {
                        if (DelayFps > 3)
                        {
                            Button_BoxCollider = new GameObject[1];
                            Button_BoxCollider[0] = GameObject.Find("/UdonBoxTrigger");
                            Button_late_BoxCollider = Button_BoxCollider;
                            //MainUIUdon.publicVariables.TrySetVariableValue("BoxColliderUdon", Button_late_BoxCollider);
                            MainUIUdon_E.BoxColliderUdon = Button_late_BoxCollider;
                        }
                    }
                    else
                    {
                        DelayFps = -2;
                    }

                    if (DelayFps > 10) DelayFps = -2;
                }
                else
                {
                    //MainUIUdon.publicVariables.TryGetVariableValue("NullObject", out nullobj);//空对象
                    nullobj = MainUIUdon_E.NullObject;
                    if (!UdonUI_Offset)
                    {
                        UdonUI_Offset = GameObject.Find("/UdonUIOffset");
                        if (!UdonUI_Offset)
                        {
                            //UdonUI_Offset = Instantiate()(GameObject)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/MainUI/Prefad/EyeOffset.prefab", typeof(GameObject));
                            UdonUI_Offset = Object.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "EyeOffset.prefab", typeof(GameObject)));
                            UdonUI_Offset.name = "UdonUIOffset";
                            //if (false)
                            //{
                            //    UdonUIOffset udonUIOffset = UdonUI_Offset.GetComponent<UdonUIOffset>();
                            //    MainUIUdon_E.udonUIOffset = udonUIOffset;
                            //    udonUIOffset.mainUI = MainUIUdon_E;
                            //}
                            Debug.LogWarning("请不要删除 UdonUIOffset");
                        }
                    }
                    else
                    {
                        if (UdonUI_Offset.name != "UdonUIOffset") UdonUI_Offset.name = "UdonUIOffset";

                        //if (false)
                        //{
                        //    UdonUIOffset udonUIOffset = UdonUI_Offset.GetComponent<UdonUIOffset>();
                        //    MainUIUdon_E.udonUIOffset = udonUIOffset;
                        //    udonUIOffset.mainUI = MainUIUdon_E;
                        //}
                    }
                    if (nullobj == null || nullobj.Length < 5)
                    {
                        nullobj = new GameObject[5];
                        nullobj[0] = (GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "GameObject_Null.prefab", typeof(GameObject));
                        nullobj[1] = (GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Audio_Main.prefab", typeof(GameObject));
                        nullobj[2] = (GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SnyPlayer.prefab", typeof(GameObject));
                        nullobj[3] = (GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "PlayerMainHit.prefab", typeof(GameObject));
                        nullobj[4] = UdonUI_Offset;
                        MainUIUdon_E.NullObject = nullobj;

                        //if (false)
                        //{
                        //    UdonUIOffset udonUIOffset = UdonUI_Offset.GetComponent<UdonUIOffset>();
                        //    MainUIUdon_E.udonUIOffset = udonUIOffset;
                        //    udonUIOffset.mainUI = MainUIUdon_E;
                        //}
                    }
                    if (nullobj[4] != UdonUI_Offset)
                    {
                        nullobj[4] = UdonUI_Offset;
                        MainUIUdon_E.NullObject = nullobj;

                        //if (false)
                        //{
                        //    UdonUIOffset udonUIOffset = UdonUI_Offset.GetComponent<UdonUIOffset>();
                        //    MainUIUdon_E.udonUIOffset = udonUIOffset;
                        //    udonUIOffset.mainUI = MainUIUdon_E;
                        //}
                    }

                    FindID++;
                    if (FindID > 2) FindID = 0;//三次交替遍历


                    switch (FindID)
                    {
                        case 0:
                            UdonButtonUpdate();
                            break;
                        case 1:
                            UdonBoxColliderUpdate();
                            break;
                        case 2:
                            //UdonFingerUpdate();
                            break;
                    }
                    //开始每帧的运算
                }
            }
        }

        static void UdonButtonUpdate()
        {
            UdonUI_Manager.Instance.ChackUdonUIButton_Update();
        }
        static void UdonBoxColliderUpdate()
        {//UdonBoxColliderUpdate
            UdonUI_Manager.Instance.UdonBoxColliderUpdate();
        }
        static void UdonFingerUpdate() { }
        #region 封装好的方法
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
            int pand = 0;

            if (Button[number].transform.childCount >= 1)
            {
                if (Button[number].transform.GetChild(0).childCount > 3)
                {
                    if (Button[number].transform.GetChild(0).GetComponent<Slider>()) pand = 2;//判定为滑条
                }
                else if (Button[number].transform.GetComponent<BoxCollider>())
                {
                    if (Button[number].transform.GetComponent<BoxCollider>().isTrigger)
                        pand = 1;//判定为普通按钮
                }
            }

            return pand;
        }

        #region 好像是以前弃用的方法？
        //void ButtonAddTo()
        //{
        //    MainUIUdon.publicVariables.TryGetVariableValue("ButtonType", out ButtonType_late);
        //    MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber", out MainEventNumber_late);
        //    MainEventNumber = new Vector2[Button.Length];
        //    ButtonType = new int[Button.Length];
        //    for (int i = 0; i < MainEventNumber_late.Length; i++)
        //    {
        //        MainEventNumber[i] = MainEventNumber_late[i];
        //        ButtonType[i] = ButtonType_late[i];
        //    }
        //    MainUIUdon.publicVariables.TrySetVariableValue("ButtonType", ButtonType);
        //    MainUIUdon.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);
        //}
        //void ButtonRemoveTo()
        //{
        //    MainUIUdon.publicVariables.TryGetVariableValue("ButtonType", out ButtonType_late);
        //    MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber", out MainEventNumber_late);
        //    ButtonType = new int[Button.Length];
        //    MainEventNumber = new Vector2[Button.Length];
        //    for (int i = 0; i < MainEventNumber.Length; i++)
        //    {
        //        ButtonType[i] = ButtonType_late[i];
        //        MainEventNumber[i] = MainEventNumber_late[i];
        //    }
        //    MainUIUdon.publicVariables.TrySetVariableValue("ButtonType", ButtonType);
        //    MainUIUdon.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);
        //}
        #endregion
        #endregion

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

        #region 手势触发
        static bool CheckRepetition_Finger(int ObjID)
        {
            bool pand = true;
            for (int i = 0; i < ObjID; i++)
            {
                if (Button_Finger[i] == Button_Finger[ObjID])
                {
                    i = ObjID + 1;
                    pand = false;
                }
            }
            return pand;
        }
        static int ButtonInput_Finger(int number)
        {
            //Debug.Log(ButtonA.transform.GetChild(0).name);
            int pand = 0;

            if (Button_Finger[number].transform.childCount >= 1)
            {
                if (Button_Finger[number].transform.GetChild(0).name == "UdonUIbuttonText")
                    pand = 1;//判定为普通按钮
            }

            return pand;
        }
        #endregion
        #endregion

        static void StartButton()
        {
            //MainUIUdon.publicVariables.TryGetVariableValue("Button", out Button);//所有的按钮
            //MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber", out MainEventNumber);//主要事件编号
            //MainUIUdon.publicVariables.TryGetVariableValue("ButtonType", out ButtonType);//按钮类型
            //MainUIUdon.publicVariables.TryGetVariableValue("synButton", out synbutton);//是否同步
            UdonUI_Manager.Instance.OnChackUdonUIButton_Start();
        }
        static void StartBoxCollider()
        {
            ////MainUIUdon.publicVariables.TryGetVariableValue("BoxColliderUdon", out Button_BoxCollider);//所有的按钮
            ////MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber_BoxCollider", out MainEventNumber_BoxCollider);//主要事件编号
            ////                                                                                                               //MainUIUdon.publicVariables.TryGetVariableValue("ButtonType_BoxCollider", out ButtonType_BoxCollider);//按钮类型
            ////MainUIUdon.publicVariables.TryGetVariableValue("synBoxCollider", out synbutton_BoxCollider);//是否同步
            //Button_BoxCollider = MainUIUdon_E.BoxColliderUdon;
            //MainEventNumber_BoxCollider = MainUIUdon_E.MainEventNumber_BoxCollider;
            //synbutton_BoxCollider = MainUIUdon_E.synBoxCollider;


            //Button_late_BoxCollider = Button_BoxCollider;
            //MainEventNumber_late_BoxCollider = MainEventNumber_BoxCollider;
            ////ButtonType_late_BoxCollider = ButtonType_BoxCollider;
            //synbutton_late_BoxCollider = synbutton_BoxCollider;
            UdonUI_Manager.Instance.OnStartBoxCollider();
        }
        static void StartFinger() { }

    }
}
