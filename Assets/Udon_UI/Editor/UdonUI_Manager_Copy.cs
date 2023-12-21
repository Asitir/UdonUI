using System.Collections;
using System.Collections.Generic;
using UdonUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
// _instance = UdonUI_Editor.UdonUI_EditorManager; 

namespace UdonUI_Editor
{

    public partial class UdonUI_Manager
    {
        public UdonUI_Button UdonUI_Button_Copy;
        public UdonUI_Trigger UdonUI_Trigger_Copy;

        //复制按钮
        public void CopyButton(int _id)
        {
            UdonUI_Button_Copy = GetUdonUI_Button(_id);
        }
        //复制触发器
        public void CopyTrigger()
        {
            UdonUI_Trigger_Copy = null;
        }

        public void PastButton()
        {
            if (UdonUI_Button_Copy != null)
            {
                GameObject[] _allObj = Selection.gameObjects;
                foreach (var item in _allObj)
                {
                    if (!EditorUtility.IsPersistent(item))
                    {
                        for (int i = 0; i < mainUI.Button.Length; i++)
                        {
                            if (item == mainUI.Button[i])
                            {//删除
                                mainUI.Button[i] = null;
                                ChackUdonUIButton_Update(false,false);
                                break;
                            }
                        }
                        AddUdonUIButton(item);
                        mainUI.synButton[mainUI.synButton.Length - 1] = UdonUI_Button_Copy.synButton;
                        for (int i = 0; i < UdonUI_Button_Copy.Action.Length; i++)
                        {
                            int eventID = UdonUI_Button_Copy.MainEvent[i];
                            if (eventID > 2 && eventID < 7)
                                SetUdonUIButtonEvent(-1, UdonUI_Button_Copy.Action[i], eventID, UdonUI_Button_Copy.MainAnimators[i], UdonUI_Button_Copy.AnimaName[i]);
                            else
                                SetUdonUIButtonEvent(-1, UdonUI_Button_Copy.Action[i], eventID, UdonUI_Button_Copy.TargetGameObject[i], UdonUI_Button_Copy.AnimaName[i]);
                        }
                    }

                }
            }
        }
        public void PastTrigger()
        {
            if (UdonUI_Trigger_Copy != null)
            {
                GameObject[] _allObj = Selection.gameObjects;
                foreach (var item in _allObj)
                {
                    if (!EditorUtility.IsPersistent(item))
                    {
                        for (int i = 0; i < mainUI.BoxColliderUdon.Length; i++)
                        {
                            if (item == mainUI.BoxColliderUdon[i])
                            {//删除
                                mainUI.BoxColliderUdon[i] = null;
                                UdonBoxColliderUpdate(false);
                                break;
                            }
                        }
                        AddUdonUIButton(item);
                        for (int i = 0; i < UdonUI_Trigger_Copy.Action_BoxCollider.Length; i++)
                        {
                            int eventID = UdonUI_Trigger_Copy.MainEvent_BoxCollider[i];
                            if (eventID > 2 && eventID < 7)
                                SetUdonUIButtonEvent(-1, UdonUI_Trigger_Copy.Action_BoxCollider[i], eventID, UdonUI_Trigger_Copy.MainAnimators_BoxCollider[i], UdonUI_Trigger_Copy.AnimaName_BoxCollider[i]);
                            else
                                SetUdonUIButtonEvent(-1, UdonUI_Trigger_Copy.Action_BoxCollider[i], eventID, UdonUI_Trigger_Copy.TargetGameObject_BoxCollider[i], UdonUI_Trigger_Copy.AnimaName_BoxCollider[i]);
                        }
                    }

                }
            }
        }

        //获取当前索引的UdonUI按钮
        public UdonUI_Button GetUdonUI_Button(int _id, Transform[] findIndex = null)
        {
            if (mainUI)
            {
                if (_id < mainUI.Button.Length)
                {
                    UdonUI_Button _value = new UdonUI_Button();
                    Vector2 _range = mainUI.MainEventNumber[_id];
                    float eventLength = _range.y;
                    int eventLengthInt = (int)eventLength;
                    int startID = (int)_range.x;

                    _value.Button = mainUI.Button[_id];
                    _value.ButtonName = mainUI.Button[_id].name;
                    _value.ButtonIndex = -1;
                    _value.MainEventNumber = new Vector2(0, eventLength);
                    _value.ButtonType = mainUI.ButtonType[_id];
                    _value.synButton = mainUI.synButton[_id];

                    _value.Action = new int[eventLengthInt];
                    _value.MainEvent = new int[eventLengthInt];
                    _value.AnimaName = new string[eventLengthInt];
                    _value.MainAnimators = new Animator[eventLengthInt];
                    _value.MainAnimatorsName = new string[eventLengthInt];
                    _value.MainAnimatorsIndex = new int[eventLengthInt];
                    _value.TargetGameObject = new GameObject[eventLengthInt];
                    _value.TargetName = new string[eventLengthInt];
                    _value.TargetIndex = new int[eventLengthInt];
                    for (int i = 0; i < eventLengthInt; i++)
                    {
                        int nowID = startID + i;
                        _value.Action[i] = mainUI.Action[nowID];
                        _value.MainEvent[i] = mainUI.MainEvent[nowID];
                        _value.AnimaName[i] = mainUI.AnimaName[nowID];
                        _value.MainAnimators[i] = mainUI.MainAnimators[nowID];
                        if (mainUI.MainAnimators[nowID])
                            _value.MainAnimatorsName[i] = mainUI.MainAnimators[nowID].name;
                        else
                            _value.MainAnimatorsName[i] = "Empty";
                        _value.MainAnimatorsIndex[i] = -1;
                        _value.TargetGameObject[i] = mainUI.TargetGameObject[nowID];
                        if (mainUI.TargetGameObject[nowID])
                            _value.TargetName[i] = mainUI.TargetGameObject[nowID].name;
                        else
                            _value.TargetName[i] = "Empty";
                        _value.TargetIndex[i] = -1;
                    }
                    if (findIndex != null) 
                    {
                        _value.ButtonIndex = FindIndex(findIndex, mainUI.Button[_id].transform);
                        for (int i = 0; i < eventLengthInt; i++)
                        {
                            int nowID = startID + i;
                            if (mainUI.MainAnimators[nowID])
                                _value.MainAnimatorsIndex[i] = FindIndex(findIndex, mainUI.MainAnimators[nowID].transform);
                            if (mainUI.TargetGameObject[nowID])
                                _value.TargetIndex[i] = FindIndex(findIndex, mainUI.TargetGameObject[nowID].transform);
                        }
                    }

                    return _value;
                }
                else
                {
                    Debug.LogWarning("操作违规，检索ID超出索引上限");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("警告", "未创建UdonUI环境", "我知道了");
            }

            return null;
        }

        private int FindIndex(Transform[] _objs,Transform _target)
        {
            for (int i = 0; i < _objs.Length; i++)
            {
                if (_objs[i] == _target) return i;
            }
            return -1;
        }

        public UdonUI_Trigger GetUdonUI_Trigger(int _id)
        {
            if (mainUI)
            {
                if (_id < mainUI.BoxColliderUdon.Length)
                {
                    UdonUI_Trigger _value = new UdonUI_Trigger();
                    Vector2 _range = mainUI.MainEventNumber_BoxCollider[_id];
                    float eventLength = _range.y;
                    int eventLengthInt = (int)eventLength;
                    int startID = (int)_range.x;

                    _value.BoxColliderUdon = mainUI.BoxColliderUdon[_id];
                    _value.MainEventNumber_BoxCollider = new Vector2(0, eventLength);
                    _value.synBoxCollider = mainUI.synBoxCollider[_id];

                    _value.Action_BoxCollider = new int[eventLengthInt];
                    _value.MainEvent_BoxCollider = new int[eventLengthInt];
                    _value.MainAnimators_BoxCollider = new Animator[eventLengthInt];
                    _value.AnimaName_BoxCollider = new string[eventLengthInt];
                    _value.TargetGameObject_BoxCollider = new GameObject[eventLengthInt];
                    for (int i = 0; i < eventLengthInt; i++)
                    {
                        int nowID = startID + i;
                        _value.Action_BoxCollider[i] = mainUI.Action_BoxCollider[nowID];
                        _value.MainEvent_BoxCollider[i] = mainUI.MainEvent_BoxCollider[nowID];
                        _value.MainAnimators_BoxCollider[i] = mainUI.MainAnimators_BoxCollider[nowID];
                        _value.AnimaName_BoxCollider[i] = mainUI.AnimaName_BoxCollider[nowID];
                        _value.TargetGameObject_BoxCollider[i] = mainUI.TargetGameObject_BoxCollider[nowID];
                    }

                    return _value;
                }
                else
                {
                    Debug.LogWarning("操作违规，检索ID超出索引上限");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("警告", "未创建UdonUI环境", "我知道了");
            }

            return null;
        }
    }
}