using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonUI;
using UnityEngine.UI;

namespace UdonUI_Editor
{
    public class UdonUI_SightInteract : EditorWindow
    {
        public enum Einteract
        {
            单次触发,
            始终绑定触发
        }
        public enum ESight
        {
            注视方向,
            参照物Z轴朝向
        }
        #region Value
        public Transform mapEditor, mapMain;//主操作台和主地图映射

        List<Transform> propMain;//视线参考对象
        List<int> interactType, SighttType;
        List<float> angle, radius;
        List<Einteract> Einteract_;
        List<ESight> ESight_;

        static UdonUI_SightInteract mainWindows = null;

        Vector2 drawBody;
        bool heatGroup = true;
        SightInteract mainEditor;
        MainUI_Script mainUI = null;
        Transform mainEditorTrans;
        //Transform propsArray, propsArrayRefer;
        #endregion

        #region Behaviour
        [MenuItem("Asitir_Tool/SightInteract")]
        public static void OpenWindow()
        {
            mainWindows = (UdonUI_SightInteract)GetWindow(typeof(UdonUI_SightInteract), true);
        }

        private void OnEnable()
        {
            propMain = new List<Transform>();
            interactType = new List<int>();
            SighttType = new List<int>();
            angle = new List<float>();
            radius = new List<float>();
            Einteract_ = new List<Einteract>();
            ESight_ = new List<ESight>();
        }

        private void OnGUI()
        {
            if (mainUI == null)
            {
                GameObject mainUIobj = UdonUI_Manager.Instance.mainUIobj;
                if (mainUIobj) mainUI = mainUIobj.GetComponent<MainUI_Script>();
            }

            if (mainUI == null)
            {
                GUILayout.Space(20);
                GUI.skin.label.fontSize = 24;
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("未检测到UdonUI必须环境，请先搭建环境");
                GUI.skin.label.fontSize = 12;
                GUI.skin.label.alignment = TextAnchor.MiddleLeft;
                GUILayout.Space(20);
                return;
            }

            GUILayout.Space(20);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("视线交互触发合集");
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Space(20);
            GuiInit();

            //Draw_Head();
            //GUILayout.Space(20);

            Draw_Body();

            //Draw_End();
            //GUILayout.Space(20);
        }

        void GuiInit()
        {
            if (mainEditor == null)
            {
                GameObject mapEditorObj = GameObject.Find("/SightInteract");
                if (!mapEditorObj)
                {
                    mapEditorObj = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "SightInteract.prefab", typeof(GameObject)));
                    Undo.RegisterCreatedObjectUndo(mapEditorObj, "创建");
                    mapEditorObj.name = "SightInteract";
                    EditorUtility.DisplayDialog("警告", "检测到场景中不存在视线交互环境，已在场景中自动创建“SightInteract”，请勿删除或对其做任何改动！！！！", "我知道了", "关闭");
                }
                mainEditor = mapEditorObj.GetComponent<SightInteract>();
                mainEditor.mainui = mainUI;
                mainEditorTrans = mapEditorObj.transform;
                Selection.activeTransform = mainEditorTrans;
            }

            InitValue();
        }
        #endregion

        #region Events
        void Draw_Head()
        {
        }

        void Draw_Body()
        {

            Color guiColor = GUI.color;
            GUI.color = Color.green;
            if (GUILayout.Button("添加视线交互对象"))
            {//创建对象
                CreatProp();
            }
            GUI.color = guiColor;
            drawBody = GUILayout.BeginScrollView(drawBody);
            for (int i = propMain.Count - 1; i > -1; i--)
            {
                GUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();

                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        propMain[i] = (Transform)EditorGUILayout.ObjectField(propMain[i], typeof(Transform), true);
                        Einteract_[i] = (Einteract)EditorGUILayout.EnumPopup(Einteract_[i]);
                        interactType[i] = (int)Einteract_[i] + 1;
                        ESight_[i] = (ESight)EditorGUILayout.EnumPopup(ESight_[i]);
                        SighttType[i] = (int)ESight_[i];
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("角度: ");
                        angle[i] = EditorGUILayout.FloatField(angle[i]);
                        GUILayout.Label("半径: ");
                        radius[i] = EditorGUILayout.FloatField(radius[i]);
                        //propAgents[i] = (Transform)EditorGUILayout.ObjectField(propAgents[i], typeof(Transform), true);
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();

                if (EditorGUI.EndChangeCheck())
                {
                    ReviseProp(i);
                }

                GUI.color = Color.red;
                if (GUILayout.Button("-"))
                {
                    RemoveProp(i);
                }
                GUILayout.EndHorizontal();
                GUI.color = guiColor;
            }
            GUILayout.EndScrollView();
        }

        void Draw_End()
        {
        }

        void CheckMainEditor()
        {
        }

        //初始化赋值
        void InitValue()
        {
            propMain.Clear();
            //propAgents.Clear();
            interactType.Clear();
            SighttType.Clear();
            angle.Clear();
            radius.Clear();
            Einteract_.Clear();
            ESight_.Clear();

            //foreach (Transform obj in mainEditor.targets) propMain.Add(obj);
            foreach (Transform obj in mainEditor.buttons) propMain.Add(obj);
            foreach (float obj in mainEditor.angle) angle.Add(obj);
            foreach (float obj in mainEditor.radius) radius.Add(obj);
            foreach (int obj in mainEditor.interactType)
            {
                interactType.Add(obj);
                Einteract_.Add(obj == 1 ? Einteract.单次触发 : Einteract.始终绑定触发);
            }
            foreach (int obj in mainEditor.SighttType)
            {
                SighttType.Add(obj);
                ESight_.Add(obj == 1 ? ESight.参照物Z轴朝向 : ESight.注视方向);
            }
        }

        //写入编辑器数据
        void SetMapMainValue()
        {
            EditorUtility.ClearDirty(mainEditor);

            //if (propMain.Count > 0)
            {
                GameObject _select = propMain[propMain.Count - 1].gameObject;
                if (_select != null)
                {
                    bool _isBoxCollider = false;
                    GameObject[] AllConllider = UdonUI_Manager.Instance.mainUI.BoxColliderUdon;
                    foreach (var item in AllConllider)
                    {
                        if (item == _select)
                        {
                            mainEditor.buttons = propMain.ToArray();
                            _isBoxCollider = true;
                            break;
                        }
                    }

                    if (!_isBoxCollider)
                    {
                        EditorUtility.DisplayDialog("警告", "当前设定对象并不是碰撞触发器", "我知道了");
                    }

                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "当前未选中任何对象", "我知道了");
                }
            }

            mainEditor.interactType = interactType.ToArray();
            mainEditor.SighttType = SighttType.ToArray();
            mainEditor.angle = angle.ToArray();
            mainEditor.radius = radius.ToArray();
            EditorUtility.SetDirty(mainEditor);
            Undo.RecordObject(mainEditor, "设定视线交互工具");
        }

        //创建对象
        void CreatProp()
        {
            GameObject _select = Selection.activeGameObject;
            if (_select != null)
            {
                if (propMain.Contains(_select.transform))
                {
                    EditorUtility.DisplayDialog("警告", "你已经添加过当前触发器", "我知道了");
                    return;
                }
                bool _isBoxCollider = false;
                GameObject[] AllConllider = UdonUI_Manager.Instance.mainUI.BoxColliderUdon;
                foreach (var item in AllConllider)
                {
                    if (item == _select)
                    {
                        //mainEditor.buttons = propMain.ToArray();
                        _isBoxCollider = true;
                        break;
                    }
                }

                if (_isBoxCollider)
                {
                    propMain.Add(_select.transform);
                    interactType.Add(1);
                    SighttType.Add(0);
                    angle.Add(-30.0f);
                    radius.Add(-5);
                    Einteract_.Add(Einteract.单次触发);
                    ESight_.Add(ESight.注视方向);
                    SetMapMainValue();
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "当前设定对象并不是碰撞触发器", "我知道了");
                }

            }
            else
            {
                EditorUtility.DisplayDialog("警告", "当前未选中任何对象", "我知道了");
            }
        }

        //修改对象
        void ReviseProp(int id)
        {
            SetMapMainValue();//应用
        }

        //删除对象
        void RemoveProp(int id)
        {
            if (propMain.Count == 1)
            {
                //Debug.LogWarning("请至少保留一个视线交互对象,你可以手动修改触发器对象");
                //清空
                mainEditor.buttons = new Transform[0];
                mainEditor.interactType = new int[0];
                mainEditor.SighttType = new int[0];
                mainEditor.angle = new float[0];
                mainEditor.radius = new float[0];
                EditorUtility.SetDirty(mainEditor);
                Undo.RecordObject(mainEditor, "设定视线交互工具");
                return;
            }
            propMain.RemoveAt(id);
            //propAgents.RemoveAt(id);
            interactType.RemoveAt(id);
            SighttType.RemoveAt(id);
            angle.RemoveAt(id);
            radius.RemoveAt(id);
            Einteract_.RemoveAt(id);
            ESight_.RemoveAt(id);

            SetMapMainValue();
        }
        #endregion

    }
}
