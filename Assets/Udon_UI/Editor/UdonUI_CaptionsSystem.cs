using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonUI;

namespace UdonUI_Editor
{
    public class UdonUI_CaptionsSystem : EditorWindow
    {
        private CaptionsSystem mMain;
        private CaptionsSystem Main
        {
            get
            {
                if (mMain == null)
                    mMain = UdonUI_Manager.Instance.FindObjects<CaptionsSystem>();
                return mMain;
            }
        }

        [MenuItem("Asitir_Tool/剧本杀插件/字幕系统")]
        public static void OnOpenWindow() 
        {
            GetWindow<UdonUI_CaptionsSystem>(false, "字幕系统");
        }

        private void OnGUI()
        {
            if (!DrawHead()) return;
            DrawBody();
        }

        private bool DrawHead() 
        {
            if (UdonUI_Manager.Instance.mainUI == null)
            {
                GUILayout.Label("请先创建UdonUI环境");
                return false;
            }

            if(Main == null)
            {
                GameObject _target = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(UdonUI_Manager.Instance.Path_SamplePrefad + "CaptionsSystem.prefab"));
                _target.name = "CaptionsSystem";
                mMain = _target.GetComponent<CaptionsSystem>();
                _target.transform.SetParent(UdonUI_Manager.Instance.mainUIobj.transform);
                Main.mainUI = UdonUI_Manager.Instance.mainUI;
                Undo.RegisterCreatedObjectUndo(_target,"创建对话系统");
            }

            if (GUILayout.Button("选中当前字幕系统"))
            {
                Selection.activeGameObject = Main.gameObject;
                EditorGUIUtility.PingObject(Main.gameObject);
            }
            return true;
        }

        private void DrawBody() 
        {
        }
    }
}
