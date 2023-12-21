
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI {
    public class UdonUI_PageTo : UdonSharpBehaviour
    {
        public MainUI_Script mainUI;
        private int nowID = -1;
        public void To() {
            if (nowID > -1)
                transform.GetChild(nowID).gameObject.SetActive(false);
            nowID = mainUI.scriptID;
            transform.GetChild(nowID).gameObject.SetActive(true);
        }
    }
}
