
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_Teleport : UdonSharpBehaviour
    {
        [Header("UdonUI环境")]
        [Header("[script] To@序号  (序号：数组ID，如To@0)")]
        public MainUI_Script mainUI;
        //[Header("VRCWorld")]
        //public Transform vrcworld;
        public Transform[] point;

        public void To() {
            int _id = mainUI.scriptID;
            if (point[_id].localScale.y > 0)
            {
                Quaternion _rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                mainUI.vrcWorld.SetPositionAndRotation(point[_id].position + (_rot * Vector3.back * point[_id].localScale.y), _rot);
            }
            else
            {
                mainUI.vrcWorld.SetPositionAndRotation(point[_id].position, point[_id].rotation);
            }
            Networking.LocalPlayer.Respawn();
        }
    }
}
