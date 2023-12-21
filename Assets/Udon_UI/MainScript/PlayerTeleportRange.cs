
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class PlayerTeleportRange : UdonSharpBehaviour
    {
        public MainUI_Script mainUI;
        //private bool SetRotation = true;
        private void OnEnable()
        {
            Quaternion _rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
            mainUI.vrcWorld.SetPositionAndRotation(transform.position + (_rot * Vector3.back * transform.localScale.y), _rot);
            Networking.LocalPlayer.Respawn();
        }
        //private void OnDisable()
        //{
        //    Quaternion _rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
        //    mainUI.vrcWorld.SetPositionAndRotation(transform.position + (_rot * Vector3.back * transform.localScale.y), _rot);
        //    Networking.LocalPlayer.Respawn();
        //}
    }
}
