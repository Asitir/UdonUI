
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class Climbing_2_Switch : UdonSharpBehaviour
    {
        Climbing_2 mainClimbing;
        bool start = false;

        private void OnEnable()
        {
            if (!start)
            {
                mainClimbing = transform.parent.GetComponent<Climbing_2>();
                start = true;
            }
            mainClimbing.ButtonSwitch = true;
        }

        private void OnDisable()
        {
            mainClimbing.ButtonSwitch = false;
        }
    }
}
