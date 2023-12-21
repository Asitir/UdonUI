
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class Climbing_2_collinder : UdonSharpBehaviour
    {
        //[HideInInspector]
        public bool left = false;
        [HideInInspector]
        public bool IsConllinder = false;
        MeshRenderer mainmesh;
        Material Mian;
        float deltatime;
        float On = -1, Off = -1;
        float AnimaSpeed = 5;
        VRCPlayerApi localplayer;
        LayerMask climpMask;

        private void Start()
        {
            climpMask = transform.parent.GetComponent<Climbing_2>().ClimbLaye;
            mainmesh = GetComponent<MeshRenderer>();
            Mian = GetComponent<MeshRenderer>().material;
            Mian.SetFloat("_Alpha", 1);
            localplayer = Networking.LocalPlayer;

        }
        private void OnTriggerEnter(Collider other)
        {
            if (IsInLayerMask(other.gameObject))
            {
                IsConllinder = true;
                On = 1;
                Off = 0;
#if !UNITY_EDITOR
                if (left) localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Left, 1f, 10f, 320);
                else localplayer.PlayHapticEventInHand(VRC_Pickup.PickupHand.Right, 1f, 10f, 320);
#endif
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsInLayerMask(other.gameObject))
            {
                IsConllinder = false;
                Off = 1;
                On = 0;
            }
        }

        private void Update()
        {
            deltatime = Time.deltaTime * AnimaSpeed;

            if (On > 0)
            {
                On -= deltatime;
                if (On <= 0) On = 0;
                Mian.SetFloat("_Alpha", On);
            }

            if (Off > 0)
            {
                Off -= deltatime;
                if (Off <= 0) Off = 0;
                Mian.SetFloat("_Alpha", 1 - Off);
            }
        }

        bool IsInLayerMask(GameObject obj)
        {
            int objLayerMask = 1 << obj.layer;
            return (climpMask.value & objLayerMask) > 0;
        }
    }
}