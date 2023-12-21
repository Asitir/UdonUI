
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class PlayerHeadHit : UdonSharpBehaviour
    {
        VRCPlayerApi LocalPlayer;
        Transform maintransform, UdonUI_AGI;
        MainUI_Script MainUI;
        int Delay = 0;
        bool IsUdonUI_AGI = false;
        void Start()
        {
//#if !UNITY_EDITOR
        //LocalPlayer = Networking.LocalPlayer;
//#endif
            maintransform = transform;

            MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            if (GameObject.Find("/UdonUI_AGI"))
            {
                IsUdonUI_AGI = true;
                UdonUI_AGI = GameObject.Find("/UdonUI_AGI").transform.GetChild(0);
            }

        }

//        private void LateUpdate()
//        {
//            if (Delay < 10)
//            {
//                Delay++;
//                if (Delay == 10)
//                {
//                }
//            }
//#if !UNITY_EDITOR
//        maintransform.position = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
//#endif
//        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerHit(0, other.gameObject.name);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerHit(1, other.gameObject.name);
        }

        void TriggerHit(int Enter, string name)
        {
            string[] IsName = name.Split('_');
            if (IsName[0] == "UCo")
            {
                MainUI.BOXC_RunEvents(Enter, int.Parse(IsName[1]));
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.2f);
            Gizmos.DrawIcon(transform.position + Vector3.up * 0.15f, "MoveThis");
        }
#endif
    }
}