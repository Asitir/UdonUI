
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_Pop_ups : UdonSharpBehaviour
    {
        [Header("UdonUI环境（勿动）")]
        [Header("示例: Send@0_网易云_正在播放爱的供养")]
        [Header("[script]发送弹窗信息 Send@iconID_标题_内容 (ID：图标所处序列)")]
        public MainUI_Script mainUI;
        public Animator animator;
        public Transform icon;
        public Text con1, con2, title;

        private int iconID = -1;
        public void Send() 
        {
            string[] _co = mainUI.scriptString.Split('_');
            if (iconID > -1)
                icon.GetChild(iconID).gameObject.SetActive(false);
            iconID = int.Parse(_co[0]);
            icon.GetChild(iconID).gameObject.SetActive(true);//显示图标
            con1.text = $"{_co[1]}:{_co[2]}";
            title.text = _co[1];
            con2.text = _co[2];
            animator.CrossFade("play", 0, 0, 0);
        }
    }
}
