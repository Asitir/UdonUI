
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class CatchPeoplePop : UdonSharpBehaviour
    {
        //public MainUI_Script mainUI;
        [HideInInspector] public CatchPeopleMG mainMG;
        /*[HideInInspector]*/ public bool isLeft = false;
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!player.isLocal)
            {
                if (isLeft)
                {
                    mainMG.isLeftCatch = true;
                    mainMG.PlayerID_L = player.playerId;
                }
                else
                {
                    mainMG.isRightCatch = true;
                    mainMG.PlayerID_R = player.playerId;
                }
                //transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
            }
            //base.OnPlayerTriggerEnter(player);
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            //base.OnPlayerTriggerExit(player);
            if (!player.isLocal)
            {
                if (isLeft) mainMG.isLeftCatch = false;
                else mainMG.isRightCatch = false;
                //transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
            }

        }

        public void OnCheck() { mainMG.mainUI.checkID++; }//检查当前脚本
    }
}
