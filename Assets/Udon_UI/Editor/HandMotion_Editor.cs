using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Udon;
using UdonUI;

namespace UdonUI_Editor
{
    public class HandMotion_Editor : EditorWindow
    {

        #region Language
        static string[] chinese = new string[]{
            "UdonUI手势菜单",//0
            "检查到你是第一次打开手势菜单属性，或者场景中不存在主要菜单。",//1
            "需要自动创建手势菜单并设定好相关属性吗?",//2
            "是的，请帮我创建手势菜单",//3
            "注意",//4
            "我知道了",//5
            "关闭",//6
            "已完成菜单创建。之后可以在手势菜单点击‘主要菜单’追踪以便编辑。如果想修改同步用的‘次要菜单’请追踪到‘次要菜单’后直接编辑，‘次要菜单’并不包含任何功能，仅用于同步显示。",//7
            "在设定主要和次要菜单的时候，它们动画机必须在他们的第一个子级下，否则在vrc运行会报错。‘主要菜单’必须放置于游戏场景中,而‘次要菜单’则只需要放置在项目资源（Porject窗口）中。",//8
            "不用，我自己设定",//9
            "请创建UdonUI手势菜单",//10
            "你可以在【UdonUI主菜单】弹窗里面创建手势菜单",//11
            "请先创建UdonUI辅助属性",//12
            "你可以在【UdonUI主菜单】弹窗里面创建辅助属性",//13
            "主要菜单(本地用,不能为空)",//14
            "次菜单(同步用，允许为空)",//15
            "PC端开启和关闭菜单的按键设置",//16
            "打开菜单的手势",//17
            "关闭菜单的手势",//18
            "(注意打开和关闭菜单的手势设置不要完全相同，不然会同时触发)",//19
            "动画机里开启菜单的动画名(主次菜单共用)",//20
            "动画机里关闭菜单的动画名(主次菜单共用)",//21
            "最大距离(玩家和菜单超过此距离将会关闭菜单)",//22
            "打开菜单时的音效",//23
            "关闭菜单时的音效",//24
            "如果想弃用手势菜单，直接到UdonUI_AGI下删除整个SAOUI即可",//25
            ""
        };
        string[] handmotin_cn = new string[19] {
            "右手/下滑（由上至下）",
            "右手/上滑（由下至上）",
            "右手/左滑（由右至左）",
            "右手/右滑（由左至右）",
            "右手/前推（由后至前）",
            "右手/后拉（由前至后）",
            "左手/下滑（由上至下）",
            "左手/上滑（由下至上）",
            "左手/左滑（由右至左）",
            "左手/右滑（由左至右）",
            "左手/前推（由后至前）",
            "左手/后拉（由前至后）",
            "右或左/下滑（由上至下）",
            "右或左/上滑（由下至上）",
            "右或左/左滑（由右至左）",
            "右或左/右滑（由左至右）",
            "右或左/前推（由后至前）",
            "右或左/后拉（由前至后）",
            "关闭手势"
        };
        string[] UIPosTo_cn = new string[2] {
            "在玩家面前打开",
            "在手部滑动的位置启动"
        };


        static string[] english = new string[]{
            "UdonUIMotion",//0
            "Check that you are opening the Gesture Menu property for the first time, or that the main menu does not exist in the scene.",//1
            "Need to automatically create a gesture menu and set the relevant properties?",//2
            "Yes, please help me create the gesture menu",//3
            "note",//4
            "I know",//5
            "off",//6
            "The menu creation is complete. After that, you can tap 'Main Menu' in the gesture menu to track for editing. If you want to modify the 'secondary menu' used for synchronization, please track down to the 'secondary menu' and edit it directly, the 'secondary menu' does not contain any functions, only for the synchronization display.",//7
            "When setting up major and secondary menus, their animators must be under their first child, otherwise an error will be reported when the vrc runs. The 'main menu' must be placed in the game scene, while the 'secondary menu' only needs to be placed in the project asset (Porject window).",//8
            "No, I set it myself",//9
            "Please create the UdinUI gesture menu",//10
            "You can create a gesture menu in the [UdonUI Main Menu] pop-up window",//11
            "Please create the UconUI auxiliary attribute first",//12
            "You can create secondary properties in the UdonUI Main Menu pop-up window",//13
            "Main menu (local, not empty)",//14
            "Secondary menu (for synchronization, Allowed to be empty)",//15
            "Button settings for opening and closing menus on the PC side",//16
            "Gestures to open the menu",//17
            "A gesture to close a menu",//18
            "(Note that the gesture settings for opening and closing the menu should not be exactly the same, otherwise it will be triggered at the same time)",//19
            "The animation name of the menu opened in the animation machine (shared between primary and secondary menus)",//20
            "The animation name of the closed menu in the animation machine (shared between the primary and secondary menus)",//21
            "Maximum distance (players and menus over this distance will close the menu)",//22
            "Sound effects when opening a menu",//23
            "Sound effects when the menu is closed",//24
            "If you want to deprecate the gesture menu, go directly to the UdonUI_AGI and delete the entire SAOUI",//25

        };
        string[] handmotin_en = new string[19] {
            "Right hand/Slip Down（Up To Down）",
            "Right hand/Slip Up（Down To Up）",
            "Right hand/Slip Left（Right To Left）",
            "Right hand/Slip Right（Left To Right）",
            "Right hand/Slip Forward（Back To Forward）",
            "Right hand/Slip Back（Forward To Back）",
            "Left hand/Slip Down（Up To Down）",
            "Left hand/Slip Up（Down To Up）",
            "Left hand/Slip Left（Right To Left）",
            "Left hand/Slip Right（Left To Right）",
            "Left hand/Slip Forward（Back To Forward）",
            "Left hand/Slip Back（Forward To Back）",
            "Right or Left/Slip Down（Up To Down）",
            "Right or Left/Slip Up（Down To Up）",
            "Right or Left/Slip Left（Right To Left）",
            "Right or Left/Slip Right（Left To Right）",
            "Right or Left/Slip Forward（Back To Forward）",
            "Right or Left/Slip Back（Forward To Back）",
            "OFF"
        };
        string[] UIPosTo_en = new string[2] {
            "Open it in front of the player",
            "Start at the position where the hand slides."
        };

        static string[] japanese = new string[]{
            "UdonUIMotion",//0
            "HandMotionメニューのプロパティを初めて開いていることを自動的に判明しました。まだシーンにHandMotionメインメニューがありません。",//1
            "HandMotionメニューのプロパティは自動的に設定することができます。ご利用いただきますか。?",//2
            "はい、設定してください。",//3
            "注意",//4
            "わかりました",//5
            "閉じる",//6
            "メニュー設定完了しました。これからHandMotionメニューに「メインメニュー」を選択して設定することはできます。ただ、同期用サブメニューを設置したいなら「サブメニュー」を選択して設定するください。「サブメニュー」はただの同期用ものです。機能はありません",//7
            "メインメニューとサブメニューを設定する際に、Animatorは必ずメインメニューとサブメニュー最初のレベルの下にあります。しないと恐らくエラーを発生すること。【メインメニュー】は必ずシーンにあることが必要。【サブメニュー】はただPorjectにあるがいい",//8
            "いいえ、利用しません",//9
            "UdonUIMotionメニューを作成してください",//10
            "【UdonUIメインメニュー】ウィンドウズにHandMotionメニューの作成はできます",//11
            "先ずはUdonUI補助プロパティを作成してください",//12
            "UdonUIメインメニュー】ウィンドウズに補助プロパティを作成はできます",//13
            "メインメニュー（ローカル用、空白にしないでください）",//14
            "サブメニュー（同期用、空にすることが許可されています）",//15
            "デスクトップモードのメニューを開閉するのキーの設定",//16
            "メニューを開くジェスチャー",//17
            "メニューを閉じるジェスチャー",//18
            "(開く手振りと閉じる手振りは同じくないでお願いします。恐らく同時に開閉することは可能です)",//19
            "Animatorにメニューを開くAnimationの名（メインメニューとサブメニューは同じ）",//20
            "Animatorニューを閉じるAnimationの名（メインメニューとサブメニューは同じ）",//21
            "最大距離（この距離以上ならニューは閉じされる）",//22
            "メニューを開く時の音",//23
            "メニューを閉じる時の音",//24
            "HandMotionを解除したい場合はUdonUI_AGIにSAOUIを削除してください",//25
        };
        string[] handmotin_jp = new string[19] {
            "右手/下振り（上から下に）",
            "右手/上振り（下から上に）",
            "右手/左振り（右から左に）",
            "右手/右振り（左じから右に）",
            "右手/前振り（後ろから前に）",
            "右手/後ろ振り（前に後ろから）",
            "左手/下振り（上から下に）",
            "左手/上振り（下から上に）",
            "左手/左振り（右から左に）",
            "左手/右振り（左じから右に）",
            "左手/前振り（後ろから前に）",
            "左手/後ろ振り（前に後ろから）",
            "右手あるいは左手/下振り（上から下に）",
            "右手あるいは左手/上振り（下から上に）",
            "右手あるいは左手/左振り（右から左に）",
            "右手あるいは左手/右振り（左じから右に）",
            "右手あるいは左手/前振り（後ろから前に）",
            "右手あるいは左手/後ろ振り（前に後ろから）",
            "ジェスチャーを閉じる"
        };
        string[] UIPosTo_jp = new string[2] {
            "プレイヤーの目の前に開く",
            "手のポジションに開く"
        };

        //porName[15]
        static string[] porName;//porName[2]
        static int languageID = 0;
        #endregion

        #region File
        bool start = true;
        static Transform SAOobj;

        [SerializeField]
        bool Audio_on = false;

        [SerializeField]
        GameObject[] MainGameObj;
        [SerializeField]
        GameObject MainGameobject, MainGameobject2;//主菜单，次菜单
        [SerializeField]
        string startname, stopName, standname, endname;
        [SerializeField]
        AudioClip MainAudio1, MainAudio2;
        [SerializeField]
        int OpenUI, OffUI, UIpos = 0;// 1  2  开关位置
        [SerializeField]
        float maxDistance = 0.2f;
        [SerializeField]
        KeyCode OpenEnumKey = KeyCode.X;

        [SerializeField]
        bool HeadPos = false;

        static GameObject MainObj;
        SerializedObject _mobj;
        SerializedProperty _MainGameobject, _MainGameobject2;
        SerializedProperty _startname, _standname, _endname;
        SerializedProperty _Audio_on;
        SerializedProperty _MainAudio1, _MainAudio2;
        SerializedProperty _OpenUI, _OffUI;
        SerializedProperty _OpenEnumKey;
        #endregion

        private void OnEnable()
        {
            GetLanguage();
            _mobj = new SerializedObject(this);
            _MainGameobject = _mobj.FindProperty("MainGameobject");
            _MainGameobject2 = _mobj.FindProperty("MainGameobject2");
            _startname = _mobj.FindProperty("startname");
            _standname = _mobj.FindProperty("standname");
            _endname = _mobj.FindProperty("endname");
            _Audio_on = _mobj.FindProperty("Audio_on");
            _MainAudio1 = _mobj.FindProperty("MainAudio1");
            _MainAudio2 = _mobj.FindProperty("MainAudio2");
            _OpenUI = _mobj.FindProperty("OpenUI");
            _OffUI = _mobj.FindProperty("OffUI");
            _OpenEnumKey = _mobj.FindProperty("OpenEnumKey");

            if (UdonUI_Manager.Instance.mainUIobj)
            {
                //UdonBehaviour mainscript = GameObject.Find("/UdonUI_AGI").transform.Find("SAOUI").GetComponent<UdonBehaviour>();
                HandMotion_UI mainscript = GameObject.Find("/UdonUI_AGI").transform.Find("SAOUI").GetComponent<HandMotion_UI>();
                Selection.activeObject = GameObject.Find("/UdonUI_AGI").transform.Find("SAOUI");
                //mainscript.publicVariables.TryGetVariableValue("UIposToHead", out HeadPos);
                HeadPos = mainscript.UIposToHead;
                if (HeadPos) UIpos = 0; else UIpos = 1;
            }
        }

        private void OnGUI()
        {
            if (GameObject.Find("/UdonUI_AGI"))
            {
                MainObj = GameObject.Find("/UdonUI_AGI");
                if (MainObj.transform.Find("SAOUI"))
                {//地图中存在该物体
                    _mobj.Update();

                    //UdonBehaviour mainscript = MainObj.transform.Find("SAOUI").GetComponent<UdonBehaviour>();
                    HandMotion_UI mainscript_E = MainObj.transform.Find("SAOUI").GetComponent<HandMotion_UI>();
                    //mainscript.publicVariables.TryGetVariableValue("MainGameobject", out MainGameObj);
                    //mainscript.publicVariables.TryGetVariableValue("startname", out startname);
                    //mainscript.publicVariables.TryGetVariableValue("stopName", out stopName);
                    //mainscript.publicVariables.TryGetVariableValue("maxDistance", out maxDistance);
                    //mainscript.publicVariables.TryGetVariableValue("Audio_on", out Audio_on);
                    //mainscript.publicVariables.TryGetVariableValue("MainAudio1", out MainAudio1);
                    //mainscript.publicVariables.TryGetVariableValue("MainAudio2", out MainAudio2);
                    //mainscript.publicVariables.TryGetVariableValue("OpenUI", out OpenUI);
                    //mainscript.publicVariables.TryGetVariableValue("OffUI", out OffUI);
                    //mainscript.publicVariables.TryGetVariableValue("OpenEnumKey", out OpenEnumKey);
                    MainGameObj = mainscript_E.MainGameobject;
                    startname = mainscript_E.startname;
                    stopName = mainscript_E.stopName;
                    maxDistance = mainscript_E.maxDistance;
                    Audio_on = mainscript_E.Audio_on;
                    MainAudio1 = mainscript_E.MainAudio1;
                    MainAudio2 = mainscript_E.MainAudio2;
                    OpenUI = mainscript_E.OpenUI;
                    OffUI = mainscript_E.OffUI;
                    OpenEnumKey = mainscript_E.OpenEnumKey;

                    MainGameobject = MainGameObj[0];
                    MainGameobject2 = MainGameObj[1];


                    SAOobj = MainObj.transform.Find("SAOUI");
                    GUILayout.Space(10);
                    GUI.skin.label.fontSize = 24;
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label(porName[0]);

                    if (MainGameobject == null)
                    {//当脚本未被设置过
                     //Debug.Log("可不就是空的吗");
                        if (start)
                        {
                            GUI.skin.label.fontSize = 12;
                            GUILayout.Label(porName[1]+ "\n" + porName[2]);
                            EditorGUILayout.BeginHorizontal();
                            if (GUILayout.Button(porName[3]))
                            {
                                GameObject MainAnima = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_HandMotion + "main/Sao_Main.prefab", typeof(GameObject)));
                                GameObject MainAnima2 = (GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_HandMotion + "player_syn/Sao_syn.prefab", typeof(GameObject));
                                MainAnima.transform.SetParent(UdonUI_Manager.Instance.mainUIobj.transform);
                                MainAnima.name = "SaoUI_Main";
                                MainGameObj[0] = MainAnima;
                                MainGameObj[1] = MainAnima2;
                                mainscript_E.MainGameobject = MainGameObj;
                                mainscript_E.OpenUI = 0;
                                mainscript_E.OffUI = 2;
                                mainscript_E.startname = "Open";
                                mainscript_E.stopName = "Off";
                                mainscript_E.MainAudio1 = (AudioClip)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Audio + "SAO_Launcher.wav", typeof(AudioClip));
                                mainscript_E.MainAudio2 = (AudioClip)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Audio + "ALO_Message.wav", typeof(AudioClip));
                                mainscript_E.Audio_on = true;

                                //设置按钮
                                Transform _SaonMenuButton = MainAnima.transform.GetChild(0);
                                Transform _SaonMenu = MainAnima.transform.GetChild(0).GetChild(0);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenuButton.GetChild(1).GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 1, _SaonMenu.GetChild(0).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(2).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(3).gameObject);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenuButton.GetChild(2).GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(0).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 1, _SaonMenu.GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(2).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(3).gameObject);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenuButton.GetChild(3).GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(0).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 1, _SaonMenu.GetChild(2).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(3).gameObject);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenuButton.GetChild(4).GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(0).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(1).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 2, _SaonMenu.GetChild(2).gameObject);
                                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 1, _SaonMenu.GetChild(3).gameObject);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenu.GetChild(0).GetChild(2).gameObject, EButtonType.Drag);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenu.GetChild(1).GetChild(2).gameObject, EButtonType.Drag);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenu.GetChild(2).GetChild(2).gameObject, EButtonType.Drag);
                                UdonUI_Manager.Instance.AddUdonUIButton(_SaonMenu.GetChild(3).GetChild(2).gameObject, EButtonType.Drag);


                                Undo.RegisterCompleteObjectUndo(mainscript_E, "手势菜单修改");

                                //在场景中创建次要菜单
                                if (MainObj.transform.GetChild(0).childCount > 0)
                                {//如果房间同步人数大于零
                                    int SynPlayer_childCount = MainObj.transform.GetChild(0).childCount;//玩家同步数量
                                    int SynSAO_SynPlayer_childCount = SAOobj.childCount;//sao菜单数量
                                    for (int i = 0; i < SynPlayer_childCount; i++)
                                    {//随同步人数创建次级同步菜单
                                        if (i < SynSAO_SynPlayer_childCount)
                                            DestroyImmediate(SAOobj.transform.GetChild(0).gameObject);//删除SAOUI现有的所有子级
                                        Transform A = Instantiate(MainGameObj[1].transform, SAOobj);
                                        A.name = "SnySaoUI_" + (i + 1);
                                    }
                                }

                                if (EditorUtility.DisplayDialog(porName[4], "    " +porName[7], porName[5], porName[6]))
                                {
                                }

                            }
                            if (GUILayout.Button(porName[9]))
                            {
                                if (EditorUtility.DisplayDialog(porName[4], "    "+porName[8], porName[5], porName[6]))
                                {
                                }
                                //Selection.activeGameObject = MainAnima;
                                start = false;
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        else
                        {
                            MainDrawUI(mainscript_E);
                        }
                    }
                    else
                    {
                        MainDrawUI(mainscript_E);
                    }

                }
                else
                {
                    GUILayout.Space(10);
                    GUI.skin.label.fontSize = 24;
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label(porName[10]);
                    GUILayout.Label(porName[11]);
                }
            }
            else
            {
                GUILayout.Space(10);
                GUI.skin.label.fontSize = 24;
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label(porName[12]);
                GUILayout.Label(porName[13]);
            }


            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUI.skin.label.fontSize = 12;
        }

        void MainDrawUI(HandMotion_UI mainscript)
        {

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_MainGameobject, new GUIContent(porName[14], porName[14]));
            if (EditorGUI.EndChangeCheck())
            {
                _mobj.ApplyModifiedProperties();
                MainGameObj[0] = MainGameobject;
                //mainscript.publicVariables.TrySetVariableValue("MainGameobject", MainGameObj);
                mainscript.MainGameobject = MainGameObj;
                Undo.RegisterCompleteObjectUndo(mainscript, "按键属性修改");
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_MainGameobject2, new GUIContent(porName[15], porName[15]));
            if (EditorGUI.EndChangeCheck())
            {
                _mobj.ApplyModifiedProperties();
                MainGameObj[1] = MainGameobject2;
                //mainscript.publicVariables.TrySetVariableValue("MainGameobject", MainGameObj);
                mainscript.MainGameobject = MainGameObj;
                Undo.RegisterCompleteObjectUndo(mainscript, "按键属性修改");

                //在场景中创建次要菜单
                if (MainObj.transform.GetChild(0).childCount > 0)
                {//如果房间同步人数大于零
                    int SynPlayer_childCount = MainObj.transform.GetChild(0).childCount;//玩家同步数量
                    int SynSAO_SynPlayer_childCount = SAOobj.childCount;//sao菜单数量
                    for (int i = 0; i < SynPlayer_childCount; i++)
                    {//随同步人数创建次级同步菜单
                        if (i < SynSAO_SynPlayer_childCount)
                            DestroyImmediate(SAOobj.transform.GetChild(0).gameObject);//删除SAOUI现有的所有子级

                        if (MainGameObj[1])
                        {//如果值不为空的话
                            Transform A = Instantiate(MainGameObj[1].transform, SAOobj);
                            A.name = "SnySaoUI_" + (i + 1);
                        }
                    }
                }
            }

            GUILayout.Space(10);

            GUI.skin.label.fontSize = 12;
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_OpenEnumKey, new GUIContent(porName[16], porName[16]));

            GUILayout.BeginHorizontal();
            GUILayout.Label(porName[17]+"：");
            OpenUI = EditorGUILayout.Popup(OpenUI, GetHandMotin());
            //GUILayout.Space(100);
            GUILayout.Label(porName[18]+"：");
            OffUI = EditorGUILayout.Popup(OffUI, GetHandMotin());
            GUILayout.EndHorizontal();
            GUILayout.Label(porName[19]);
            GUILayout.Space(10);

            //EditorGUILayout.PropertyField(_startname, new GUIContent("动画机里开关菜单的Bool变量名"));
            GUILayout.BeginHorizontal();
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(porName[20]+"：");
            startname = EditorGUILayout.TextField(startname);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label(porName[21]+"：");
            stopName = EditorGUILayout.TextField(stopName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(porName[22]+"：");
            maxDistance = EditorGUILayout.FloatField(maxDistance);
            GUILayout.EndHorizontal();


            //GUILayout.Label("(打开菜单手势设置其Bool为true，反之关闭菜单手势设置其Bool为false)");
            if (EditorGUI.EndChangeCheck())
            {
                _mobj.ApplyModifiedProperties();
                //mainscript.publicVariables.TrySetVariableValue("OpenUI", OpenUI);
                //mainscript.publicVariables.TrySetVariableValue("OffUI", OffUI);
                //mainscript.publicVariables.TrySetVariableValue("startname", startname);
                //mainscript.publicVariables.TrySetVariableValue("stopName", stopName);
                //mainscript.publicVariables.TrySetVariableValue("maxDistance", maxDistance);
                //mainscript.publicVariables.TrySetVariableValue("OpenEnumKey", OpenEnumKey);
                mainscript.OpenUI = OpenUI;
                mainscript.OffUI = OffUI;
                mainscript.startname = startname;
                mainscript.stopName = stopName;
                mainscript.maxDistance = maxDistance;
                mainscript.OpenEnumKey = OpenEnumKey;
                Undo.RegisterCompleteObjectUndo(mainscript, "按键属性修改");
            }
            EditorGUI.BeginChangeCheck();
            UIpos = EditorGUILayout.Popup(UIpos, GetUIPosTo());
            if (EditorGUI.EndChangeCheck())
            {
                if (UIpos == 0)
                {
                    //mainscript.publicVariables.TrySetVariableValue("UIposToHead", true);
                    mainscript.UIposToHead = true;
                }
                else
                {
                    //mainscript.publicVariables.TrySetVariableValue("UIposToHead", false);
                    mainscript.UIposToHead = false;
                }
                Undo.RegisterCompleteObjectUndo(mainscript, "按键属性修改");
            }

            GUILayout.Space(10);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_Audio_on, new GUIContent("是否开启菜单音效"));
            if (EditorGUI.EndChangeCheck())
            {
                _mobj.ApplyModifiedProperties();
                //mainscript.publicVariables.TrySetVariableValue("Audio_on", Audio_on);
                mainscript.Audio_on = Audio_on;
                Undo.RegisterCompleteObjectUndo(mainscript, "按键属性修改");
            }
            if (Audio_on)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(_MainAudio1, new GUIContent(porName[23]));
                EditorGUILayout.PropertyField(_MainAudio2, new GUIContent(porName[24]));
                if (EditorGUI.EndChangeCheck())
                {
                    _mobj.ApplyModifiedProperties();
                    //mainscript.publicVariables.TrySetVariableValue("MainAudio1", MainAudio1);
                    //mainscript.publicVariables.TrySetVariableValue("MainAudio2", MainAudio2);
                    mainscript.MainAudio1 = MainAudio1;
                    mainscript.MainAudio2 = MainAudio2;
                    Undo.RegisterCompleteObjectUndo(mainscript, "按键属性修改");
                }
            }

            GUILayout.Space(20);
            GUI.skin.label.fontSize = 12;
            GUILayout.Label(porName[25]);

        }

        void GetLanguage()
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
        string[] GetHandMotin()
        {
            if (languageID == 0) return handmotin_cn;
            else if (languageID == 1) return handmotin_en;
            else if (languageID == 2) return handmotin_jp;

            return handmotin_cn;
        }
        string[] GetUIPosTo()
        {
            if (languageID == 0) return UIPosTo_cn;
            else if (languageID == 1) return UIPosTo_en;
            else if (languageID == 2) return UIPosTo_jp;

            return UIPosTo_cn;
        }

        ///if (MainGameobject == null)
        ///{//当脚本未被设置过
        /// //Debug.Log("可不就是空的吗");
        ///    if (start)
        ///    {
        ///        if (EditorUtility.DisplayDialog("注意", "    检测到你是初次打开SAO菜单属性，是否需要自动创建SAOUI预制对象并设置参数?", "是，请帮我创建", "不，我自己设置"))
        ///        {
        ///            GameObject MainAnima = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/SampleUI/HandMotion/main/Sao_Main.prefab", typeof(GameObject)));
        ///            MainAnima.transform.SetParent(GameObject.Find("UdonUI_Main").transform);
        ///            //GameObject.Find("UdonUI_AGI").transform.Find("SAOUI").GetComponent<UdonBehaviour>().publicVariables.TrySetVariableValue("MainGameobject", MainAnima);
        ///            Undo.RegisterCompleteObjectUndo(mainscript, "手势菜单修改");
        ///            if (EditorUtility.DisplayDialog("完成", "    设置完成，请在 UdonUI_Main 下找到你的手势菜单,你也可以通过点击确定自动帮你选中创建好的手势UI", "确定", "关闭"))
        ///            {
        ///                Selection.activeGameObject = MainAnima;
        ///                //if (GUILayout.Button("在场景中选中自动创建好的手势UI"))
        ///                //{
        ///                //    Selection.activeGameObject = MainAnima;
        ///                //}
        ///            }
        ///        }
        ///        start = false;
        ///    }
        ///}
    }
}
