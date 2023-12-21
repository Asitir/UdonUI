
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonUI
{
    public class Climbing_2 : UdonSharpBehaviour
    {
        //public GameObject swit;
        //public Transform L1, R1,L2,R2,L3,R3;
        //public bool left;
        //public Transform Main1, Main2, Main3;

        [HideInInspector]
        public bool ButtonSwitch = false;//是否使用中指的扳机键
        Vector3 Point;
        Vector3 Tpos;
        Transform Lhand, Rhand;
        //MainUI_Script MainUI;
        VRCPlayerApi localplayer;
        bool LH = false, RH = false;
        int DelayT = 0;
        Vector3 LockPos;
        bool LConllinder = false, RConllinder = false;
        Climbing_2_collinder LL, RR;
        float deltatime;
        bool Climb = false, Climb_last = false;
        Vector3 PlayerPosition;
        Vector3 ClimbDirection;
        [Header("可攀爬的层级")]
        public LayerMask ClimbLaye;
        RaycastHit ClimbRay, ClimbRay2;
        float TreporTo = -1;
        void Start()
        {
            LH = false; RH = false;
            //MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            localplayer = Networking.LocalPlayer;
            Lhand = transform.GetChild(0);
            Rhand = transform.GetChild(1);
            LL = transform.GetChild(0).GetComponent<Climbing_2_collinder>();
            RR = transform.GetChild(1).GetComponent<Climbing_2_collinder>();
            //ClimbLaye = 1 << 28;
        }

        private void Update()
        {
            //ButtonSwitch = swit.activeSelf;
            deltatime = Time.deltaTime;
            LConllinder = LL.IsConllinder;
            RConllinder = RR.IsConllinder;
//#if !UNITY_EDITOR
            Lhand.position = localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position;
            Rhand.position = localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position;
            PlayerPosition = localplayer.GetPosition();
//#else
//            gameObject.SetActive(false);
//            //Rhand.position = localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.RightHand).position;
//            //Rhand.Translate(0, 0, 0.5f);
//#endif

            if (DelayT < 10) DelayT++;
            else
            {
                if (LH || RH)
                {
                    if (LH) localplayer.SetVelocity((LockPos - Lhand.position) / deltatime);
                    else localplayer.SetVelocity((LockPos - Rhand.position) / deltatime);
                    Point = LockPos;
                    Climb = true;

                }
                else
                {
                    Climb = false;
                }
            }

            if (Climb_last != Climb)
            {
                if (!Climb)
                {
                    //撒手的一瞬间
                    if (Physics.Raycast(PlayerPosition, ClimbDirection, out ClimbRay, 2, ClimbLaye))
                    {
                        ClimbDirection = ClimbRay.normal * -1;
                        Vector3 Ray2Point2 = PlayerPosition;
                        Ray2Point2.y += 2f;//2m高

                        //Main2.position = Ray2Point2;
                        if (!Physics.Raycast(Ray2Point2, ClimbDirection, 0.5f, ClimbLaye))
                        {//如果没有遮蔽物
                            Ray2Point2 += ClimbDirection * 0.5f;
                            if (Physics.Raycast(Ray2Point2, Vector3.down, out ClimbRay2, 2, ClimbLaye))
                            {
                                //Main3.position = ClimbRay2.point;
                                Tpos = ClimbRay2.point;
                                Tpos.y += 0.5f;
                                //localplayer.TeleportTo(Tpos, localplayer.GetRotation());
                                TreporTo = 0.2f;
                                //localplayer.SetVelocity(Vector3.zero);
                            }
                        }
                    }
                    //localplayer.SetVelocity(localplayer.GetVelocity() * 2);
                }
                Climb_last = Climb;
            }

            if (TreporTo > 0)
            {
                //localplayer.TeleportTo(Tpos, localplayer.GetRotation());
                //localplayer.SetVelocity(Vector3.zero);
                localplayer.SetVelocity((Tpos - localplayer.GetPosition()) / deltatime);
                TreporTo -= deltatime;
            }
        }

        //扳机键
        public override void InputUse(bool value, UdonInputEventArgs args)
        {
            if (ButtonSwitch)
                SetPosition(value, args.handType == HandType.RIGHT);
        }
        //握取键
        public override void InputGrab(bool value, UdonInputEventArgs args)
        {
            if (!ButtonSwitch)
                SetPosition(value, args.handType == HandType.RIGHT);
        }
        //public override void InputDrop(bool value, UdonInputEventArgs args)
        //{
        //    //TransRote(value, args.handType == HandType.RIGHT, L3, R3);
        //}


        //void TransRote(bool IsInput,bool Right,Transform L,Transform R) {
        //    if (IsInput)
        //    {
        //        if (Right) R.Rotate(0, 30, 0);
        //        else L.Rotate(0, 30, 0); 
        //    }
        //    else {
        //        if (Right) R.Rotate(0, -30, 0);
        //        else L.Rotate(0, -30, 0);
        //    }
        //}

        /// <summary>
        /// 按下按钮时记录状态
        /// </summary>
        /// <param name="IsInput">按下还是抬起</param>
        /// <param name="IsRight">左手还是右手</param>
        void SetPosition(bool IsInput, bool IsRight)
        {
            //if(IsRight)
            if (IsInput)
            {
                if (IsRight)
                {
                    if (RConllinder)
                    { RH = true; LockPos = Rhand.position; LH = false; SetClimbDirection(); }
                }
                else if (LConllinder) { LH = true; LockPos = Lhand.position; RH = false; SetClimbDirection(); }
            }
            else
            {
                if (IsRight) RH = false; else LH = false;
            }
        }

        /// <summary>
        /// 刷新检测朝向
        /// </summary>
        void SetClimbDirection()
        {
            Vector3 Ppos = Point - localplayer.GetPosition();
            Ppos.y = 0;
            ClimbDirection = Ppos;
        }

    }
}