
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_PickUpState : UdonSharpBehaviour
    {
        public GameObject onState;
        public GameObject target;
        bool nowState = false;
        public override void OnDrop()
        {
            if (onState.activeSelf)
            {
                if (!nowState)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnSet");
            }
            else
            {
                if (nowState)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "OnSetOff");
            }
        }

        public void OnSet()
        {
            nowState = true;
            target.SetActive(true);
        }
        public void OnSetOff()
        {
            nowState = false;
            target.SetActive(false);
        }
    }
}
