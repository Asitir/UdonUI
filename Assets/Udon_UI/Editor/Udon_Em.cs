using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Udon;
using UnityEditorInternal;
using UdonUI;

namespace UdonUI_Editor
{
    public class Udon_Em : EditorWindow
    {
        #region Language
        static string[] chinese = new string[] {
            "UdonUI属性菜单",//0
            "UdonUI详细编辑菜单",//1
            "请创建您的UdonUI",//2
            "请选择UdonUI对象",//3
            "事件集",//4
            "事件",//5
            "事件数量修改",//6
            "当前编辑的按钮",//7
            "选中当前正在编辑的按钮",//8
            "同步(会同步该按钮的事件集)",//9
            "同步按钮",//10
            "请选择您的UdonUI对象",//11
            "当前触发器",//12
            "选中当前正在编辑的触发器",//13
            "序号",//14
            ""
        };
        static string[] english = new string[] {
            "UdonUI Properties menu",
            "UdonUI Detailed edit menu",
            "Please create your UdonUI",
            "Please select the UconUI",
            "events",
            "event",
            "The number of events is modified",
            "Name",
            "Select the button that you are currently editing",
            "Synchronization (the set of events that synchronize the button)",
            "Sync button",
            "Please select your UconUI object",
            "The current trigger",
            "Select the trigger you are currently editing",
            "ordinal",
            ""

        };
        static string[] japanese = new string[] {
            "UdonUIプロパティー",
            "UdonUI詳細メニュー編集",
            "新しいUdonUIを作成してください",
            "UdonUI対象を選択",
            "イベントセット",
            "イベント",
            "イベント数の変更",
            "現在編集中のボタン",
            "現在編集中のボタンを選択",
            "同期（ボタンのイベントセットが同期されます）",
            "同期ボタン",
            "UdonUIオブジェクトを選択してください",
            "現在のトリガー",
            "現在編集中のトリガーを選択",
            "シリアル番号",
            ""
        };

        static string[] porName;//porName[8]
        static int languageID = 0;

        #endregion
        #region 显示用
        enum DisEnum
        {
            UdonUIButton,
            BoxCollider,
            Finger,
            innoun
        }
        DisEnum MainDis = DisEnum.innoun;
        Vector2 hxd;

        #region 触发器
        bool[] synbutton_to_Collider;//同步
        [SerializeField]
        GameObject[] BoxCollider_udon, ALLObject_BoxCollider;//所有按钮  被控制活动状态的开关
        [SerializeField]
        bool synboxcollider;
        [SerializeField]
        int[] Action_BoxCollider, MainEvent_BoxCollider, MainEvents_BoxCollider;
        [SerializeField]
        Animator[] MainAnimators_BoxCollider;
        [SerializeField]
        string[] AnimaName_BoxCollider;
        [SerializeField]
        Vector2[] MainEventNumber_BoxCollider;

        SerializedProperty SynBoxCollider_;
        [SerializeField]
        List<BoxColliderListMain> BoxColliderEvents;
        [SerializeField]
        BoxColliderListMain[] BoxColliderEventNumber;
        ReorderableList BoxColliderList_Reor;
        int[] Action_Late_BoxCollider, MainEvent_Late_BoxCollider;

        Animator[] MainAnimators_late_BoxCollider;//动画机
        string[] AnimaName_late_BoxCollider;//动画机名
        GameObject[] ALLObject_late_BoxCollider;//物体对象
        int[] MainEvents_Late_BoxCollider;//编号

        #endregion

        #region 手势
        [SerializeField]
        GameObject[] Finger, ALLObject_Finger;//所有按钮  被控制活动状态的开关
        [SerializeField]
        bool synfinger;
        [SerializeField]
        int[] Action_Finger, MainEvent_Finger, MainEvents_Finger;
        [SerializeField]
        Animator[] MainAnimators_Finger;
        [SerializeField]
        string[] AnimaName_Finger;
        [SerializeField]
        Vector2[] MainEventNumber_Finger;

        SerializedProperty synFinger_;
        [SerializeField]
        List<ButtonListMain> FingerEvents;
        [SerializeField]
        ButtonListMain[] FingerEventNumber;
        ReorderableList FingerList_Reor;
        int[] Action_Late_Finger, MainEvent_Late_Finger;

        Animator[] MainAnimators_late_Finger;//动画机
        string[] AnimaName_late_Finger;//动画机名
        GameObject[] ALLObject_late_Finger;//物体对象
        int[] MainEvents_Late_Finger;//编号

        #endregion

        #region 按钮
        [SerializeField]
        bool synbutton;

        [SerializeField]
        GameObject[] Button, ALLObject;//所有按钮  被控制活动状态的开关
        [SerializeField]
        Vector2[] MainEventNumber;//所以按钮事件管理
        [SerializeField]
        int[] ButtonType, Action, MainEvent;
        int[] Action_Late, MainEvent_Late;
        int m_ButtonType = 1;

        Animator[] MainAnimators_late;//动画机
        string[] AnimaName_late;//动画机名
        GameObject[] ALLObject_late;//物体对象
        int[] MainEvents_Late;//编号
        bool[] synbutton_to;
        SerializedProperty synbutton_;

        [SerializeField]
        Animator[] MainAnimators;//事件中的动画机
        [SerializeField]
        string[] AnimaName;//事件中的动画机动画名
        [SerializeField]
        public int[] MainEvents;//自定义事件编号

        [SerializeField]
        List<ButtonListMain> ButtonEvents;
        [SerializeField]
        ButtonListMain[] ButtonEventNumber;
        ReorderableList ButtonList_Reor;

        #endregion

        SerializedObject _MainObj;
        bool tt;
        #endregion

        MainUI_Script MainUI_E;
        UdonBehaviour m_MainUI;
        UdonBehaviour MainUI
        {
            set { 
                m_MainUI = value;
                MainUI_E = value.GetComponent<MainUI_Script>();
            }
            get { return m_MainUI; }
        }
        int GameObjID;
        int SelectEvents = -1;

        static EditorWindow editorwindows;
        [MenuItem("Asitir_Tool/UdonUI Properties menu", false, 2)]
        static void windows()
        {
            GetLanguage();
            editorwindows = GetWindow(typeof(Udon_Em), true, porName[1]);
            //editorwindows.minSize = new Vector2(300, 350);
            //editorwindows.maxSize = new Vector2(300, 350);
        }

        private void OnEnable()
        {
            if (MainUI_E == null) MainUI_E = UdonUI_Manager.Instance.mainUI;
            GetLanguage();
            #region 初始化
            ButtonEvents = new List<ButtonListMain>();
            BoxColliderEvents = new List<BoxColliderListMain>();
            FingerEvents = new List<ButtonListMain>();
            GameObjID = 0;
            tt = false;
            #endregion
            _MainObj = new SerializedObject(this);
            Start_Button();
            Start_BoxCollider();
            Start_Finger();
        }
        void OnInspectorUpdate()
        {    //OnInspectorUpdate 实时刷新OnGUI
            Repaint();
        }
        private void OnGUI()
        {
            _MainObj.Update();

            if (MainUI_E == null)
            {
                GUI.skin.label.fontSize = 24;
                GUILayout.Label(porName[2]);

                if (UdonUI_Manager.Instance.mainUIobj)
                    MainUI_E = UdonUI_Manager.Instance.mainUI;
            }
            else
            {
                //MainUI.publicVariables.TryGetVariableValue("Button", out Button);//所有按钮  
                //MainUI.publicVariables.TryGetVariableValue("BoxColliderUdon", out BoxCollider_udon);//所有碰撞盒
                //MainUI.publicVariables.TryGetVariableValue("Finger", out Finger);//所有手势
                Button = MainUI_E.Button;
                BoxCollider_udon = MainUI_E.BoxColliderUdon;
                //Finger = MainUI_E.Finger;

                //得到被选中对象的ID 
                if (Selection.activeGameObject)
                {
                    bool Find = false;
                    for (int i = 0; i < Button.Length; i++)
                    {
                        if (Button[i] == Selection.activeGameObject)
                        {
                            GameObjID = i;
                            //i = Button.Length + 1;
                            MainDis = DisEnum.UdonUIButton;
                            Find = true;
                            break;
                        }
                    }

                    if (!Find)
                    {
                        for (int i = 0; i < BoxCollider_udon.Length; i++)
                        {
                            if (BoxCollider_udon[i] == Selection.activeGameObject)
                            {
                                GameObjID = i;
                                MainDis = DisEnum.BoxCollider;
                                Find = true;
                                break;
                            }
                        }
                    }

                    //if (!Find)
                    //{
                    //    for (int i = 0; i < Finger.Length; i++)
                    //    {
                    //        if (Finger[i] == Selection.activeGameObject)
                    //        {
                    //            GameObjID = i;
                    //            MainDis = DisEnum.Finger;
                    //            break;
                    //        }
                    //    }
                    //}

                    //if (!Find)
                    //{
                    //    GUI.skin.label.fontSize = 24;
                    //    GUILayout.Label("请选择UdonUI对象" + Button[GameObjID].name);
                    //    GUI.skin.label.fontSize = 12;
                    //}
                }

                //if((int)MainDis == 3){
                //    GUI.skin.label.fontSize = 24;
                //    GUILayout.Label("请选择UdonUI对象" + Button[GameObjID].name);
                //    GUI.skin.label.fontSize = 12;
                //}

                switch ((int)MainDis)
                {
                    case 0:
                        OnGUI_Button();
                        break;
                    case 1:
                        OnGUI_BoxCollider();
                        break;
                    case 2:
                        //OnGUI_Finger();
                        break;
                    case 3:
                        GUI.skin.label.fontSize = 24;
                        GUILayout.Label(porName[3]);
                        GUI.skin.label.fontSize = 12;
                        break;
                }

            }
            GUI.skin.label.fontSize = 12;
            //---------------------------------------//
        }
        void Start_Button()
        {
            //MainUI.publicVariables.TryGetVariableValue("ButtonType", out ButtonType);//ButtonType: 按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮  4为窗口
            if (MainUI_E == null) return;
            ButtonType = MainUI_E.ButtonType;
            synbutton_ = _MainObj.FindProperty("synbutton");
            ButtonList_Reor = new ReorderableList(_MainObj, _MainObj.FindProperty("ButtonEvents"), false, true, true, true);
            ButtonList_Reor.drawHeaderCallback = (Rect rct) =>
            {
                GUI.Label(rct, porName[4]);
            };
            ButtonList_Reor.elementHeight = 24;
            ButtonList_Reor.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = ButtonList_Reor.serializedProperty.GetArrayElementAtIndex(index);
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, item, new GUIContent(porName[5] + index));
            };

            ButtonList_Reor.onAddCallback = (ReorderableList Lists) =>
            {
                MainEventNumber[GameObjID].y += 1;//增加事件量
                #region 修改事件量后保留事件内的变量
                Action_Late = new int[Action.Length + 1];
                MainEvent_Late = new int[Action_Late.Length];
                //----------------------------//
                Animator[] MainAnimators_late = new Animator[Action_Late.Length];//动画机
                string[] AnimaName_late = new string[Action_Late.Length];//动画机名
                GameObject[] ALLObject_late = new GameObject[Action_Late.Length];//物体对象
                int[] MainEvents_Late = new int[Action_Late.Length];//编号
                                                                    //----------------------------//
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
                    else if (i >= (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y - 1)
                    {//当前事件集末端ID以上
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
                //MainUI.publicVariables.TrySetVariableValue("Action", Action_Late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvent", MainEvent_Late);
                ////----------------------------------------------------------------------//
                //MainUI.publicVariables.TrySetVariableValue("MainAnimators", MainAnimators_late);
                //MainUI.publicVariables.TrySetVariableValue("AnimaName", AnimaName_late);
                //MainUI.publicVariables.TrySetVariableValue("TargetGameObject", ALLObject_late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvents", MainEvents_Late);
                MainUI_E.Action = Action_Late;
                MainUI_E.MainEvent = MainEvent_Late;
                MainUI_E.MainAnimators = MainAnimators_late;
                MainUI_E.AnimaName = AnimaName_late;
                MainUI_E.TargetGameObject = ALLObject_late;
                MainUI_E.MainEvents = MainEvents_Late;
                #endregion
                for (int i = 0; i < MainEventNumber.Length; i++)
                {
                    //将该变量后的所有变量向前推一位
                    if (i > GameObjID)
                    {
                        MainEventNumber[i].x += 1;
                    }
                }

                //MainUI.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);//事件编号
                MainUI_E.MainEventNumber = MainEventNumber;
                Undo.RegisterCompleteObjectUndo(MainUI_E, "事件数量修改");
            };

            ButtonList_Reor.onRemoveCallback = (ReorderableList Lists) =>
            {
                MainEventNumber[GameObjID].y -= 1;//减少事件量
                #region 修改事件量后保留事件内的变量
                Action_Late = new int[Action.Length - 1];
                MainEvent_Late = new int[Action_Late.Length];
                //----------------------------//
                MainAnimators_late = new Animator[Action_Late.Length];//动画机
                AnimaName_late = new string[Action_Late.Length];//动画机名
                ALLObject_late = new GameObject[Action_Late.Length];//物体对象
                MainEvents_Late = new int[Action_Late.Length];//编号
                                                              //----------------------------//
                if (SelectEvents < (int)MainEventNumber[GameObjID].y)
                {
                    for (int i = 0; i < MainEvent_Late.Length; i++)
                    {
                        if (i < (int)MainEventNumber[GameObjID].x + SelectEvents)
                        {//当前选择的事件ID以下
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
                        {//当前选择的事件ID以上
                            Action_Late[i] = Action[i + 1];
                            MainEvent_Late[i] = MainEvent[i + 1];
                            //----------------------------//
                            MainAnimators_late[i] = MainAnimators[i + 1];
                            AnimaName_late[i] = AnimaName[i + 1];
                            ALLObject_late[i] = ALLObject[i + 1];
                            MainEvents_Late[i] = MainEvents[i + 1];
                            //----------------------------//
                        }
                    }
                    //Debug.Log("删除了:" + ((int)MainEventNumber[GameObjID].x + SelectEvents) + "    当前选中的ID：" + SelectEvents);
                }
                else
                {
                    for (int i = 0; i < MainEvent_Late.Length; i++)
                    {
                        if (i < (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y)
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
                            Action_Late[i] = Action[i + 1];
                            MainEvent_Late[i] = MainEvent[i + 1];
                            //----------------------------//
                            MainAnimators_late[i] = MainAnimators[i + 1];
                            AnimaName_late[i] = AnimaName[i + 1];
                            ALLObject_late[i] = ALLObject[i + 1];
                            MainEvents_Late[i] = MainEvents[i + 1];
                            //----------------------------//
                        }
                    }
                    //Debug.Log("删除了末端，当前按钮的事件长度: " + (int)MainEventNumber[GameObjID].y + "    当前选中的ID：" + SelectEvents);
                }
                //MainUI.publicVariables.TrySetVariableValue("Action", Action_Late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvent", MainEvent_Late);
                ////----------------------------------------------------------------------//
                //MainUI.publicVariables.TrySetVariableValue("MainAnimators", MainAnimators_late);
                //MainUI.publicVariables.TrySetVariableValue("AnimaName", AnimaName_late);
                //MainUI.publicVariables.TrySetVariableValue("TargetGameObject", ALLObject_late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvents", MainEvents_Late);
                MainUI_E.Action = Action_Late;
                MainUI_E.MainEvent = MainEvent_Late;
                MainUI_E.MainAnimators = MainAnimators_late;
                MainUI_E.AnimaName= AnimaName_late;
                MainUI_E.TargetGameObject= ALLObject_late;
                MainUI_E.MainEvents= MainEvents_Late;

                #endregion
                for (int i = 0; i < MainEventNumber.Length; i++)
                {
                    //将该变量后的所有变量向后推一位
                    if (i > GameObjID)
                    {
                        MainEventNumber[i].x -= 1;
                    }
                }

                //MainUI.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);//事件编号
                MainUI_E.MainEventNumber = MainEventNumber;
                Undo.RegisterCompleteObjectUndo(MainUI_E, "事件数量修改");
            };

            ButtonList_Reor.onSelectCallback = (ReorderableList Lists) =>
            {
                SelectEvents = Lists.index;
            };
        }
        void Start_BoxCollider()
        {
            SynBoxCollider_ = _MainObj.FindProperty("synboxcollider");
            BoxColliderList_Reor = new ReorderableList(_MainObj, _MainObj.FindProperty("BoxColliderEvents"), false, true, true, true);
            BoxColliderList_Reor.drawHeaderCallback = (Rect rct) =>
            {
                GUI.Label(rct, porName[4]);
            };
            BoxColliderList_Reor.elementHeight = 24;
            BoxColliderList_Reor.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = BoxColliderList_Reor.serializedProperty.GetArrayElementAtIndex(index);
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, item, new GUIContent(porName[5] + index));
            };

            BoxColliderList_Reor.onAddCallback = (ReorderableList Lists) =>
            {
                //Debug.Log("ADDColider");
                MainEventNumber_BoxCollider[GameObjID].y += 1;//增加事件量
                #region 修改事件量后保留事件内的变量
                Action_Late_BoxCollider = new int[Action_BoxCollider.Length + 1];
                MainEvent_Late_BoxCollider = new int[Action_Late_BoxCollider.Length];
                //----------------------------//
                Animator[] MainAnimators_late_BoxCollider = new Animator[Action_Late_BoxCollider.Length];//动画机
                string[] AnimaName_late_BoxCollider = new string[Action_Late_BoxCollider.Length];//动画机名
                GameObject[] ALLObject_late_BoxCollider = new GameObject[Action_Late_BoxCollider.Length];//物体对象
                int[] MainEvents_Late_BoxCollider = new int[Action_Late_BoxCollider.Length];//编号
                                                                                            //----------------------------//
                for (int i = 0; i < MainEvent_BoxCollider.Length; i++)
                {
                    if (i < (int)MainEventNumber_BoxCollider[GameObjID].x + (int)MainEventNumber_BoxCollider[GameObjID].y - 1)
                    {//当前事件集末端ID以下
                        Action_Late_BoxCollider[i] = Action_BoxCollider[i];
                        MainEvent_Late_BoxCollider[i] = MainEvent_BoxCollider[i];
                        //----------------------------//
                        MainAnimators_late_BoxCollider[i] = MainAnimators_BoxCollider[i];
                        AnimaName_late_BoxCollider[i] = AnimaName_BoxCollider[i];
                        ALLObject_late_BoxCollider[i] = ALLObject_BoxCollider[i];
                        MainEvents_Late_BoxCollider[i] = MainEvents_BoxCollider[i];
                        //----------------------------//
                    }
                    else if (i >= (int)MainEventNumber_BoxCollider[GameObjID].x + (int)MainEventNumber_BoxCollider[GameObjID].y - 1)
                    {//当前事件集末端ID以上
                        Action_Late_BoxCollider[i + 1] = Action_BoxCollider[i];
                        MainEvent_Late_BoxCollider[i + 1] = MainEvent_BoxCollider[i];
                        //----------------------------//
                        MainAnimators_late_BoxCollider[i + 1] = MainAnimators_BoxCollider[i];
                        AnimaName_late_BoxCollider[i + 1] = AnimaName_BoxCollider[i];
                        ALLObject_late_BoxCollider[i + 1] = ALLObject_BoxCollider[i];
                        MainEvents_Late_BoxCollider[i + 1] = MainEvents_BoxCollider[i];
                        //----------------------------//
                    }
                }
                //MainUI.publicVariables.TrySetVariableValue("Action_BoxCollider", Action_Late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("MainEvent_BoxCollider", MainEvent_Late_BoxCollider);
                ////----------------------------------------------------------------------//
                //MainUI.publicVariables.TrySetVariableValue("MainAnimators_BoxCollider", MainAnimators_late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("AnimaName_BoxCollider", AnimaName_late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("TargetGameObject_BoxCollider", ALLObject_late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("MainEvents_BoxCollider", MainEvents_Late_BoxCollider);
                MainUI_E.Action_BoxCollider = Action_Late_BoxCollider;
                MainUI_E.MainEvent_BoxCollider = MainEvent_Late_BoxCollider;
                MainUI_E.MainAnimators_BoxCollider = MainAnimators_late_BoxCollider;
                MainUI_E.AnimaName_BoxCollider = AnimaName_late_BoxCollider;
                MainUI_E.TargetGameObject_BoxCollider = ALLObject_late_BoxCollider;
                MainUI_E.MainEvents_BoxCollider = MainEvents_Late_BoxCollider;

                #endregion
                for (int i = 0; i < MainEventNumber_BoxCollider.Length; i++)
                {
                    //将该变量后的所有变量向前推一位
                    if (i > GameObjID)
                    {
                        MainEventNumber_BoxCollider[i].x += 1;
                    }
                }

                //MainUI.publicVariables.TrySetVariableValue("MainEventNumber_BoxCollider", MainEventNumber_BoxCollider);//事件编号
                MainUI_E.MainEventNumber_BoxCollider = MainEventNumber_BoxCollider;
                Undo.RegisterCompleteObjectUndo(MainUI_E, "事件数量修改");
            };

            BoxColliderList_Reor.onRemoveCallback = (ReorderableList Lists) =>
            {
                MainEventNumber_BoxCollider[GameObjID].y -= 1;//减少事件量
                #region 修改事件量后保留事件内的变量
                Action_Late_BoxCollider = new int[Action_BoxCollider.Length - 1];
                MainEvent_Late_BoxCollider = new int[Action_Late_BoxCollider.Length];
                //----------------------------//
                MainAnimators_late_BoxCollider = new Animator[Action_Late_BoxCollider.Length];//动画机
                AnimaName_late_BoxCollider = new string[Action_Late_BoxCollider.Length];//动画机名
                ALLObject_late_BoxCollider = new GameObject[Action_Late_BoxCollider.Length];//物体对象
                MainEvents_Late_BoxCollider = new int[Action_Late_BoxCollider.Length];//编号
                                                                                      //----------------------------//
                if (SelectEvents < (int)MainEventNumber_BoxCollider[GameObjID].y)
                {
                    for (int i = 0; i < MainEvent_Late_BoxCollider.Length; i++)
                    {
                        if (i < (int)MainEventNumber_BoxCollider[GameObjID].x + SelectEvents)
                        {//当前选择的事件ID以下
                            Action_Late_BoxCollider[i] = Action_BoxCollider[i];
                            MainEvent_Late_BoxCollider[i] = MainEvent_BoxCollider[i];
                            //----------------------------//
                            MainAnimators_late_BoxCollider[i] = MainAnimators_BoxCollider[i];
                            AnimaName_late_BoxCollider[i] = AnimaName_BoxCollider[i];
                            ALLObject_late_BoxCollider[i] = ALLObject_BoxCollider[i];
                            MainEvents_Late_BoxCollider[i] = MainEvents_BoxCollider[i];
                            //----------------------------//
                        }
                        else
                        {//当前选择的事件ID以上
                            Action_Late_BoxCollider[i] = Action_BoxCollider[i + 1];
                            MainEvent_Late_BoxCollider[i] = MainEvent_BoxCollider[i + 1];
                            //----------------------------//
                            MainAnimators_late_BoxCollider[i] = MainAnimators_BoxCollider[i + 1];
                            AnimaName_late_BoxCollider[i] = AnimaName_BoxCollider[i + 1];
                            ALLObject_late_BoxCollider[i] = ALLObject_BoxCollider[i + 1];
                            MainEvents_Late_BoxCollider[i] = MainEvents_BoxCollider[i + 1];
                            //----------------------------//
                        }
                    }
                    //Debug.Log("删除了:" + ((int)MainEventNumber_BoxCollider[GameObjID].x + SelectEvents) + "    当前选中的ID：" + SelectEvents);
                }
                else
                {
                    for (int i = 0; i < MainEvent_Late_BoxCollider.Length; i++)
                    {
                        if (i < (int)MainEventNumber_BoxCollider[GameObjID].x + (int)MainEventNumber_BoxCollider[GameObjID].y)
                        {//当前事件集末端ID以下
                            Action_Late_BoxCollider[i] = Action_BoxCollider[i];
                            MainEvent_Late_BoxCollider[i] = MainEvent_BoxCollider[i];
                            //----------------------------//
                            MainAnimators_late_BoxCollider[i] = MainAnimators_BoxCollider[i];
                            AnimaName_late_BoxCollider[i] = AnimaName_BoxCollider[i];
                            ALLObject_late_BoxCollider[i] = ALLObject_BoxCollider[i];
                            MainEvents_Late_BoxCollider[i] = MainEvents_BoxCollider[i];
                            //----------------------------//
                        }
                        else
                        {//当前事件集末端ID以上
                            Action_Late_BoxCollider[i] = Action_BoxCollider[i + 1];
                            MainEvent_Late_BoxCollider[i] = MainEvent_BoxCollider[i + 1];
                            //----------------------------//
                            MainAnimators_late_BoxCollider[i] = MainAnimators_BoxCollider[i + 1];
                            AnimaName_late_BoxCollider[i] = AnimaName_BoxCollider[i + 1];
                            ALLObject_late_BoxCollider[i] = ALLObject_BoxCollider[i + 1];
                            MainEvents_Late_BoxCollider[i] = MainEvents_BoxCollider[i + 1];
                            //----------------------------//
                        }
                    }
                    //Debug.Log("删除了末端，当前按钮的事件长度: " + (int)MainEventNumber_BoxCollider[GameObjID].y + "    当前选中的ID：" + SelectEvents);
                }
                //MainUI.publicVariables.TrySetVariableValue("Action_BoxCollider", Action_Late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("MainEvent_BoxCollider", MainEvent_Late_BoxCollider);
                ////----------------------------------------------------------------------//
                //MainUI.publicVariables.TrySetVariableValue("MainAnimators_BoxCollider", MainAnimators_late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("AnimaName_BoxCollider", AnimaName_late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("TargetGameObject_BoxCollider", ALLObject_late_BoxCollider);
                //MainUI.publicVariables.TrySetVariableValue("MainEvents_BoxCollider", MainEvents_Late_BoxCollider);
                MainUI_E.Action_BoxCollider = Action_Late_BoxCollider;
                MainUI_E.MainEvent_BoxCollider = MainEvent_Late_BoxCollider;
                MainUI_E.MainAnimators_BoxCollider = MainAnimators_late_BoxCollider;
                MainUI_E.AnimaName_BoxCollider = AnimaName_late_BoxCollider;
                MainUI_E.TargetGameObject_BoxCollider = ALLObject_late_BoxCollider;
                MainUI_E.MainEvents_BoxCollider = MainEvents_Late_BoxCollider;

                #endregion
                for (int i = 0; i < MainEventNumber_BoxCollider.Length; i++)
                {
                    //将该变量后的所有变量向后推一位
                    if (i > GameObjID)
                    {
                        MainEventNumber_BoxCollider[i].x -= 1;
                    }
                }

                //MainUI.publicVariables.TrySetVariableValue("MainEventNumber_BoxCollider", MainEventNumber_BoxCollider);//事件编号
                MainUI_E.MainEventNumber_BoxCollider = MainEventNumber_BoxCollider;
                Undo.RegisterCompleteObjectUndo(MainUI_E, "事件数量修改");
            };

            BoxColliderList_Reor.onSelectCallback = (ReorderableList Lists) =>
            {
                SelectEvents = Lists.index;
            };

        }
        void Start_Finger()
        {
            synFinger_ = _MainObj.FindProperty("synFinger");
            ButtonList_Reor = new ReorderableList(_MainObj, _MainObj.FindProperty("ButtonEvents"), false, true, true, true);
            ButtonList_Reor.drawHeaderCallback = (Rect rct) =>
            {
                GUI.Label(rct, porName[4]);
            };
            ButtonList_Reor.elementHeight = 24;
            ButtonList_Reor.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = ButtonList_Reor.serializedProperty.GetArrayElementAtIndex(index);
                rect.height -= 4;
                rect.y += 2;
                EditorGUI.PropertyField(rect, item, new GUIContent("事件 " + index));
            };

            ButtonList_Reor.onAddCallback = (ReorderableList Lists) =>
            {
                MainEventNumber[GameObjID].y += 1;//增加事件量
                #region 修改事件量后保留事件内的变量
                Action_Late = new int[Action.Length + 1];
                MainEvent_Late = new int[Action_Late.Length];
                //----------------------------//
                Animator[] MainAnimators_late = new Animator[Action_Late.Length];//动画机
                string[] AnimaName_late = new string[Action_Late.Length];//动画机名
                GameObject[] ALLObject_late = new GameObject[Action_Late.Length];//物体对象
                int[] MainEvents_Late = new int[Action_Late.Length];//编号
                                                                    //----------------------------//
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
                    else if (i >= (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y - 1)
                    {//当前事件集末端ID以上
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
                //MainUI.publicVariables.TrySetVariableValue("Action", Action_Late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvent", MainEvent_Late);
                ////----------------------------------------------------------------------//
                //MainUI.publicVariables.TrySetVariableValue("MainAnimators", MainAnimators_late);
                //MainUI.publicVariables.TrySetVariableValue("AnimaName", AnimaName_late);
                //MainUI.publicVariables.TrySetVariableValue("TargetGameObject", ALLObject_late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvents", MainEvents_Late);
                MainUI_E.Action = Action_Late;
                MainUI_E.MainEvent = MainEvent_Late;
                MainUI_E.MainAnimators = MainAnimators_late;
                MainUI_E.AnimaName = AnimaName_late;
                MainUI_E.TargetGameObject = ALLObject_late;
                MainUI_E.MainEvents = MainEvents_Late;

                #endregion
                for (int i = 0; i < MainEventNumber.Length; i++)
                {
                    //将该变量后的所有变量向前推一位
                    if (i > GameObjID)
                    {
                        MainEventNumber[i].x += 1;
                    }
                }

                //MainUI.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);//事件编号
                MainUI_E.MainEventNumber = MainEventNumber;
                Undo.RegisterCompleteObjectUndo(MainUI_E, "事件数量修改");
            };

            ButtonList_Reor.onRemoveCallback = (ReorderableList Lists) =>
            {
                MainEventNumber[GameObjID].y -= 1;//减少事件量
                #region 修改事件量后保留事件内的变量
                Action_Late = new int[Action.Length - 1];
                MainEvent_Late = new int[Action_Late.Length];
                //----------------------------//
                MainAnimators_late = new Animator[Action_Late.Length];//动画机
                AnimaName_late = new string[Action_Late.Length];//动画机名
                ALLObject_late = new GameObject[Action_Late.Length];//物体对象
                MainEvents_Late = new int[Action_Late.Length];//编号
                                                              //----------------------------//
                if (SelectEvents < (int)MainEventNumber[GameObjID].y)
                {
                    for (int i = 0; i < MainEvent_Late.Length; i++)
                    {
                        if (i < (int)MainEventNumber[GameObjID].x + SelectEvents)
                        {//当前选择的事件ID以下
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
                        {//当前选择的事件ID以上
                            Action_Late[i] = Action[i + 1];
                            MainEvent_Late[i] = MainEvent[i + 1];
                            //----------------------------//
                            MainAnimators_late[i] = MainAnimators[i + 1];
                            AnimaName_late[i] = AnimaName[i + 1];
                            ALLObject_late[i] = ALLObject[i + 1];
                            MainEvents_Late[i] = MainEvents[i + 1];
                            //----------------------------//
                        }
                    }
                    //Debug.Log("删除了:" + ((int)MainEventNumber[GameObjID].x + SelectEvents) + "    当前选中的ID：" + SelectEvents);
                }
                else
                {
                    for (int i = 0; i < MainEvent_Late.Length; i++)
                    {
                        if (i < (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y)
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
                            Action_Late[i] = Action[i + 1];
                            MainEvent_Late[i] = MainEvent[i + 1];
                            //----------------------------//
                            MainAnimators_late[i] = MainAnimators[i + 1];
                            AnimaName_late[i] = AnimaName[i + 1];
                            ALLObject_late[i] = ALLObject[i + 1];
                            MainEvents_Late[i] = MainEvents[i + 1];
                            //----------------------------//
                        }
                    }
                    //Debug.Log("删除了末端，当前按钮的事件长度: " + (int)MainEventNumber[GameObjID].y + "    当前选中的ID：" + SelectEvents);
                }
                //MainUI.publicVariables.TrySetVariableValue("Action", Action_Late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvent", MainEvent_Late);
                ////----------------------------------------------------------------------//
                //MainUI.publicVariables.TrySetVariableValue("MainAnimators", MainAnimators_late);
                //MainUI.publicVariables.TrySetVariableValue("AnimaName", AnimaName_late);
                //MainUI.publicVariables.TrySetVariableValue("TargetGameObject", ALLObject_late);
                //MainUI.publicVariables.TrySetVariableValue("MainEvents", MainEvents_Late);
                MainUI_E.Action = Action_Late;
                MainUI_E.MainEvent = MainEvent_Late;
                MainUI_E.MainAnimators = MainAnimators_late;
                MainUI_E.AnimaName = AnimaName_late;
                MainUI_E.TargetGameObject = ALLObject_late;
                MainUI_E.MainEvents = MainEvents_Late;
                #endregion
                for (int i = 0; i < MainEventNumber.Length; i++)
                {
                    //将该变量后的所有变量向后推一位
                    if (i > GameObjID)
                    {
                        MainEventNumber[i].x -= 1;
                    }
                }

                //MainUI.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);//事件编号
                MainUI_E.MainEventNumber = MainEventNumber;
                Undo.RegisterCompleteObjectUndo(MainUI_E, "事件数量修改");
            };

            ButtonList_Reor.onSelectCallback = (ReorderableList Lists) =>
            {
                SelectEvents = Lists.index;
            };

        }
        void OnGUI_Button()
        {
            //MainUI.publicVariables.TryGetVariableValue("TargetGameObject", out ALLObject);//被控制活动状态的开关
            //MainUI.publicVariables.TryGetVariableValue("Action", out Action);//Action: 行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
            //MainUI.publicVariables.TryGetVariableValue("MainEvent", out MainEvent);//MainEvent:  事件、执行: 0为播放动画机  2为开关物体活动   3为自定义事件
            //MainUI.publicVariables.TryGetVariableValue("ButtonType", out ButtonType);//按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮  4为窗口
            ALLObject = MainUI_E.TargetGameObject;
            Action = MainUI_E.Action;
            MainEvent = MainUI_E.MainEvent;
            ButtonType = MainUI_E.ButtonType;


            if (ButtonType.Length > GameObjID)
            {
                m_ButtonType = ButtonType[GameObjID];
            }
            else
            {
                m_ButtonType = 1;
            }

            //MainUI.publicVariables.TryGetVariableValue("MainEvents", out MainEvents);//事件编号
            //MainUI.publicVariables.TryGetVariableValue("MainAnimators", out MainAnimators);//事件中的动画机
            //MainUI.publicVariables.TryGetVariableValue("AnimaName", out AnimaName);//事件中的动画机动画名
            //MainUI.publicVariables.TryGetVariableValue("MainEventNumber", out MainEventNumber);//事件集编号
            //MainUI.publicVariables.TryGetVariableValue("synButton", out synbutton_to);//事件集同步
            MainEvents = MainUI_E.MainEvents;
            MainAnimators = MainUI_E.MainAnimators;
            AnimaName = MainUI_E.AnimaName;
            MainEventNumber = MainUI_E.MainEventNumber;
            synbutton_to = MainUI_E.synButton;

            if (GameObjID < Button.Length)
            {//判断上一次所选中的对象是否在范围内
                if (Button[GameObjID])
                {//判断当前被选中的对象是否还存在
                    ButtonEvents.Clear();
                    try
                    {
                        ButtonEventNumber = new ButtonListMain[(int)MainEventNumber[GameObjID].y];//创建当前按钮的所有事件
                        for (int i = 0; i < ButtonEventNumber.Length; i++)
                        {
                            ButtonEventNumber[i] = new ButtonListMain();
                            int ID = (int)MainEventNumber[GameObjID].x + i;//推算出当前事件ID

                            ButtonEventNumber[i].ID = ID;//发送当前事件ID
                            ButtonEventNumber[i].Action = Action[ID];//发送当前事件的行为
                            ButtonEventNumber[i].MainEvent = MainEvent[ID];//发送当前事件的事件类型
                            //Debug.Log("长度: " + GameObjID);
                            //ButtonEventNumber[i].ButtonType = ButtonType[GameObjID];//发送当前按钮类型
                                                                           //--------------------------------------------------------------------//
                            ButtonEventNumber[i].GameObjectTargt = ALLObject[ID];//发送当前事件的游戏对象
                            ButtonEventNumber[i].MainAnimators = MainAnimators[ID];//发送当前事件的事件类型
                            ButtonEventNumber[i].AnimaName = AnimaName[ID];//发送当前事件的事件类型
                            ButtonEventNumber[i].MainEvents = MainEvents[ID];//发送当前事件的事件编号
                                                                             //--------------------------------------------------------------------//

                            ButtonEvents.Add(ButtonEventNumber[i]);//整理到List集合里面显示
                        }

                        synbutton = synbutton_to[GameObjID];
                        //错开空值帧
                        if (tt == true)
                        {
                            GUI.skin.label.fontSize = 24;
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(porName[7] + "：" + Button[GameObjID].name);
                            GUILayout.Label(porName[14] + "：" + GameObjID);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginVertical();
                            GUILayout.BeginHorizontal();
                            if (GUILayout.Button(porName[8]))
                                Selection.activeGameObject = Button[GameObjID];
                            if (UdonUI_Manager.Instance.UdonUI_Button_Copy != null && UdonUI_Manager.Instance.UdonUI_Button_Copy.Button)
                            {
                                string name = UdonUI_Manager.Instance.UdonUI_Button_Copy.Button.name;
                                if (GUILayout.Button($"copy({name})", GUILayout.Width(50 + name.Length * 12)))
                                    UdonUI_Manager.Instance.CopyButton(GameObjID);
                            }
                            else
                            {
                                if (GUILayout.Button("copy", GUILayout.Width(50)))
                                    UdonUI_Manager.Instance.CopyButton(GameObjID);
                            }
                            if (GUILayout.Button("past", GUILayout.Width(50)))
                                UdonUI_Manager.Instance.PastButton();
                            GUILayout.EndHorizontal();

                            EditorGUI.BeginChangeCheck();
                            EditorGUILayout.PropertyField(synbutton_, new GUIContent(porName[9], porName[9]));
                            if (EditorGUI.EndChangeCheck())
                            {
                                synbutton_to[GameObjID] = synbutton_.boolValue;
                                //MainUI.publicVariables.TrySetVariableValue("synButton", synbutton_to);//事件集同步
                                MainUI_E.synButton = synbutton_to;
                                Undo.RegisterCompleteObjectUndo(MainUI_E, porName[10]);
                                //Debug.Log("触发");
                            }
                            GUILayout.EndVertical();

                            GUI.skin.label.fontSize = 12;
                            hxd = GUILayout.BeginScrollView(hxd);
                            if (m_ButtonType == 1)
                            {
                            }
                            else if (m_ButtonType == 2)
                            {
                                //GUILayout.Space(20);
                                GUILayout.Label("This Is Slider");
                                GUILayout.Label("Only the value of slider can be synchronized");
                                GUILayout.Space(10);
                            }

                            ButtonList_Reor.DoLayoutList();
                            GUILayout.EndScrollView();
                        }
                        else tt = true;
                    }
                    catch (System.Exception)
                    {
                        //Debug.LogError("绘制失败: " + "\n");
                        //throw;
                    }
                }
                else
                {
                    GUI.skin.label.fontSize = 24;
                    GUILayout.Label(porName[11]);
                }
            }
            else
            {
                GUI.skin.label.fontSize = 24;
                GUILayout.Label(porName[11]);
            }
        }
        void OnGUI_BoxCollider()
        {
            //MainUI.publicVariables.TryGetVariableValue("TargetGameObject_BoxCollider", out ALLObject_BoxCollider);//被控制活动状态的开关
            //MainUI.publicVariables.TryGetVariableValue("Action_BoxCollider", out Action_BoxCollider);//Action: 行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
            //MainUI.publicVariables.TryGetVariableValue("MainEvent_BoxCollider", out MainEvent_BoxCollider);//MainEvent:  事件、执行: 0为播放动画机  2为开关物体活动   3为自定义事件
            //MainUI.publicVariables.TryGetVariableValue("MainEvents_BoxCollider", out MainEvents_BoxCollider);//事件编号
            //MainUI.publicVariables.TryGetVariableValue("MainAnimators_BoxCollider", out MainAnimators_BoxCollider);//事件中的动画机
            //MainUI.publicVariables.TryGetVariableValue("AnimaName_BoxCollider", out AnimaName_BoxCollider);//事件中的动画机动画名
            //MainUI.publicVariables.TryGetVariableValue("MainEventNumber_BoxCollider", out MainEventNumber_BoxCollider);//事件集编号
            //MainUI.publicVariables.TryGetVariableValue("synBoxCollider", out synbutton_to_Collider);//事件集同步
            ALLObject_BoxCollider = MainUI_E.TargetGameObject_BoxCollider;
            Action_BoxCollider = MainUI_E.Action_BoxCollider;
            MainEvent_BoxCollider = MainUI_E.MainEvent_BoxCollider;
            MainEvents_BoxCollider = MainUI_E.MainEvents_BoxCollider;
            MainAnimators_BoxCollider = MainUI_E.MainAnimators_BoxCollider;
            AnimaName_BoxCollider = MainUI_E.AnimaName_BoxCollider;
            MainEventNumber_BoxCollider = MainUI_E.MainEventNumber_BoxCollider;
            synbutton_to_Collider = MainUI_E.synBoxCollider;

            if (GameObjID < BoxCollider_udon.Length)
            {//判断上一次所选中的对象是否在范围内
                if (BoxCollider_udon[GameObjID])
                {//判断当前被选中的对象是否还存在
                    BoxColliderEvents.Clear();
                    try
                    {
                        BoxColliderEventNumber = new BoxColliderListMain[(int)MainEventNumber_BoxCollider[GameObjID].y];//创建当前按钮的所有事件
                        for (int i = 0; i < BoxColliderEventNumber.Length; i++)
                        {
                            BoxColliderEventNumber[i] = new BoxColliderListMain();
                            int ID = (int)MainEventNumber_BoxCollider[GameObjID].x + i;//推算出当前事件ID

                            BoxColliderEventNumber[i].ID = ID;//发送当前事件ID
                            BoxColliderEventNumber[i].Action = Action_BoxCollider[ID];//发送当前事件的行为
                            BoxColliderEventNumber[i].MainEvent = MainEvent_BoxCollider[ID];//发送当前事件的事件类型
                                                                                            //--------------------------------------------------------------------//
                            BoxColliderEventNumber[i].GameObjectTargt = ALLObject_BoxCollider[ID];//发送当前事件的游戏对象
                            //Debug.Log("长度: " + ALLObject_BoxCollider[2].name);
                            BoxColliderEventNumber[i].MainAnimators = MainAnimators_BoxCollider[ID];//发送当前事件的事件类型
                            BoxColliderEventNumber[i].AnimaName = AnimaName_BoxCollider[ID];//发送当前事件的事件类型
                            BoxColliderEventNumber[i].MainEvents = MainEvents_BoxCollider[ID];//发送当前事件的事件编号
                                                                                              //--------------------------------------------------------------------//

                            BoxColliderEvents.Add(BoxColliderEventNumber[i]);//整理到List集合里面显示
                        }

                        synboxcollider = synbutton_to_Collider[GameObjID];
                        //错开空值帧
                        if (tt == true)
                        {
                            GUI.skin.label.fontSize = 24;
                            GUILayout.Label(porName[12]+ "：" + BoxCollider_udon[GameObjID].name);

                            GUILayout.BeginVertical();
                            if (GUILayout.Button(porName[13]))
                                Selection.activeGameObject = BoxCollider_udon[GameObjID];

                            EditorGUI.BeginChangeCheck();
                            EditorGUILayout.PropertyField(SynBoxCollider_, new GUIContent(porName[9]));
                            if (EditorGUI.EndChangeCheck())
                            {
                                synbutton_to_Collider[GameObjID] = SynBoxCollider_.boolValue;
                                //MainUI.publicVariables.TrySetVariableValue("synBoxCollider", synbutton_to_Collider);//事件集同步
                                MainUI_E.synBoxCollider = synbutton_to_Collider;
                                Undo.RegisterCompleteObjectUndo(MainUI_E, "同步按钮");
                                //Debug.Log("触发");
                            }
                            GUILayout.EndVertical();
                            GUI.skin.label.fontSize = 12;
                            hxd = GUILayout.BeginScrollView(hxd);
                            BoxColliderList_Reor.DoLayoutList();
                            GUILayout.EndScrollView();
                        }
                        else tt = true;
                    }
                    catch (System.Exception)
                    {
                        //throw;
                    }
                }
                else
                {
                    GUI.skin.label.fontSize = 24;
                    GUILayout.Label(porName[11]);
                }
            }
            else
            {
                GUI.skin.label.fontSize = 24;
                GUILayout.Label(porName[11]);
            }

        }
        void OnGUI_Finger()
        {
            //MainUI.publicVariables.TryGetVariableValue("TargetGameObject_Finger", out ALLObject_Finger);//被控制活动状态的开关
            //MainUI.publicVariables.TryGetVariableValue("Action_Finger", out Action_Finger);//Action: 行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
            //MainUI.publicVariables.TryGetVariableValue("MainEvent_Finger", out MainEvent_Finger);//MainEvent:  事件、执行: 0为播放动画机  2为开关物体活动   3为自定义事件
            //MainUI.publicVariables.TryGetVariableValue("MainEvents_Finger", out MainEvents_Finger);//事件编号
            //MainUI.publicVariables.TryGetVariableValue("MainAnimators_Finger", out MainAnimators_Finger);//事件中的动画机
            //MainUI.publicVariables.TryGetVariableValue("AnimaName_Finger", out AnimaName_Finger);//事件中的动画机动画名
            //MainUI.publicVariables.TryGetVariableValue("MainEventNumber_Finger", out MainEventNumber_Finger);//事件集编号
            //MainUI.publicVariables.TryGetVariableValue("synFinger", out synfinger);//事件集同步
            //ALLObject_BoxCollider = MainUI_E.TargetGameObject_BoxCollider;
            //Action_BoxCollider = MainUI_E.Action_BoxCollider;
            //MainEvent_BoxCollider = MainUI_E.MainEvent_BoxCollider;
            //MainEvents_BoxCollider = MainUI_E.MainEvents_BoxCollider;
            //MainAnimators_BoxCollider = MainUI_E.MainAnimators_BoxCollider;
            //AnimaName_BoxCollider = MainUI_E.AnimaName_BoxCollider;
            //MainEventNumber_BoxCollider = MainUI_E.MainEventNumber_BoxCollider;
            //synbutton_to_Collider = MainUI_E.synFinger;

            if (GameObjID < Finger.Length)
            {//判断上一次所选中的对象是否在范围内
                if (Finger[GameObjID])
                {//判断当前被选中的对象是否还存在
                    ButtonEvents.Clear();
                    ButtonEventNumber = new ButtonListMain[(int)MainEventNumber[GameObjID].y];//创建当前按钮的所有事件
                    for (int i = 0; i < ButtonEventNumber.Length; i++)
                    {
                        ButtonEventNumber[i] = new ButtonListMain();
                        int ID = (int)MainEventNumber[GameObjID].x + i;//推算出当前事件ID

                        ButtonEventNumber[i].ID = ID;//发送当前事件ID
                        ButtonEventNumber[i].Action = Action[ID];//发送当前事件的行为
                        ButtonEventNumber[i].MainEvent = MainEvent[ID];//发送当前事件的事件类型
                                                                       //--------------------------------------------------------------------//
                        ButtonEventNumber[i].GameObjectTargt = ALLObject[ID];//发送当前事件的游戏对象
                        ButtonEventNumber[i].MainAnimators = MainAnimators[ID];//发送当前事件的事件类型
                        ButtonEventNumber[i].AnimaName = AnimaName[ID];//发送当前事件的事件类型
                        ButtonEventNumber[i].MainEvents = MainEvents[ID];//发送当前事件的事件编号
                                                                         //--------------------------------------------------------------------//

                        ButtonEvents.Add(ButtonEventNumber[i]);//整理到List集合里面显示
                    }

                    synbutton = synbutton_to[GameObjID];
                    //错开空值帧
                    if (tt == true)
                    {
                        GUI.skin.label.fontSize = 24;
                        GUILayout.Label("当前编辑的按钮：" + Finger[GameObjID].name);

                        GUILayout.BeginVertical();
                        if (GUILayout.Button("选中当前正在编辑的按钮"))
                            Selection.activeGameObject = Finger[GameObjID];

                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(synFinger_, new GUIContent("同步(会同步该按钮的事件集)"));
                        if (EditorGUI.EndChangeCheck())
                        {
                            synbutton_to[GameObjID] = synFinger_.boolValue;
                            //MainUI.publicVariables.TrySetVariableValue("synFinger", synfinger);//事件集同步
                            //MainUI_E.synFinger = synfinger;
                            Undo.RegisterCompleteObjectUndo(MainUI_E, "同步按钮");
                            //Debug.Log("触发");
                        }
                        GUILayout.EndVertical();
                        GUI.skin.label.fontSize = 12;
                        hxd = GUILayout.BeginScrollView(hxd);
                        ButtonList_Reor.DoLayoutList();
                        GUILayout.EndScrollView();
                    }
                    else tt = true;
                }
                else
                {
                    GUI.skin.label.fontSize = 24;
                    GUILayout.Label("请选择您的UdonUI对象");
                }
            }
            else
            {
                GUI.skin.label.fontSize = 24;
                GUILayout.Label("请选择您的UdonUI对象");
            }
        }

        static void GetLanguage()
        {
            try
            {
                languageID = int.Parse(EditorUserSettings.GetConfigValue("languageID"));
            }
            catch (System.Exception)
            {
                languageID = 0;
                EditorUserSettings.SetConfigValue("languageID", languageID.ToString());
                //throw;
            }

            if (languageID == 0) porName = chinese;
            else if (languageID == 1) porName = english;
            else if (languageID == 2) porName = japanese;
        }

    }

    [System.Serializable]
    public class ButtonListMain
    {
        [SerializeField]
        public int ID;
        [SerializeField]
        public int ButtonType, Action, MainEvent;//按钮类型，按钮状态，事件类型
        //ButtonType: 按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮  4为窗口
        //Action:     行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
        //MainEvent:  事件、执行: 0为播放动画机  2为开关物体活动   3为自定义事件

        [SerializeField]
        public Animator MainAnimators;//事件中的动画机
        [SerializeField]
        public string AnimaName;//事件中的动画机动画名
        [SerializeField]
        public int MainEvents;//事件编号
        [SerializeField]
        public GameObject GameObjectTargt;//目标游戏对象

        [SerializeField]
        public Texture icon;
        [SerializeField]
        public GameObject pref;
        [SerializeField]
        public string name;
        [SerializeField]
        public int attack;
    }

    [System.Serializable]
    public class BoxColliderListMain
    {
        [SerializeField]
        public int ID;
        [SerializeField]
        public int ButtonType, Action, MainEvent;//按钮类型，按钮状态，事件类型
        //ButtonType: 按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮  4为窗口
        //Action:     行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
        //MainEvent:  事件、执行: 0为播放动画机  2为开关物体活动   3为自定义事件

        [SerializeField]
        public Animator MainAnimators;//事件中的动画机
        [SerializeField]
        public string AnimaName;//事件中的动画机动画名
        [SerializeField]
        public int MainEvents;//事件编号
        [SerializeField]
        public GameObject GameObjectTargt;//目标游戏对象

        [SerializeField]
        public Texture icon;
        [SerializeField]
        public GameObject pref;
        [SerializeField]
        public string name;
        [SerializeField]
        public int attack;
    }
}

