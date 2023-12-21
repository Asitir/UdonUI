using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Udon;

namespace UdonUI_Editor
{
    public class HeadUI_Editor : EditorWindow
    {
        static Transform SAOobj;

        [SerializeField]
        bool Audio_on = false;

        [SerializeField]
        GameObject MainGameobject, MainGameobject2;//主菜单，次菜单
        [SerializeField]
        string startname, standname, endname;
        [SerializeField]
        AudioClip MainAudio1, MainAudio2;

        static GameObject MainObj;
        SerializedObject _mobj;
        SerializedProperty _MainGameobject, _MainGameobject2;
        SerializedProperty _startname, _standname, _endname;
        SerializedProperty _Audio_on;
        SerializedProperty _MainAudio1, _MainAudio2;

        private void OnEnable()
        {
            _mobj = new SerializedObject(this);
            _MainGameobject = _mobj.FindProperty("MainGameobject");
            _MainGameobject2 = _mobj.FindProperty("MainGameobject2");
            _startname = _mobj.FindProperty("startname");
            _standname = _mobj.FindProperty("standname");
            _endname = _mobj.FindProperty("endname");
            _Audio_on = _mobj.FindProperty("Audio_on");
            _MainAudio1 = _mobj.FindProperty("MainAudio1");
            _MainAudio2 = _mobj.FindProperty("MainAudio2");

        }

        private void OnGUI()
        {
            if (GameObject.Find("UdonUI_AGI"))
            {
                MainObj = GameObject.Find("UdonUI_AGI");
                if (MainObj.transform.Find("SAOUI"))
                {//地图中存在该物体
                    _mobj.Update();

                    UdonBehaviour mainscript = MainObj.transform.Find("SAOUI").GetComponent<UdonBehaviour>();
                    mainscript.publicVariables.TryGetVariableValue("MainGameobject", out MainGameobject);
                    mainscript.publicVariables.TryGetVariableValue("MainGameobject2", out MainGameobject2);
                    mainscript.publicVariables.TryGetVariableValue("startname", out startname);
                    mainscript.publicVariables.TryGetVariableValue("standname", out standname);
                    mainscript.publicVariables.TryGetVariableValue("endname", out endname);
                    mainscript.publicVariables.TryGetVariableValue("Audio_on", out Audio_on);
                    mainscript.publicVariables.TryGetVariableValue("MainAudio1", out MainAudio1);
                    mainscript.publicVariables.TryGetVariableValue("MainAudio2", out MainAudio2);


                    SAOobj = MainObj.transform.Find("SAOUI");
                    GUILayout.Space(10);
                    GUI.skin.label.fontSize = 24;
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label("UdonUI手势菜单");

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_MainGameobject, new GUIContent("主要菜单(本地用)"));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("MainGameobject", MainGameobject);
                    }

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_MainGameobject2, new GUIContent("次菜单(同步用)"));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("MainGameobject2", MainGameobject2);
                    }

                    GUILayout.Space(10);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_startname, new GUIContent("打开菜单的动画名"));
                    EditorGUILayout.PropertyField(_standname, new GUIContent("打开菜单后待机的动画名"));
                    EditorGUILayout.PropertyField(_endname, new GUIContent("结束动画的动画名"));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("startname", startname);
                        mainscript.publicVariables.TrySetVariableValue("standname", standname);
                        mainscript.publicVariables.TrySetVariableValue("endname", endname);
                    }

                    GUILayout.Space(10);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_Audio_on, new GUIContent("是否开启菜单音效"));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("Audio_on", Audio_on);
                    }

                    if (Audio_on)
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_MainAudio1, new GUIContent("打开菜单时的音效"));
                        EditorGUILayout.PropertyField(_MainAudio1, new GUIContent("关闭菜单时的音效"));
                        if (EditorGUI.EndChangeCheck())
                        {
                            _mobj.ApplyModifiedProperties();
                            mainscript.publicVariables.TrySetVariableValue("MainAudio1", MainAudio1);
                            mainscript.publicVariables.TrySetVariableValue("MainAudio2", MainAudio2);
                        }
                    }

                    GUILayout.Space(20);
                    GUI.skin.label.fontSize = 12;
                    GUILayout.Label("如果想弃用手势菜单，直接到UdonUI_AGI下删除整个SAOUI即可");
                }
                else
                {
                    GUILayout.Space(10);
                    GUI.skin.label.fontSize = 24;
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label("请创建UdonUI手势菜单");
                    GUILayout.Label("你可以在【UdonUI主菜单】弹窗里面创建手势菜单");
                }
            }
            else
            {
                GUILayout.Space(10);
                GUI.skin.label.fontSize = 24;
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label("请先创建UdonUI辅助属性");
                GUILayout.Label("你可以在【UdonUI主菜单】弹窗里面创建辅助属性");

            }

            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUI.skin.label.fontSize = 12;
        }
    }
}