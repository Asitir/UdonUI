
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class HeadPos : UdonSharpBehaviour
    {
        //MainUI_Script MainUI;
        VRCPlayerApi LocalPlayer;
        //float start = 1;

        void Start()
        {
            //MainUI = transform.parent.parent.GetComponent<MainUI_Script>();
            LocalPlayer = Networking.LocalPlayer;
        }

        private void LateUpdate()
        {
#if !UNITY_EDITOR
        transform.SetPositionAndRotation(LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position, LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation);
#endif
        }
    }
}