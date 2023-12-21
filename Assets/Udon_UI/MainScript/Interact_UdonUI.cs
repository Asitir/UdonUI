    
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class Interact_UdonUI : UdonSharpBehaviour
    {
        bool IsOnece = false;
        MainUI_Script MainUI;
        int ButtonID;
        public override void Interact()
        {
            if (!IsOnece)
            {
                MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
                string[] adb = gameObject.name.Split('_');
                ButtonID = int.Parse(adb[1]);
                IsOnece = true;
            }
            MainUI.But_RunEvents(0, ButtonID, true);//按下的一瞬间
        }
    }
}
