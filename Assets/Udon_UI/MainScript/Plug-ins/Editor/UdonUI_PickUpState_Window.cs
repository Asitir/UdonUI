using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonUI;

namespace UdonUI_Editor
{
    public class UdonUI_PickUpState_Window : EditorWindow
    {
        private Transform PickUpTarget = null;
        private GameObject recordTarget = null, stateObj = null;

        private GameObject CreactObj;

        [MenuItem("Asitir_Tool/剧本杀插件/收集品记录")]
        static void OpenWindow() 
        {
            GetWindow<UdonUI_PickUpState_Window>("收集品记录器").Show();
        }
        private void OnGUI()
        {
            DrawHead("收集品记录器(粗版)");
            PickUpTarget = (Transform)EditorGUILayout.ObjectField(PickUpTarget, typeof(Transform), true);
            if (GUILayout.Button("写入数据"))
            {
                bool _b = (PickUpTarget.childCount <= recordTarget.transform.childCount);
                for (int i = 0; i < PickUpTarget.childCount; i++)
                {
                    Transform _target = PickUpTarget.GetChild(i);
                    if(_target.TryGetComponent<UdonUI_PickUpState>(out var _com))
                    {
                        if (_b) _com.target = recordTarget.transform.GetChild(i).gameObject;
                        _com.onState = stateObj;
                    }
                    else
                    {
                        UdonUI_PickUpState _a = _target.gameObject.AddComponent<UdonUI_PickUpState>();
                        if (_b) _a.target = recordTarget.transform.GetChild(i).gameObject;
                        _a.onState = stateObj;
                    }
                }
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("状态绑定:", GUILayout.Width(60));
            recordTarget = (GameObject)EditorGUILayout.ObjectField(recordTarget, typeof(GameObject), true);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("开关:", GUILayout.Width(60));
            stateObj = (GameObject)EditorGUILayout.ObjectField(stateObj, typeof(GameObject), true);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            CreactObj = (GameObject)EditorGUILayout.ObjectField(CreactObj, typeof(GameObject), true);
            if (GUILayout.Button("创建绑定对象"))
            {
                for (int i = 0; i < PickUpTarget.childCount; i++)
                {
                    Transform _target = PickUpTarget.GetChild(i);
                    GameObject _a = Instantiate(CreactObj, recordTarget.transform);
                    _a.name = _target.name;
                }

            }
        }
        private void DrawHead(string _headName)
        {
            GUILayout.Space(20);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(_headName);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUILayout.Space(20);
        }

    }
}
