
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonUI
{
    public class CatchPeopleMG : UdonSharpBehaviour
    {
        [Header("传送范围")] public float teleportRange = 0.0f;
        [Header("死亡特效")] public ParticleSystem dieEffe;
        [HideInInspector] public bool isLeftCatch = false, isRightCatch = false;
        [HideInInspector] public bool isLeftButton = false, isRightButton = false;
        [HideInInspector] public bool isLeftOnce = false, isRightOnce = false;
        [HideInInspector] public int PlayerID_L = 0, PlayerID_R = 0;
        [HideInInspector] public MainUI_Script mainUI = null;
        [HideInInspector] public Chat mainChat;
        [Header("执行ID，非必要不要动")]
        /*[HideInInspector]*/ public int eventsID = 0;//按钮ID

        //private float stopTime = 0.0f;
        private Vector3 startPos = Vector3.zero;
        private Transform Lhand, Rhand;
        private int initFrame = 120;
        //扳机键
        public override void InputUse(bool value, UdonInputEventArgs args)
        {
            if(args.handType == HandType.LEFT)
                isLeftButton = value;
            else if(args.handType == HandType.RIGHT)
                isRightButton = value;
        }
        //握取键
        /*        
                public override void InputGrab(bool value, UdonInputEventArgs args)
                {
        *//*            if (!ButtonSwitch)
                        SetPosition(value, args.handType == HandType.RIGHT);
        *//*        
                }
        */

        private void Start()
        {
            Lhand = transform.GetChild(0);
            Rhand = transform.GetChild(1);
        }
        private void Update()
        {
            if (initFrame > 0)
            {
                initFrame--;
                return;
            }
            if (mainUI.VRgm)
            {
                Vector3 LPos = mainUI.Lhand.position;
                Vector3 RPos = mainUI.Rhand.position;
                if (isLeftCatch && isLeftButton)
                {//左手抓住
                    if (!isLeftOnce)
                    {
                        isLeftOnce = true;
                        SendPlayerStop(PlayerID_L);
                    }
                }
                else
                {
                    if (isLeftOnce)
                    {
                        isLeftOnce = false;
                    }
                }

                if (isRightCatch && isRightButton)
                {//右手抓住
                    if (!isRightOnce)
                    {
                        isRightOnce = true;
                        SendPlayerCatch(PlayerID_R, RPos);
                    }
                }
                else
                {
                    if (isRightOnce)
                    {
                        isRightOnce = false;
                    }
                }
                float _height = Networking.LocalPlayer.GetPosition().y;
                LPos.y = _height;
                RPos.y = _height;
                Lhand.position = LPos;
                Rhand.position = RPos;
            }
            else
            {
                Vector3 LPos = mainUI.Head.TransformPoint(0, 0, 0.3f);
                if (Input.GetMouseButton(0) && isLeftCatch)
                {
                    if (!isRightOnce)
                    {
                        isRightOnce = true;
                        SendPlayerCatch(PlayerID_L, LPos);
                    }
                }
                else
                {
                    if (isRightOnce)
                    {
                        isRightOnce = false;
                    }
                }

                if (Input.GetMouseButton(1) && isLeftCatch)
                {
                    if (!isLeftOnce)
                    {
                        isLeftOnce = true;
                        SendPlayerStop(PlayerID_L);
                    }
                }
                else
                {
                    if (isLeftOnce)
                    {
                        isLeftOnce = false;
                    }
                }
                LPos.y = Networking.LocalPlayer.GetPosition().y;
                Lhand.position = LPos;
            }
        }

        #region 发送
        private void SendPlayerStop(int _playerID) 
        {
            mainChat.SendMG($"action@{_playerID}@{eventsID}@0");
        }
        private void SendPlayerCatch(int _playerID,Vector3 _effPos) 
        {
            mainChat.SendMG($"action@{_playerID}@{eventsID}@1");
            dieEffe.transform.position = _effPos;
            dieEffe.Play();
        }
        #endregion

        #region 接收
        public void OnSetStop()
        {
            return;
            if (gameObject.activeSelf) return;
            VRCPlayerApi _localPlayer = Networking.LocalPlayer;
            walkSpeed = _localPlayer.GetWalkSpeed();
            runSpeed = _localPlayer.GetRunSpeed();
            strafeSpeed = _localPlayer.GetStrafeSpeed();
            _localPlayer.SetWalkSpeed(0);
            _localPlayer.SetRunSpeed(0);
            _localPlayer.SetStrafeSpeed(0);
            this.SendCustomEventDelayedSeconds("ResetSpeed", 10.0f);
        }
        public void OnSetCatch() 
        {
            if (gameObject.activeSelf) return;
            //VRCPlayerApi _localPlayer = Networking.LocalPlayer;

            if (teleportRange > 0)
            {
                Quaternion _rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
                //Networking.LocalPlayer.TeleportTo(transform.position + (_rot * Vector3.back * teleportRange), _rot);
                mainUI.vrcWorld.SetPositionAndRotation(transform.position + (_rot * Vector3.back * teleportRange), _rot);
                Networking.LocalPlayer.Respawn();
            }
            else
            {
                //Networking.LocalPlayer.TeleportTo(transform.position, transform.rotation);
                mainUI.vrcWorld.SetPositionAndRotation(transform.position, transform.rotation);
                Networking.LocalPlayer.Respawn();
            }
        }

        public float jump = 0;
        public float walkSpeed = 0;
        public float runSpeed = 0;
        public float strafeSpeed = 0;
        public void ResetSpeed() {
            VRCPlayerApi _localPlayer = Networking.LocalPlayer;
            _localPlayer.SetWalkSpeed(walkSpeed);
            _localPlayer.SetRunSpeed(runSpeed);
            _localPlayer.SetStrafeSpeed(strafeSpeed);
            _localPlayer.SetStrafeSpeed(jump);
        }

        public void OnCheck() { mainUI.checkID++; }//检查当前脚本
        #endregion

        #region 修改玩家速度
        public void SetSpeed() 
        {
            float _b = 1.3f;
            VRCPlayerApi _localPlayer = Networking.LocalPlayer;
            //walkSpeed = _localPlayer.GetWalkSpeed();
            //runSpeed = _localPlayer.GetRunSpeed();
            //strafeSpeed = _localPlayer.GetStrafeSpeed();
            _localPlayer.SetWalkSpeed(walkSpeed * _b);
            _localPlayer.SetRunSpeed(runSpeed * _b);
            _localPlayer.SetStrafeSpeed(strafeSpeed * _b);
            _localPlayer.SetStrafeSpeed(jump * _b);
        }
        #endregion

    }
}
