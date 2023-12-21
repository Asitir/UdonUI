using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UdonSharp;
using VRC.Udon;
using UdonUI;

namespace UdonUI_Editor
{
    [CustomPropertyDrawer(typeof(BoxColliderListMain))]
    public class BTMain_Collider_Editor : PropertyDrawer
    {
        #region Language
        string[] ActionNameb_cn = new string[6] {
            "按下",//0
            "长按",//1
            "抬起",//2
            "进入悬停",//3
            "持续悬停",//4
            "离开悬停"//5
        };
        string[] ActionNameb_en = new string[6] {
            "Press",//0
            "Hold",//1
            "lift",//2
            "Enter hovering",//3
            "keep hovering",//4
            "leave hovering"//5
        };
        string[] ActionNameb_jp = new string[6] {
            "クリック",//0
            "長押し",//1
            "離す",//2
            "マウスオーバーに入る",//3
            "マウスオーバー留まる",//4
            "マウスオーバー離脱"//5
        };


        string[] ActionName_cn = new string[2] {
            "进入时",
            "离开时"
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
        //"自定义函数/A组(1~20)/（1）",
        //"自定义函数/A组(1~20)/（2）",
        //"自定义函数/A组(1~20)/（3）",
        //"自定义函数/A组(1~20)/（4）",
        //"自定义函数/A组(1~20)/（5）",
        //"自定义函数/A组(1~20)/（6）",
        //"自定义函数/A组(1~20)/（7）",
        //"自定义函数/A组(1~20)/（8）",
        //"自定义函数/A组(1~20)/（9）",
        //"自定义函数/A组(1~20)/（10）",
        //"自定义函数/A组(1~20)/（11）",
        //"自定义函数/A组(1~20)/（12）",
        //"自定义函数/A组(1~20)/（13）",
        //"自定义函数/A组(1~20)/（14）",
        //"自定义函数/A组(1~20)/（15）",
        //"自定义函数/A组(1~20)/（16）",
        //"自定义函数/A组(1~20)/（17）",
        //"自定义函数/A组(1~20)/（18）",
        //"自定义函数/A组(1~20)/（19）",
        //"自定义函数/A组(1~20)/（20）",
        //"自定义函数/B组(21~40)/（21）",
        //"自定义函数/B组(21~40)/（22）",
        //"自定义函数/B组(21~40)/（23）",
        //"自定义函数/B组(21~40)/（24）",
        //"自定义函数/B组(21~40)/（25）",
        //"自定义函数/B组(21~40)/（26）",
        //"自定义函数/B组(21~40)/（27）",
        //"自定义函数/B组(21~40)/（28）",
        //"自定义函数/B组(21~40)/（29）",
        //"自定义函数/B组(21~40)/（30）",
        //"自定义函数/B组(21~40)/（31）",
        //"自定义函数/B组(21~40)/（32）",
        //"自定义函数/B组(21~40)/（33）",
        //"自定义函数/B组(21~40)/（34）",
        //"自定义函数/B组(21~40)/（35）",
        //"自定义函数/B组(21~40)/（36）",
        //"自定义函数/B组(21~40)/（37）",
        //"自定义函数/B组(21~40)/（38）",
        //"自定义函数/B组(21~40)/（39）",
        //"自定义函数/B组(21~40)/（40）",
        //"自定义函数/C组(41~60)/（41）",
        //"自定义函数/C组(41~60)/（42）",
        //"自定义函数/C组(41~60)/（43）",
        //"自定义函数/C组(41~60)/（44）",
        //"自定义函数/C组(41~60)/（45）",
        //"自定义函数/C组(41~60)/（46）",
        //"自定义函数/C组(41~60)/（47）",
        //"自定义函数/C组(41~60)/（48）",
        //"自定义函数/C组(41~60)/（49）",
        //"自定义函数/C组(41~60)/（50）",
        //"自定义函数/C组(41~60)/（51）",
        //"自定义函数/C组(41~60)/（52）",
        //"自定义函数/C组(41~60)/（53）",
        //"自定义函数/C组(41~60)/（54）",
        //"自定义函数/C组(41~60)/（55）",
        //"自定义函数/C组(41~60)/（56）",
        //"自定义函数/C组(41~60)/（57）",
        //"自定义函数/C组(41~60)/（58）",
        //"自定义函数/C组(41~60)/（59）",
        //"自定义函数/C组(41~60)/（60）"
    };

        string[] ActionName_en = new string[2] {
            "When entering",
            "When leaving"
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
        //"Custom functions/Group A(1~20)/（1）",
        //"Custom functions/Group A(1~20)/（2）",
        //"Custom functions/Group A(1~20)/（3）",
        //"Custom functions/Group A(1~20)/（4）",
        //"Custom functions/Group A(1~20)/（5）",
        //"Custom functions/Group A(1~20)/（6）",
        //"Custom functions/Group A(1~20)/（7）",
        //"Custom functions/Group A(1~20)/（8）",
        //"Custom functions/Group A(1~20)/（9）",
        //"Custom functions/Group A(1~20)/（10）",
        //"Custom functions/Group A(1~20)/（11）",
        //"Custom functions/Group A(1~20)/（12）",
        //"Custom functions/Group A(1~20)/（13）",
        //"Custom functions/Group A(1~20)/（14）",
        //"Custom functions/Group A(1~20)/（15）",
        //"Custom functions/Group A(1~20)/（16）",
        //"Custom functions/Group A(1~20)/（17）",
        //"Custom functions/Group A(1~20)/（18）",
        //"Custom functions/Group A(1~20)/（19）",
        //"Custom functions/Group A(1~20)/（20）",
        //"Custom functions/Group B(21~40)/（21）",
        //"Custom functions/Group B(21~40)/（22）",
        //"Custom functions/Group B(21~40)/（23）",
        //"Custom functions/Group B(21~40)/（24）",
        //"Custom functions/Group B(21~40)/（25）",
        //"Custom functions/Group B(21~40)/（26）",
        //"Custom functions/Group B(21~40)/（27）",
        //"Custom functions/Group B(21~40)/（28）",
        //"Custom functions/Group B(21~40)/（29）",
        //"Custom functions/Group B(21~40)/（30）",
        //"Custom functions/Group B(21~40)/（31）",
        //"Custom functions/Group B(21~40)/（32）",
        //"Custom functions/Group B(21~40)/（33）",
        //"Custom functions/Group B(21~40)/（34）",
        //"Custom functions/Group B(21~40)/（35）",
        //"Custom functions/Group B(21~40)/（36）",
        //"Custom functions/Group B(21~40)/（37）",
        //"Custom functions/Group B(21~40)/（38）",
        //"Custom functions/Group B(21~40)/（39）",
        //"Custom functions/Group B(21~40)/（40）",
        //"Custom functions/Group C(41~60)/（41）",
        //"Custom functions/Group C(41~60)/（42）",
        //"Custom functions/Group C(41~60)/（43）",
        //"Custom functions/Group C(41~60)/（44）",
        //"Custom functions/Group C(41~60)/（45）",
        //"Custom functions/Group C(41~60)/（46）",
        //"Custom functions/Group C(41~60)/（47）",
        //"Custom functions/Group C(41~60)/（48）",
        //"Custom functions/Group C(41~60)/（49）",
        //"Custom functions/Group C(41~60)/（50）",
        //"Custom functions/Group C(41~60)/（51）",
        //"Custom functions/Group C(41~60)/（52）",
        //"Custom functions/Group C(41~60)/（53）",
        //"Custom functions/Group C(41~60)/（54）",
        //"Custom functions/Group C(41~60)/（55）",
        //"Custom functions/Group C(41~60)/（56）",
        //"Custom functions/Group C(41~60)/（57）",
        //"Custom functions/Group C(41~60)/（58）",
        //"Custom functions/Group C(41~60)/（59）",
        //"Custom functions/Group C(41~60)/（60）"
    };


        string[] ActionName_jp = new string[2] {
            "入る",
            "出る"
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
        //"カスタム関数/グループA(1~20)/（1）",
        //"カスタム関数/グループA(1~20)/（2）",
        //"カスタム関数/グループA(1~20)/（3）",
        //"カスタム関数/グループA(1~20)/（4）",
        //"カスタム関数/グループA(1~20)/（5）",
        //"カスタム関数/グループA(1~20)/（6）",
        //"カスタム関数/グループA(1~20)/（7）",
        //"カスタム関数/グループA(1~20)/（8）",
        //"カスタム関数/グループA(1~20)/（9）",
        //"カスタム関数/グループA(1~20)/（10）",
        //"カスタム関数/グループA(1~20)/（11）",
        //"カスタム関数/グループA(1~20)/（12）",
        //"カスタム関数/グループA(1~20)/（13）",
        //"カスタム関数/グループA(1~20)/（14）",
        //"カスタム関数/グループA(1~20)/（15）",
        //"カスタム関数/グループA(1~20)/（16）",
        //"カスタム関数/グループA(1~20)/（17）",
        //"カスタム関数/グループA(1~20)/（18）",
        //"カスタム関数/グループA(1~20)/（19）",
        //"カスタム関数/グループA(1~20)/（20）",
        //"カスタム関数/グループB(21~40)/（21）",
        //"カスタム関数/グループB(21~40)/（22）",
        //"カスタム関数/グループB(21~40)/（23）",
        //"カスタム関数/グループB(21~40)/（24）",
        //"カスタム関数/グループB(21~40)/（25）",
        //"カスタム関数/グループB(21~40)/（26）",
        //"カスタム関数/グループB(21~40)/（27）",
        //"カスタム関数/グループB(21~40)/（28）",
        //"カスタム関数/グループB(21~40)/（29）",
        //"カスタム関数/グループB(21~40)/（30）",
        //"カスタム関数/グループB(21~40)/（31）",
        //"カスタム関数/グループB(21~40)/（32）",
        //"カスタム関数/グループB(21~40)/（33）",
        //"カスタム関数/グループB(21~40)/（34）",
        //"カスタム関数/グループB(21~40)/（35）",
        //"カスタム関数/グループB(21~40)/（36）",
        //"カスタム関数/グループB(21~40)/（37）",
        //"カスタム関数/グループB(21~40)/（38）",
        //"カスタム関数/グループB(21~40)/（39）",
        //"カスタム関数/グループB(21~40)/（40）",
        //"カスタム関数/グループC(41~60)/（41）",
        //"カスタム関数/グループC(41~60)/（42）",
        //"カスタム関数/グループC(41~60)/（43）",
        //"カスタム関数/グループC(41~60)/（44）",
        //"カスタム関数/グループC(41~60)/（45）",
        //"カスタム関数/グループC(41~60)/（46）",
        //"カスタム関数/グループC(41~60)/（47）",
        //"カスタム関数/グループC(41~60)/（48）",
        //"カスタム関数/グループC(41~60)/（49）",
        //"カスタム関数/グループC(41~60)/（50）",
        //"カスタム関数/グループC(41~60)/（51）",
        //"カスタム関数/グループC(41~60)/（52）",
        //"カスタム関数/グループC(41~60)/（53）",
        //"カスタム関数/グループC(41~60)/（54）",
        //"カスタム関数/グループC(41~60)/（55）",
        //"カスタム関数/グループC(41~60)/（56）",
        //"カスタム関数/グループC(41~60)/（57）",
        //"カスタム関数/グループC(41~60)/（58）",
        //"カスタム関数/グループC(41~60)/（59）",
        //"カスタム関数/グループC(41~60)/（60）"
    };

        static int languageID = -1;
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
                }
                else if (MainEvents.intValue == 32)
                {
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    EditorGUI.PropertyField(AnimaNameRect, AnimaName, new GUIContent("EventName:"));
                }
                else if (MainEvents.intValue == 33)
                {
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    if (int.TryParse(AnimaName.stringValue, out int _index))
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, _index, GetActionNameB()).ToString();
                    else
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, 0, GetActionNameB()).ToString();
                    //EditorGUI.LabelField(AnimaNameRect, "按钮事件");
                }
                else if (MainEvents.intValue == 34)
                {
                    EditorGUI.PropertyField(GameObjRect, GameObjTargt, new GUIContent("GameObj"));
                    if (int.TryParse(AnimaName.stringValue, out int _index))
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, Mathf.Min(1, _index), GetActionName()).ToString();
                    else
                        AnimaName.stringValue = EditorGUI.Popup(AnimaNameRect, 0, GetActionName()).ToString();
                    //EditorGUI.LabelField(AnimaNameRect,"触发器事件");
                }
                if (EditorGUI.EndChangeCheck())
                {
                    //Debug.Log("123");
                    int ID = property.FindPropertyRelative("ID").intValue;
                    int[] ButtonAction, ButtonEvent;
                    //--------------//
                    int[] MainInt;
                    Animator[] MainAnimator;
                    string[] MainAnimaName;
                    GameObject[] MainObj;
                    //--------------//
                    //UdonBehaviour mainudon = (UdonBehaviour)property.FindPropertyRelative("MainUdonUI").objectReferenceValue;
                    MainUI_Script mainudon_E = UdonUI_Manager.Instance.mainUI;
                    //mainudon.publicVariables.TryGetVariableValue("Action_BoxCollider", out ButtonAction);
                    //mainudon.publicVariables.TryGetVariableValue("MainEvent_BoxCollider", out ButtonEvent);
                    //mainudon.publicVariables.TryGetVariableValue("MainEvents_BoxCollider", out MainInt);//事件编号
                    //mainudon.publicVariables.TryGetVariableValue("MainAnimators_BoxCollider", out MainAnimator);//事件中的动画机
                    //mainudon.publicVariables.TryGetVariableValue("AnimaName_BoxCollider", out MainAnimaName);//事件中的动画机动画名
                    //mainudon.publicVariables.TryGetVariableValue("TargetGameObject_BoxCollider", out MainObj);//被控制活动状态的开关
                    ButtonAction = mainudon_E.Action_BoxCollider;
                    ButtonEvent = mainudon_E.MainEvent_BoxCollider;
                    MainInt = mainudon_E.MainEvent_BoxCollider;
                    MainAnimator = mainudon_E.MainAnimators_BoxCollider;
                    MainAnimaName = mainudon_E.AnimaName_BoxCollider;
                    MainObj = mainudon_E.TargetGameObject_BoxCollider;

                    ButtonAction[ID] = Action.intValue;
                    ButtonEvent[ID] = MainEvents.intValue;
                    MainObj[ID] = (GameObject)GameObjTargt.objectReferenceValue;
                    MainAnimator[ID] = (Animator)PlayAnimator.objectReferenceValue;
                    MainAnimaName[ID] = AnimaName.stringValue;

                    //mainudon.publicVariables.TrySetVariableValue("Action_BoxCollider", ButtonAction);
                    //mainudon.publicVariables.TrySetVariableValue("MainEvent_BoxCollider", ButtonEvent);
                    //mainudon.publicVariables.TrySetVariableValue("MainEvents_BoxCollider", MainInt);//事件编号
                    //mainudon.publicVariables.TrySetVariableValue("MainAnimators_BoxCollider", MainAnimator);//事件中的动画机
                    //mainudon.publicVariables.TrySetVariableValue("AnimaName_BoxCollider", MainAnimaName);//事件中的动画机动画名
                    //mainudon.publicVariables.TrySetVariableValue("TargetGameObject_BoxCollider", MainObj);//被控制活动状态的开关
                    mainudon_E.Action_BoxCollider = ButtonAction;
                    mainudon_E.MainEvent_BoxCollider = ButtonEvent;
                    mainudon_E.MainEvents_BoxCollider = MainInt;
                    mainudon_E.MainAnimators_BoxCollider = MainAnimator;
                    mainudon_E.AnimaName_BoxCollider = MainAnimaName;
                    mainudon_E.TargetGameObject_BoxCollider = MainObj;

                    Undo.RegisterCompleteObjectUndo(mainudon_E, "按键属性修改");
                    //Debug.Log("按键状态修改" + ButtonAction[ID] + " 当前事件ID： " + ID);
                    //Debug.Log(mainudon.gameObject.name);
                }
            }
        }
        string[] GetActionName()
        {
            if (languageID == 0) return ActionName_cn;
            else if (languageID == 1) return ActionName_en;
            else if (languageID == 2) return ActionName_jp;

            return ActionName_cn;
        }
        string[] GetActionNameB()
        {
            if (languageID == 0) return ActionNameb_cn;
            else if (languageID == 1) return ActionNameb_en;
            else if (languageID == 2) return ActionNameb_jp;

            return ActionNameb_cn;
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