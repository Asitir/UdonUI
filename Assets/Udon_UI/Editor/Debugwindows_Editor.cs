using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace UdonUI_Editor
{
    public class Debugwindows_Editor : EditorWindow
    {
        [SerializeField]
        public string MainHead, MainHead_BoxCollider;
        [SerializeField]
        public string MainTex, MainListHead, MainTex_BoxCollider, MainListHead_BoxCollider;
        [SerializeField]
        public GameObject[] MainObjn, MainObjn_BoxCollider;
        [SerializeField]
        List<GameObject> MainObj, MainObj_BoxCollider;
        ReorderableList _MainObj, _MainObj_BoxCollider;//List列表
        [SerializeField]
        public bool ErrBotton = false, ErrBoxCollider = false;
        [SerializeField]
        Vector2 Hdt;
        SerializedObject _mobj;
        private void OnEnable()
        {
            _mobj = new SerializedObject(this);
            MainObj = new List<GameObject>();
            MainObj_BoxCollider = new List<GameObject>();

            //if (ErrBotton)
            {
                _MainObj = new ReorderableList(_mobj, _mobj.FindProperty("MainObj"), false, true, false, false);
                _MainObj.drawHeaderCallback = (Rect rect) =>
                {//List列表命名
                GUI.Label(rect, MainListHead);
                };
                _MainObj.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
                {
                    SerializedProperty item = _MainObj.serializedProperty.GetArrayElementAtIndex(index);
                    rect.height = EditorGUIUtility.singleLineHeight;
                    rect.y += 2;
                    EditorGUI.PropertyField(rect, item, new GUIContent("按钮:" + (index + 1)));
                };
            }
            //if (ErrBoxCollider)
            {
                _MainObj_BoxCollider = new ReorderableList(_mobj, _mobj.FindProperty("MainObj_BoxCollider"), false, true, false, false);
                _MainObj_BoxCollider.drawHeaderCallback = (Rect rect) =>
                {//List列表命名
                GUI.Label(rect, MainListHead_BoxCollider);
                };
                _MainObj_BoxCollider.drawElementCallback = (Rect rect, int index, bool selected, bool focu) =>
                {
                    SerializedProperty item = _MainObj_BoxCollider.serializedProperty.GetArrayElementAtIndex(index);
                    rect.height = EditorGUIUtility.singleLineHeight;
                    rect.y += 2;
                    EditorGUI.PropertyField(rect, item, new GUIContent("触发器:" + (index + 1)));
                };

            }
        }

        private void OnGUI()
        {
            _mobj.Update();
            GUILayout.Space(10);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(MainHead);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Space(10);
            GUILayout.Label(MainTex);
            if (GUILayout.Button("Open UdonUI Properties menu"))
                GetWindow(typeof(Udon_Em), true, "UdonUIProperty");

            Hdt = GUILayout.BeginScrollView(Hdt);
            if (ErrBotton)
            {
                MainObj.Clear();
                for (int i = 0; i < MainObjn.Length; i++)
                    MainObj.Add(MainObjn[i]);

                _MainObj.DoLayoutList();
            }

            if (ErrBoxCollider)
            {
                MainObj_BoxCollider.Clear();
                for (int i = 0; i < MainObjn_BoxCollider.Length; i++)
                    MainObj_BoxCollider.Add(MainObjn_BoxCollider[i]);

                _MainObj_BoxCollider.DoLayoutList();
            }
            GUILayout.EndScrollView();
        }
    }
}
