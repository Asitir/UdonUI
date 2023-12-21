
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonUI
{
    public class Pickup_UdonUI : UdonSharpBehaviour
    {
        [Header("抓取距离")]
        public float distance;
        float pickdistance;
        bool IsOnece = false, isMaster = false;
        bool leftHand, rightHand;
        VRCPlayerApi localplayer;
        MainUI_Script MainUI;
        int ButtonID;
        Matrix4x4 convertPosition, convertSet;

        Vector3 leftPos, rightPos;

        public override void Interact()
        {
            StartVoid();
            MainUI.But_RunEvents(0, ButtonID, true);//按下的一瞬间
        }

        public override void InputGrab(bool value, UdonInputEventArgs args)
        {
            StartVoid();

            if (value)
            {
                if (args.handType == HandType.RIGHT)
                {
                    rightPos = MainUI.Rhand.position;
                    if (Vector3.Distance(rightPos, transform.position) < pickdistance)
                    {
                        rightHand = true;
                        leftHand = false;
                        convertPosition = MainUI.Rhand.worldToLocalMatrix * transform.localToWorldMatrix;
                        Networking.SetOwner(localplayer, gameObject);
                        pickdistance = 0.1f;
                    }
                }
                else
                {
                    leftPos = MainUI.Lhand.position;
                    if (Vector3.Distance(leftPos, transform.position) < pickdistance)
                    {
                        rightHand = false;
                        leftHand = true;
                        convertPosition = MainUI.Lhand.worldToLocalMatrix * transform.localToWorldMatrix;
                        Networking.SetOwner(localplayer, gameObject);
                        pickdistance = 0.1f;

                    }
                }
            }
            else
            {
                if (rightHand) if (args.handType == HandType.RIGHT) { rightHand = false; pickdistance = distance; }


                if (leftHand) if (args.handType == HandType.LEFT) { leftHand = false; pickdistance = distance; }

            }
            //if (!ButtonSwitch)
            //    SetPosition(value, args.handType == HandType.RIGHT);
        }

        private void LateUpdate()
        {
            if (Networking.GetOwner(gameObject) == localplayer)
            {
                if (leftHand)
                {
                    convertSet = MainUI.Lhand.localToWorldMatrix * convertPosition;
                    transform.SetPositionAndRotation(convertSet.GetColumn(3), convertSet.rotation);
                }
                else if (rightHand)
                {
                    convertSet = MainUI.Rhand.localToWorldMatrix * convertPosition;
                    transform.SetPositionAndRotation(convertSet.GetColumn(3), convertSet.rotation);

                }

                if (!isMaster)
                {
                    isMaster = true;
                }
            }
            else
            {
                if (isMaster)
                {
                    isMaster = false;
                    rightHand = false;
                    leftHand = false;
                }
            }
        }

        void StartVoid()
        {
            if (!IsOnece)
            {
                MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
                string[] adb = gameObject.name.Split('_');
                ButtonID = int.Parse(adb[1]);
                IsOnece = true;
                localplayer = Networking.LocalPlayer;
                pickdistance = distance;
            }

        }
    }
}