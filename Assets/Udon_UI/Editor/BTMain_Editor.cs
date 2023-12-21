using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonSharp;
using VRC.Udon;
using UdonUI;

namespace UdonUI_Editor
{
    [CustomPropertyDrawer(typeof(ButtonListMain))]
    public class BTMain_Editor : PropertyDrawer
    {
        #region Language
        string[] ActionNamec_cn = new string[2] {
            "进入时",
            "离开时"
        };
        string[] ActionNamec_en = new string[2] {
            "When entering",
            "When leaving"
        };
        string[] ActionNamec_jp = new string[2] {
            "入る",
            "出る"
        };

        string[] ActionName_cn = new string[6] {
            "按下",//0
            "长按",//1
            "抬起",//2
            "进入悬停",//3
            "持续悬停",//4
            "离开悬停"//5
        };
        string[] MainEventsName_cn = new string[35] {
        "(SetActive)/on <-> off",
        "(SetActive)/on",
        "(SetActive)/off",
        "(Animator)/PlayAnim",
        "(Animator)/Set Trigger",
        "(Animator)/SetBool Ture",
        "(Animator)/SetBool False",
        "音效 / 其它(1)",
        "音效 / 其它(2)",
        "音效 / 其它(3)",
        "音效 / 其它(4)",
        "音效 / 其它(5)",
        "音效 / 其它(6)",
        "音效 / 其它(7)",
        "音效 / 其它(8)",
        "音效 / 其它(9)",
        "音效 / 其它(10)",
        "音效 / 其它(11)",
        "音效 / 其它(12)",
        "音效 / 其它(13)",
        "音效 / 其它(14)",
        "音效 / 其它(15)",
        "音效 / 其它(16)",
        "音效 / 其它(17)",
        "音效 / 其它(18)",
        "音效 / 其它(19)",
        "音效 / 其它(20)",
        "震动 / 弱",
        "震动 / 较弱",
        "震动 / 一般",
        "震动 / 较强",
        "震动 / 强",
        "自定义函数",
        "执行UdonUI按钮",
        "执行UdonUI触发器"
        };

        string[] ActionName_en = new string[6] {
            "Press",//0
            "Hold",//1
            "lift",//2
            "Enter hovering",//3
            "keep hovering",//4
            "leave hovering"//5
        };
        string[] MainEventsName_en = new string[35] {
        "(SetActive)/on <-> off",
        "(SetActive)/on",
        "(SetActive)/off",
        "(Animator)/PlayAnim",
        "(Animator)/Set Trigger",
        "(Animator)/SetBool Ture",
        "(Animator)/SetBool False",
        "Sound effects / other(1)",
        "Sound effects / other(2)",
        "Sound effects / other(3)",
        "Sound effects / other(4)",
        "Sound effects / other(5)",
        "Sound effects / other(6)",
        "Sound effects / other(7)",
        "Sound effects / other(8)",
        "Sound effects / other(9)",
        "Sound effects / other(10)",
        "Sound effects / other(11)",
        "Sound effects / other(12)",
        "Sound effects / other(13)",
        "Sound effects / other(14)",
        "Sound effects / other(15)",
        "Sound effects / other(16)",
        "Sound effects / other(17)",
        "Sound effects / other(18)",
        "Sound effects / other(19)",
        "Sound effects / other(20)",
        "Vibration / weak - 1",
        "Vibration / weak",
        "Vibration / average",
        "Vibration / strong",
        "Vibration / strong + 1",
        "Custom functions",
        "UdonUI_Button",
        "UdonUI_trigger"
        };


        string[] ActionName_jp = new string[6] {
            "クリック",//0
            "長押し",//1
            "離す",//2
            "マウスオーバーに入る",//3
            "マウスオーバー留まる",//4
            "マウスオーバー離脱"//5
        };
        string[] MainEventsName_jp = new string[35] {
        "(SetActive)/on <-> off",
        "(SetActive)/on",
        "(SetActive)/off",
        "(Animator)/PlayAnim",
        "(Animator)/Set Trigger",
        "(Animator)/SetBool Ture",
        "(Animator)/SetBool False",
        "サウンド / その他(1)",
        "サウンド / その他(2)",
        "サウンド / その他(3)",
        "サウンド / その他(4)",
        "サウンド / その他(5)",
        "サウンド / その他(6)",
        "サウンド / その他(7)",
        "サウンド / その他(8)",
        "サウンド / その他(9)",
        "サウンド / その他(10)",
        "サウンド / その他(11)",
        "サウンド / その他(12)",
        "サウンド / その他(13)",
        "サウンド / その他(14)",
        "サウンド / その他(15)",
        "サウンド / その他(16)",
        "サウンド / その他(17)",
        "サウンド / その他(18)",
        "サウンド / その他(19)",
        "サウンド / その他(20)",
        "振動 / 弱い-1",
        "振動 / 弱い",
        "振動 / 普通",
        "振動 / 強い",
        "振動 / 強い+1",
        "カスタム関数",
        "UdonUI_Button",
        "UdonUI_trigger"
        };

        static string[] porName;
        static int languageID = -1;

        int m_ButtonType = 1; //按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮  4为窗口
        #endregion

        #region File
        // 0:Action（on/off）
        #endregion
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                if (languageID == -1)
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
                }
                else
                {
                    languageID = int.Parse(EditorUserSettings.GetConfigValue("languageID"));
                }

                //设置属性名宽度
                EditorGUIUtility.labelWidth = 60;
                position.height = EditorGUIUtility.singleLineHeight;

                var ActionRect = new Rect(position)
                {
                    width = 64,
                    height = 15
                };

                var EventRect = new Rect(position)
                {
                    height = 15,
                    width = 150,
                    x = position.x + 80
                    //width = position.width - 80,

                };

                var GameObjRect = new Rect(EventRect)
                {
                    height = 15,
                    width = 180,
                    x = EventRect.x + 160
                };

                var AnimatorRect = new Rect(GameObjRect)
                {
                    height = 15,
                    width = 180,
                    x = GameObjRect.x
                };

                var AnimaNameRect = new Rect(AnimatorRect)
                {
                    height = 15,
                    width = 180,
                    x = AnimatorRect.x + 190
                };

                var Action = property.FindPropertyRelative("Action");    //Action:     行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
                var MainEvents = property.FindPropertyRelative("MainEvent");    //MainEvent:  事件、执行: 0为播放动画机  1为开关物体活动   2为自定义事件
                var PlayAnimator = property.FindPropertyRelative("MainAnimators");//动画机
                var AnimaName = property.FindPropertyRelative("AnimaName");//动画机相关名
                var GameObjTargt = property.FindPropertyRelative("GameObjectTargt");//控制开关的游戏对象

                //int m_ButtonType = property.FindPropertyRelative("ButtonType").intValue;

                //if (m_ButtonType == 1)
                //GetLanguage();
                EditorGUI.BeginChangeCheck();
                Action.intValue = EditorGUI.Popup(ActionRect, Action.intValue, GetActionName());
                MainEvents.intValue = EditorGUI.Popup(EventRect, MainEvents.intValue, GetMainEventsName());
                if (MainEvents.intValue < 3)
                {//绘制目标游戏对象
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    //GameObjTargt.
                }
                else if (MainEvents.intValue < 7)
                {//绘制动画机
                    EditorGUI.PropertyField(AnimatorRect, PlayAnimator, new GUIContent("Animator"));
                    EditorGUI.PropertyField(AnimaNameRect, AnimaName, new GUIContent("Name:"));
                }else if(MainEvents.intValue == 32)
                {
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    EditorGUI.PropertyField(AnimaNameRect, AnimaName, new GUIContent("EventName:"));
                }else if(MainEvents.intValue == 33)
                {
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    if (int.TryParse(AnimaName.stringValue, out int _index))
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, _index, GetActionName()).ToString();
                    else
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, 0, GetActionName()).ToString();
                    //EditorGUI.LabelField(AnimaNameRect, "按钮事件");
                }
                else if(MainEvents.intValue == 34)
                {
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    if (int.TryParse(AnimaName.stringValue, out int _index))
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, Mathf.Min(1, _index), GetActionNameC()).ToString();
                    else
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, 0, GetActionNameC()).ToString();
                    //EditorGUI.LabelField(AnimaNameRect,"触发器事件");
                }
                if (EditorGUI.EndChangeCheck())
                {
                    //Debug.Log("123");
                    int ID = property.FindPropertyRelative("ID").intValue;
                    int[] ButtonAction, ButtonEvent;
                    //int[] ButtonType;//按钮类型
                    //--------------//
                    int[] MainInt;
                    Animator[] MainAnimator;
                    string[] MainAnimaName;
                    GameObject[] MainObj;
                    //--------------//
                    MainUI_Script mainudon_E = UdonUI_Manager.Instance.mainUI;
                    ButtonAction = mainudon_E.Action;
                    ButtonEvent = mainudon_E.MainEvent;
                    MainInt = mainudon_E.MainEvents;
                    MainAnimator = mainudon_E.MainAnimators;
                    MainAnimaName = mainudon_E.AnimaName;
                    MainObj = mainudon_E.TargetGameObject;

                    ButtonAction[ID] = Action.intValue;
                    ButtonEvent[ID] = MainEvents.intValue;
                    MainObj[ID] = (GameObject)GameObjTargt.objectReferenceValue;
                    MainAnimator[ID] = (Animator)PlayAnimator.objectReferenceValue;
                    MainAnimaName[ID] = AnimaName.stringValue;

                    mainudon_E.Action = ButtonAction;
                    mainudon_E.MainEvent = ButtonEvent;
                    mainudon_E.MainEvents = MainInt;
                    mainudon_E.MainAnimators = MainAnimator;
                    mainudon_E.AnimaName = MainAnimaName;
                    mainudon_E.TargetGameObject = MainObj;
                    Undo.RegisterCompleteObjectUndo(mainudon_E, "按键属性修改");
                    //Debug.Log("按键状态修改" + ButtonAction[ID] + " 当前事件ID： " + ID);
                    //Debug.Log(mainudon.gameObject.name);
                }
                
            }
        }
        static void GetLanguage() { 
            if(porName == null)
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
            }

            //if (languageID == 0) porName = chinese;
            //else if (languageID == 1) porName = english;
            //else if (languageID == 2) porName = japanese;
        }

        string[] GetActionName()
        {
            if (languageID == 0) return ActionName_cn;
            else if (languageID == 1) return ActionName_en;
            else if (languageID == 2) return ActionName_jp;

            return ActionName_cn;
        }
        string[] GetActionNameC()
        {
            if (languageID == 0) return ActionNamec_cn;
            else if (languageID == 1) return ActionNamec_en;
            else if (languageID == 2) return ActionNamec_jp;

            return ActionNamec_cn;
        }
        string[] GetMainEventsName()
        {
            if (languageID == 0) return MainEventsName_cn;
            else if (languageID == 1) return MainEventsName_en;
            else if (languageID == 2) return MainEventsName_jp;

            return MainEventsName_cn;
        }

    }
}