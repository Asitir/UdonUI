
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class AvatarCamePos : UdonSharpBehaviour
    {
        VRCPlayerApi MyPlayer;
        float delaytime;

        private void OnEnable()
        {
            //int MyID = 
            MyPlayer = Networking.GetOwner(transform.parent.parent.GetChild(0).GetChild(transform.GetSiblingIndex()).gameObject);
            transform.SetPositionAndRotation(MyPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position, MyPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation);
            transform.GetChild(0).GetComponent<Camera>().enabled = true;
            delaytime = 0.5f;
        }

        private void Update()
        {
            transform.SetPositionAndRotation(MyPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position, MyPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation);
            delaytime -= Time.deltaTime;
            if (delaytime < 0) gameObject.SetActive(false);
        }
    }
}
