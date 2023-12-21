using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.SDK3.Components;

namespace UdonUI_Editor
{
    public class UdonUI_Wrap : MonoBehaviour
    {
        public bool isFindToName = true;
        public UdonUI_Button[] allButton = new UdonUI_Button[0];
        public UdonUI_Trigger[] allTrigger = new UdonUI_Trigger[0];

    }
    [System.Serializable]
    public class UdonUI_Button
    {
        public GameObject Button;//按钮
        public string ButtonName = "";
        public int ButtonIndex = 0;
        public Vector2 MainEventNumber = new Vector2(0, 0);//主要方法的编码
        public int ButtonType = 1;//按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮
        public bool synButton = false;//按钮是否同步

        public int[] Action;//行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
        public int[] MainEvent;//事件、执行: 0为物体开关切换  1为开启物体   2为关闭物体  3为播放动画机动画  4为设置动画机触发  5为设置动画机bool为true  6为啥子动画机bool为false  7为设置音效  8为设置自定义事件
        public Animator[] MainAnimators;//由事件执行的动画机------------------------------------//
        public string[] MainAnimatorsName = new string[0];
        public int[] MainAnimatorsIndex = new int[0];

        public string[] AnimaName;//由事件执行的动画机里面的动画名-----------------------------------//
        public GameObject[] TargetGameObject;//被作为开关物体活动的物体目标---------------------------------------//
        public string[] TargetName = new string[0];
        public int[] TargetIndex = new int[0];

    }
    [System.Serializable]
    public class UdonUI_Trigger
    {
        public GameObject BoxColliderUdon;//触发器
        public Vector2 MainEventNumber_BoxCollider = new Vector2(0, 0);//主要方法的编码
        public bool synBoxCollider = false;//是否同步

        public int[] Action_BoxCollider;//行为、状态:  0为进入  1为退出
        public int[] MainEvent_BoxCollider;//事件、执行: 0为物体开关切换  1为开启物体   2为关闭物体  3为播放动画机动画  4为设置动画机触发  5为设置动画机bool为true  6为啥子动画机bool为false  7为设置音效  8为设置自定义事件
        public Animator[] MainAnimators_BoxCollider;//由事件执行的动画机------------------------------------//
        public string[] AnimaName_BoxCollider;//由事件执行的动画机里面的动画名-----------------------------------//
        public GameObject[] TargetGameObject_BoxCollider;//被作为开关物体活动的物体目标---------------------------------------//

    }

}
