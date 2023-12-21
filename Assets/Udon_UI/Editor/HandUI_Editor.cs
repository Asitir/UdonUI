using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRC.Udon;
using UdonUI;

namespace UdonUI_Editor
{
    public class HandUI_Editor : EditorWindow
    {
        #region Language
        static string[] chinese = new string[] {
            "UdonUI手势菜单1",//0
            "主要菜单(本地用)2",//1
            "次菜单(同步用)3",//2
            "打开菜单的动画名4",//3
            "打开菜单后待机的动画名5",//4
            "结束动画的动画名6",//5
            "是否开启菜单音效7",//6
            "打开菜单时的音效8",//7
            "关闭菜单时的音效9",//8
            "如果想弃用手势菜单，直接到UdonUI_AGI下删除整个SAOUI即可1",//9
            "请创建UdonUI手势菜单2",//10
            "你可以在【UdonUI主菜单】弹窗里面创建手势菜单3",//11
            "请先创建UdonUI辅助属性4",//12
            "你可以在【UdonUI主菜单】弹窗里面创建辅助属性5",//13
            ""
        };
        static string[] english = new string[] {
            "UdonUI手势菜单",//0
            "主要菜单(本地用)",//1
            "次菜单(同步用)",//2
            "打开菜单的动画名",//3
            "打开菜单后待机的动画名",//4
            "结束动画的动画名",//5
            "是否开启菜单音效",//6
            "打开菜单时的音效",//7
            "关闭菜单时的音效",//8
            "如果想弃用手势菜单，直接到UdonUI_AGI下删除整个SAOUI即可",//9
            "请创建UdonUI手势菜单",//10
            "你可以在【UdonUI主菜单】弹窗里面创建手势菜单",//11
            "请先创建UdonUI辅助属性",//12
            "你可以在【UdonUI主菜单】弹窗里面创建辅助属性",//13
            ""
        };
        static string[] japanese = new string[] {
            "UdonUIジェスチャーメニュー",//0
            "メインメニュー（ローカル用）",//1
            "サブメニュー（同期用）",//2
            "メニューのアニメーション名を開き",//3
            "ニューを開いて待機している動画の名前",//4
            "終了アニメーションのアニメーション名",//5
            "メニューサウンドをオンにするかどうか",//6
            "メニューを開いたときのサウンド",//7
            "メニューを閉じたときのサウンド",//8
            "ジェスチャーメニューを破棄したい場合は、UdonUI_AGIのパスに移動してSAOUI全体を削除してください",//9
            "UdonUIジェスチャーメニューを作成してください",//10
            "UdonUIメインメニューのウィンドウでジェスチャーメニューを作成できます",//11
            "まずUdonUI補助プロパティを作成してください",//12
            "UdonUIメインメニューのウィンドウで補助プロパティを作成できます",//13
            ""

        };

        static string[] porName;
        static int languageID = 0;

        #endregion
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
            GetLanguage();
            //porName = japanese;
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
                    //HandMotion_UI mainscript = MainObj.transform.Find("SAOUI").GetComponent<HandMotion_UI>();
                    mainscript.publicVariables.TryGetVariableValue("MainGameobject", out MainGameobject);
                    mainscript.publicVariables.TryGetVariableValue("MainGameobject2", out MainGameobject2);
                    mainscript.publicVariables.TryGetVariableValue("startname", out startname);
                    mainscript.publicVariables.TryGetVariableValue("standname", out standname);
                    mainscript.publicVariables.TryGetVariableValue("endname", out endname);
                    mainscript.publicVariables.TryGetVariableValue("Audio_on", out Audio_on);
                    mainscript.publicVariables.TryGetVariableValue("MainAudio1", out MainAudio1);
                    mainscript.publicVariables.TryGetVariableValue("MainAudio2", out MainAudio2);
                    //MainGameobject = mainscript.MainGameobject;


                    SAOobj = MainObj.transform.Find("SAOUI");
                    GUILayout.Space(10);
                    GUI.skin.label.fontSize = 24;
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label(porName[0]);

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_MainGameobject, new GUIContent(porName[1], porName[1]));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("MainGameobject", MainGameobject);
                    }

                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_MainGameobject2, new GUIContent(porName[2], porName[2]));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("MainGameobject2", MainGameobject2);
                    }

                    GUILayout.Space(10);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_startname, new GUIContent(porName[3], porName[3]));
                    EditorGUILayout.PropertyField(_standname, new GUIContent(porName[4], porName[4]));
                    EditorGUILayout.PropertyField(_endname, new GUIContent(porName[5], porName[5]));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("startname", startname);
                        mainscript.publicVariables.TrySetVariableValue("standname", standname);
                        mainscript.publicVariables.TrySetVariableValue("endname", endname);
                    }

                    GUILayout.Space(10);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(_Audio_on, new GUIContent(porName[6], porName[6]));
                    if (EditorGUI.EndChangeCheck())
                    {
                        _mobj.ApplyModifiedProperties();
                        mainscript.publicVariables.TrySetVariableValue("Audio_on", Audio_on);
                    }

                    if (Audio_on)
                    {
                        EditorGUI.BeginChangeCheck();
                        EditorGUILayout.PropertyField(_MainAudio1, new GUIContent(porName[7], porName[7]));
                        EditorGUILayout.PropertyField(_MainAudio1, new GUIContent(porName[8], porName[8]));
                        if (EditorGUI.EndChangeCheck())
                        {
                            _mobj.ApplyModifiedProperties();
                            mainscript.publicVariables.TrySetVariableValue("MainAudio1", MainAudio1);
                            mainscript.publicVariables.TrySetVariableValue("MainAudio2", MainAudio2);
                        }
                    }

                    GUILayout.Space(20);
                    GUI.skin.label.fontSize = 12;
                    GUILayout.Label(porName[9]);
                }
                else
                {
                    GUILayout.Space(10);
                    GUI.skin.label.fontSize = 24;
                    GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                    GUILayout.Label(porName[10]);
                    GUILayout.Label(porName[11]);
                }
            }
            else
            {
                GUILayout.Space(10);
                GUI.skin.label.fontSize = 24;
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label(porName[12]);
                GUILayout.Label(porName[13]);

            }

            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUI.skin.label.fontSize = 12;
        }

        static void GetLanguage()
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
