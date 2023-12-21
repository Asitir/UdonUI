using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UdonUI;
using UnityEngine.UI;

namespace UdonUI_Editor
{
    public class UdonUI_CreateMG : EditorWindow
    {
        #region Value
        static UdonUI_CreateMG mainWindows = null;

        private MgConsole mMgConsole { get { return UdonUI_Manager.Instance.mgConsole; } set { UdonUI_Manager.Instance.mgConsole = value; } }
        private Vector2 bodyRange, bodyRangeB;
        private int selectID = 0;
        #endregion

        #region Behaviour
        [MenuItem("Asitir_Tool/CreateMGSystem")]
        public static void OpenWindow()
        {
            mainWindows = (UdonUI_CreateMG)GetWindow(typeof(UdonUI_CreateMG), true);
        }

        private void OnEnable()
        {
            if (mMgConsole!=null)
            {
                mMgConsole.displayContent = mMgConsole.transform.GetChild(1).GetChild(1).GetComponent<Text>();
                mMgConsole.mainUI = UdonUI_Manager.Instance.mainUI;
                mMgConsole.chatSystem = UdonUI_Manager.Instance.udonChat;
            }
        }
        private void OnGUI()
        {
            if (DrawHead()) return;

            DrawBody();
        }

        #endregion

        #region Events
        bool DrawHead()
        {
            bool isNode = false;
            string headText = "管理者系统设定";
            if (!UdonUI_Manager.Instance.mainUI)
            {
                isNode = true;
                headText = "未检测到UdonUI必须环境，请先搭建环境";
            }
            if (!UdonUI_Manager.Instance.udonChat)
            {
                if (!isNode)
                {
                    isNode = true;
                    headText = "未检测到UdonUI的对话系统，请先搭建对话系统";
                }
            }
            if (!UdonUI_Manager.Instance.saoUI)
            {
                if (!isNode)
                {
                    isNode = true;
                    headText = "未检测到手势菜单，请先搭建手势菜单";
                }
            }


            GUILayout.Space(20);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(headText);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Space(20);
            return isNode;
        }

        List<string> actionName = new List<string>();
        List<string> managers = new List<string>();
        List<int> actionID = new List<int>();
        void DrawBody() {
            if (mMgConsole == null)
            {//生成MG系统
                Transform _mgParent = UdonUI_Manager.Instance.saoUI.MainGameobject[0].transform.GetChild(0).GetChild(5);
                Transform _MgObj = Instantiate(UdonUI_Manager.Instance.LoadAssetAtPath<GameObject>(UdonUI_Manager.Instance.Path_SamplePrefad + "MGConsole.prefab")).transform;
                _MgObj.name = "MGConsole";
                Undo.RegisterCreatedObjectUndo(_MgObj, "创建管理面板");
                mMgConsole = _MgObj.GetComponent<MgConsole>();
                _MgObj.SetParent(_mgParent);
                _MgObj.localRotation = Quaternion.Euler(0, 0, 0);
                _MgObj.localPosition = new Vector3(0.092f, -0.045f, 0);
                _MgObj.localScale = new Vector3(0.625f, 0.625f, 1);

                mMgConsole.displayContent = _MgObj.GetChild(1).GetChild(1).GetComponent<Text>();
                mMgConsole.mainUI = UdonUI_Manager.Instance.mainUI;
                mMgConsole.chatSystem = UdonUI_Manager.Instance.udonChat;

                Transform _activeObj;

                //设定第一组按钮  (管理)
                _activeObj = mMgConsole.transform.GetChild(2).GetChild(0);
                for (int i = 0; i < _activeObj.childCount; i++)
                {
                    UdonUI_Manager.Instance.AddUdonUIButton(_activeObj.GetChild(i).gameObject);
                    string ActionEvent = "";
                    switch (i)
                    {
                        case 0:
                            ActionEvent = "ShutUp";
                            break;
                        case 1:
                            ActionEvent = "TeleportTo";
                            break;
                        case 2:
                            ActionEvent = "OnAction";
                            break;
                        case 3:
                            ActionEvent = "SetMG";
                            break;
                        case 4:
                            ActionEvent = "OnPa";
                            break;
                    }
                    UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 2, 32, mMgConsole.gameObject, ActionEvent);
                }

                _activeObj = mMgConsole.transform.GetChild(3).GetChild(0);
                for (int i = 0; i < _activeObj.childCount; i++)
                {
                    UdonUI_Manager.Instance.AddUdonUIButton(_activeObj.GetChild(i).gameObject);
                    UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 2, 32, mMgConsole.gameObject, "SendActionID");
                }

                UdonUI_Manager.Instance.AddUdonUIButton(mMgConsole.transform.GetChild(3).GetChild(1).gameObject);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 2, 32, mMgConsole.gameObject, "PageUp");
                UdonUI_Manager.Instance.AddUdonUIButton(mMgConsole.transform.GetChild(3).GetChild(2).gameObject);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 2, 32, mMgConsole.gameObject, "PageNext");
                UdonUI_Manager.Instance.AddUdonUIButton(mMgConsole.transform.GetChild(4).gameObject, EButtonType.Drag);
                UdonUI_Manager.Instance.AddUdonUIButton(mMgConsole.transform.GetChild(5).gameObject);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 2, 32, mMgConsole.gameObject, "ResetCommand");
            }
            else
            {
                if (GUILayout.Button("选中场景种的管理面板"))
                {
                    Selection.activeObject = mMgConsole.gameObject;
                    EditorGUIUtility.PingObject(mMgConsole.gameObject);
                }
                GUILayout.Space(20);

                GUILayout.BeginHorizontal();
                Color _color = GUI.color;
                if(selectID==0) GUI.color = Color.red;
                if (GUILayout.Button("Udon按钮列表"))
                {
                    selectID = 0;
                }
                GUI.color = _color;
                if (selectID == 1) GUI.color = Color.red;
                if (GUILayout.Button("按钮事件名称"))
                {
                    selectID = 1;
                }
                GUI.color = _color;
                if (selectID == 2) GUI.color = Color.red;
                if (GUILayout.Button("管理者名单"))
                {
                    selectID = 2;
                }
                GUI.color = _color;
                GUILayout.EndHorizontal();
                if (selectID == 0)
                    DrawBodyEnumA();
                else if (selectID == 1)
                    DrawBodyEnumC();
                else if (selectID == 2)
                    DrawBodyEnumB();
            }
        }

        private void DrawBodyEnumInitA()
        {
            actionName.Clear();
            actionID.Clear();
            bool isd = false;
            for (int i = 0; i < mMgConsole.actionID.Length; i++)
            {
                int _id = mMgConsole.actionID[i];
                if (_id < UdonUI_Manager.Instance.mainUI.Button.Length)
                {
                    actionName.Add(UdonUI_Manager.Instance.mainUI.Button[_id].name);
                    actionID.Add(mMgConsole.actionID[i]);
                }
                else
                {
                    isd = true;
                }
            }
            if (isd)
            {
                mMgConsole.actionName = actionName.ToArray();
                mMgConsole.actionID = actionID.ToArray();
                Undo.RecordObject(mMgConsole, "增减GM按钮");
                EditorUtility.SetDirty(mMgConsole);
            }
        }
        private void DrawBodyEnumA() {
            DrawBodyEnumInitA();

            GUILayout.Label("设定管理员可操作的 UdonUI 按钮");
            Color _color = GUI.color;
            GUI.color = Color.green;
            if (GUILayout.Button("拾取当前选中的UdonUI按钮"))
            {
                //AddToGMButton(Selection.activeObject);
                if (Selection.activeObject is GameObject _obj)
                {
                    int _UdonUIID = -1;
                    for (int i = 0; i < UdonUI_Manager.Instance.mainUI.Button.Length; i++)
                    {
                        if (_obj == UdonUI_Manager.Instance.mainUI.Button[i])
                        {
                            _UdonUIID = i;
                            if (actionID.Contains(i))
                            {
                                EditorUtility.DisplayDialog("警告", "已添加过当前按钮！！", "我知道了");
                                break;
                            }
                            actionName.Add(_obj.name);
                            actionID.Add(i);

                            mMgConsole.actionName = actionName.ToArray();
                            mMgConsole.actionID = actionID.ToArray();
                            Undo.RecordObject(mMgConsole, "增减GM按钮");
                            EditorUtility.SetDirty(mMgConsole);
                            break;
                        }
                    }
                    if (_UdonUIID < 0)
                        EditorUtility.DisplayDialog("警告", "当前选中对象并非 UdonUI 按钮！！", "我知道了");
                }
            }
            GUI.color = _color;

            bodyRange = EditorGUILayout.BeginScrollView(bodyRange);
            for (int i = 0; i < actionID.Count; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(actionName[i]))
                {
                    GameObject _selectObj = UdonUI_Manager.Instance.mainUI.Button[actionID[i]];
                    Selection.activeObject = _selectObj;
                    EditorGUIUtility.PingObject(_selectObj);

                    mMgConsole.actionName = actionName.ToArray();
                    mMgConsole.actionID = actionID.ToArray();
                    Undo.RecordObject(mMgConsole, "更新命名");
                    EditorUtility.SetDirty(mMgConsole);
                }
                Color _mcolor = GUI.color;
                GUI.color = Color.red;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    actionName.RemoveAt(i);
                    actionID.RemoveAt(i);

                    mMgConsole.actionName = actionName.ToArray();
                    mMgConsole.actionID = actionID.ToArray();
                    Undo.RecordObject(mMgConsole, "增减GM按钮");
                    EditorUtility.SetDirty(mMgConsole);
                    i--;
                }
                GUI.color = _mcolor;
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

        }
        private void DrawBodyEnumInitB()
        {
            managers.Clear();
            foreach (var item in UdonUI_Manager.Instance.udonChat.Managers)
                managers.Add(item);
        }
        private void DrawBodyEnumB()
        {
            DrawBodyEnumInitB();

            GUILayout.Label("设定管理员名单(VRChatID)");
            UdonUI_Manager.Instance.udonChat.MasterIsMG = EditorGUILayout.Toggle("设定房主为管理员:", UdonUI_Manager.Instance.udonChat.MasterIsMG);

            Color _color = GUI.color;
            GUI.color = Color.green;
            if (GUILayout.Button("添加管理员"))
            {
                managers.Add("");
                UdonUI_Manager.Instance.udonChat.Managers = managers.ToArray();
                Undo.RecordObject(UdonUI_Manager.Instance.udonChat, "增减GM按钮");
                EditorUtility.SetDirty(UdonUI_Manager.Instance.udonChat);
            }
            GUI.color = _color;

            bodyRangeB = EditorGUILayout.BeginScrollView(bodyRangeB);
            for (int i = 0; i < managers.Count; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                managers[i] = EditorGUILayout.TextField(managers[i]);
                if (EditorGUI.EndChangeCheck()) 
                {
                    UdonUI_Manager.Instance.udonChat.Managers[i] = managers[i];
                    Undo.RecordObject(UdonUI_Manager.Instance.udonChat, "增减GM按钮");
                    EditorUtility.SetDirty(UdonUI_Manager.Instance.udonChat);
                }
                Color _mcolor = GUI.color;
                GUI.color = Color.red;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    managers.RemoveAt(i);

                    UdonUI_Manager.Instance.udonChat.Managers = managers.ToArray();
                    Undo.RecordObject(UdonUI_Manager.Instance.udonChat, "增减GM按钮");
                    EditorUtility.SetDirty(UdonUI_Manager.Instance.udonChat);
                    i--;
                }
                GUI.color = _mcolor;
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
        private void DrawBodyEnumC() 
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("按下     :", GUILayout.Width(60));
            mMgConsole.ActionEvenName[0] = EditorGUILayout.TextField(mMgConsole.ActionEvenName[0]);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("长按     :", GUILayout.Width(60));
            mMgConsole.ActionEvenName[1] = EditorGUILayout.TextField(mMgConsole.ActionEvenName[1]);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("抬起     :", GUILayout.Width(60));
            mMgConsole.ActionEvenName[2] = EditorGUILayout.TextField(mMgConsole.ActionEvenName[2]);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("进入悬停 :", GUILayout.Width(60));
            mMgConsole.ActionEvenName[3] = EditorGUILayout.TextField(mMgConsole.ActionEvenName[3]);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("持续悬停 :", GUILayout.Width(60));
            mMgConsole.ActionEvenName[4] = EditorGUILayout.TextField(mMgConsole.ActionEvenName[4]);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("离开悬停 :", GUILayout.Width(60));
            mMgConsole.ActionEvenName[5] = EditorGUILayout.TextField(mMgConsole.ActionEvenName[5]);
            GUILayout.EndHorizontal();

        }

        public static void AddToGMButton(UnityEngine.Object _target)
        {
            //Debug.Log("AA");
            if (_target is GameObject _obj)
            {
                MgConsole _mg = UdonUI_Manager.Instance.mgConsole;

                List<string> _actionName = new List<string>();
                List<int> _actionID = new List<int>();
                if (_mg.actionID != null)
                {
                    foreach (var item in _mg.actionName)
                    {
                        _actionName.Add(item);
                    }
                }
                if (_mg.actionID != null)
                {
                    foreach (var item in _mg.actionID)
                    {
                        _actionID.Add(item);
                    }
                }

                int _UdonUIID = -1;
                for (int i = 0; i < UdonUI_Manager.Instance.mainUI.Button.Length; i++)
                {
                    if (_obj == UdonUI_Manager.Instance.mainUI.Button[i])
                    {
                        //Debug.Log("BB");
                        _UdonUIID = i;
                        if (_actionID.Contains(i))
                        {
                            //EditorUtility.DisplayDialog("警告", "已添加过当前按钮！！", "我知道了");
                            break;
                        }
                        _actionName.Add(_obj.name);
                        _actionID.Add(i);

                        _mg.actionName = _actionName.ToArray();
                        _mg.actionID = _actionID.ToArray();
                        Undo.RecordObject(_mg, "增减GM按钮");
                        EditorUtility.SetDirty(_mg);
                        break;
                    }
                }
                //if (_UdonUIID < 0)
                //    EditorUtility.DisplayDialog("警告", "当前选中对象并非 UdonUI 按钮！！", "我知道了");
            }

        }
        #endregion
    }
}
