
using UdonSharp;
using UnityEngine;
using UnityEditor;
using VRC.SDKBase;
using VRC.Udon;
using UnityEditorInternal;
using System.Collections.Generic;
using UdonUI;

namespace UdonUI_Editor
{
    public class UdonUI_RidingObjects : EditorWindow
    {
        bool displayList = true;
        Vector2 hxd;
        MainUI_Script mainui;
        UdonUI_Riding udonRiding = null;

        [SerializeField]
         List<Transform> RidingObjects, RidingObjects_Use;

        SerializedObject _Mobj;
        ReorderableList _RidingList;

        private void OnEnable()
        {
            _Mobj = new SerializedObject(this);
            RidingObjects = new List<Transform>();
            RidingObjects_Use = new List<Transform>();

            udonRiding = GameObject.Find("/UdonUI_Riding")?.GetComponent<UdonUI_Riding>();
            if (udonRiding != null)
            {
                foreach (Transform a in udonRiding.allRiding)
                {
                    RidingObjects_Use.Add(a);
                    RidingObjects.Add(a);
                }
            }

            _RidingList = new ReorderableList(_Mobj, _Mobj.FindProperty("RidingObjects"), false, false, false, false);
            _RidingList.drawHeaderCallback = (Rect rect) =>
            {//List列表命名
                GUI.Label(rect, "所有乘骑对象");
            };
            _RidingList.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
            {
                SerializedProperty item = _RidingList.serializedProperty.GetArrayElementAtIndex(index);
                rect.height = EditorGUIUtility.singleLineHeight;
                rect.y += 2;
                EditorGUI.PropertyField(rect, item, new GUIContent("乘骑对象: " + (index + 1)));
            };

        }
        private void OnGUI()
        {
            _Mobj.Update();
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label("乘骑系统");
            GUILayout.Space(10);

            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            if (GUILayout.Button("显示所有可乘骑对象")) displayList = !displayList;

            hxd = GUILayout.BeginScrollView(hxd);
            if (displayList) _RidingList.DoLayoutList();//绘制List列表

            GUILayout.BeginHorizontal();
            for(int i=0;i< RidingObjects_Use.Count; i++)
            {//检查列表是否有空对象
                if (RidingObjects_Use[i] == null)
                {
                    RidingObjects_Use.RemoveAt(i);
                    i--;
                }
            }
            if (GUILayout.Button("将所选对象添加到乘骑对象列表"))
            {
                Transform[] allTrans = Selection.transforms;
                foreach(Transform a in allTrans)
                {
                    if (!RidingObjects_Use.Contains(a))
                    {
                        RidingObjects_Use.Add(a);
                    }
                }

                RidingObjects.Clear();
                foreach (Transform a in RidingObjects_Use)
                    RidingObjects.Add(a);
                if (udonRiding)
                {
                    GameObject se = Selection.activeGameObject;
                    Selection.activeGameObject = udonRiding.gameObject;//很诡异的现象  不是选中这个对象的话无法写入参数
                    udonRiding.allRiding = RidingObjects_Use.ToArray();
                    udonRiding.allRidingActiv = new bool[RidingObjects_Use.Count];
                    udonRiding.allRidingChildID = new Vector2[RidingObjects_Use.Count];
                    //EditorUtility.ClearDirty(udonRiding.gameObject);
                    Selection.activeGameObject = se;
                    EditorUtility.SetDirty(udonRiding);
                }
            }
            if (GUILayout.Button("从列表中清除选中对象"))
            {
                Transform[] allTrans = Selection.transforms;
                foreach (Transform a in allTrans)
                {
                    if (RidingObjects_Use.Contains(a))
                        RidingObjects_Use.Remove(a);
                }

                RidingObjects.Clear();
                foreach (Transform a in RidingObjects_Use)
                    RidingObjects.Add(a);
                if (udonRiding)
                {
                    GameObject se = Selection.activeGameObject;
                    Selection.activeGameObject = udonRiding.gameObject;//很诡异的现象  不是选中这个对象的话无法写入参数
                    udonRiding.allRiding = RidingObjects_Use.ToArray();
                    udonRiding.allRidingActiv = new bool[RidingObjects_Use.Count];
                    udonRiding.allRidingChildID = new Vector2[RidingObjects_Use.Count];
                    Selection.activeGameObject = se;
                    //EditorUtility.ClearDirty(udonRiding.gameObject);
                    EditorUtility.SetDirty(udonRiding);
                }
                //udonRiding.gameObject;
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("从列表中自动生成乘骑对象并刷新参数"))
            {
                if(udonRiding == null)
                {
                    udonRiding = GameObject.Find("/UdonUI_Riding")?.GetComponent<UdonUI_Riding>();
                    if(udonRiding == null)
                        udonRiding = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_SamplePrefad + "UdonUI_Riding.prefab", typeof(GameObject))).GetComponent<UdonUI_Riding>();
                    udonRiding.gameObject.name = "UdonUI_Riding";
                }
                if (mainui == null)
                {
                    mainui = UdonUI_Manager.Instance.mainUI;
                    if(mainui == null)
                    {
                        EditorUtility.DisplayDialog("警告", "未找到UdonUI环境，请先配置你的第一个UdonUI按钮", "我知道了", "关闭");
                        return;
                    }
                    else
                    {
                        udonRiding.mainUI = mainui;
                    }
                }
                else
                {
                    udonRiding.mainUI = mainui;
                }
                GameObject se = Selection.activeGameObject;
                Selection.activeGameObject = udonRiding.gameObject;//很诡异的现象  不是选中这个对象的话无法写入参数
                udonRiding.allRiding = RidingObjects_Use.ToArray();
                SetRiding(ref udonRiding);
                Selection.activeGameObject = se;
                //EditorUtility.ClearDirty(udonRiding.gameObject);
                EditorUtility.SetDirty(udonRiding);
            }


            GUILayout.EndScrollView();
        }

        void SetRiding(ref UdonUI_Riding mRiding) 
        {
            bool isActivity = false;

            List<Transform> _allChild = new List<Transform>();//所有子级
            Vector2[] _allChildID = new Vector2[RidingObjects_Use.Count];//所有子级组


            //Camera VRCWorld = GameObject.Find("/VRCWorld")?.GetComponent<VRC_SceneDescriptor>()?.ReferenceCamera?.GetComponent<Camera>();
            Camera VRCWorld = null;
            GameObject ReferenceCamera = FindObjectOfType<VRC_SceneDescriptor>()?.ReferenceCamera;
            if (ReferenceCamera)
                VRCWorld = ReferenceCamera.GetComponent<Camera>();//消耗高些  但是有保障
            if (VRCWorld)
            {
                mRiding.cameClipPlane.x = VRCWorld.nearClipPlane;
                mRiding.cameClipPlane.y = VRCWorld.farClipPlane;
            }
            else
            {
                mRiding.cameClipPlane.x = 0.01f;
                mRiding.cameClipPlane.y = 100.0f;
            }


            int i = 0;
            foreach (Transform _trans in RidingObjects_Use)
            {//收集所有子级
                int _allChildLastLength = _allChild.Count;
                List<Collider> _collinder = new List<Collider>();
                foreach (Collider _collider in _trans.GetComponentsInChildren<Collider>()) 
                {
                    if (!_allChild.Contains(_collider.transform))
                    {
                        _allChild.Add(_collider.transform);//避免同一个transform挂载多个collider
                    }
                }
                _allChildID[i].x = _allChildLastLength;//起始ID
                _allChildID[i].y = _allChild.Count - _allChildLastLength;//数量
                i++;
            }
            mRiding.allRidingChildID = _allChildID;
            mRiding.allRidingChild = _allChild.ToArray();

            Transform refRoot = mRiding.transform.GetChild(1);
            int childCount = refRoot.childCount;
            for (i = 0; i < childCount; i++)
            {
                //DestroyImmediate(refRoot.GetChild(0).gameObject);//先清空所有子级
                Undo.DestroyObjectImmediate(refRoot.GetChild(0).gameObject);
            }

            i = 0;
            foreach (Transform _trans in RidingObjects_Use)
            {
                GameObject gameobject = new GameObject();
                Undo.RegisterCreatedObjectUndo(gameobject, "创建");
                //gameobject.name = "Ref_" + i + "_" + _trans.name;
                gameobject.name = "Ref_" + (_trans.parent ? _trans.parent.name : _trans.name);
                gameobject.transform.parent = refRoot;
                Vector3 startPos_ = new Vector3(-600, 600, -600);//-------------------------------------------------
                int number = 4;//行数量-------------------------------------
                int number2 = 300;//每行单位-------------------------------------
                int asd = (int)((float)i / number);
                startPos_.x += ((i - (asd * number)) * number2);
                startPos_.z += asd * number2;
                gameobject.transform.position = startPos_;

                //gameobject.transform.localScale = _trans.lossyScale;//设置初始反射缩放
                for (int c = 0; c < _allChildID[i].y; c++)
                {//得到参考对象的所有集合
                    int nowID = (int)_allChildID[i].x + c;
                    GameObject gameobject2 = new GameObject();
                    Undo.RegisterCreatedObjectUndo(gameobject2, "创建");
                    gameobject2.name = _allChild[nowID].name;
                    gameobject2.transform.parent = gameobject.transform;

                    Matrix4x4 refTransMatrix = _trans.worldToLocalMatrix * _allChild[nowID].localToWorldMatrix;//得到相对矩阵
                    Matrix4x4 getLocalMatrix = gameobject.transform.localToWorldMatrix * refTransMatrix;//得到映射后的最终矩阵
                    gameobject2.transform.SetPositionAndRotation(getLocalMatrix.GetColumn(3), getLocalMatrix.rotation);
                    gameobject2.transform.localScale = getLocalMatrix.lossyScale;

                    gameobject2.name = "Ref_" + i + "_" + _allChild[nowID].name;
#if true
                    foreach (Collider _collider in _allChild[nowID].GetComponents<Collider>())
                    {//复制粘贴所有碰撞对象
                        ComponentUtility.CopyComponent(_collider);
                        ComponentUtility.PasteComponentAsNew(gameobject2);
                    }
#else
                    foreach (Component _collider in _allChild[nowID].GetComponents<Component>())
                    {//复制粘贴所有碰撞对象
                        if (!(_collider as Transform))
                        {
                            ComponentUtility.CopyComponent(_collider);
                            ComponentUtility.PasteComponentAsNew(gameobject2);
                        }
                    }//测试用
#endif
                    _allChild[nowID].gameObject.layer = 8;
                    gameobject2.layer = 8;
                }
                i++;
            }



        }

    }
}
