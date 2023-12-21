using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonUI;
using UnityEngine.UI;

namespace UdonUI_Editor
{
    public class UdonUI_CreateMap : EditorWindow
    {
        #region Value
        public Transform mapEditor, mapMain;//主操作台和主地图映射

        List<GameObject> propMain, propAgents;//会创建的道具和道具代理
        List<int> propNumber;
        List<bool> propRefer;

        static UdonUI_CreateMap mainWindows = null;

        Vector2 drawBody;
        bool heatGroup = true;
        UdonUI_MapEditor mainEditor;
        //MainUI_Script mainUI = null;
        Transform mainEditorTrans;
        Transform propsArray, propsArrayRefer;
        #endregion

        #region Behaviour
        //[MenuItem("Asitir_Tool/CreateMap")]
        public static void OpenWindow()
        {
            mainWindows = (UdonUI_CreateMap)GetWindow(typeof(UdonUI_CreateMap), true);
        }

        private void OnEnable()
        {
            propMain = new List<GameObject>();
            propAgents = new List<GameObject>();
            propNumber = new List<int>();
            propRefer = new List<bool>();
        }

        private void OnGUI()
        {
            if(!UdonUI_Manager.Instance.mainUI)
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
            GUILayout.Label("创建地图编辑器");
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Space(20);
            GuiInit();

            Draw_Head();
            GUILayout.Space(20);

            drawBody = GUILayout.BeginScrollView(drawBody);
            Draw_Body();
            GUILayout.EndScrollView();

            Draw_End();
            GUILayout.Space(20);
        }

        void GuiInit() {
            CheckMainEditor();       
        }
        #endregion

        #region Events
        void Draw_Head() {

            EditorGUILayout.BeginBuildTargetSelectionGrouping();
            GUILayout.BeginHorizontal();
            GUILayout.Label("编辑平面    :");
            mainEditor.mapEditor = (Transform)EditorGUILayout.ObjectField(mainEditor.mapEditor, typeof(Transform), true);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("地图主平面:");
            mainEditor.mapMain = (Transform)EditorGUILayout.ObjectField(mainEditor.mapMain, typeof(Transform), true);
            GUILayout.EndHorizontal();
            EditorGUILayout.EndBuildTargetSelectionGrouping();
        }

        void Draw_Body() {

            Color guiColor = GUI.color;
            GUI.color = Color.green;
            if (GUILayout.Button("添加可创建对象"))
            {//创建对象
                CreatProp();
            }
            GUI.color = guiColor;
            for (int i = 0; i < propNumber.Count; i++)
            {
                GUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();

                propMain[i] = (GameObject)EditorGUILayout.ObjectField(propMain[i], typeof(GameObject), true);
                propAgents[i] = (GameObject)EditorGUILayout.ObjectField(propAgents[i], typeof(GameObject), true);
                propNumber[i] = EditorGUILayout.IntField(propNumber[i]);

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

        }

        void Draw_End() {
            GUILayout.Label("游戏运行时的实时编辑界面，自动创建，可自定义外观，点击即可追踪所在位置");
            GUILayout.BeginHorizontal();
            //GUILayout.Label("实时编辑界面: ");
            EditorGUILayout.ObjectField(mainEditor.MainInterface, typeof(Transform), true);
            GUILayout.EndHorizontal();
            GUILayout.Label("PS：删除地图编辑器时此对象不会一起销毁，请先追踪此对象销毁后再销毁编辑器");
        }

        void CheckMainEditor() 
        {
            if (mainEditor == null)
            {
                GameObject mapEditorObj = GameObject.Find("/MapEditor");
                if (!mapEditorObj)
                {
                    mapEditorObj = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "MapEditor.prefab", typeof(GameObject)));
                    Undo.RegisterCreatedObjectUndo(mapEditorObj, "创建");
                    mapEditorObj.name = "MapEditor";
                    //EditorUtility.ClearProgressBar();
                    EditorUtility.DisplayDialog("警告", "检测到场景中不存在地图编辑器环境，已在场景中自动创建“MapEditor”，请勿删除或对其做任何改动！！！！", "我知道了", "关闭");
                    Selection.activeTransform = mapEditorObj.transform;
                }
                mainEditor = mapEditorObj.GetComponent<UdonUI_MapEditor>();
                mainEditorTrans = mapEditorObj.transform;
            }

            InitValue();

            if (mainEditor.MainInterface == null)
            {
                GameObject A = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "MapEditorSystem.prefab", typeof(GameObject)));
                Undo.RegisterCreatedObjectUndo(A, "创建");
                A.name = "MapEditorSystem";
                A.transform.parent = UdonUI_Manager.Instance.mainUI.transform;
                mainEditor.MainInterface = A.transform;
                ReSetMainInterface(A.transform);
            }
        }

        //初始化赋值
        void InitValue() {
            propsArrayRefer = mainEditorTrans.GetChild(0);// 代理
            propsArray = mainEditorTrans.GetChild(1);// 真实

            propMain.Clear();
            propAgents.Clear();
            propNumber.Clear();
            propRefer.Clear();

            foreach (GameObject obj in mainEditor.propMain) propMain.Add(obj);
            foreach (GameObject obj in mainEditor.propAgents) propAgents.Add(obj);
            foreach (int obj in mainEditor.propNumber) propNumber.Add(obj);
            foreach (bool obj in mainEditor.propRefer) propRefer.Add(obj);

            if (buttonRefer == null)
            {
                Transform _target = mainEditor.MainInterface;
                if (_target && _target.GetChild(0).childCount > 1)
                {
                    buttonRefer = _target.GetChild(0).GetChild(1).gameObject;
                }
            }
        }

        //写入地图编辑器数据
        void SetMapMainValue() {
            EditorUtility.ClearDirty(mainEditor);

            mainEditor.propMain = propMain.ToArray();
            mainEditor.propAgents = propAgents.ToArray();
            mainEditor.propNumber = propNumber.ToArray();
            mainEditor.propRefer = propRefer.ToArray();
            EditorUtility.SetDirty(mainEditor);
            Undo.RecordObject(mainEditor, "设定地图编辑器");
        }

        //创建对象
        void CreatProp()
        {
            GameObject newObj1 = new GameObject();//主要对象
            GameObject newObj2 = new GameObject();//激活主体
            GameObject newObj3 = new GameObject();//未激活主体
            GameObject newObj0 = new GameObject();//创建对象
            GameObject newObj1_r = new GameObject();//主要对象
            GameObject newObj2_r = new GameObject();//激活主体
            GameObject newObj3_r = new GameObject();//未激活主体
            //GameObject newObj0_r = new GameObject();//创建对象

            Undo.RegisterCreatedObjectUndo(newObj1, "创建");
            Undo.RegisterCreatedObjectUndo(newObj2, "创建");
            Undo.RegisterCreatedObjectUndo(newObj3, "创建");
            Undo.RegisterCreatedObjectUndo(newObj0, "创建");
            Undo.RegisterCreatedObjectUndo(newObj1_r, "创建");
            Undo.RegisterCreatedObjectUndo(newObj2_r, "创建");
            Undo.RegisterCreatedObjectUndo(newObj3_r, "创建");
            //Undo.RegisterCreatedObjectUndo(newObj0_r, "创建");

            newObj1.transform.parent = propsArray;
            newObj2.transform.parent = newObj1.transform;
            newObj3.transform.parent = newObj1.transform;
            newObj0.transform.parent = newObj3.transform;
            newObj1_r.transform.parent = propsArrayRefer;
            newObj2_r.transform.parent = newObj1_r.transform;
            newObj3_r.transform.parent = newObj1_r.transform;
            //newObj0_r.transform.parent = newObj3_r.transform;

            //newObj1.name = "GameObject" + newObj1.transform.GetInstanceID();
            //newObj1.name = "GameObject " + newObj1.transform.GetSiblingIndex();
            newObj1.name = "GameObject " + propsArray.childCount;
            newObj2.name = "Life";
            newObj3.name = "Die";
            newObj0.name = "GameObject " + newObj3.transform.childCount;
            newObj1_r.name = "GameObject " + propsArrayRefer.childCount;
            newObj2_r.name = "Life";
            newObj3_r.name = "Die";
            //newObj0_r.name = "GameObject " + newObj0_r.transform.GetSiblingIndex();

            newObj3.SetActive(false);
            newObj3_r.SetActive(false);

            propMain.Add(newObj0);
            propAgents.Add(null);
            propNumber.Add(1);
            propRefer.Add(false);

            ButtonRevis(mainEditor.MainInterface, 1);//新增一个按钮
            SetMapMainValue();
        }

        //修改对象
        void ReviseProp(int id) 
        {
            if (propNumber[id] < 1)
                propNumber[id] = 1;

            if(propMain[id] == null)
            {
                RemoveProp(id);
                return;
            }//主对象一定不能为空

            if (propAgents[id] != mainEditor.propAgents[id])
            {
                //Debug.Log("修改了反射对象");

                Transform target = propsArrayRefer.GetChild(id).GetChild(1);
                int length = target.childCount;
                for (int i = 0; i < length; i++)
                    Undo.DestroyObjectImmediate(target.GetChild(0).gameObject);
                if (propAgents[id] == null)
                {
                    propRefer[id] = false;//是否启用代理对象
                }
                else
                {
                    propRefer[id] = true;//是否启用代理对象

                    for (int i = 0; i < propNumber[id]; i++)
                    {
                        GameObject A = Instantiate(propAgents[id], target);
                        Undo.RegisterCreatedObjectUndo(A, "创建场景对象");
                        A.name = propAgents[id].name + target.childCount;
                    }
                    target.parent.name = propAgents[id].name + target.GetSiblingIndex();
                }
            }//修改代理对象
            else if (propMain[id] != mainEditor.propMain[id])
            {
                //Debug.Log("修改了对象");

                Transform target = propsArray.GetChild(id).GetChild(1);
                int length = target.childCount;
                for (int i = 0; i < length; i++)
                    Undo.DestroyObjectImmediate(target.GetChild(0).gameObject);

                for (int i = 0; i < propNumber[id]; i++)
                {
                    GameObject A = Instantiate(propMain[id], target);
                    Undo.RegisterCreatedObjectUndo(A, "创建场景对象");
                    A.name = propMain[id].name + target.childCount;
                }
                target.parent.name = propMain[id].name + target.GetSiblingIndex();
                ButtonReName(mainEditor.MainInterface, id);
            }//修改场景对象
            else if(propNumber[id] != mainEditor.propNumber[id])
            {//重新生成所有对象
                //Debug.Log("修改了数量");
                int compare = propNumber[id] - mainEditor.propNumber[id];
                if (compare > 0)
                {//添加对象

                    Transform target = propsArrayRefer.GetChild(id).GetChild(1);
                    Transform target2 = propsArray.GetChild(id).GetChild(1);
                    for (int i = 0; i < compare; i++)
                    {
                        GameObject B = Instantiate(propMain[id], target2);
                        Undo.RegisterCreatedObjectUndo(B, "创建场景对象");
                        B.name = propMain[id].name + target2.childCount;

                        if (propRefer[id])
                        {
                            GameObject A = Instantiate(propAgents[id], target);
                            Undo.RegisterCreatedObjectUndo(A, "创建反射对象");
                            A.name = propAgents[id].name + target.childCount;
                        }
                    }
                }
                else
                {//删除对象
                    compare *= -1;
                    Transform target2 = propsArrayRefer.GetChild(id).GetChild(1);
                    Transform target = propsArray.GetChild(id).GetChild(1);
                    for (int i = 0; i < compare; i++)
                    {
                        if (propRefer[id])
                            Undo.DestroyObjectImmediate(target2.GetChild(target2.childCount - 1).gameObject);
                        Undo.DestroyObjectImmediate(target.GetChild(target.childCount - 1).gameObject);
                        
                    }
                }
            }//修改数量

            SetMapMainValue();//应用
        }

        //删除对象
        void RemoveProp(int id) 
        {
            Undo.DestroyObjectImmediate(propsArrayRefer.GetChild(id).gameObject);// 代理
            Undo.DestroyObjectImmediate(propsArray.GetChild(id).gameObject);// 真实

            propMain.RemoveAt(id);
            propAgents.RemoveAt(id);
            propNumber.RemoveAt(id);
            propRefer.RemoveAt(id);
            ButtonRemove(mainEditor.MainInterface,id);//删除按钮

            SetMapMainValue();
        }

        //创建按钮
        GameObject buttonRefer;//参考按钮
        int vNumber = 5;
        //Vector2 vDis = new Vector2(0.00417f, -0.015f);
        Vector2 buttonStartPos;
        void ReSetMainInterface(Transform _Interface) 
        {
            Transform function = _Interface.GetChild(2);//功能按钮
            Transform objects = _Interface.GetChild(3).GetChild(0);//对象池创建按钮
            buttonRefer = Instantiate(_Interface.GetChild(3).GetChild(0).GetChild(0).gameObject, _Interface.GetChild(0));
            Undo.RegisterCreatedObjectUndo(buttonRefer, "创建");
            buttonRefer.SetActive(false);
            buttonStartPos = objects.GetChild(0).localPosition;

            ButtonRevis(_Interface, propsArray.childCount - GetAllButton(_Interface));//补正按钮数量
            ButtonReName(_Interface);//重置名字
        }

        /// <summary>
        /// 修改按钮数量
        /// </summary>
        /// <param name="_Interface">控制面板</param>
        /// <param name="_number">删除或者增加数量</param>
        void ButtonRevis(Transform _Interface, int _number)
        {
            if (_number == 0) return;

            Transform objects = _Interface.GetChild(3).GetChild(_Interface.GetChild(3).childCount - 1);//获取最后一页

            if (_number > 0)
            {//追加按钮
                for (int i = 0; i < _number; i++)
                {
                    int _buttonNumber = objects.childCount;
                    int _hNumber = _buttonNumber / vNumber;//列数
                    int _vNumber = _buttonNumber - (_hNumber * vNumber);//行数
                    GameObject A = Instantiate(buttonRefer, objects);
                    A.SetActive(true);
                    A.transform.rotation = objects.rotation;
                    Vector2 startPos = objects.childCount > 0 ? (Vector2)objects.GetChild(0).localPosition : buttonStartPos;
                    Vector2 setPos = startPos + new Vector2(_hNumber * 0.0417f, _vNumber * -0.015f);
                    bool isOverlap = false;
                    for (int t = 0; t < objects.childCount; t++)
                    {//避免重叠
                        if((setPos - (Vector2)objects.GetChild(t).localPosition).sqrMagnitude < 0.0001f)
                        {
                            isOverlap = true;
                            Debug.LogWarning("检测到新增按钮和旧按钮重叠，已关闭自动排序");
                            break;
                        }
                    }
                    if (isOverlap)
                        A.transform.localPosition = (Vector2)objects.GetChild(objects.childCount - 2).localPosition + new Vector2(0.01f, -0.015f);
                    else
                        A.transform.localPosition = setPos;
                    ADDUdonUIButton(A);
                    ButtonReName(mainEditor.MainInterface, GetAllButton(_Interface) - 1);
                    Undo.RegisterCreatedObjectUndo(A, "创建按钮");
                }
            }
            else
            {//缩减按钮
                for (int i = 0; i < -_number; i++)
                    Undo.DestroyObjectImmediate(objects.GetChild(objects.childCount - 1).gameObject);
            }
        }

        //重置所有按钮名字
        void ButtonReName(Transform _Interface)
        {
            Transform objects = _Interface.GetChild(3);//对象池创建按钮

            int nowID = 0;
            for (int i = 0; i < objects.childCount; i++)
            {
                for(int t =0;t< objects.GetChild(i).childCount; t++)
                {
                    string _name = mainEditor.propAgents[nowID + i].name;
                    objects.GetChild(i).GetChild(t).name = _name;
                    objects.GetChild(i).GetChild(t).GetChild(0).GetComponent<Text>().text = _name;
                }
                nowID += objects.GetChild(i).childCount;//累加每个组的数量
            }

        }
        void ButtonReName(Transform _Interface,int _ID)
        {
            Transform objects = _Interface.GetChild(3);//对象池创建按钮

            int nowID = 0;
            for (int i = 0; i < objects.childCount; i++)
            {
                int nowID2 = objects.GetChild(i).childCount + nowID;//累加每个组的数量
                if (nowID2 > _ID)
                {
                    string _name = propMain[_ID].name;
                    objects.GetChild(i).GetChild(_ID - nowID).name = _name;
                    objects.GetChild(i).GetChild(_ID - nowID).GetChild(0).GetComponent<Text>().text = _name;
                    break;
                }
                else
                {
                    nowID = nowID2;

                }
            }

        }

        //删除指定按钮
        void ButtonRemove(Transform _Interface, int _ID)
        {
            Transform objects = _Interface.GetChild(3);//对象池创建按钮

            int nowID = 0;
            for (int i = 0; i < objects.childCount; i++)
            {
                int nowID2 = objects.GetChild(i).childCount + nowID;//累加每个组的数量
                if (nowID2 > _ID)
                {
                    Undo.DestroyObjectImmediate(objects.GetChild(i).GetChild(_ID - nowID).gameObject);
                    break;
                }
                else
                {
                    nowID = nowID2;
                }
            }
        }

        /// <summary>
        /// 添加常规物体创建的按钮
        /// </summary>
        /// <param name="_target">按钮</param>
        void ADDUdonUIButton(GameObject _target) 
        { 
            
        }

        //获取按钮数量
        int GetAllButton(Transform _Interface) {
            Transform objects = _Interface.GetChild(3);//对象池创建按钮
            int _number = 0;
            for(int i=0;i< objects.childCount; i++)
            {
                _number += objects.GetChild(i).childCount;
            }
            return _number;
        }
        #endregion
    }
}
