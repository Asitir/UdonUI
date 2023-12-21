using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using VRC.Udon;
using UdonUI;

namespace UdonUI_Editor
{
    public class UdonUI_Create : Editor
    {
        static int RunID = 0;
        //# UNITY_EDITOR
        static GameObject VRCWorld = null;
        //static int MainID = 0;
        static int[] ButtonType;

        //GameObject[] NullObject;//初始化 给予空对象处理
        static int Lenth = 0;
        static MainUI_Script MainUdonUI_E;
        static UdonBehaviour m_MainUdonUI;
        //static UdonBehaviour MainUdonUI {
        //    set { 
        //        m_MainUdonUI = value;
        //        MainUdonUI_E = value.GetComponent<MainUI_Script>();
        //    }
        //    get { return m_MainUdonUI; }
        //}//UdonUI环境
        static GameObject[] Button, Button_late;
        static GameObject[] Button_BoxCollider, Button_late_BoxCollider;
        static GameObject[] Button_Finger, Button_late_Finger;

        [MenuItem("GameObject/UdonUI/Button", false, 12)]
        static void But()
        {
            if (RunID == 0)
            {
                Lenth = Selection.gameObjects.Length;
                //Debug.Log(
                GameObject A = UdonUI();
                GameObject B;

                if (!A)
                {
                    //场景中有UdonUI环境
                    B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Udon_Button.prefab", typeof(GameObject)));
                    Undo.RegisterCreatedObjectUndo(B, "创建Udon按钮");//记录Udon创建操作
                    B.name = "Button_1";

                    GameObject Select = Selection.activeGameObject;
                    if (Select)
                    {
                        B.name = "Button_" + (Select.transform.childCount + 1);
                        B.transform.position = Select.transform.position;
                        B.transform.rotation = Select.transform.rotation;
                        B.transform.SetParent(Select.transform);
                    }
                    else
                    {
                        B.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(0, 0, 0.2f);
                        B.transform.rotation = Quaternion.Euler(0, SceneView.lastActiveSceneView.camera.transform.eulerAngles.y, 0);
                    }
                    Selection.activeGameObject = B;
                    SceneView.FrameLastActiveSceneView();

                    //为UdonUI添加按钮参数
                    //if (MainUdonUI == null) MainUdonUI = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                    if (MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;
                    //MainUdonUI.publicVariables.TryGetVariableValue("Button", out Button);//所有按钮  
                    Button = MainUdonUI_E.Button;
                    Button_late = new GameObject[Button.Length + 1];
                    for (int i = 0; i < Button_late.Length; i++)
                    {
                        if (i == Button_late.Length - 1) Button_late[i] = B;
                        else Button_late[i] = Button[i];
                    }

                    //MainUdonUI.publicVariables.TryGetVariableValue("ButtonType", out ButtonType);
                    ButtonType = MainUdonUI_E.ButtonType;
                    if (ButtonType.Length < 1)
                    {
                        //if (MainUdonUI == null) Debug.Log("初始化失败！！ 出现致命错误！！  请尽快联系作者！！");
                        Vector2[] Event = new Vector2[1];
                        int[] ButtonTyp = new int[1];
                        //bool[] snybutton = new bool[1];
                        //AudioClip[] AudioMain = new AudioClip[20];
                        ButtonTyp[0] = 1;//设置初始按钮属性为普通按钮
                        //                 //MainUdonUI.publicVariables.TrySetVariableValue("synButton", snybutton);
                        //                 //MainUdonUI.publicVariables.TrySetVariableValue("MainAudio", AudioMain);
                        //MainUdonUI.publicVariables.TrySetVariableValue("MainEventNumber", Event);
                        //MainUdonUI.publicVariables.TrySetVariableValue("ButtonType", ButtonTyp);
                        MainUdonUI_E.MainEventNumber = Event;
                        MainUdonUI_E.ButtonType = ButtonTyp;
                    }
                    //MainUdonUI.publicVariables.TrySetVariableValue("Button", Button_late);
                    MainUdonUI_E.Button = Button_late;
                    Undo.RegisterCompleteObjectUndo(MainUdonUI_E, "按钮修改");
                }
            }
            RunID++;
            if (RunID >= Lenth) RunID = 0;
        }
        [MenuItem("GameObject/UdonUI/Slider", false, 13)]
        static void Slider()
        {
            if (RunID == 0)
            {
                Lenth = Selection.gameObjects.Length;
                //Debug.Log(
                GameObject A = UdonUI();
                GameObject B;

                if (!A)
                {
                    //场景中有UdonUI环境
                    B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Udon_Slider.prefab", typeof(GameObject)));
                    Undo.RegisterCreatedObjectUndo(B, "创建Udon按钮");//记录Udon创建操作
                    B.name = "Slider_1";

                    GameObject Select = Selection.activeGameObject;
                    if (Select)
                    {
                        B.name = "Slider_" + (Select.transform.childCount + 1);
                        B.transform.position = Select.transform.position;
                        B.transform.rotation = Select.transform.rotation;
                        B.transform.SetParent(Select.transform);
                    }
                    else
                    {
                        B.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(0, 0, 0.2f);
                        B.transform.rotation = Quaternion.Euler(0, SceneView.lastActiveSceneView.camera.transform.eulerAngles.y, 0);
                    }
                    Selection.activeGameObject = B;
                    SceneView.FrameLastActiveSceneView();

                    //为UdonUI添加按钮参数
                    //if (MainUdonUI == null) MainUdonUI = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                    if (MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;
                    //MainUdonUI.publicVariables.TryGetVariableValue("Button", out Button);//所有按钮  
                    Button = MainUdonUI_E.Button;
                    Button_late = new GameObject[Button.Length + 1];
                    for (int i = 0; i < Button_late.Length; i++)
                    {
                        if (i == Button_late.Length - 1) Button_late[i] = B;
                        else Button_late[i] = Button[i];
                    }

                    //MainUdonUI.publicVariables.TryGetVariableValue("ButtonType", out ButtonType);
                    ButtonType = MainUdonUI_E.ButtonType;
                    if (ButtonType.Length < 1)
                    {
                        //if (MainUdonUI == null) Debug.Log("初始化失败！！ 出现致命错误！！  请尽快联系作者！！");
                        Vector2[] Event = new Vector2[1];
                        int[] ButtonTyp = new int[1];
                        //bool[] snybutton = new bool[1];
                        //AudioClip[] AudioMain = new AudioClip[20];
                        ButtonTyp[0] = 1;//设置初始按钮属性为滑条
                        //                 //MainUdonUI.publicVariables.TrySetVariableValue("synButton", snybutton);
                        //                 //MainUdonUI.publicVariables.TrySetVariableValue("MainAudio", AudioMain);
                        //MainUdonUI.publicVariables.TrySetVariableValue("MainEventNumber", Event);
                        //MainUdonUI.publicVariables.TrySetVariableValue("ButtonType", ButtonTyp);
                        MainUdonUI_E.MainEventNumber = Event;
                        MainUdonUI_E.ButtonType = ButtonTyp;

                    }
                    //MainUdonUI.publicVariables.TrySetVariableValue("Button", Button_late);
                    MainUdonUI_E.Button = Button_late;
                    Undo.RegisterCompleteObjectUndo(MainUdonUI_E, "按钮修改");
                }
            }
            RunID++;
            if (RunID >= Lenth) RunID = 0;
        }

        [MenuItem("GameObject/UdonUI/ControlPanel", false, 14)]
        static void ControlPanel()
        {
            if (RunID == 0)
            {
                Lenth = Selection.gameObjects.Length;
                GameObject A = UdonUI();
                GameObject B;
                if (!A)
                {
                    B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Udon_ControlPanel.prefab", typeof(GameObject)));
                    Undo.RegisterCreatedObjectUndo(B, "创建Udon面板");//记录Udon创建操作
                    //Transform add = Selection.activeGameObject.transform;
                    B.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(0, 0, 0.2f);
                    B.name = "Udon_ControlPanel";
                    if (Selection.activeGameObject)
                    {
                        Debug.Log("设置父级");
                        B.transform.SetParent(Selection.activeGameObject.transform);
                        B.transform.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        B.transform.rotation = Quaternion.Euler(0, SceneView.lastActiveSceneView.camera.transform.eulerAngles.y, 0);
                    }
                    Selection.activeObject = B;
                    SceneView.FrameLastActiveSceneView();
                }
            }
            RunID++;
            if (RunID >= Lenth) RunID = 0;
        }

        [MenuItem("GameObject/UdonUI/BoxTrigger", false, 15)]
        static void BoxCollider_()
        {
            if (RunID == 0)
            {
                Lenth = Selection.gameObjects.Length;
                GameObject A = UdonUI();
                GameObject B;

                if (!A)
                {
                    //场景中有UdonUI环境
                    B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "UdonBoxCollider.prefab", typeof(GameObject)));
                    Undo.RegisterCreatedObjectUndo(B, "创建Udon触发器");//记录Udon创建操作
                    B.name = "UdonBoxTrigger_1";

                    GameObject Select = Selection.activeGameObject;
                    {
                        B.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(0, 0, 2);
                    }
                    Selection.activeGameObject = B;
                    SceneView.FrameLastActiveSceneView();

                    //为UdonUI添加按钮参数
                    //if (MainUdonUI == null) MainUdonUI = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                    if (MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;

                    //MainUdonUI.publicVariables.TryGetVariableValue("BoxColliderUdon", out Button_BoxCollider);//所有按钮
                    Button_BoxCollider = MainUdonUI_E.BoxColliderUdon;
                    Button_late_BoxCollider = new GameObject[Button_BoxCollider.Length + 1];
                    for (int i = 0; i < Button_late_BoxCollider.Length; i++)
                    {
                        if (i == Button_late_BoxCollider.Length - 1) Button_late_BoxCollider[i] = B;
                        else Button_late_BoxCollider[i] = Button_BoxCollider[i];
                    }

                    //MainUdonUI.publicVariables.TryGetVariableValue("ButtonType", out ButtonType);
                    //if (ButtonType.Length < 1)
                    //{
                    //    if (MainUdonUI == null) Debug.Log("初始化失败！！ 出现致命错误！！  请尽快联系作者！！");
                    //    Vector2[] Event = new Vector2[1];
                    //    int[] ButtonTyp = new int[1];
                    //    ButtonTyp[0] = 1;//设置初始按钮属性为普通按钮
                    //    MainUdonUI.publicVariables.TrySetVariableValue("MainEventNumber", Event);
                    //    MainUdonUI.publicVariables.TrySetVariableValue("ButtonType", ButtonTyp);
                    //}
                    //MainUdonUI.publicVariables.TrySetVariableValue("BoxColliderUdon", Button_late_BoxCollider);
                    MainUdonUI_E.BoxColliderUdon = Button_late_BoxCollider;
                    Undo.RegisterCompleteObjectUndo(MainUdonUI_E, "按钮修改");

                }

            }
            RunID++;
            if (RunID >= Lenth) RunID = 0;

        }
        //[MenuItem("GameObject/UdonUI/map", false, 8)]
        //static void Map()
        //{

        //    MainID++;
        //    Debug.Log("主要ID：" + MainID);

        //    //Debug.Log("创建了Udon图片");
        //    GameObject A = UdonUI();
        //    if (!A)
        //    {
        //        //场景中有UdonUI环境
        //        GameObject B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/MainUI/Prefad/Udon_ControlPanel.prefab", typeof(GameObject)));
        //        Undo.RegisterCreatedObjectUndo(B, "创建了Udon图片");
        //        B.name = "UdonControlPanel";
        //        if (Selection.activeGameObject)
        //        {
        //            Transform Select = Selection.activeGameObject.transform;

        //            int ID = 1;
        //            foreach (Transform Obj in Select)
        //            {
        //                string[] name = Obj.name.Split('_');
        //                if (name[0] == "UdonControlPanel")
        //                {
        //                    ID++;
        //                    B.name = "UdonControlPanel_" + ID;
        //                }
        //            }
        //            B.transform.position = Select.transform.position;
        //            B.transform.SetParent(Select);

        //        }//如果有选中的对象  将创建的对象作为该对象子级
        //        Selection.activeGameObject = B;
        //    }
        //}

        [MenuItem("GameObject/VRCWorld", false, 10)]
        static void VRC_World()
        {
            if (RunID == 0)
            {
                if (GameObject.Find("VRCWorld") == null)
                {
                    //在场景中创建预制体
                    //var CCD = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/VRChat Examples/Prefabs/VRCWorld.prefab", typeof(GameObject)));
                    //Undo.RegisterCreatedObjectUndo(CCD, "创建VRC环境");//记录Udon创建操作
                    string _nowPath = "Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCWorld.prefab";
                    {
                        var CCD = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadMainAssetAtPath(_nowPath));
                        Undo.RegisterCreatedObjectUndo(CCD, "创建VRC环境");//记录Udon创建操作
                    }
                }
                else
                {
                    Debug.Log("已存在VRCWorld,请勿重复创建");
                }
            }
            RunID++;
            if (RunID >= Selection.gameObjects.Length) RunID = 0;
            //Instantiate(VRCWorld);
        }

        [MenuItem("GameObject/UdonUI/ADD To/UdonUIButton Or Slider", false, 16)]
        public static void ADDButton()
        {
            //if(MainObject_ != null)
            //{
            //    GameObject[] a = new GameObject[1];
            //    a[0] = MainObject_;
            //    //Debug.Log("导入了" + a.Length + "个UdonUI按钮");
            //    UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
            //    UdonUiButton.publicVariables.TryGetVariableValue("Button", out Button_late);//所有按钮  
            //    Button = new GameObject[Button_late.Length + a.Length];
            //    for (int i = 0; i < Button.Length; i++)
            //    {
            //        if (i < Button_late.Length)
            //            Button[i] = Button_late[i];
            //        else
            //            Button[i] = a[i - Button_late.Length];
            //    }
            //    UdonUiButton.publicVariables.TrySetVariableValue("Button", Button);
            //    Undo.RegisterCompleteObjectUndo(UdonUiButton, "按钮修改");
            //    return;
            //}
            if (Selection.gameObjects.Length > 0)
            {
                if (RunID == 0)
                {
                    if (UdonUI_Manager.Instance.mainUIobj)
                    {
                        GameObject[] a = Selection.gameObjects;
                        //Debug.Log("导入了" + a.Length + "个UdonUI按钮");
                        //UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                        if(MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;
                        //UdonUiButton.publicVariables.TryGetVariableValue("Button", out Button_late);//所有按钮  
                        Button_late = MainUdonUI_E.Button;
                        Button = new GameObject[Button_late.Length + a.Length];
                        for (int i = 0; i < Button.Length; i++)
                        {
                            if (i < Button_late.Length)
                                Button[i] = Button_late[i];
                            else
                                Button[i] = a[i - Button_late.Length];
                        }
                        //UdonUiButton.publicVariables.TrySetVariableValue("Button", Button);
                        MainUdonUI_E.Button = Button;
                        Undo.RegisterCompleteObjectUndo(MainUdonUI_E, "按钮修改");
                    }
                }

                RunID++;
                if (RunID >= Selection.gameObjects.Length) RunID = 0;
            }
            else
            {
                Debug.Log("请选中对象再进行添加UdonUI对象");
            }
        }

        [MenuItem("GameObject/UdonUI/ADD To/UdonTrigger", false, 16)]
        static void ADDTrigger()
        {
            if (Selection.gameObjects.Length > 0)
            {
                if (RunID == 0)
                {
                    if (UdonUI_Manager.Instance.mainUIobj)
                    {
                        GameObject[] a = Selection.gameObjects;
                        //UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                        if(MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;
                        //UdonUiButton.publicVariables.TryGetVariableValue("BoxColliderUdon", out Button_late_BoxCollider);//所有按钮
                        Button_late_BoxCollider = MainUdonUI_E.BoxColliderUdon;
                        Button_BoxCollider = new GameObject[Button_late_BoxCollider.Length + a.Length];
                        for (int i = 0; i < Button_BoxCollider.Length; i++)
                        {
                            if (i < Button_late_BoxCollider.Length)
                                Button_BoxCollider[i] = Button_late_BoxCollider[i];
                            else
                                Button_BoxCollider[i] = a[i - Button_late_BoxCollider.Length];
                        }
                        //UdonUiButton.publicVariables.TrySetVariableValue("BoxColliderUdon", Button_BoxCollider);
                        MainUdonUI_E.BoxColliderUdon = Button_BoxCollider;
                        Undo.RegisterCompleteObjectUndo(MainUdonUI_E, "按钮修改");
                    }
                }

                RunID++;
                if (RunID >= Selection.gameObjects.Length) RunID = 0;
            }
            else
            {
                Debug.Log("请选中对象再进行添加UdonUI对象");
            }
        }

        [MenuItem("GameObject/UdonUI/SetButton To/Drag panel", false, 17)]
        static void SetButtonToDrag()
        {
            if (Selection.gameObjects.Length > 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请勿选择多个对象", "我知道了", "关闭")) { }
            }
            else if (Selection.gameObjects.Length < 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请选中UdonButton对象", "我知道了", "关闭")) { }
            }
            else
            {
                if (UdonUI_Manager.Instance.mainUIobj)
                {
                    //UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                    if(MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;
                    GameObject[] Buttons = new GameObject[0];
                    //UdonUiButton.publicVariables.TryGetVariableValue("Button", out Buttons);
                    Buttons = MainUdonUI_E.Button;

                    int id = -1;
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (Buttons[i] == Selection.gameObjects[0])
                        {
                            id = i;
                            break;
                        }
                    }
                    if (id > -1)
                    {//主逻辑
                        //for (int i = 0; i < Buttons[id].transform.childCount; i++)
                        //{
                        //    if (Buttons[id].transform.GetChild(i).name == "MoveWindows")
                        //    {
                        //        if (EditorUtility.DisplayDialog("警告", "        当前按钮已存在Windows功能组件，请勿重复添加", "我知道了", "关闭")) { }
                        //        return;
                        //    }
                        //}

                        if (!Buttons[id].transform.parent)
                        {
                            if (EditorUtility.DisplayDialog("通知", "        设定成功，检测到按钮未存在父级，请将该按钮放置在窗口对象子级下", "我知道了", "关闭")) { }
                            return;
                        }
                        else
                        {
                            if (EditorUtility.DisplayDialog("通知", "        设定成功，当按住此按钮时，会拖动此按钮的父级\n\n      已自动向该按钮配置相关事件，请勿删除或谨慎修改此事件，否则可能会使‘窗口拖拽’功能失效！！！", "我知道了", "关闭")) { }
                            UdonUI_Manager.Instance.SetUdonUIButtonEvent(id, 0, 32, UdonUI_Manager.Instance.mainUIobj, "OnMoveWindow");
                            //GameObject MoveWindow = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "MoveWindows.prefab", typeof(GameObject)));//创建UdonUI环境
                            //Undo.RegisterCreatedObjectUndo(MoveWindow, "创建Windows组件");
                            //MoveWindow.transform.SetParent(Buttons[id].transform);
                            //MoveWindow.transform.position = Buttons[id].transform.position;
                            //MoveWindow.name = "MoveWindows";
                            //MoveWindow.SetActive(false);

                            //UdonBehaviour MoveWind = MoveWindow.GetComponent<UdonBehaviour>();
                            //MoveWindows MoveWind = MoveWindow.GetComponent<MoveWindows>();
                            //MoveWind.publicVariables.TrySetVariableValue("MoveTargetParent", false);
                            //MoveWind.MoveTargetParent = false;
                            ////MainUdonUI_E.MoveTargetParent = false;
                            //AddWindowsMenu(id, MoveWindow, MainUdonUI_E);//配置该窗口
                            //AddWindowsMenu(id, MoveWindow, MainUdonUI_E);//配置该窗口

                            //AddWindowsMenu(id, MoveWindow, MainUdonUI_E, 0, 1);
                            //AddWindowsMenu(id, MoveWindow, MainUdonUI_E, 2, 2);
                        }
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("警告", "        当前对象并非UdonUI下的Button，请重新选择UdonUI的Button进行设置", "我知道了", "关闭")) { }
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("警告", "        请右键点击‘UdonUI/Button’，或者直接点击‘创建UdonUI’以创建UdonUI环境", "创建UdonUI", "关闭")) { ADDButton(); }
                }

            }

        }

        [MenuItem("GameObject/UdonUI/SetButton To/Sliding panel", false, 18)]
        static void SetButtonToSlid()
        {
            if (Selection.gameObjects.Length > 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请勿选择多个对象", "我知道了", "关闭")) { }
            }
            else if (Selection.gameObjects.Length < 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请选中UdonButton对象", "我知道了", "关闭")) { }
            }
            else
            {
                if (UdonUI_Manager.Instance.mainUIobj)
                {
                    if (MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;
                    GameObject[] Buttons = new GameObject[0];
                    //MainUdonUI_E.publicVariables.TryGetVariableValue("Button", out Buttons);
                    Buttons = MainUdonUI_E.Button;
                    int id = -1;
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (Buttons[i] == Selection.gameObjects[0])
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
                            GameObject SlidingWindows = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SlidingWindows.prefab", typeof(GameObject)));//创建UdonUI环境
                            Undo.RegisterCreatedObjectUndo(SlidingWindows, "创建Windows组件");
                            SlidingWindows.transform.SetParent(MainObj.transform);
                            SlidingWindows.transform.position = MainObj.transform.position;
                            SlidingWindows.name = "SlidingWindows";
                            SlidingWindows.SetActive(false);
                            AddWindowsMenu(id, SlidingWindows, MainUdonUI_E);//配置该窗口

                            GameObject SlidingWindows_Out = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SlidingWindows_Out.prefab", typeof(GameObject)));//创建UdonUI环境
                            Undo.RegisterCreatedObjectUndo(SlidingWindows_Out, "创建Windows组件");
                            SlidingWindows_Out.transform.SetParent(MainObj.transform);
                            SlidingWindows_Out.transform.position = MainObj.transform.position;
                            SlidingWindows_Out.name = "SlidingWindows_Out";
                            SlidingWindows_Out.SetActive(false);
                            AddWindowsMenu(id, SlidingWindows, MainUdonUI_E, 2, 2);//配置该窗口

                            if (EditorUtility.DisplayDialog("通知", "        设定成功，当滑动此按钮时，滑动该组件画面\n\n      已自动向该按钮配置相关事件，请勿删除或谨慎修改此事件，否则可能会使‘滑动窗口’功能失效！！！", "我知道了", "关闭")) { }
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
                else
                {
                    if (EditorUtility.DisplayDialog("警告", "        请右键点击‘UdonUI/Button’，或者直接点击‘创建UdonUI’以创建UdonUI环境", "创建UdonUI", "关闭")) { ADDButton(); }
                }

            }
        }

        [MenuItem("GameObject/UdonUI/SetButton To/Interact", false, 19)]
        static void SetToInteract()
        {
            if (Selection.gameObjects.Length > 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请勿选择多个对象", "我知道了", "关闭")) { }
            }
            else if (Selection.gameObjects.Length < 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请选中UdonButton对象", "我知道了", "关闭")) { }
            }
            else
            {
                if (UdonUI_Manager.Instance.mainUIobj)
                {
                    //UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                    MainUI_Script UdonUiButton = UdonUI_Manager.Instance.mainUI;
                    GameObject[] Buttons = new GameObject[0];
                    //UdonUiButton.publicVariables.TryGetVariableValue("Button", out Buttons);
                    Buttons = UdonUiButton.Button;
                    int id = -1;
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (Buttons[i] == Selection.gameObjects[0])
                        {
                            id = i;
                            break;
                        }
                    }
                    if (id > -1)
                    {//主逻辑
                     //Vector3 pos = Buttons[id].transform.position;
                        GameObject MoveWindow = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Interact_UdonUI.prefab", typeof(GameObject)));//创建UdonUI环境
                        Undo.RegisterCreatedObjectUndo(MoveWindow, "更替组件");
                        MoveWindow.transform.SetParent(Buttons[id].transform.parent);
                        MoveWindow.transform.position = Buttons[id].transform.position;
                        MoveWindow.transform.rotation = Buttons[id].transform.rotation;
                        MoveWindow.name = Buttons[id].name;
                        Undo.DestroyObjectImmediate(Buttons[id]);
                        DestroyImmediate(Buttons[id]);
                        Buttons[id] = MoveWindow;
                        //UdonUiButton.publicVariables.TrySetVariableValue("Button", Buttons);
                        UdonUiButton.Button = Buttons;
                        Undo.RegisterCompleteObjectUndo(UdonUiButton, "按钮修改");
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("警告", "        当前对象并非UdonUI下的Button，请重新选择UdonUI的Button进行设置", "我知道了", "关闭")) { }
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("警告", "        请右键点击‘UdonUI/Button’，或者直接点击‘创建UdonUI’以创建UdonUI环境", "创建UdonUI", "关闭")) { ADDButton(); }
                }

            }

        }

        [MenuItem("GameObject/UdonUI/SetButton To/InteractToButton", false, 20)]
        static void SetToButton()
        {
            if (Selection.gameObjects.Length > 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请勿选择多个对象", "我知道了", "关闭")) { }
            }
            else if (Selection.gameObjects.Length < 1)
            {
                if (EditorUtility.DisplayDialog("警告", "        请选中UdonButton对象", "我知道了", "关闭")) { }
            }
            else
            {
                if (UdonUI_Manager.Instance.mainUIobj)
                {
                    //UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
                    if (MainUdonUI_E == null) MainUdonUI_E = UdonUI_Manager.Instance.mainUI;

                    GameObject[] Buttons = new GameObject[0];
                    //UdonUiButton.publicVariables.TryGetVariableValue("Button", out Buttons);
                    Buttons = MainUdonUI_E.Button;

                    int id = -1;
                    for (int i = 0; i < Buttons.Length; i++)
                    {
                        if (Buttons[i] == Selection.gameObjects[0])
                        {
                            id = i;
                            break;
                        }
                    }
                    if (id > -1)
                    {//主逻辑
                     //Vector3 pos = Buttons[id].transform.position;
                        GameObject MoveWindow = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Udon_Button.prefab", typeof(GameObject)));//创建UdonUI环境
                        Undo.RegisterCreatedObjectUndo(MoveWindow, "更替组件");
                        MoveWindow.transform.SetParent(Buttons[id].transform.parent);
                        MoveWindow.transform.position = Buttons[id].transform.position;
                        MoveWindow.transform.rotation = Buttons[id].transform.rotation;
                        MoveWindow.name = Buttons[id].name;
                        Undo.DestroyObjectImmediate(Buttons[id]);
                        DestroyImmediate(Buttons[id]);
                        Buttons[id] = MoveWindow;
                        //UdonUiButton.publicVariables.TrySetVariableValue("Button", Buttons);
                        MainUdonUI_E.Button = Buttons;
                        Undo.RegisterCompleteObjectUndo(MainUdonUI_E, "按钮修改");

                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("警告", "        当前对象并非UdonUI下的Button，请重新选择UdonUI的Button进行设置", "我知道了", "关闭")) { }
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("警告", "        请右键点击‘UdonUI/Button’，或者直接点击‘创建UdonUI’以创建UdonUI环境", "创建UdonUI", "关闭")) { ADDButton(); }
                }

            }

        }
        //[MenuItem("GameObject/UdonUI/ADD To/UdonFinger", false, 9)]
        //static void ADDFinger()
        //{
        //    if (Selection.gameObjects.Length > 0)
        //    {
        //        if (RunID == 0)
        //        {
        //            if (GameObject.Find("/UdonUI_Main"))
        //            {
        //                GameObject[] a = Selection.gameObjects;
        //                //Debug.Log("导入了" + a.Length + "个UdonUI按钮");
        //                UdonBehaviour UdonUiButton = GameObject.Find("/UdonUI_Main").GetComponent<UdonBehaviour>();
        //                UdonUiButton.publicVariables.TryGetVariableValue("Button", out Button_late);//所有按钮  
        //                Button = new GameObject[Button_late.Length + a.Length];
        //                for (int i = 0; i < Button.Length; i++)
        //                {
        //                    if (i < Button_late.Length)
        //                        Button[i] = Button_late[i];
        //                    else
        //                        Button[i] = a[i - Button_late.Length];
        //                }
        //                UdonUiButton.publicVariables.TrySetVariableValue("Button", Button);
        //                Undo.RegisterCompleteObjectUndo(UdonUiButton, "按钮修改");
        //            }
        //        }

        //        RunID++;
        //        if (RunID >= Selection.gameObjects.Length) RunID = 0;
        //    }
        //    else
        //    {
        //        Debug.Log("请选中对象再进行添加UdonUI对象");
        //    }
        //}

        [MenuItem("GameObject/UdonUI/Warp", false, 21)]
        static void OnUdonUIWarp() => UdonUI_Wrap_Editor.OnUdonUI_Warp();
        [MenuItem("GameObject/UdonUI/同步UdonUI世界单位", false, 22)]
        static void OnObjSync() { 
            if(UdonUI_Manager.Instance.mainUI != null)
            {
                UdonUI_ObjSync _sync = UdonUI_Manager.Instance.FindObjects<UdonUI_ObjSync>();
                if(_sync == null)
                {
                    GameObject _obj = new GameObject();
                    _obj.name = "同步UdonUI世界单位";
                    _sync = _obj.AddComponent<UdonUI_ObjSync>();
                    Undo.RegisterCreatedObjectUndo(_sync, "创建 [同步UdonUI世界单位]");
                }

                List<GameObject> syncGamenObj = new List<GameObject>();
                MainUI_Script mainUI = UdonUI_Manager.Instance.mainUI;
                bool[] _syncState = mainUI.synButton;
                for (int i = 0; i < _syncState.Length; i++)
                {
                    if (_syncState[i])
                    {//是否同步
                        Vector2 _range = mainUI.MainEventNumber[i];
                        for (int t = 0; t < (int)_range.y; t++)
                        {
                            int _id = (int)_range.x + t;
                            if (mainUI.MainEvent[_id]<3)
                            {//如果是关于物体操作的对象
                                if (!syncGamenObj.Contains(mainUI.TargetGameObject[_id]))
                                {
                                    syncGamenObj.Add(mainUI.TargetGameObject[_id]);//受同步影响的对象。
                                }
                            }
                        }
                    }
                }

                bool[] _syncStateCon = mainUI.synBoxCollider;
                for (int i = 0; i < _syncStateCon.Length; i++)
                {
                    if (_syncStateCon[i])
                    {//是否同步
                        Vector2 _range = mainUI.MainEventNumber_BoxCollider[i];
                        for (int t = 0; t < (int)_range.y; t++)
                        {
                            int _id = (int)_range.x + t;
                            if (mainUI.MainEvent_BoxCollider[_id] < 3)
                            {//如果是关于物体操作的对象
                                if (!syncGamenObj.Contains(mainUI.TargetGameObject_BoxCollider[_id]))
                                {
                                    syncGamenObj.Add(mainUI.TargetGameObject_BoxCollider[_id]);//受同步影响的对象。
                                }
                            }
                        }
                    }
                }

                _sync._object = syncGamenObj.ToArray();
                EditorUtility.SetDirty(_sync);
                UdonUI_Manager.Instance.PingObj(_sync);
            }
            else
            {
                EditorUtility.DisplayDialog("警告", "尚未创建UdonUI环境，请先创建环境", "我知道了");
            }
        }
        /// <summary>
        /// 返回控制面板
        /// </summary>
        /// <returns></returns>
        static GameObject UdonUI()
        {
            GameObject F = null;

            if (!UdonUI_Manager.Instance.mainUIobj)
            {
                GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "UdonUI_Main.prefab", typeof(GameObject)));//创建UdonUI环境
                Selection.activeGameObject = A;//选中才能被更新到
                GameObject B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Udon_ControlPanel.prefab", typeof(GameObject)));//创建Udon面板
                //MainUdonUI = A.GetComponent<UdonBehaviour>();
                //MainUdonUI_E = A.GetComponent<MainUI_Script>();
                MainUI_Editor.MainUI = A;//给窗口赋值UdonUI环境
                //MainUI_Editor.MainUIUdon = A.GetComponent<UdonBehaviour>();
                if (!GameObject.Find("/VRCWorld"))
                //if(!FindObjectOfType<>)
                {
                    //var CCD = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/VRChat Examples/Prefabs/VRCWorld.prefab", typeof(GameObject)));
                    string _nowPath = "Packages/com.vrchat.worlds/Samples/UdonExampleScene/Prefabs/VRCWorld.prefab";
                    //if (System.IO.Directory.Exists(_nowPath))
                    {
                        var CCD = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadMainAssetAtPath(_nowPath));
                        Undo.RegisterCreatedObjectUndo(CCD, "创建VRC环境");//记录Udon创建操作
                    }
                    //else
                    //{
                    //    Debug.LogWarning("创建VRCWorld失败，请确认路径：" + _nowPath);
                    //}
                }
                Undo.RegisterCreatedObjectUndo(A, "创建UdonUI环境");//记录Udon创建操作
                Undo.RegisterCreatedObjectUndo(B, "创建Udon面板");//记录Udon创建操作
                A.name = "UdonUI_Main";
                B.name = "UdonControlPanel";
                B.transform.SetParent(A.transform);
                F = B;

                //场景中没有UdonUI环境
                GameObject But = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "Udon_Button.prefab", typeof(GameObject)));
                Undo.RegisterCreatedObjectUndo(But, "创建Udon按钮");//记录Udon创建操作
                But.name = "Button";
                But.transform.SetParent(B.transform);
                Button = new GameObject[1];
                Button[0] = But;
                Button_late = Button;
                //MainUdonUI.publicVariables.TrySetVariableValue("Button", Button_late);
                UdonUI_Manager.Instance.mainUI.Button = Button_late;
                //MainUdonUI_E.Button = Button_late;
                ButtonType = new int[1];
                ButtonType[0] = 1;
                //MainUdonUI.publicVariables.TrySetVariableValue("ButtonType", ButtonType);
                UdonUI_Manager.Instance.mainUI.ButtonType = ButtonType;
                //MainUdonUI_E.ButtonType = ButtonType;
                Vector2[] Event = new Vector2[1];
                //MainUdonUI.publicVariables.TrySetVariableValue("MainEventNumber", Event);
                UdonUI_Manager.Instance.mainUI.MainEventNumber = Event;
                //MainUdonUI_E.MainEventNumber = Event;

                GameObject BoxC = GameObject.Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "UdonBoxCollider.prefab", typeof(GameObject)));
                Undo.RegisterCreatedObjectUndo(BoxC, "创建Udon碰撞体");//记录Udon创建操作
                BoxC.name = "UdonBoxTrigger";
            }

            return F;
        }

        /// <summary>
        /// 给UdonUIButton添加操作对象
        /// </summary>
        /// <param name="GameObjID">按钮的ID</param>
        /// <param name="target">添加的对象</param>
        /// <param name="MainUI">Udon组件</param>
        public static void AddWindowsMenu(int GameObjID, GameObject target, MainUI_Script MainUI)
        {
            Vector2[] MainEventNumber = new Vector2[1];
            int[] Action = new int[1];
            int[] MainEvent = new int[1];
            Animator[] MainAnimators = new Animator[1];
            string[] AnimaName = new string[1];
            GameObject[] ALLObject = new GameObject[1];
            int[] MainEvents = new int[1];
            //MainUI.publicVariables.TryGetVariableValue("MainEventNumber", out MainEventNumber);//事件编号
            //MainUI.publicVariables.TryGetVariableValue("Action", out Action);
            //MainUI.publicVariables.TryGetVariableValue("MainEvent", out MainEvent);
            ////----------------------------------------------------------------------//
            //MainUI.publicVariables.TryGetVariableValue("MainAnimators", out MainAnimators);
            //MainUI.publicVariables.TryGetVariableValue("AnimaName", out AnimaName);
            //MainUI.publicVariables.TryGetVariableValue("TargetGameObject", out ALLObject);
            //MainUI.publicVariables.TryGetVariableValue("MainEvents", out MainEvents);
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
                else //if (i >= (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y - 1)
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
            ALLObject_late[SetID] = target;
            Action_Late[SetID] = 0;
            MainEvent_Late[SetID] = 1;
            //MainUI.publicVariables.TrySetVariableValue("Action", Action_Late);
            //MainUI.publicVariables.TrySetVariableValue("MainEvent", MainEvent_Late);
            ////----------------------------------------------------------------------//
            //MainUI.publicVariables.TrySetVariableValue("MainAnimators", MainAnimators_late);
            //MainUI.publicVariables.TrySetVariableValue("AnimaName", AnimaName_late);
            //MainUI.publicVariables.TrySetVariableValue("TargetGameObject", ALLObject_late);
            //MainUI.publicVariables.TrySetVariableValue("MainEvents", MainEvents_Late);
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

            //MainUI.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);//事件编号
            MainUI.MainEventNumber = MainEventNumber;
            Undo.RegisterCompleteObjectUndo(MainUI, "事件数量修改");
        }

        /// <summary>
        /// 给UdonUIButton添加操作对象
        /// </summary>
        /// <param name="GameObjID">按钮的ID</param>
        /// <param name="target">添加的对象</param>
        /// <param name="MainUI">Udon组件</param>
        /// <param name="Event">触发条件</param>
        public static void AddWindowsMenu(int GameObjID, GameObject target, MainUI_Script MainUI, int sj, int Event)
        {
            Vector2[] MainEventNumber = new Vector2[1];
            int[] Action = new int[1];
            int[] MainEvent = new int[1];
            Animator[] MainAnimators = new Animator[1];
            string[] AnimaName = new string[1];
            GameObject[] ALLObject = new GameObject[1];
            int[] MainEvents = new int[1];
            //MainUI.publicVariables.TryGetVariableValue("MainEventNumber", out MainEventNumber);//事件编号
            //MainUI.publicVariables.TryGetVariableValue("Action", out Action);
            //MainUI.publicVariables.TryGetVariableValue("MainEvent", out MainEvent);
            ////----------------------------------------------------------------------//
            //MainUI.publicVariables.TryGetVariableValue("MainAnimators", out MainAnimators);
            //MainUI.publicVariables.TryGetVariableValue("AnimaName", out AnimaName);
            //MainUI.publicVariables.TryGetVariableValue("TargetGameObject", out ALLObject);
            //MainUI.publicVariables.TryGetVariableValue("MainEvents", out MainEvents);
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
                else //if (i >= (int)MainEventNumber[GameObjID].x + (int)MainEventNumber[GameObjID].y - 1)
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
            ALLObject_late[SetID] = target;
            Action_Late[SetID] = sj;
            MainEvent_Late[SetID] = Event;
            //MainUI.publicVariables.TrySetVariableValue("Action", Action_Late);
            //MainUI.publicVariables.TrySetVariableValue("MainEvent", MainEvent_Late);
            ////----------------------------------------------------------------------//
            //MainUI.publicVariables.TrySetVariableValue("MainAnimators", MainAnimators_late);
            //MainUI.publicVariables.TrySetVariableValue("AnimaName", AnimaName_late);
            //MainUI.publicVariables.TrySetVariableValue("TargetGameObject", ALLObject_late);
            //MainUI.publicVariables.TrySetVariableValue("MainEvents", MainEvents_Late);
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

            //MainUI.publicVariables.TrySetVariableValue("MainEventNumber", MainEventNumber);//事件编号
            MainUI.MainEventNumber = MainEventNumber;
            Undo.RegisterCompleteObjectUndo(MainUI, "事件数量修改");
        }

        static GameObject FindObject(string objName)
        {
            foreach (GameObject name in Resources.FindObjectsOfTypeAll(typeof(GameObject))) {
                if (name.name == objName)
                {
                    Selection.activeObject = name;
                    return name;
                }
                //if(!EditorUtility.IsPersistent(name.transform.root.gameObject) && !(name.hideFlags))
            }
            return null;
        }
    }
}