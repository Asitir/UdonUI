using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UdonUI;

namespace UdonUI_Editor
{
    public class CharacterScripts_Window : EditorWindow
    {
        private UdonUI_Dashboards _Dashboards = null;
        private CharacterScripts _cs = null;
        private Transform nowCharacter;
        private Mask mainMask, scriptMask;
        private int NowPage;
        private string appName;
        private Sprite appImage;
        private Image appImageCom;
        private GameObject scriptApp;


        private GameObject mainPage, lockUI,bodyObj,homeObj, scriptObj;
        public UdonUI_Dashboards Dashboards
        {
            get { if (_Dashboards == null) _Dashboards = UdonUI_Manager.Instance.FindObjects<UdonUI_Dashboards>(); return _Dashboards; }
            private set { _Dashboards = value; }
        }
        public CharacterScripts cs 
        {
            get { if (_cs == null) _cs = UdonUI_Manager.Instance.FindObjects<CharacterScripts>(); return _cs; }
        }

        [MenuItem("Asitir_Tool/剧本杀插件/剧本查看器")]
        static void OpenWindow() {
            var _window = GetWindow<CharacterScripts_Window>("剧本查看器编辑");
        }

        private void OnEnable()
        {
            if (Dashboards)
            {
                OnValueInit();
            }

        }

        private void OnGUI()
        {
            DrawHead("剧本查看器");
            if (cs == null)
            {
                if (GUILayout.Button("创建剧本查看器"))
                {
                    CreateScript();
                }
            }
            else
            {
                if (Dashboards)
                {
                    if (!mainPage)
                    {
                        OnValueInit();
                    }
                }
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("选中查看器")) { UdonUI_Manager.Instance.PingObj(Dashboards); }
                if (GUILayout.Button("更新查看器(上传前更新)")) { ResetScript(); }
                if (GUILayout.Button("仅创建MG")) { OnCreateMG(); }
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                DrawBody();
            }

        }

        private void OnValueInit() {
            //Debug.Log("有初始化");

            mainPage = _Dashboards.transform.GetChild(1).GetChild(1).gameObject;
            lockUI = _Dashboards.transform.GetChild(1).GetChild(2).gameObject;
            bodyObj = mainPage.transform.GetChild(1).gameObject;
            homeObj = mainPage.transform.GetChild(0).gameObject;

            mainMask = mainPage.GetComponent<Mask>();
            scriptMask = cs.transform.parent.GetComponent<Mask>();

            scriptApp = mainPage.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).gameObject;
            appName = scriptApp.name;
            appImageCom = scriptApp.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            appImage = appImageCom.sprite;
            //scriptObj = bodyObj.transform.GetChild(0).gameObject;
        }

        private void SetMask(bool _isEnble)
        {
            mainMask.enabled = _isEnble;
            scriptMask.enabled = _isEnble;
        }

        private void DrawHead(string _headName) {
            GUILayout.Space(20);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(_headName);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Space(20);
        }
        private void DrawBody() {
            GUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            appImageCom.sprite = (Sprite)EditorGUILayout.ObjectField("", appImageCom.sprite, typeof(Sprite), true, GUILayout.Width(65));
            if (EditorGUI.EndChangeCheck())
            {//改图标之后
                if(Selection.activeObject != appImageCom)
                    Selection.activeObject = appImageCom;
                else
                    Selection.activeObject = appImageCom.transform.parent;
            }
            GUILayout.BeginVertical();
            GUILayout.Label("剧本APP应用名称:");
            EditorGUI.BeginChangeCheck();
            appName = EditorGUILayout.TextField(scriptApp.name);
            if (EditorGUI.EndChangeCheck())
            {//改名之后
                scriptApp.transform.GetChild(2).GetComponent<Text>().text = appName;
                scriptApp.name = appName;
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("在场景中选中剧本APP"))
            {
                SetMask(true);
                lockUI.SetActive(false);
                mainPage.SetActive(true);
                bodyObj.SetActive(false);
                homeObj.SetActive(true);
                scriptApp.SetActive(true);
                UdonUI_Manager.Instance.PingObj(scriptApp);
            }
            if (GUILayout.Button("显示"))
            {
                scriptApp.SetActive(true);
            }
            if (GUILayout.Button("隐藏"))
            {
                scriptApp.SetActive(false);
            }
            GUILayout.EndHorizontal();
            //appName = EditorGUILayout.TextField(scriptApp.name);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            //EditorGUILayout.ObjectField(null, typeof(Texture), true);

            for (int i = 0; i < cs.transform.childCount; i++)
            {
                Transform _nowCS = cs.transform.GetChild(i);
                if (GUILayout.Button($"打开 [{_nowCS.name}] 的剧本"))
                {
                    SetMask(false);
                    lockUI.SetActive(false);
                    mainPage.SetActive(true);
                    bodyObj.SetActive(true);
                    homeObj.SetActive(false);
                    bodyObj.transform.GetChild(0).gameObject.SetActive(true);

                    _cs.transform.parent.GetComponent<Mask>().enabled = false;
                    cs.PGUP.SetActive(true);
                    cs.PGDN.SetActive(true);
                    Dashboards.gameObject.SetActive(true);
                    for (int t = 0; t < cs.transform.childCount; t++)
                        cs.transform.GetChild(t).gameObject.SetActive(false);
                    nowCharacter = _nowCS;
                    nowCharacter.gameObject.SetActive(true);
                    UdonUI_Manager.Instance.PingObj(_nowCS);
                    NowPage = 0;
                    OnPage(NowPage,false);
                }
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("上一章"))
            {
                if (nowCharacter)
                {
                    NowPage--;
                    NowPage = Mathf.Max(0, NowPage);
                    OnPage(NowPage);
                }
            }
            if (GUILayout.Button("下一章"))
            {
                if (nowCharacter)
                {
                    NowPage++;
                    NowPage = Mathf.Min(nowCharacter.childCount - 1, NowPage);
                    OnPage(NowPage);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("查看锁屏界面"))
            {
                SetMask(true);
                lockUI.SetActive(true);
                mainPage.SetActive(true);
                bodyObj.SetActive(false);
                homeObj.SetActive(true);
                UdonUI_Manager.Instance.PingObj(lockUI);
                //UdonUI_Manager.Instance.PingObj(lockUI.transform.GetChild(0).GetChild(0).GetChild(0));
            }
            if (GUILayout.Button("查看主界面"))
            {
                SetMask(true);
                lockUI.SetActive(false);
                mainPage.SetActive(true);
                bodyObj.SetActive(false);
                homeObj.SetActive(true);
                UdonUI_Manager.Instance.PingObj(homeObj);
                //UdonUI_Manager.Instance.PingObj(lockUI.transform.GetChild(0).GetChild(0).GetChild(0));
            }
            //lockUI.gameObject.SetActive(GUILayout.Toggle(lockUI.gameObject.activeSelf,"启用锁屏界面"));
            GUILayout.EndHorizontal();
            if (GUILayout.Button("修正空格换行"))
            {
                SetText();
            }
        }
        private void OnPage(int _page,bool pin = true)
        {
            if (nowCharacter)
            {
                for (int i = 0; i < nowCharacter.childCount; i++)
                {
                    nowCharacter.GetChild(i).gameObject.SetActive(false);
                }
                _page = Mathf.Clamp(_page, 0, nowCharacter.childCount - 1);
                nowCharacter.GetChild(_page).gameObject.SetActive(true);
                if(pin) UdonUI_Manager.Instance.PingObj(nowCharacter.GetChild(_page));
            }
        }
        private void CreateScript()
        {
            if (!UdonUI_Manager.Instance.mgConsole)
            {
                UnityEditor.EditorUtility.DisplayDialog("警告！", "未创建MGSystem环境", "我知道了");
                return;
            }
            GameObject _CSObj = Instantiate(UdonUI_Manager.Instance.LoadAssetAtPath<GameObject>(UdonUI_Manager.Instance.Path_SampleObj + "CharacterScript_.prefab"), UdonUI_Manager.Instance.mainUIobj.transform);
            _CSObj.name = "剧本查看器";

            _Dashboards = _CSObj.GetComponent<UdonUI_Dashboards>();
            mainPage = _Dashboards.transform.GetChild(1).GetChild(1).gameObject;
            lockUI = _Dashboards.transform.GetChild(1).GetChild(2).gameObject;

            OnValueInit();

            //设定通常按钮
            UdonUI_Manager.Instance.AddUdonUIButton(cs.PGUP);
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 32, cs.gameObject, "OnPGUP");
            UdonUI_Manager.Instance.AddUdonUIButton(cs.PGDN);
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 32, cs.gameObject, "OnPGDN");
            UdonUI_Manager.Instance.AddUdonUIButton(cs.transform.parent.parent.GetChild(2).gameObject);//滑动
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, cs.transform.parent.GetChild(1).gameObject, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 2, 2, cs.transform.parent.GetChild(1).gameObject, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 3, 1, cs.transform.parent.GetChild(3).gameObject, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 5, 2, cs.transform.parent.GetChild(3).gameObject, "");
            GameObject _dragF = lockUI.transform.GetChild(0).GetChild(2).gameObject;//滑屏解锁结束
            UdonUI_Manager.Instance.AddUdonUIButton(_dragF);
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 3, 1, mainPage, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 3, 2, lockUI, "");
            UdonUI_Manager.Instance.AddUdonUIButton(lockUI.transform.GetChild(0).GetChild(3).gameObject);//滑屏解锁
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, lockUI.transform.GetChild(0).GetChild(0).GetChild(1).gameObject, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, _dragF, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 2, 2, lockUI.transform.GetChild(0).GetChild(0).GetChild(1).gameObject, "");
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 2, 2, _dragF, "");

            //添加App
            Transform _appParent = scriptApp.transform.parent;
            Transform _appBody = mainPage.transform.GetChild(1);
            for (int i = 0; i < _appParent.childCount; i++)
            {
                Transform _nowApp = _appParent.GetChild(i);
                UdonUI_Manager.Instance.AddUdonUIButton(_nowApp.gameObject);//App
                if (i == 5)
                {
                    UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, homeObj, "");
                }
                else
                {
                    UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 2, mainPage.transform.parent.GetChild(2).gameObject, "");

                    switch (i)
                    {
                        case 0:
                            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, _appBody.GetChild(0).gameObject, "");
                            break;
                        case 1:
                            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, _appBody.GetChild(3).gameObject, "");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, _appBody.GetChild(2).gameObject, "");
                            break;
                    }
                }
            }
            for (int i = 0; i < _appBody.childCount; i++)
            {
                Transform _nowBody = _appBody.GetChild(i);
                if (i == 0)
                    UdonUI_Manager.Instance.AddUdonUIButton(_nowBody.GetChild(6).gameObject);//App
                else
                    UdonUI_Manager.Instance.AddUdonUIButton(_nowBody.GetChild(0).gameObject);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, homeObj.gameObject, "");
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 2, _appBody.gameObject, "");
            }


            //设定MG按钮
            UdonUI_Manager.Instance.AddUdonUIButton(Dashboards.transform.GetChild(3).GetChild(0).gameObject);
            UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 32, cs.gameObject, "EnterNestScript");
            UdonUI_CreateMG.AddToGMButton(Dashboards.transform.GetChild(3).GetChild(0).gameObject);

            //UdonUI_Manager.Instance.AddUdonUIButton(Dashboards.transform.GetChild(3).GetChild(1).gameObject);
            //UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 32, cs.gameObject, "OnOpenScript");
            //UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, Dashboards.gameObject, "");
            //UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 1, 2, Dashboards.gameObject, "");


            //初始化参数
            Dashboards.mainUI = UdonUI_Manager.Instance.mainUI;
            cs.mainUI = UdonUI_Manager.Instance.mainUI;

            //ResetScript();
            UdonUI_Manager.Instance.PingObj(_CSObj);
        }
        private void ResetScript()
        {
            SetMask(true);
            //_cs.transform.parent.GetComponent<Mask>().enabled = true;
            //Dashboards.gameObject.SetActive(false);
            for (int t = 0; t < cs.transform.childCount; t++)
            {
                Transform _nowTarget = cs.transform.GetChild(t);
                _nowTarget.gameObject.SetActive(false);
                for (int i = 0; i < _nowTarget.childCount; i++)
                {
                    _nowTarget.GetChild(i).transform.localPosition = Vector3.zero;
                    _nowTarget.GetChild(i).gameObject.SetActive(false);
                }
            }
            cs.PGUP.SetActive(false);
            cs.PGDN.SetActive(false);
            lockUI.SetActive(true);
            mainPage.SetActive(true);
            bodyObj.SetActive(false);
            homeObj.SetActive(true);

            scriptApp.SetActive(false);
            Transform _appPage = mainPage.transform.GetChild(1);
            for (int i = 0; i < _appPage.childCount; i++)
            {
                _appPage.gameObject.SetActive(false);
            }
            UdonUI_Manager.Instance.PingObj(lockUI);
            //OnCreateMG();
        }
        private void OnCreateMG() {
            //先清除已有的GM按钮
            //List<GameObject> _allButton = new List<GameObject>();
            //_allButton.AddRange(UdonUI_Manager.Instance.mainUI.Button);
            Transform _mgMain = Dashboards.transform.GetChild(3);

            int _allTarget = _mgMain.childCount;
            if (_allTarget > 1)
            {
                for (int i = 1; i < _allTarget; i++)
                {
                    GameObject _nowObj = _mgMain.GetChild(1).gameObject;
                    DestroyImmediate(_nowObj);
                }
                UdonUI_Manager.Instance.ChackUdonUIButton_Update(false);
            }

            //设置玩家名牌
            Transform _NameMain = Dashboards.transform.GetChild(4).GetChild(1);
            _allTarget = _NameMain.childCount;
            int _proportion = _allTarget - cs.transform.childCount;
            if (_proportion > 0)
            {
                for (int i = 0; i < _proportion; i++)
                {
                    if (_NameMain.childCount > 1)
                        DestroyImmediate(_NameMain.GetChild(_NameMain.childCount - 1).gameObject);
                }
            }
            else if (_proportion < 0)
            {
                _proportion *= -1;
                for (int i = 0; i < _proportion; i++)
                {
                    Instantiate(_NameMain.GetChild(0).gameObject, _NameMain);
                }
            }
            List<CharNameDisplay_s> _allNameList = new List<CharNameDisplay_s>();
            for (int i = 0; i < _NameMain.childCount; i++)
            {
                Transform _target = _NameMain.GetChild(i);
                _target.name = cs.transform.GetChild(i).name;
                _target.GetChild(0).GetChild(0).GetChild(1).name = cs.transform.GetChild(i).name;
                _allNameList.Add(_target.GetComponent<CharNameDisplay_s>());
            }
            CharNameDisplay _Cnd = _NameMain.parent.GetChild(0).GetComponent<CharNameDisplay>();
            _Cnd.allPlayer = _allNameList.ToArray();
            _Cnd.mainUI = UdonUI_Manager.Instance.mainUI;


            //return;
            //毁灭后的新生
            GameObject _referObj = _mgMain.GetChild(0).gameObject;
            GameObject _messageObj = mainPage.transform.GetChild(2).gameObject;
            for (int i = 0; i < cs.transform.childCount; i++)
            {
                GameObject _newCharacter = Instantiate(_referObj, _mgMain);
                _newCharacter.name = "轻渡_" + cs.transform.GetChild(i).name;
                UdonUI_Manager.Instance.AddUdonUIButton(_newCharacter);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 32, cs.gameObject, "OnOpenScript@" + i);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, Dashboards.gameObject, "");
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 1, 2, Dashboards.gameObject, "");
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 32, _messageObj, $"Send@0_{_NameMain.GetChild(i).name}_      已安装(济世游医)" + i);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 0, 1, _NameMain.GetChild(i).gameObject, "SetOn");
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, 1, 2, _NameMain.GetChild(i).gameObject, "SetOff");
                UdonUI_CreateMG.AddToGMButton(_newCharacter);//设定MG按钮
            }

        }

        private string nonBreakingSpace = "\u00A0";
        private void SetText() { 
            if(Selection.activeGameObject.TryGetComponent<Text>(out Text _target))
            {
                //if (_target.text.Contains("\n"))
                //{
                //    Debug.Log("检测到换行字符");
                //}
                //else
                //{
                //    Debug.Log($"字符：{_target.text}");
                //}
                string[] _t = _target.text.Split('\n');
                string outString = "";
                for (int i = 0; i < _t.Length; i++)
                    outString += (textHand() + _t[i] + "\n");
                _target.text = outString;
                EditorUtility.SetDirty(_target);
            }
        }
        private string textHand() 
        {
            string outString = "";
            for (int i = 0; i < 8; i++)
                outString += nonBreakingSpace;
            return outString;
        }

    }
}
