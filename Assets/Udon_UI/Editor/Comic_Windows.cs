
using UdonSharp;
using UnityEngine;
using UnityEditor;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using UdonUI;

namespace UdonUI_Editor
{
    public class Comic_Windows : EditorWindow
    {
        #region 
        static string[] chinese = new string[]
        {
            "漫画",//0
            "漫画封面",//1
            "漫画尾页",//2
            "创建",//3
            "步长",//4
            "漫画名",//5
            "配置属性(创建漫画后点击)",//6
            "是否允许抓取",//7
            ""
        };
        static string[] english = new string[]
        {
            "漫画",//0
            "漫画封面",//1
            "漫画尾页",//2
            "创建",//3
            "步长",//4
            "漫画名",//5
            "配置属性(创建漫画后点击)",//6
            "是否允许抓取",//7
            ""
        };
        static string[] japanese = new string[]
        {
            "漫画",//0
            "漫画封面",//1
            "漫画尾页",//2
            "创建",//3
            "步长",//4
            "漫画名",//5
            "配置属性(创建漫画后点击)",//6
            "是否允许抓取",//7
            ""
        };

        static string[] porName;
        static int languageID = 0;
        #endregion

        #region File
        [SerializeField]
        public string Comic_name = null;
        [SerializeField]
        public Sprite[] AllComic = null;
        [SerializeField]
        public Sprite Comic_start = null, Comic_end = null;
        [SerializeField]
        public int Comic_Step = 1;
        [SerializeField]
        bool isPickUp = true;

        bool _isSliding = false;

        SerializedObject _mobj;
        SerializedProperty _Comic_Name;
        SerializedProperty _AllComic;
        SerializedProperty _Comic_start, _Comic_end;
        SerializedProperty _Comic_Step;
        SerializedProperty _isPickUp;

        GameObject _MainUdon = null;
        GameObject _MainComic = null;
        GameObject _MainComic_B1 = null;
        GameObject _MainComic_B2 = null;
        GameObject _MainComic_Slider = null;
        GameObject PGDN, PGUP;
        GameObject GetMainUdonUI {
            get
            {
                return UdonUI_Manager.Instance.mainUIobj;
            }
        }
        #endregion
        private void OnEnable()
        {
            GetLanguage();
            _mobj = new SerializedObject(this);
            _Comic_Name = _mobj.FindProperty("Comic_name");
            _AllComic = _mobj.FindProperty("AllComic");
            _Comic_start = _mobj.FindProperty("Comic_start");
            _Comic_end = _mobj.FindProperty("Comic_end");
            _Comic_Step = _mobj.FindProperty("Comic_Step");
            _isPickUp = _mobj.FindProperty("isPickUp");
        }

        private void OnGUI()
        {
            _mobj.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_Comic_Name, new GUIContent(porName[5]));
            EditorGUILayout.PropertyField(_Comic_start, new GUIContent(porName[1]));
            EditorGUILayout.PropertyField(_AllComic, new GUIContent(porName[0]));
            EditorGUILayout.PropertyField(_Comic_Step, new GUIContent(porName[4]));
            GUILayout.Label("步长：多少张图片作为一页，并自动判断转换为滑动界面");
            EditorGUILayout.PropertyField(_Comic_end, new GUIContent(porName[2]));
            EditorGUILayout.PropertyField(_isPickUp, new GUIContent(porName[7]));

            if (EditorGUI.EndChangeCheck())
            {
                _mobj.ApplyModifiedProperties();
                if (Comic_Step < 1) Comic_Step = 1;
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(porName[3])) CreateComic();
            if (GUILayout.Button(porName[6])) SetComic();
            GUILayout.EndHorizontal();
        }

        void CreateComic() {
            if (!GetMainUdonUI)
            {
                EditorUtility.DisplayDialog("警告", "请先创建UdonUI！！", "OK");
                return;
            }

            if (Comic_start != null && (AllComic != null ? AllComic.Length > 0 : false))
            {//有封面
                _isSliding = false;

                _MainComic = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "ComicRefer.prefab", typeof(GameObject)));
                _MainComic.transform.parent = GetMainUdonUI.transform;
                _MainComic_B1 = _MainComic.transform.GetChild(4).gameObject;
                _MainComic_B2 = _MainComic.transform.GetChild(5).gameObject;
                _MainComic_Slider = _MainComic.transform.GetChild(2).GetChild(3).gameObject;
                PGDN = _MainComic.transform.GetChild(6).GetChild(1).gameObject;
                PGUP = _MainComic.transform.GetChild(6).GetChild(2).gameObject;
                _MainComic.transform.position = SceneView.lastActiveSceneView.camera.transform.TransformPoint(0, 0, 0.2f);
                if (Comic_name != null && Comic_name != "") _MainComic.name = Comic_name;

                float porprotion = (float)Comic_start.texture.height / Comic_start.texture.width;//高宽比例
                Transform _MainComic_T = _MainComic.transform;
                Image image_start = _MainComic_T.GetChild(0).GetComponent<Image>();
                image_start.sprite = Comic_start;//封面
                RectTransform Rect_start = _MainComic_T.GetChild(0).GetComponent<RectTransform>();
                Vector2 MainComicWH = new Vector2(Rect_start.rect.size.x, Rect_start.rect.size.y * porprotion);//书本比例按封面走
                Rect_start.sizeDelta = MainComicWH;//修改封面比例
                _MainComic.GetComponent<RectTransform>().sizeDelta = MainComicWH;
                if (Comic_end == null)
                {
                    _MainComic_T.GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    RectTransform _rtf = _MainComic_T.GetChild(3).GetComponent<RectTransform>();
                    Sprite _sp = Comic_end;

                    float porprotion_e = (float)_sp.texture.height / _sp.texture.width;//高宽比例
                    Vector2 MainComicWH_e = new Vector2(_rtf.rect.size.x, _rtf.rect.size.y * porprotion_e);
                    _rtf.sizeDelta = MainComicWH_e;
                    Image _a = _rtf.GetComponent<Image>();
                    _a.sprite = _sp;
                }


                if (Comic_Step > 1)
                {//合成多页
                    int page = AllComic.Length / Comic_Step;//页数
                    int remainder = AllComic.Length - Comic_Step * page;
                    _isSliding = true;

                    Transform page_P = _MainComic_T.GetChild(2).GetChild(0).GetChild(0);
                    GameObject page_t = page_P.GetChild(0).gameObject;//页面
                    Transform page_refer = page_t.transform.GetChild(0);
                    page_refer.parent = page_t.transform.parent.parent;
                    page_t.name = "page";
                    for (int i = 0; i < page; i++)
                    {
                        GameObject nowt = Instantiate(page_t, page_t.transform.position, page_t.transform.rotation, page_P);
                        nowt.name = page_t.name + (i + 1).ToString();
                        for (int t = 0; t < Comic_Step; t++)
                        {//一页里面包含的图片 数量

                        }
                    }

                    if (remainder > 0)
                    {
                        GameObject nowt = Instantiate(page_t, page_t.transform.position, page_t.transform.rotation, page_P);
                        nowt.name = page_t.name + (page + 1).ToString();
                    }

                    DestroyImmediate(page_t);
                    DestroyImmediate(page_refer.gameObject);
                }
                else
                {
                    int wlength = -1;//最宽宽度
                    int hlength = -1;//最高高度
                    for (int i = 0; i < AllComic.Length; i++)
                    {
                        Sprite a = AllComic[i];
                        int h = a.texture.height;
                        int w = a.texture.width;
                        if (w > wlength) wlength = w;
                        if (h > hlength) hlength = h;
                    }

                    if (wlength < Comic_start.texture.width) wlength = Comic_start.texture.width;
                    int Comic_start_h = Comic_start.texture.height;
                    if ((float)hlength / Comic_start_h > 1.5f) _isSliding = true;//确定内容为可以滑动的漫画
                    //if (_isSliding)
                    //    Debug.LogWarning("滑动");

                    Transform Comic_PageRoot = _MainComic.transform.GetChild(1);
                    Transform Comic_Page = _MainComic.transform.GetChild(1).GetChild(0);
                    for(int i = 1; i< AllComic.Length; i++)
                    {
                        Transform nowPage = Instantiate(Comic_Page, Comic_Page.position, Comic_Page.rotation, Comic_PageRoot);
                        nowPage.name = "page" + Comic_PageRoot.childCount;
                        nowPage.GetComponent<Image>().sprite = AllComic[i];
                        Sprite nowImage = nowPage.GetComponent<Image>().sprite;
                        float proportion = (float)nowImage.texture.height / nowImage.texture.width;

                        RectTransform nowPage_ = nowPage.GetComponent<RectTransform>();
                        nowPage_.sizeDelta = new Vector2(nowPage_.rect.size.x, nowPage_.rect.size.y * proportion);
                    }

                    Comic_Page.GetComponent<Image>().sprite = AllComic[0];
                    Sprite nowImage1 = Comic_Page.GetComponent<Image>().sprite;
                    float proportion1 = (float)nowImage1.texture.height / nowImage1.texture.width;
                    RectTransform nowPage_1 = Comic_Page.GetComponent<RectTransform>();
                    nowPage_1.sizeDelta = new Vector2(nowPage_1.rect.size.x, nowPage_1.rect.size.y * proportion1);
                }

                GameObject[] _objects = new GameObject[3];
                _objects[0] = _MainComic_B1;
                _objects[1] = _MainComic_B2;
                _objects[2] = _MainComic_Slider;
                Selection.objects = _objects;
                UdonUI_Create.ADDButton();//添加按钮

                Undo.RegisterCreatedObjectUndo(_MainComic, "漫画创建");
                //EditorUtility.DisplayDialog("通知", "漫画 <" + _MainComic.name + "> 已经成功创建", "关闭", "我知道了");

                Selection.activeObject = _MainComic;
                SceneView.FrameLastActiveSceneView();

            }
            else
            {//无封面
                EditorUtility.DisplayDialog("警告！", "封面或者内容为空", "关闭", "我知道了");
            }


        }

        void SetComic()
        {
            if (!GetMainUdonUI)
            {
                EditorUtility.DisplayDialog("警告", "请先创建UdonUI！！", "OK");
                return;
            }

            if (!_MainComic)
            {
                EditorUtility.DisplayDialog("警告！", "请先创建漫画", "关闭", "我知道了");
                return;
            }


            ComicScript ComicScript1 = _MainComic.transform.GetChild(6).GetComponent<ComicScript>();
            ComicScript1.start = _MainComic.transform.GetChild(0).gameObject;
            ComicScript1.end = _MainComic.transform.GetChild(3).gameObject;
            ComicScript1.isEnd = Comic_end != null;
            ComicScript1.ComicMain = _isSliding ? _MainComic.transform.GetChild(2).GetChild(0).GetChild(0) : _MainComic.transform.GetChild(1);
            ComicScript1.isSlider = _isSliding;
            ComicScript1.sliderRoot = _MainComic.transform.GetChild(2).gameObject;

            Transform _MainComic_T = _MainComic.transform;
            _MainComic_T.GetChild(1).gameObject.SetActive(false);
            _MainComic_T.GetChild(2).gameObject.SetActive(false);
            _MainComic_T.GetChild(3).gameObject.SetActive(false);
            _MainComic_T.GetChild(6).GetChild(1).gameObject.SetActive(false);
            _MainComic_T.GetChild(6).GetChild(2).gameObject.SetActive(false);
            if (!isPickUp)
            {
                DestroyImmediate(_MainComic_T.GetComponent<Pickup_UdonUI>());
            }

            if (_isSliding)
            {
                //_MainComic_Slider;
                MainUI_Script _m = GetMainUdonUI.GetComponent<MainUI_Script>();
                GameObject[] _mui = _m.Button;
                int NowId = 0;

                for (int i = 0; i < _mui.Length; i++)
                {
                    if (_MainComic_Slider == _mui[i])
                    {
                        NowId = i;
                        break;
                    }
                }
                UdonUI_Create.AddWindowsMenu(NowId, _MainComic_T.GetChild(2).GetChild(0).GetChild(1).gameObject, _m, 0, 1);
                UdonUI_Create.AddWindowsMenu(NowId, _MainComic_T.GetChild(2).GetChild(0).GetChild(1).gameObject, _m, 2, 2);
                //UdonUI_Create.AddWindowsMenu(NowId, _MainComic_T.GetChild(6).GetChild(2).gameObject, _m);

                DestroyImmediate(_MainComic.transform.GetChild(1).gameObject);
            }
            else
            {
                MainUI_Script _m = GetMainUdonUI.GetComponent<MainUI_Script>();
                GameObject[] _mui = _m.Button;
                int NowId = 0;
                
                for(int i=0;i< _mui.Length; i++)
                {
                    if(_MainComic_B1 == _mui[i])
                    {
                        NowId = i;
                        break;
                    }
                }
                UdonUI_Create.AddWindowsMenu(NowId, _MainComic_T.GetChild(6).GetChild(2).gameObject, _m);

                for (int i = 0; i < _mui.Length; i++)
                {
                    if (_MainComic_B2 == _mui[i])
                    {
                        NowId = i;
                        break;
                    }
                }
                UdonUI_Create.AddWindowsMenu(NowId, _MainComic_T.GetChild(6).GetChild(1).gameObject, _m);

                DestroyImmediate(_MainComic.transform.GetChild(2).gameObject);
            }
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
    }
}
