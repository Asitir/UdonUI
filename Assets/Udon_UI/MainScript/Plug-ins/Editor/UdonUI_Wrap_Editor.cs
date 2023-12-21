using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UdonUI_Editor
{
    [CustomEditor(typeof(UdonUI_Wrap))]
    public class UdonUI_Wrap_Editor : Editor
    {
        UdonUI_Wrap main { get { return target as UdonUI_Wrap; } }
        public override void OnInspectorGUI()
        {
            GUILayout.Label("点击解析自动装将此对象的按钮加载至当前UdonUI环境");

            GUILayout.Space(10);

            main.isFindToName = EditorGUILayout.Toggle("是否按名字查找按钮", main.isFindToName);//是否按名字查找
            EditorGUI.BeginChangeCheck();
            var DW = EditorUserSettings.GetConfigValue("Wrap_DW");
            bool dw_b = EditorGUILayout.Toggle("加载完成后是否弹窗", string.IsNullOrEmpty(DW));//是否显示弹窗
            if (EditorGUI.EndChangeCheck())
            {
                EditorUserSettings.SetConfigValue("Wrap_DW", dw_b  ? "" : "N");
            }

            if (GUILayout.Button("解析UdonUI按钮"))
            {
                if (!EditorUtility.IsPersistent(main))
                {
                    if (UdonUI_Manager.Instance.mainUI != null)
                    {
                        Transform[] _allObj = main.GetComponentsInChildren<Transform>();
                        //Debug.Log($"UdonUI_Button的数量: {main.allButton.Length}");
                        foreach (var _button in main.allButton)
                        {
                            GameObject _addTarget = null;

                            if (main.isFindToName || _button.ButtonIndex >= _allObj.Length || _button.ButtonIndex < 0)
                                _addTarget = FindObjToName(_allObj, _button.ButtonName,"按钮");
                            else
                                _addTarget = _allObj[_button.ButtonIndex].gameObject;

                            if (_addTarget)
                            {
                                UdonUI_Manager.Instance.AddUdonUIButton(_addTarget);
                                UdonUI_Manager.Instance.mainUI.synButton[UdonUI_Manager.Instance.mainUI.synButton.Length - 1] = _button.synButton;
                                for (int i = 0; i < _button.Action.Length; i++)
                                {
                                    int eventID = _button.MainEvent[i];
                                    if (eventID > 2 && eventID < 7)
                                    {
                                        Animator _target = null;
                                        if (main.isFindToName || _button.MainAnimatorsIndex[i]>= _allObj.Length || _button.MainAnimatorsIndex[i] < 0)
                                        {
                                            if (FindObjToName(_allObj, _button.MainAnimatorsName[i],"状态机").TryGetComponent<Animator>(out var _anim)) _target = _anim;
                                        }
                                        else
                                        {
                                            if (_allObj[_button.MainAnimatorsIndex[i]].TryGetComponent<Animator>(out var _anim)) _target = _anim;
                                        }
                                        if (_target == null) _target = _button.MainAnimators[i];

                                        UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, _button.Action[i], eventID, _target, _button.AnimaName[i]);
                                    }
                                    else
                                    {
                                        GameObject _target = null;
                                        if (main.isFindToName || _button.TargetIndex[i] >= _allObj.Length || _button.TargetIndex[i] < 0)
                                        {
                                            _target = FindObjToName(_allObj, _button.TargetName[i],"目标对象");
                                        }
                                        else
                                        {
                                            _target = _allObj[_button.TargetIndex[i]].gameObject;
                                        }
                                        if (_target == null) _target = _button.TargetGameObject[i];

                                        UdonUI_Manager.Instance.SetUdonUIButtonEvent(-1, _button.Action[i], eventID, _target, _button.AnimaName[i]);
                                    }
                                }
                            }
                        }
                        if (dw_b)
                            EditorUtility.DisplayDialog("通知", "已成功装配UdonUI", "我知道了");
                        DestroyImmediate(main);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("警告", "请创建UdonUI环境", "我知道了");
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("警告", "请拖拽到场景后再操作", "我知道了");
                }
            }
            //base.DrawDefaultInspector();
        }

        private GameObject FindObjToName(Transform[] _objs, string _name,string debugType)
        {
            foreach (var item in _objs)
            {
                if(item.name == _name)
                    return item.gameObject;
            }
            //Debug.LogError($"未找到名为 {_name} 的" + debugType);
            return null;
        }
        public static void OnUdonUI_Warp() {
            if (UdonUI_Manager.Instance.mainUI != null)
            {
                foreach (var item in Selection.gameObjects)
                {
                    if (!EditorUtility.IsPersistent(item))
                    {
                        List<UdonUI_Button> _allButton = new List<UdonUI_Button>();

                        Transform[] _allObj = item.GetComponentsInChildren<Transform>();
                        GameObject[] _allButtons = UdonUI_Manager.Instance.mainUI.Button;
                        for (int i = _allButtons.Length - 1; i >= 0; i--)
                        {
                            foreach (var item2 in _allObj)
                            {
                                if(_allButtons[i].transform == item2)
                                {//找到了按钮
                                    _allButton.Add(UdonUI_Manager.Instance.GetUdonUI_Button(i, _allObj));
                                }
                            }
                        }
                        if (_allButton.Count > 0)
                        {
                            if (item.TryGetComponent<UdonUI_Wrap>(out var _udonWrap))
                            {
                                _udonWrap.allButton = _allButton.ToArray();
                                EditorUtility.SetDirty(_udonWrap);
                                EditorUtility.DisplayDialog("通知", "已更新打包内容", "我知道了");
                            }
                            else
                            {
                                var _warp = item.AddComponent<UdonUI_Wrap>();
                                _warp.allButton = _allButton.ToArray();
                                EditorUtility.SetDirty(_warp);
                                EditorUtility.DisplayDialog("通知", "已生成打包内容", "我知道了");
                            }
                        }
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("警告", "请创建UdonUI环境", "我知道了");
            }
        }
    }
}
