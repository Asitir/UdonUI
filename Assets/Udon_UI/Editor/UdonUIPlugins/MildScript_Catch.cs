
//using UdonSharp;
using UnityEngine;
using UnityEditor;
using UdonUI;
//using VRC.SDKBase;
//using VRC.Udon;

namespace UdonUI_Editor {
    public class MildScript_Catch2 : Editor
    {
        [MenuItem("Asitir_Tool/剧本杀插件/鬼抓人")]
        static public void SetMgConsole()
        {
            CatchPeopleMG _cmg = UdonUI_Manager.Instance.FindObjects<CatchPeopleMG>();
            if (!_cmg)
            {
                GameObject _CatchPeople = AssetDatabase.LoadAssetAtPath<GameObject>(UdonUI_Manager.Instance.Path_SamplePrefad + "CatchPeople.prefab");
                GameObject _target = Instantiate(_CatchPeople);
                _target.name = "CatchPeople";
                _cmg = _target.GetComponent<CatchPeopleMG>();
                CatchPeoplePop _cmg_L = _target.transform.GetChild(0).GetComponent<CatchPeoplePop>();
                CatchPeoplePop _cmg_R = _target.transform.GetChild(1).GetComponent<CatchPeoplePop>();

                _cmg.mainUI = UdonUI_Manager.Instance.mainUI;
                _cmg.mainChat = UdonUI_Manager.Instance.udonChat;
                _cmg.eventsID = UdonUI_Manager.Instance.mainUI.Button.Length;
                _cmg_L.isLeft = true;
                _cmg_L.mainMG = _cmg;
                _cmg_R.isLeft = false;
                _cmg_R.mainMG = _cmg;

                UdonUI_Manager.Instance.AddUdonUIButton(_target.transform.GetChild(2).gameObject);
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 0, 32, _target, "OnSetStop");
                UdonUI_Manager.Instance.SetUdonUIButtonEvent(UdonUI_Manager.Instance.mainUI.Button.Length - 1, 1, 32, _target, "OnSetCatch");

                Selection.activeGameObject = _cmg.gameObject;
                EditorGUIUtility.PingObject(_cmg.gameObject);
            }
            else
            {
                EditorUtility.DisplayDialog("已创建鬼抓人系统，请勿重复创建", "我知道了", "关闭");
                Selection.activeGameObject = _cmg.gameObject;
                EditorGUIUtility.PingObject(_cmg.gameObject);
            }
        }
    }
}
