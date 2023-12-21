
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class PlayerTeleport : UdonSharpBehaviour
    {
        private bool SetRotation = true;
        private void OnEnable()
        {
            Networking.LocalPlayer.TeleportTo(transform.position, SetRotation ? transform.rotation : Networking.LocalPlayer.GetRotation());
        }
        private void OnDisable()
        {
            Networking.LocalPlayer.TeleportTo(transform.position, SetRotation ? transform.rotation : Networking.LocalPlayer.GetRotation());
        }
    }
}
