
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class HandMotion_Main : UdonSharpBehaviour
    {
        [HideInInspector]
        public bool[] synButton = new bool[1] {
        false
    };//按钮是否同步
        [HideInInspector]
        public Vector2[] MainEventNumber = new Vector2[1] { new Vector2(0, 0) };//主要方法的编码
        [HideInInspector]
        public GameObject[] Button = new GameObject[1];//按钮
                                                       //[HideInInspector]
                                                       //public int[] ButtonType = new int[1] { 1 };//按钮类型: 0为未选择类型 1为按钮  2为滑条  3为旋钮  4为窗口


        [HideInInspector]
        public int[] Action;//行为、状态:  0为按下  1为长按  2为抬起  3为进入悬停  4为持续悬停  5为离开悬停
        [HideInInspector]
        public int[] MainEvent;//事件、执行: 0为物体开关切换  1为开启物体   2为关闭物体  3为播放动画机动画  4为设置动画机触发  5为设置动画机bool为true  6为啥子动画机bool为false  7为设置音效  8为设置自定义事件
        [HideInInspector]
        public Animator[] MainAnimators;//由事件执行的动画机------------------------------------//
        [HideInInspector]
        public string[] AnimaName;//由事件执行的动画机里面的动画名-----------------------------------//
        [HideInInspector]
        public GameObject[] TargetGameObject;//被作为开关物体活动的物体目标---------------------------------------//
        [HideInInspector]
        public int[] MainEvents;//自定义 事件编号--->
        VRCPlayerApi localplayer;

        void Start()
        {
            localplayer = Networking.LocalPlayer;
        }

        private void Update()
        {

        }
    }
}