using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Udon;
using UnityEditorInternal;
using UnityEngine.UI;
using UdonUI;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace UdonUI_Editor
{
    public class MainUI_Editor : EditorWindow
    {
        #region 变量申请
        static EditorWindow MainWindow;
        //int Editorupdate = 30;
        int playf = 0;
        #region 语言
        static string[] chinese = new string[] {
        "请在场景面板空白处右键点击'UdonUI/Button'创建UdonUI环境",//0
        "当前版本",//1
        "当前版本为",//2
        "作者",//3
        "作者主页",//4
        "UdonUI主菜单",//5
        "打开UdonUI属性设置菜单",//6
        "UdonUI属性",//7
        "编辑在事件中会用到的音效素材",//8
        "音频修改",//9
        "按钮列表",//10
        "碰撞触发列表",//11
        "每次上传前建议使用检查按钮排查问题,避免致命错误",//12
        "是否启用引导图标",//13
        "开关图标",//14
        "引导图标",//15
        "设置图标",//16
        "请设置你的引导图标，最好是稍微有厚度的3d模型保持美观",//17
        "引导图标类似鼠标，会显示你落点的位置，PC和VR均可用",//18
        "创建UdonUI辅助属性",//19
        "注意",//20
        "创建",//21
        "取消",//22
        "我知道了",//23
        "关闭",//24
        "完成",//25
        "警告",//26
        "返回",//27
        "在需要同步按钮时必须创建UdonUI辅助,请勿改动即将创建的【UdonUI_AGI】任何属性（包含父子级）,点击取消则取消创建，之后希望关闭该功能可以直接删掉【UdonUI_AGI】及子级",//28
        "按键属性修改",//29
        "如果某个按钮已经设置了【同步】，那就必须创建UdonUI辅助属性，否则可能无法运行",//30
        "房间人数(和VRC填写一致)",//31
        "已在场景中创建【UdonUI_AGI】，请勿对其做任何操作",//32
        "如果是想关闭同步功能则可以直接删掉整个【UdonUI_AGI】",//33
        "打开手势菜单属性",//34
        "UdonUI手势菜单",//35
        "创建手势菜单",//36
        "打开Head属性（未完成）",//37
        "UdonUI头部菜单",//38
        "创建Head菜单（未完成）",//39
        "打开Hand菜单属性（未完成）",//40
        "UdonUI手部菜单",//41
        "创建Hand菜单（未完成）",//42
        "按键属性修改",//43
        "创建追加辅助对象（重力操作）",//44
        "你已经在场景中创建重力操作对象，请勿重复创建",//45
        "创建成功，请注意以下事项",//46
        "1、请将受重力操作影响的物体放置PickUp-Obj下",//47
        "2、放置在PickUp-Obj下的物体在程序运行后不能动态删除",//48
        "3、你可以通过开关MainScript物体来决定是否启用重力操作",//49
        "4、需要手动给物体添加【Rgidbody】组件",//50
        "检查UdonUI是否存在致命错误",//51
        "以下UdonUI存在异常!!!",//52
        "请打开属性逐个检查以下按钮属性里面是否存在动画机或者对象为空",//53
        "双击里面的对象 可以自动在场景中选中它   以便于修正异常按钮属性",//54
        "检查完成",//55
        "发现",//56
        "个致命异常按钮",//57
        "原因",//58
        "可能是按钮中设置了动画机却没赋予动画机",//59
        "可能是按钮中设置了对象状态却没赋予对象",//60
        "异常按钮对象",//61
        "异常触发器对象",//62
        "检查完成",//63
        "并无发现异常",//64
        "请先创建UdonUI环境！！！",//65---------------
        "",//66
        "创建追加辅助对象（攀爬）",//67
        "音频",//68
        "我所有的UdonUI按钮",//69
        "我所有的触发器",//70
        "你已经在场景中创建",//71
        "对象，请勿重复创建",//72
        "1、请在‘Climbing’对象内设置可攀爬的层级",//73
        "2、可通过开启 ‘Climbing’ 下的子级 ‘forefinger climbing?’ 来让用户使用手柄食指那里的按键攀爬",//74
        "创建追加辅助对象（第三人称控制器）",//75
        "1、你可以在‘Third’对象内设置打开第三人称控制器相关的按键",//76
        "创建追加辅助对象（聊天系统）",//77
        "进入VRC后直接回车即可发送信息，'TAB'键开关聊天窗口，上下方向键快捷输入信息发送历史，鼠标滚轮查阅聊天历史",//78
        "未检测到UdonUId 手势菜单,请先手动创建下",//79
        "未检测到UdonUI辅助对象或手势菜单，请手动创建手势菜单",//80
        "发现组件丢失错误",//81
        "已发现有",//82
        "个按钮出现重要组件丢失",//83
        "但是已被自动添加并修复",//84
        "丢失的组件为<MeshRender>",//85
        "创建漫画",//86
        "漫画创作",//87
        "创建玩家脚步声",//88
        "创建传送门",//89
        "创建黑白名单",//90
        "创建乘坐对象",//91
        "创建玩家ID遮罩",//92
        "创建或打开字幕系统",//93
        ""
    };
        static string[] japanese = new string[] {
        "シーンの空白部分で「UdonUI/Button」を右クリックしてUdonUI環境を作成してください",//0
        "現在バージョン",//1
        "現在のバージョンは",//2
        "作者",//3
        "作者ホームページ",//4
        "UdonUIメインメニュー",//5
        "UdonUIプロパティ設定メニューを開きます",//6
        "UdonUIプロパティ",//7
        "イベントで使用するサウンド素材を編集します",//8
        "音声修正",//9
        "ボタンリスト",//10
        "衝突トリガリスト",//11
        "毎回アップロードする前にチェックボタンを使用して問題を排除し、致命的なエラーを避けることをお勧めします",//12
        "ガイドアイコンを有効にしますか",//13
        "スイッチアイコン",//14
        "ガイドアイコン",//15
        "アイコンを設定",//16
        "ガイドアイコンを設定してください、少し厚さのある3 dモデルの方がおすすめです",//17
        "ガイドアイコンはマウスのようなもので、落下点の位置が表示され、PCとVRの両方が使用できます。",//18
        "UdonUI補助属性を作成します。",//19
        "注意",//20
        "作成",//21
        "キャンセル",//22
        "わかりました",//23
        "閉じる",//24
        "完了",//25
        "警告",//26
        "戻る",//27
        "同期ボタンが必要な場合は,UdonUI補助を作成する必要があります。これから作成するUdonUI_AGI属性（親子レベルを含む）を変更しないでください。キャンセルをクリックすると、作成がキャンセルされます。その後、この機能をオフにしたい場合は、直接削除できます。UdonUI_AGIと親子レベル。",//28
        "キーのプロパティ",//29
        "ボタンが設定されている場合は、UdonUI補助プロパティを作成する必要があります、そうしないと、実行できない可能性があります。",//30
        "部屋の人数（VRC記入と一致）",//31
        "UdonUI_AGIはシーンに作成されました。いじらないでください。",//32
        "同期機能をオフにしたい場合は、UdonUI_AGI全体を直接削除できます。",//33
        "ジェスチャーメニューのプロパティを開く",//34
        "UdonUIジェスチャーメニュー",//35
        "ジェスチャーメニューを作成します",//36
        "Headプロパティを開く（未完成）",//37
        "UdonUIヘッドメニュー",//38
        "Headメニュー作成（未完成）",//39
        "Handメニューのプロパティを開く（未完成）",//40
        "UdonUIハンドメニュー",//41
        "Handメニュー作成（未完成）",//42
        "キーのプロパティの編集",//43
        "追加ヘルパーの作成（重力アクション）",//44
        "シーン内に重力オペランドを作成しました。繰り返し作成しないでください。",//45
        "作成に成功しました。次の点に注意してください。",//46
        "1、重力操作の影響を受ける物体をPickUp-Objの下に置いてください",//47
        "2、PickUp-Objの下に置かれたオブジェクトはプログラム実行後に動的に削除できない",//48
        "3、MainScriptオブジェクトをオン/オフすることで重力操作を有効にするかどうかを決定できます",//49
        "4、手動で物体にRgidbodyコンポーネントを追加する必要がある",//50
        "UdonUIに致命的なエラーをチェック",//51
        "以下のUdonUIに異常があります!!!",//52
        "プロパティを開いて、次のボタンのプロパティにアニメーションマシンが存在するか、オブジェクトが空であるかを確認してください。",//53
        "中のオブジェクトをダブルクリックすると、異常ボタンのプロパティを修正するためにシーン内で自動的に選択されます。",//54
        "チェックが完了しました",//55
        " ",//56
        "個の致命的な異常ボタンが見つかりました",//57
        "原因",//58
        "ボタンにアニメーション機が設定されているのにアニメーション機が付与されていない可能性があり",//59
        "ボタンにオブジェクト状態が設定されているのにオブジェクトが付与されていない可能性がある",//60
        "異常ボタンオブジェクト",//61
        "異常トリガーオブジェクト",//62
        "検査が完了し",//63
        "異常は見つかりませんでした",//64
        "先にUdonUI環境を作ってください！！！",//65---------------
        "",//66
        "追加補助対象を作成（クライミング）",//67
        "サウンド",//68
        "すべてのUdonUIボタン",//69
        "すべてのトリガー",//70
        "既に作成済みです",//71
        "対象を重複作成しないでください",//72
        "1、Climbing’対象内にクライミング可能なレベルを設定してください",//73
        "2、‘Climbing’ のチャイルド‘forefinger climbing?’ を起用することで、r1またはr2ボタンでクライミングできるようになります",//74
        "追加補助対象（サードパーソンビュー・コントローラー）を作成",//75
        "1、‘Third’対象内でサードパーソンビュー・コントローラーに関連するボタン配置を設定できます",//76
        "追加補助対象（チャットシステム）を作成",//77
        "VRC内ではエンターキーでメッセージを送り，'TAB'キーでチャット欄を表示/非表示に，上下の矢印キーで過去のメッセージを全て再送、マウスホイルで過去のチャットメッセージを閲覧できます",//78
        "UdonUIdのハンドサインメニューを発見できませんでした,まずは作成してください",//79
        "UdonUI補助対象又はハンドサインメニューを発見できませんでした、まずはハンドサインメニューを作成してください",//80
        "コンポーネントエラーが発生しました",//81
        "コンポーネントを確認しました",//82
        "個のボタンにコンポーネントエラーが発生しました",//83
        "コンポーネントエラーは自動修復されました",//84
        "ロストしたコンポーネントは<MeshRender>です",//85
        "漫画を作成します",//86
        "漫画を作成します",//87
        "创建玩家脚步",//88
        "创建传送门",//89
        "创建黑白名单",//90
        "创建乘坐对象",//91
        "创建玩家ID遮罩",//92
        "创建或打开字幕系统",//93
        ""
        };
        static string[] english = new string[] {
            "Please right click the 'UdonUI>Button' option in the hierarchy panel to create the UdonUI environment",//0
            "Current version",//1
            "The current version is",//2
            "Author",//3
            "Author URI",//4
            "UdonUI Main Menu",//5
            "Open the UdonUI property setting menu",//6
            "UdonUI Properties",//7
            "Edit the audio resources used in the event",//8
            "Audio resource modification",//9
            "Button List",//10
            "Collision Trigger List",//11
            "It is recommended to use the check button to check the problem before each upload to avoid fatal errors",//12
            "Enable Guide Icon",//13
            "Switch Icon",//14
            "Guide Icon",//15
            "Settings Icon",//16
            "Please set your guide icon. In order to keep it beautiful, it is best to use a 3D model with thickness",//17
            "The guide icon is similar to the mouse and will display the location of your landing point. Both PC and VR are available",//18
            "Create UdonUI auxiliary properties",//19
            "Warning",//20
            "Create",//21
            "Cancel",//22
            "I Know",//23
            "Close",//24
            "Finish",//25
            "Warning",//26
            "Back",//27
            "When the synchronization button is needed, UdonUI auxiliary must be created. Do not change any attributes of the [UdonUI_AGI] to be created (including parent and child levels). Click Cancel to cancel the creation. If you want to close the function later, you can directly delete [UdonUI_AGI] and its child levels",//28
            "Press key attribute modification",//29
            "If synchronization has been set for a button, you must create UdonUI auxiliary attribute, otherwise it may not work",//30
            "Number of people in the room (consistent with VRC)",//31
            "[UdonUI_AGI] has been created in the scene. Please do not do anything about it",//32
            "If you want to turn off the synchronization function, you can directly delete the whole [UdonUI_AGI]",//33
            "Open Gesture Menu Properties",//34
            "UdonUI Gesture Menu",//35
            "Create Gesture Menu",//36
            "Open the Head Property (incomplete)",//37
            "UdonUI Header Menu",//38
            "Create Head Menu (incomplete)",//39
            "Open Hand Menu Properties (not completed)",//40
            "UdonUI Hand Menu",//41
            "Create Hand Menu (incomplete)",//42
            "Press key attribute modification",//43
            "Create (gravity operation)",//44
            "You have created gravity operation objects in the scene. Please do not create them again",//45
            "The creation is successful. Please note the following",//46
            "1、Please place objects affected by gravity operation under PickUp-Obj",//47
            "2、Objects placed under PickUp-Obj cannot be deleted dynamically after the program runs",//48
            "3、You can decide whether to enable gravity operation by turning on and off the mainscript object",//49
            "4、[rgidbody] component needs to be added to the object manually",//50
            "Check UdonUI for fatal errors",//51
            "The following UdonUI has an exception!!!",//52
            "Please open the attribute and check whether there is an animator or the object is empty in the following button attributes one by one",//53
            "Double click the object inside to automatically select it in the scene to facilitate the correction of abnormal button attributes",//54
            "Check complete",//55
            "Find",//56
            "fatal exception button",//57
            "Reason",//58
            "It may be that the button is set with an animator but not given an animator",//59
            "The object state is set in the button, but it is not assigned to the object",//60
            "Exception button object",//61
            "Exception trigger object",//62
            "Check complete",//63
            "No abnormality was found",//64
            "Please create UdonUI environment first!!!",//65---------------
            "",//66
            "Create (climb)",//67
            "Audio",//68
            "All my UdonUI buttons",//69
            "All my triggers",//70
            "You have created in the scene",//71
            "Object, do not create it repeatedly",//72
            "1、Please set the climbing level in the 'climbing' object",//73
            "2、Open the child 'Forefinger Clipping?' under 'Clipping' To allow users to climb with the keys of the handle",//74
            "Create (third person controller)",//75
            "1、You can set the buttons related to opening the third person controller in the 'Third' object",//76
            "Create (chat system)",//77
            "After entering VRC, press enter directly to send the information, press' TAB 'to switch the chat window, press up and down arrow keys to quickly input the information sending history, and use the mouse wheel to check the chat history",//78
            "UdonUI gesture menu is not detected. Please create it manually first",//79
            "UdonUI helper or gesture menu is not detected, please create gesture menu manually",//80
            "Component missing error found",//81
            "Find",//82
            "the button appears and important components are missing",//83
            "But it has been automatically added and repaired",//84
            "The missing component is < MeshRender>",//85
            "Create a comic",//86
            "Create a comic",//87
            "创建玩家脚步",//88
            "创建传送门",//89
            "创建黑白名单",//90
            "创建乘坐对象",//91
            "创建玩家ID遮罩",//92
            "创建或打开字幕系统",//93
            ""
        };

        static string[] porName;//porName[60
        static int languageID = 0;
        #endregion
        #region 基础变量
        float windowhight = 180;
        float TimeLine = 0;
        float deltatime;
        Vector2 hdt, hxd2;
        public static GameObject MainUI;
        private static UdonBehaviour m_MainUIUdon = null;
        public static MainUI_Script MainUIUdon_E {
            get { return UdonUI_Manager.Instance.mainUI; }
        }
        public static MainUI_Script MainUIUdon {
            get { return MainUIUdon_E; }
        }
        Debugwindows_Editor wind;
        [SerializeField]
        GameObject[] SAOobj;
        [SerializeField]
        public List<GameObject> ButtonT, BoxColliderT, FingerT;//按钮的list列表,内部变量变化前的list列表
        [SerializeField]
        GameObject[] button, boxcollider, finger;//初始化 给予空对象处理
        SerializedObject _Mobj;
        enum ListDisplay
        {
            UdonUI,
            BoxCollider,
            Finger
        }
        ListDisplay SwitchDisplay = ListDisplay.UdonUI;
        ReorderableList _ButtonT, _BoxConllider, _Finger;//List列表

        bool AudioEditorV;
        [SerializeField]
        bool Mouse_switch = false;
        [SerializeField]
        GameObject Mouse_obj;
        AudioClip[] AudioEditor;
        ReorderableList AudioEditorlistv;
        [SerializeField]
        List<AudioClip> AudioEditorlist;

        [SerializeField]
        int Playersny = 0;
        SerializedProperty _Playersny;
        SerializedProperty _Mouse_switch;
        SerializedProperty _Mouse_obj;

        private GameObject UdonUI_AGI_;
        [SerializeField]
        GameObject UdonUI_AGI
        {
            get
            {
                if (UdonUI_AGI_ == null)
                    UdonUI_AGI_ = GameObject.Find("/UdonUI_AGI");
                return UdonUI_AGI_;
            }
        }

        [SerializeField]
        GameObject SAOSyn, HandSyn, VRSyn;
        #endregion
        #region 图标
        //Texture Editor_tex = Texture2D.find;
        //[SerializeField]
        //Texture2D MainTexture, MainTexture2;
        //[SerializeField]
        //float texX, texY;

        SerializedProperty _MainTexture;
        //SerializedProperty _texX, _texY;
        #endregion
        #endregion
        [MenuItem("Asitir_Tool/Udon_UI", false, 1)]
        static void windows()
        {

            Translate(languageID, true);
            MainUI = UdonUI_Manager.Instance.mainUIobj;
            MainWindow = GetWindow(typeof(MainUI_Editor), false);
            return;
            if (MainUI)
            {
                MainWindow = GetWindow(typeof(MainUI_Editor), false);

                //if (EditorUtility.DisplayDialog("注意！！！", "        在使用前请到 ‘Udon_UI’ 目录下找到 ‘MainUI_Script’ 点击 ‘Compile All...’ 完成初始化。\n        此插件不支持撤销，撤销与UdoUI相关的操作将可能导致严重错误！！！\n        如果不小心撤销了，可以尝试按‘Ctrl+Y’挽回错误。 \n        很抱歉每次打开UdonUI都会弹出这个窗口，这些很重要", "继续启用UdonUI主面板", "返回"))
                //{
                //    MainWindow = GetWindow(typeof(MainUI_Editor), false);
                //    //MainWindow.maxSize = new Vector2(320, 180);
                //    //MainWindow.minSize = new Vector2(320, 180);
                //    //MainWindow.maxSize = new Vector2(float.MaxValue, float.MaxValue);
                //    //MainWindow.minSize = new Vector2(100, 100);

                //}
            }
            else
            {
                //Debug.Log("请先在场景中创建UdonUI");
                if (EditorUtility.DisplayDialog(porName[20], "        " + porName[0], porName[23], porName[27]))
                {
                    //MainWindow = GetWindow(typeof(MainUI_Editor), false);
                    //MainWindow.maxSize = new Vector2(320, 180);
                    //MainWindow.minSize = new Vector2(320, 180);
                    //MainWindow.maxSize = new Vector2(float.MaxValue, float.MaxValue);
                    //MainWindow.minSize = new Vector2(100, 100);

                }

            }
        }
        [MenuItem("Asitir_Tool/Version", false, 3)]
        static void Ver()
        {
            Translate(languageID, true);

            if (EditorUtility.DisplayDialog(porName[1], "        " + porName[2] + ": " + UdonUI_Manager.Instance.udonUIver + "\n\n        " + "QQ群:759862786  (VRCUdon世界创作交流)" + "\n\n        " + porName[3] + ": Asitir", porName[4], "将QQ群号复制到剪贴板"))
            {
                Application.OpenURL("https://space.bilibili.com/4671627");
            }
            else
            {
                GUIUtility.systemCopyBuffer = "759862786";
            }
        }
        [MenuItem("Asitir_Tool/Language/Chinese", false, 4)]
        static void SetChinese()
        {
            languageID = 0;
            Translate(languageID);
        }
        [MenuItem("Asitir_Tool/Language/English", false, 4)]
        static void SetEnglish()
        {
            languageID = 1;
            Translate(languageID);
        }
        [MenuItem("Asitir_Tool/Language/Japanese", false, 4)]
        static void SetJapanese()
        {
            languageID = 2;
            Translate(languageID);
        }
        #region 初始化
        private void OnEnable()
        {
            Init();
        }
        #endregion
        void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            UpdateScript();
        }

        #region Update

        void Init() {
            Translate(languageID, true);
            //Debug.Log("长度： " + porName.Length);
            //MainWindow.maxSize = new Vector2(int.MaxValue, int.MaxValue);

            //windowhight = 180;
            TimeLine = 0;
            playf = 0;
            SAOSyn = (GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_HandMotion + "player_syn/Sao_syn.prefab", typeof(GameObject));
            if (UdonUI_AGI)
                Playersny = UdonUI_AGI.transform.GetChild(0).childCount;
            //MainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/Editor/Texture/ASD.png", typeof(Texture2D));
            //MainTexture2 = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/Editor/Texture/SAD.png", typeof(Texture2D));
            //Debug.Log(MainTexture.name);
            AudioEditorlist = new List<AudioClip>();
            ButtonT = new List<GameObject>();
            BoxColliderT = new List<GameObject>();
            FingerT = new List<GameObject>();
            //GuiOption = new[] { GUILayout.Width(70) };
            if (UdonUI_Manager.Instance.mainUIobj)
                MainUI = UdonUI_Manager.Instance.mainUIobj;
            else
                MainUI = null;

            if (MainUI)
            {
                Selection.activeGameObject = MainUI;

                //MainUIUdon = MainUI.GetComponent<UdonBehaviour>();
                Undo.RegisterCompleteObjectUndo(MainUIUdon, "按钮数量修改");
                //MainUIUdon.publicVariables.@("Mouse_switch", out Mouse_switch);
                Mouse_switch = MainUIUdon_E.Mouse_switch;
            }

            _Mobj = new SerializedObject(this);
            _Playersny = _Mobj.FindProperty("Playersny");
            _Mouse_switch = _Mobj.FindProperty("Mouse_switch");
            _Mouse_obj = _Mobj.FindProperty("Mouse_obj");
            _MainTexture = _Mobj.FindProperty("MainTexture");
            //_texX = _Mobj.FindProperty("texX");
            //_texY = _Mobj.FindProperty("texY");
            AudioEditorlistv = new ReorderableList(_Mobj, _Mobj.FindProperty("AudioEditorlist"), false, false, false, false);
            AudioEditorlistv.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = AudioEditorlistv.serializedProperty.GetArrayElementAtIndex(index);
                rect.height = EditorGUIUtility.singleLineHeight;
                rect.y += 2;
                EditorGUI.PropertyField(rect, item, new GUIContent(porName[68] + ": " + (index + 1)));
            };

            //初始化List列表
            SwitchDisplay = ListDisplay.UdonUI;
            //MainUIUdon.publicVariables.TryGetVariableValue("Button", out button);//所有按钮
            if (MainUIUdon_E == null) return;
            button = MainUIUdon_E.Button;
            //MainUIUdon.publicVariables.TryGetVariableValue("BoxColliderUdon", out boxcollider);//所有碰撞
            //MainUIUdon.publicVariables.TryGetVariableValue("Finger", out finger);//所有手势
            foreach (GameObject A in button)
                ButtonT.Add(A);
            //foreach (GameObject A in boxcollider)
            //    BoxColliderT.Add(A);
            //foreach (GameObject A in finger)
            //    FingerT.Add(A);
            if (button.Length > 0) Selection.activeObject = button[0];
            _ButtonT = new ReorderableList(_Mobj, _Mobj.FindProperty("ButtonT"), false, true, false, true);
            _ButtonT.drawHeaderCallback = (Rect rect) =>
            {//List列表命名
                GUI.Label(rect, porName[69]);
            };

            _ButtonT.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = _ButtonT.serializedProperty.GetArrayElementAtIndex(index);
                rect.height = EditorGUIUtility.singleLineHeight;
                rect.y += 2;
                if (index < button.Length) EditorGUI.PropertyField(rect, item, new GUIContent(string.Format("({0})-{1}", index, button[index].name)));//button[index].name + " ID:" + index
            };

            _ButtonT.onRemoveCallback = (ReorderableList index) =>
            {
                //Debug.Log($"删除？: {index.index}");
                UdonUI_Manager.Instance.mainUI.Button[index.index] = null;
                UdonUI_Manager.Instance.ChackUdonUIButton_Update(false);
            };

            _BoxConllider = new ReorderableList(_Mobj, _Mobj.FindProperty("BoxColliderT"), false, true, false, true);
            _BoxConllider.drawHeaderCallback = (Rect rect) =>
            {//List列表命名
                GUI.Label(rect, porName[70]);//porName[]
            };

            _BoxConllider.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = _BoxConllider.serializedProperty.GetArrayElementAtIndex(index);
                rect.height = EditorGUIUtility.singleLineHeight;
                rect.y += 2;
                if (index < boxcollider.Length) EditorGUI.PropertyField(rect, item, new GUIContent(string.Format("({0})-{1}", index, boxcollider[index].name)));
            };
            _BoxConllider.onRemoveCallback = (ReorderableList index) =>
            {
                UdonUI_Manager.Instance.mainUI.BoxColliderUdon[index.index] = null;
                UdonUI_Manager.Instance.UdonBoxColliderUpdate(false);
            };

            _Finger = new ReorderableList(_Mobj, _Mobj.FindProperty("FingerT"), false, true, false, false);
            _Finger.drawHeaderCallback = (Rect rect) =>
            {//List列表命名
                GUI.Label(rect, "我所有的手势触发");
            };

            _Finger.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = _Finger.serializedProperty.GetArrayElementAtIndex(index);
                rect.height = EditorGUIUtility.singleLineHeight;
                rect.y += 2;
                if (index < finger.Length) EditorGUI.PropertyField(rect, item, new GUIContent(finger[index].name));
                //if (index < finger.Length) EditorGUI.PropertyField(rect, item, new GUIContent("11"));
            };
        }
        void UpdateScript()
        {
            deltatime = Time.deltaTime;
            if (UdonUI_Manager.Instance.mainUIobj)
                MainUI = UdonUI_Manager.Instance.mainUIobj;
            else
                MainUI = null;

            GUILayout.Space(10);
            //MainTexture.dra
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            if (MainUI)
            {
                _Mobj.Update();
                //MainUIUdon = MainUI.GetComponent<UdonBehaviour>();//设置Udon环境

                //MainUIUdon.publicVariables.TryGetVariableValue("Button", out button);//所有按钮
                //ButtonT.Clear();
                //for (int i = 0; i < button.Length; i++)
                //    ButtonT.Add(button[i]);

                //MainUIUdon.publicVariables.TryGetVariableValue("BoxColliderUdon", out boxcollider);//所有碰撞
                //BoxColliderT.Clear();
                //for (int i = 0; i < boxcollider.Length; i++)
                //    BoxColliderT.Add(boxcollider[i]);

                //MainUIUdon.publicVariables.TryGetVariableValue("Finger", out finger);//所有手势
                //FingerT.Clear();
                //for (int i = 0; i < finger.Length; i++)
                //    FingerT.Add(finger[i]);

                //如果得到主要UI的话  就将拿到的UdonUI环境的参数来填充当前脚本参数
                //switch ((int)SwitchDisplay) {
                //    case 0:

                //        MainUIUdon.publicVariables.TryGetVariableValue("Button", out button);//所有按钮
                //        ButtonT.Clear();
                //        for (int i = 0; i < button.Length; i++)
                //            ButtonT.Add(button[i]);

                //        break;
                //    case 1:

                //        MainUIUdon.publicVariables.TryGetVariableValue("BoxColliderUdon", out boxcollider);//所有碰撞
                //        BoxColliderT.Clear();
                //        for (int i = 0; i < boxcollider.Length; i++)
                //            BoxColliderT.Add(boxcollider[i]);

                //        break;
                //    case 2:

                //        MainUIUdon.publicVariables.TryGetVariableValue("Finger", out finger);//所有手势
                //        FingerT.Clear();
                //        for (int i = 0; i < finger.Length; i++)
                //            FingerT.Add(finger[i]);

                //        break;
                //}
            }
            else
            {
                //GUILayout.Label(porName[60]);
                //GUILayout.Label(porName[60]);
                //GUILayout.Label(porName[60]);

            }

            GUILayout.Label(porName[5]);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;

            if (GUILayout.Button(porName[6]))
                GetWindow(typeof(Udon_Em), true, porName[7]);
            if (GUILayout.Button(porName[8])) AudioEditorV = !AudioEditorV;
            if (AudioEditorV)
            {
                //MainUIUdon.publicVariables.TryGetVariableValue("MainAudio", out AudioEditor);//所有音频
                AudioEditor = MainUIUdon_E.MainAudio;
                AudioEditorlist.Clear();
                foreach (AudioClip A in AudioEditor)
                    AudioEditorlist.Add(A);

                EditorGUI.BeginChangeCheck();
                hxd2 = GUILayout.BeginScrollView(hxd2);
                AudioEditorlistv.DoLayoutList();//绘制List列表
                GUILayout.EndScrollView();

                _Mobj.ApplyModifiedProperties();
                if (EditorGUI.EndChangeCheck())
                {
                    //Debug.Log("111");
                    //MainUIUdon.publicVariables.TrySetVariableValue("MainAudio", AudioEditorlist.ToArray());//所有音频
                    MainUIUdon_E.MainAudio = AudioEditorlist.ToArray();
                    Undo.RegisterCompleteObjectUndo(MainUIUdon, porName[9]);
                }
            }
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(porName[10])) SwitchDisplay = ListDisplay.UdonUI;
            if (GUILayout.Button(porName[11])) SwitchDisplay = ListDisplay.BoxCollider;
            //if (GUILayout.Button("手势触发列表")) SwitchDisplay = ListDisplay.Finger;
            GUILayout.EndHorizontal();

            hdt = GUILayout.BeginScrollView(hdt);
            #region UdonUI环境参数
            if (MainUI)
            {
                switch ((int)SwitchDisplay)
                {
                    case 0:
                        //MainUIUdon.publicVariables.TryGetVariableValue("Button", out button);//所有按钮
                        button = MainUIUdon_E.Button;
                        ButtonT.Clear();
                        for (int i = 0; i < button.Length; i++)
                            ButtonT.Add(button[i]);

                        if (_ButtonT == null) Init();
                        _ButtonT.DoLayoutList();//绘制List列表
                        break;
                    case 1:
                        //MainUIUdon.publicVariables.TryGetVariableValue("BoxColliderUdon", out boxcollider);//所有碰撞
                        boxcollider = MainUIUdon_E.BoxColliderUdon;
                        BoxColliderT.Clear();
                        for (int i = 0; i < boxcollider.Length; i++)
                            BoxColliderT.Add(boxcollider[i]);

                        if(_BoxConllider == null) Init();
                        _BoxConllider.DoLayoutList();
                        break;
                    case 2:
                        //MainUIUdon.publicVariables.TryGetVariableValue("Finger", out finger);//所有手势
                        //finger = MainUIUdon_E.Finger;
                        FingerT.Clear();
                        for (int i = 0; i < finger.Length; i++)
                            FingerT.Add(finger[i]);

                        _Finger.DoLayoutList();
                        break;
                }
                CheckUdonUI();
                GUILayout.Label(porName[12]);
                GUILayout.Space(20);

                GameObject[] ASD;
                //MainUIUdon.publicVariables.TryGetVariableValue("MousePos", out ASD);
                ASD = MainUIUdon_E.MousePos;
                Mouse_obj = ASD[0];

                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(_Mouse_switch, new GUIContent(porName[13], porName[13]));
                if (EditorGUI.EndChangeCheck())
                {
                    _Mobj.ApplyModifiedProperties();
                    Undo.RegisterCompleteObjectUndo(MainUIUdon, porName[14]);
                }
                if (Mouse_switch)
                {
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_Mouse_obj, new GUIContent(porName[15]));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _Mobj.ApplyModifiedProperties();

                        GameObject[] A = new GameObject[1];
                        A[0] = Mouse_obj;
                        //MainUIUdon.publicVariables.TrySetVariableValue("MousePos", A);//
                        MainUIUdon_E.MousePos = A;
                        Undo.RegisterCompleteObjectUndo(MainUIUdon, porName[16]);
                    }
                    GUILayout.Label(porName[17]);
                }
                else
                {
                    GUILayout.Label(porName[18]);
                }
                //MainUIUdon.publicVariables.TryGetVariableValue("Mouse_switch", out Mouse_switch);
                if (Mouse_switch && Mouse_obj)
                {
                    //MainUIUdon.publicVariables.TrySetVariableValue("Mouse_switch", true);
                    MainUIUdon_E.Mouse_switch = true;
                }
                else
                {
                    //MainUIUdon.publicVariables.TrySetVariableValue("Mouse_switch", false);
                    MainUIUdon_E.Mouse_switch = false;
                }

                GUILayout.Space(20);

            }
            if (!GameObject.Find("/UdonUI_AGI"))
            {
                if (GUILayout.Button(porName[19]))
                {
                    if (EditorUtility.DisplayDialog(porName[20], "      " + porName[28], porName[21], porName[22]))
                    {
                        GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "UdonUI_AGI.prefab", typeof(GameObject)));
                        A.name = "UdonUI_AGI";
                        GameObject B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "PlayerCount.prefab", typeof(GameObject)));
                        B.name = "PlayerCount";
                        B.transform.SetParent(A.transform);

                        GameObject[] Add;
                        //MainUIUdon.publicVariables.TryGetVariableValue("NullObject", out Add);
                        Add = MainUIUdon_E.NullObject;
                        GameObject[] ADD2 = new GameObject[6];
                        for (int i = 0; i < ADD2.Length; i++)
                        {
                            if (i == 5)
                                ADD2[i] = A;
                            else
                                ADD2[i] = Add[i];
                        }
                        //MainUIUdon.publicVariables.TrySetVariableValue("NullObject", ADD2);
                        MainUIUdon_E.NullObject = ADD2;
                        Undo.RegisterCompleteObjectUndo(MainUIUdon, porName[29]);
                    }
                }
                EditorGUILayout.LabelField(porName[30]);
                //MainUIUdon.publicVariables.TrySetVariableValue("UdonUI_AGI_bool", false);
                if (MainUIUdon_E)
                    MainUIUdon_E.UdonUI_AGI_bool = false;

            }
            else
            {
                //MainUIUdon.publicVariables.TrySetVariableValue("UdonUI_AGI_bool", true);
                MainUIUdon_E.UdonUI_AGI_bool = true;
                Transform MainA = GameObject.Find("/UdonUI_AGI").transform;
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(_Playersny, new GUIContent(porName[31], porName[31]));
                EditorGUILayout.LabelField(porName[32]);
                EditorGUILayout.LabelField(porName[33]);
                //EditorGUILayout.LabelField("既然创建了【UdonUI_AGI】，那么房间人数不能为零，否则报错！！");
                bool SAOUI = false, VRUI = false, HandUI = false;
                for (int i = 0; i < MainA.childCount; i++)
                {
                    string name = MainA.GetChild(i).name;
                    //if (name == "SAOUI") SAOUI = true;
                    //if (name == "VRUI") VRUI = true;
                    //if (name == "HandUI") HandUI = true;
                    switch (name)
                    {
                        case "SAOUI":
                            SAOUI = true;
                            break;
                        case "VRUI":
                            VRUI = true;
                            break;
                        case "HandUI":
                            SAOUI = true;
                            break;
                    }
                }
                //------------------------------------------------------------------------------------
                if (SAOUI)
                {
                    if (GUILayout.Button(porName[34]))
                    {
                        GetWindow(typeof(HandMotion_Editor), true, porName[35]);
                    }
                }
                else
                {
                    if (GUILayout.Button(porName[36]))
                    {
                        GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SAOUI.prefab", typeof(GameObject)));
                        A.name = "SAOUI";
                        A.transform.SetParent(MainA);
                        Selection.activeGameObject = A;
                    }
                }
                //------------------------------------------------------------------------------------
                if (VRUI)
                {
                    if (GUILayout.Button(porName[37]))
                    {
                        GetWindow(typeof(HeadUI_Editor), true, porName[38]);
                    }
                }
                else
                {
                    if (GUILayout.Button(porName[39]))
                    {
                        GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "VRUI.prefab", typeof(GameObject)));
                        A.name = "VRUI";
                        A.transform.SetParent(MainA);
                    }
                }
                //------------------------------------------------------------------------------------
                if (HandUI)
                {
                    if (GUILayout.Button(porName[40]))
                    {
                        GetWindow(typeof(HandUI_Editor), true, porName[41]);
                    }
                }
                else
                {
                    if (GUILayout.Button(porName[42]))
                    {
                        GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "HandUI.prefab", typeof(GameObject)));
                        A.name = "HandUI";
                        A.transform.SetParent(MainA);
                    }
                }
                //if(GUILayout.Button(""))
                _Mobj.ApplyModifiedProperties();
                if (EditorGUI.EndChangeCheck())
                {
                    int JS = MainA.GetChild(0).childCount;
                    if (JS < Playersny)
                    {
                        HandMotion_UI Syn_SaoUI = null;
                        if (SAOUI)
                        {//如果存在手势菜单
                            Syn_SaoUI= MainA.Find("SAOUI").GetComponent<HandMotion_UI>();
                            SAOobj = Syn_SaoUI.MainGameobject;
                            SAOUI = SAOobj[1];//是否存在Sao的同步菜单
                        }

                        for (int i = 0; i < (Playersny - JS); i++)
                        {
                            GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "SnyPlayer.prefab", typeof(GameObject)));
                            A.name = "SnyPlayer_" + (MainA.GetChild(0).childCount + 1);
                            A.transform.SetParent(MainA.GetChild(0));
                            if (SAOUI)
                            {
                                if (SAOobj[1])
                                {
                                    GameObject Syn_SaoUI_ = Instantiate(SAOobj[1], Syn_SaoUI.transform);
                                    Syn_SaoUI_.name = "SnySaoUI_" + (Syn_SaoUI.transform.childCount);
                                }
                                else
                                {
                                    GameObject Syn_SaoUI_ = Instantiate(SAOSyn, Syn_SaoUI.transform);
                                    Syn_SaoUI_.name = "SnySaoUI_" + (Syn_SaoUI.transform.childCount);
                                }

                            }
                        }
                    }
                    else if (JS > Playersny)
                    {
                        Transform Syn_SaoUI = UdonUI_AGI.transform.Find("SAOUI");
                        for (int i = 0; i < (JS - Playersny); i++)
                        {
                            DestroyImmediate(MainA.GetChild(0).GetChild(MainA.GetChild(0).childCount - 1).gameObject);
                            if (Syn_SaoUI && SAOobj[1])
                                DestroyImmediate(Syn_SaoUI.GetChild(Syn_SaoUI.childCount - 1).gameObject);
                        }
                    }
                    Undo.RegisterCompleteObjectUndo(MainA, porName[43]);
                }


            }

            GUILayout.Space(20);
            if (GUILayout.Button(porName[44]))
            {
                if (GameObject.Find("/Rigidbody-controller"))
                {
                    if (EditorUtility.DisplayDialog(porName[20], porName[45], porName[23], porName[25]))
                    {
                        Selection.activeObject = GameObject.Find("/Rigidbody-controller");
                    }
                }
                else
                {
                    GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "Rigidbody-controller.prefab", typeof(GameObject)));
                    A.transform.position = Vector3.zero;
                    A.name = "Rigidbody-controller";
                    if (EditorUtility.DisplayDialog(porName[25], porName[46] + "：\n     " + porName[47] + "\n     " + porName[48] + "\n     " + porName[49] + " \n    " + porName[50], porName[23], porName[24]))
                    {
                        Selection.activeObject = GameObject.Find("/Rigidbody-controller");
                    }
                }
            }

            if (GUILayout.Button(porName[67]))
            {
                if (GameObject.Find("/Climbing"))
                {
                    if (EditorUtility.DisplayDialog(porName[20], porName[71] + "‘Climbing’" + porName[72], porName[23], porName[24]))
                    {
                        Selection.activeObject = GameObject.Find("/Climbing");
                    }
                }
                else
                {
                    GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "Climbing.prefab", typeof(GameObject)));
                    A.transform.position = Vector3.zero;
                    A.name = "Climbing";
                    if (EditorUtility.DisplayDialog(porName[25], porName[46] + "：\n     " + porName[73] + " \n     " + porName[74], porName[23], porName[24]))
                    {
                        Selection.activeObject = GameObject.Find("/Climbing");
                    }
                }
            }


            if (GUILayout.Button(porName[75]))
            {
                if (GameObject.Find("/Third"))
                {
                    if (EditorUtility.DisplayDialog(porName[20], porName[71] + "‘Third’" + porName[72], porName[23], porName[24]))
                    {
                        Selection.activeObject = GameObject.Find("/Third");
                    }
                }
                else
                {
                    GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "Third.prefab", typeof(GameObject)));
                    A.transform.position = Vector3.zero;
                    A.name = "Third";
                    if (EditorUtility.DisplayDialog(porName[25], porName[46] + "：\n     " + porName[76], porName[23], porName[24]))
                    {
                        Selection.activeObject = GameObject.Find("/Third");
                    }
                }
            }


            GUILayout.BeginHorizontal();
            if (GUILayout.Button(porName[77]))
            {
                if (GameObject.Find("/ChatUI_Root_PC"))
                {
                    if (EditorUtility.DisplayDialog(porName[20], porName[71] + "‘ChatUI_Root_PC’" + porName[72], porName[23], porName[24]))
                    {
                        if (UdonUI_Manager.Instance.mainUIobj.transform.Find("SaoUI_Main").GetChild(0).GetChild(5).GetChild(0).GetChild(0))
                            Selection.activeObject = UdonUI_Manager.Instance.mainUIobj.transform.Find("SaoUI_Main").GetChild(0).GetChild(5).GetChild(0).GetChild(0);
                        else
                            Selection.activeObject = GameObject.Find("/ChatUI_Root_PC");
                    }
                }
                else
                {
                    //GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/SampleUI/SamplePrefad/ChatUI_Root_PC.prefab", typeof(GameObject)));
                    //A.transform.position = Vector3.zero;
                    //A.name = "ChatUI_Root_PC";
                    //if (EditorUtility.DisplayDialog("完成", "创建成功，请注意以下事项：\n     1、你可以在‘Third’对象内设置打开第三人称控制器相关的按键 ", "我知道了", "关闭"))
                    //{
                    //    Selection.activeObject = GameObject.Find("/ChatUI_Root_PC");
                    //}
                    if (UdonUI_Manager.Instance.mainUIobj)
                    {
                        if (GameObject.Find("/UdonUI_AGI/SAOUI"))
                        {
                            GameObject[] saoObject;
                            //GameObject.Find("/UdonUI_AGI/SAOUI").GetComponent<UdonBehaviour>().publicVariables.TryGetVariableValue("MainGameobject", out saoObject);
                            saoObject = GameObject.Find("/UdonUI_AGI/SAOUI").GetComponent<HandMotion_UI>().MainGameobject;
                            if (saoObject[0])
                            {
                                GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "ChatUI_Root_PC.prefab", typeof(GameObject)));
                                A.transform.position = Vector3.zero;
                                A.name = "ChatUI_Root_PC";
                                RectTransform target = A.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponent<RectTransform>();
                                if (saoObject[0].transform.GetChild(0))
                                {
                                    if (saoObject[0].transform.GetChild(0).GetChild(5))
                                    {
                                        if (saoObject[0].transform.GetChild(0).GetChild(5).Find("Chat"))
                                        {
                                            target.SetParent(saoObject[0].transform.GetChild(0).GetChild(5).Find("Chat"));
                                            target.localPosition = Vector3.zero;
                                            target.localScale = Vector3.one;
                                            target.localRotation = Quaternion.identity;
                                        }
                                        else
                                        {
                                            target.SetParent(saoObject[0].transform.GetChild(0).GetChild(5));
                                            target.localPosition = new Vector3(-0.0647f, -0.0634f, 0);
                                        }
                                    }
                                    else
                                    {
                                        target.SetParent(saoObject[0].transform.GetChild(0));
                                        target.localPosition = new Vector3(-0.0647f, 0, 0);
                                    }
                                }
                                else
                                {
                                    target.SetParent(saoObject[0].transform);
                                    target.localPosition = new Vector3(-0.0647f, 0, 0);
                                }
                                GameObject botton = target.GetChild(0).GetChild(0).GetChild(0).GetChild(2).gameObject;
                                UdonUI_Manager.Instance.AddUdonUIButton(botton, EButtonType.Slid);
                                UdonUI_Manager.Instance.AddUdonUIButton(target.GetChild(0).GetChild(4).gameObject, EButtonType.Drag);
                                //SetButtonToSlid(botton);
                                //SteButtonType(botton);
                                //StartCoroutine
                                A.transform.GetChild(0).GetChild(1).SetParent(UdonUI_Manager.Instance.mainUIobj.transform);
                                if (EditorUtility.DisplayDialog(porName[25], porName[46] + "：\n     " + porName[78], porName[23], porName[24]))
                                {
                                    //if (GameObject.Find("/UdonUI_Main").transform.Find("SaoUI_Main").GetChild(0).GetChild(5).GetChild(0).GetChild(0))
                                    //    Selection.activeObject = GameObject.Find("/ChatUI_Root_PC").transform.Find("SaoUI_Main").GetChild(0).GetChild(5).GetChild(0).GetChild(0);
                                    //else
                                    Selection.activeObject = GameObject.Find("/ChatUI_Root_PC");
                                }

                            }
                            else
                            {
                                if (EditorUtility.DisplayDialog(porName[20], porName[79], porName[23], porName[24]))
                                {
                                }

                            }
                        }
                        else
                        {
                            if (EditorUtility.DisplayDialog(porName[20], porName[80], porName[23], porName[24]))
                            {
                            }
                        }
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog(porName[20], porName[0], porName[24], porName[24]))
                        {
                        }

                    }
                }
            }
            if (GUILayout.Button(porName[86]))
            {
                GetWindow(typeof(Comic_Windows), false, porName[87]);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(porName[88]))
            {
                GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "PlayerFootSteps.prefab", typeof(GameObject)));
                A.name = "PlayerFootSteps";
                Selection.activeObject = A;
            }
            if (GUILayout.Button(porName[89]))
            {
                GameObject A = GameObject.Find("/UdonUI_Portal");
                if (A)
                {
                    if (EditorUtility.DisplayDialog(porName[20], porName[71] + "‘UdonUI_Portal’" + porName[72], porName[23], porName[24]))
                    {
                        Selection.activeObject = GameObject.Find("/UdonUI_Portal");
                    }
                }
                else
                {
                    A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SampleObj + "UdonUI_Portal_Test.prefab", typeof(GameObject)));
                    A.name = "UdonUI_Portal";
                    Selection.activeObject = A;
                }

            }
            if (GUILayout.Button(porName[90]))
            {
                GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SampleObj + "PlayerListSet.prefab", typeof(GameObject)));
                A.name = "PlayerListSet";
                Selection.activeObject = A;
            }
            if (GUILayout.Button(porName[91]))
            {
                var windows = GetWindow(typeof(UdonUI_RidingObjects), false, "RidingObjects");
                //windows.name = "RidingObjects";
                //GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Udon_UI/SampleObject/PlayerListSet.prefab", typeof(GameObject)));
                //A.name = "PlayerListSet";
                //Selection.activeObject = A;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            //string creatPath = "Assets/Udon_UI/SampleUI/SamplePrefad/";
            if (GUILayout.Button(porName[93]))
            {
                GetWindow<UdonUI_CaptionsSystem>(false, "字幕系统");
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button(porName[92]))
            {
                //GameObject A = GameObject.Find("/HeadMaskRender");
                //if (A == null)
                {
                    GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "HeadMaskRender.prefab", typeof(GameObject)));
                    HeadMaskRender mainMask = A.GetComponent<HeadMaskRender>();
                    GameObject ReferenceCamera = FindObjectOfType<VRC_SceneDescriptor>()?.ReferenceCamera;
                    Camera referCame = null;
                    if (ReferenceCamera)
                        referCame = ReferenceCamera.GetComponent<Camera>();

                    if (referCame != null)
                        mainMask.axisZ = referCame.nearClipPlane + 0.01f;
                    else
                        mainMask.axisZ = 0.11f;
                    mainMask.mainUI = MainUIUdon_E;
                    A.name = "HeadMaskRender";
                    Selection.activeObject = A;
                }
            }
            GUILayout.EndScrollView();
            #endregion
        }
        #endregion


        #region 动画相关
/*        static MainUI_Editor()
        {
            EditorApplication.update += Update;
        }
        static void Update() {
            //Debug.Log("AAAA");
            if (time > -1)
            {
                time--;
                //Debug.Log(time);
                if (time < 0)
                {

                    //SteButtonType(TargetMainStatic);
                    SetButtonToSlid(TargetMainStatic);
                    time = -2;
                }
            }
        }
*/        
        static void SteButtonType(GameObject Target) {
            Debug.Log("执行一次: " + Target.name);

            //yield return null;
        }

        void PlayeStartAnima()
        {
            //GUI.DrawTexture(new Rect(0f, 0f, 54.2f, 75.6f), MainTexture, ScaleMode.StretchToFill, false);
            //GUI.DrawTextureWithTexCoords(new Rect(0f, 0f, 54.2f, 75.6f), MainTexture, new Rect(texX, texY, 0.5f, 0.5f));
            //playf = TextTuerGif.Play(playf, 2, MainTexture, 4, 7, new Rect(0, 0, 500, 255f), 27);
            //playf = TextTuerGif.Play(playf,2, MainTexture2, 6, 10, new Rect(0, 0, 320, 180),60);
            //windowhight = EditorGUILayout.Slider(windowhight, 180, 500);
            //MainWindow.maxSize = new Vector2(320, windowhight);
            //MainWindow.minSize = new Vector2(320, windowhight);
            //GUILayout.Space(300);

            //Debug.Log(MainTexture.texelSize);
            //texX = EditorGUILayout.Slider(texX, 0, 1);
            //texY = EditorGUILayout.Slider(texY, 0, 1);

        }

        class TextTuerGif
        {

            /// <summary>
            /// 播放GIF动画，只能固定10帧每秒,从左上往右下读取
            /// </summary>
            /// <param name="GifFra">设置当前帧</param>
            /// <param name="Text">设置图源</param>
            /// <param name="width">帧宽</param>
            /// <param name="hight">帧高</param>
            /// <param name="Position">图源大小</param>
            /// <returns></returns>
            public static int Play(int GifFra, Texture2D Text, int width, int hight, Rect Position)
            {
                int MapHight = GifFra / width;//第几排
                int MapWidth = GifFra - (MapHight * width);//第几个
                GifFra++;
                if (MapHight > hight) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
                return GifFra;
            }

            public static int Play(int GifFra, Texture2D Text, int width, int hight, Rect Position, int MaxEnd)
            {
                int MapHight = GifFra / width;//第几排
                int MapWidth = GifFra - (MapHight * width);//第几个
                GifFra++;
                if (MapHight > hight || GifFra >= MaxEnd) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
                return GifFra;
            }

            public static int Play(int GifFra, int step, Texture2D Text, int width, int hight, Rect Position, int MaxEnd)
            {
                int m_GifFra = GifFra / step;
                int m_MaxEnd = MaxEnd * step;

                int MapHight = m_GifFra / width;//第几排
                int MapWidth = m_GifFra - (MapHight * width);//第几个
                GifFra++;
                if (MapHight > hight || GifFra >= m_MaxEnd) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
                return GifFra;
            }

            public static int Play(int GifFra, int step, Texture2D Text, int width, int hight, Rect Position)
            {
                int m_GifFra = GifFra / step;

                int MapHight = m_GifFra / width;//第几排
                int MapWidth = m_GifFra - (MapHight * width);//第几个
                GifFra++;
                if (MapHight > hight) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
                return GifFra;
            }

            /// <summary>
            /// 播放GIF动画,从左上往右下读取
            /// </summary>
            /// <param name="GifFra"></param>
            /// <param name="Text"></param>
            /// <param name="width"></param>
            /// <param name="hight"></param>
            /// <param name="Position"></param>
            public static void Play_(int GifFra, Texture2D Text, int width, int hight, Rect Position)
            {
                int MapHight = GifFra / width;//第几排
                int MapWidth = GifFra - (MapHight * width);//第几个
                if (MapHight > hight) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
            }

            public static void Play_(int GifFra, Texture2D Text, int width, int hight, Rect Position, int MaxEnd)
            {
                int MapHight = GifFra / width;//第几排
                int MapWidth = GifFra - (MapHight * width);//第几个
                if (MapHight > hight || GifFra >= MaxEnd) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
            }

            public static void Play_(int GifFra, int step, Texture2D Text, int width, int hight, Rect Position, int MaxEnd)
            {
                int m_GifFra = GifFra / step;
                int m_MaxEnd = MaxEnd * step;

                int MapHight = m_GifFra / width;//第几排
                int MapWidth = m_GifFra - (MapHight * width);//第几个
                if (MapHight > hight || GifFra >= m_MaxEnd) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
            }

            public static void Play_(int GifFra, int step, Texture2D Text, int width, int hight, Rect Position)
            {
                int m_GifFra = GifFra / step;

                int MapHight = m_GifFra / width;//第几排
                int MapWidth = m_GifFra - (MapHight * width);//第几个
                if (MapHight > hight) { GifFra = 0; MapHight = 0; MapWidth = 0; }
                int DT = hight - 1 - MapHight;

                float Wbl = 1.0f / width;
                float Hbl = 1.0f / hight;

                GUI.DrawTextureWithTexCoords(Position, Text, new Rect(MapWidth * Wbl, DT * Hbl, Wbl, Hbl));
                //Vector2 TextSize = Text.texelSize;
            }

        }

        void CheckUdonUI()
        {
            if (GUILayout.Button(porName[51]))
            {
                #region 排查按钮
                int buttonMeshRender = 0;
                Vector2[] ALLEvents;
                int[] ALLType;
                GameObject[] ALLobj;
                Animator[] ALLAnimator;

                //MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber", out ALLEvents);
                //MainUIUdon.publicVariables.TryGetVariableValue("MainEvent", out ALLType);
                //MainUIUdon.publicVariables.TryGetVariableValue("TargetGameObject", out ALLobj);
                //MainUIUdon.publicVariables.TryGetVariableValue("MainAnimators", out ALLAnimator);
                ALLEvents = MainUIUdon_E.MainEventNumber;
                ALLType = MainUIUdon_E.MainEvent;
                ALLobj = MainUIUdon_E.TargetGameObject;
                ALLAnimator = MainUIUdon_E.MainAnimators;
                //Vector3.ProjectOnPlane
                //Vector3.SignedAngle
                int ErAnima = 0, ErObj = 0;

                //Debug.Log("ALLEvents的长度:" + ALLEvents.Length);
                //Debug.Log("button的长度:" + button.Length);

                int Er = 0;
                int[] ErNumber = new int[ALLEvents.Length];
                for (int i = 0; i < ALLEvents.Length; i++)
                {
                    if (!button[i].GetComponent<MeshRenderer>())
                    {
                        MeshRenderer a = button[i].AddComponent<MeshRenderer>();
                        a.enabled = false;
                        buttonMeshRender++;
                    }

                    for (int t = 0; t < ALLEvents[i].y; t++)
                    {
                        switch (ALLType[(int)(ALLEvents[i].x) + t])
                        {
                            case 0:
                            case 1:
                            case 2:
                                if (ALLobj[(int)(ALLEvents[i].x) + t] == null)
                                {//缺少对象
                                    ErNumber[Er] = i;
                                    Er++;
                                    t = 1999999999;
                                    ErObj++;
                                }
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                if (ALLAnimator[(int)(ALLEvents[i].x) + t] == null)
                                {//缺少动画机组件
                                    ErNumber[Er] = i;
                                    Er++;
                                    t = 1999999999;
                                    ErAnima++;
                                }
                                break;
                        }

                    }
                }
                #endregion

                #region 排查触发器
                Vector2[] ALLEvents_Con;
                int[] ALLType_Con;
                GameObject[] ALLobj_Con;
                Animator[] ALLAnimator_Con;
                int Er_Con = 0;
                int ErAnima_Con = 0, ErObj_Con = 0;

                //MainUIUdon.publicVariables.TryGetVariableValue("MainEventNumber_BoxCollider", out ALLEvents_Con);
                //MainUIUdon.publicVariables.TryGetVariableValue("MainEvent_BoxCollider", out ALLType_Con);
                //MainUIUdon.publicVariables.TryGetVariableValue("TargetGameObject_BoxCollider", out ALLobj_Con);
                //MainUIUdon.publicVariables.TryGetVariableValue("MainAnimators_BoxCollider", out ALLAnimator_Con);
                ALLEvents_Con = MainUIUdon_E.MainEventNumber_BoxCollider;
                ALLType_Con = MainUIUdon_E.MainEvent_BoxCollider;
                ALLobj_Con = MainUIUdon_E.TargetGameObject_BoxCollider;
                ALLAnimator_Con = MainUIUdon_E.MainAnimators_BoxCollider;


                int[] ErNumber_Con = new int[ALLEvents_Con.Length];
                for (int i = 0; i < ALLEvents_Con.Length; i++)
                {
                    for (int t = 0; t < ALLEvents_Con[i].y; t++)
                    {
                        switch (ALLType_Con[(int)(ALLEvents_Con[i].x) + t])
                        {
                            case 0:
                            case 1:
                            case 2:
                                if (ALLobj_Con[(int)(ALLEvents_Con[i].x) + t] == null)
                                {//缺少对象
                                    ErNumber_Con[Er_Con] = i;
                                    Er_Con++;
                                    t = 1999999999;
                                    ErObj_Con++;
                                }
                                break;
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                if (ALLAnimator_Con[(int)(ALLEvents_Con[i].x) + t] == null)
                                {//缺少动画机组件
                                    ErNumber_Con[Er_Con] = i;
                                    Er_Con++;
                                    t = 1999999999;
                                    ErAnima_Con++;
                                }
                                break;
                        }

                    }
                }
                #endregion


                if (Er > 0 || Er_Con > 0)
                {
                    wind = (Debugwindows_Editor)GetWindow(typeof(Debugwindows_Editor), true);
                    wind.Show();
                    wind.MainHead = porName[26];
                    wind.MainTex = "          " + porName[52] + " \n " + porName[53] + " \n " + porName[54];

                    if (Er > 0)
                    {//存在报错对象
                        if (EditorUtility.DisplayDialog(porName[55], porName[56] + Er + porName[57] + "\n " + porName[58] + "：\n " + porName[59] + ErAnima + "\n " + porName[60] + ErObj, "展开异常按钮", porName[24]))
                        {

                        }

                        wind.ErrBotton = true;
                        wind.MainListHead = porName[61];
                        wind.MainObjn = new GameObject[Er];
                        for (int i = 0; i < Er; i++)
                        {
                            wind.MainObjn[i] = button[ErNumber[i]];
                        }

                    }
                    if (Er_Con > 0)
                    {
                        wind.ErrBoxCollider = true;
                        //wind.MainHead_BoxCollider = "警告";
                        //wind.MainTex_BoxCollider = "          以下触发器存在异常!!!";
                        wind.MainListHead_BoxCollider = porName[62];
                        wind.MainObjn_BoxCollider = new GameObject[Er_Con];
                        for (int i = 0; i < Er_Con; i++)
                        {
                            wind.MainObjn_BoxCollider[i] = boxcollider[ErNumber_Con[i]];
                        }
                    }
                }
                else if (buttonMeshRender == 0)
                {//不存在报错对象  一切安好
                    if (EditorUtility.DisplayDialog(porName[63], porName[64], porName[23], porName[24])) { }
                }

                if (buttonMeshRender != 0)
                {
                    if (EditorUtility.DisplayDialog(porName[81], porName[82] + buttonMeshRender + porName[83] + "\n\n" + porName[84] + "\n\n" + porName[85], porName[23], porName[24])) { }
                }
            }
        }

        #endregion

        static void Translate(int language = 0, bool Iint = false)
        {
            //Debug.Log("当前ID：" + languageID);
            if (Iint)
            {
                if (porName == null)
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
            else
            {
                EditorUserSettings.SetConfigValue("languageID", languageID.ToString());
                if (languageID == 0) porName = chinese;
                else if (languageID == 1) porName = english;
                else if (languageID == 2) porName = japanese;
            }
        }
    }
}


