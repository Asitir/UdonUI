
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_Gate : UdonSharpBehaviour
    {
        public float m_scale = 0.66f;
        public Transform MainBody;
        public Transform[] ClipObject;
        public Transform RenderCamera;
        public Transform target01, target02;
        public Transform NullObj;
        public Transform PlayerHead;
        Camera main_Came;
        Vector3 player_LatePos;
        bool checkTeleport = false;
        float Freeze = -1;
        Vector3 GatePos = Vector3.zero;
        Vector3 LocalScale = Vector3.zero;
        bool InWork = false;
        Vector2 limit = Vector2.zero;
        Quaternion localRot;
        Vector3 localPos;

        Matrix4x4 Tc;
        bool NowA = true, NowB = false;
        private void Start()
        {
//#if !UNITY_EDITOR
            PlayerHead = Instantiate(NullObj.gameObject).transform;
//#endif
            main_Came = RenderCamera.GetChild(0).GetComponent<Camera>();
            Tc = main_Came.projectionMatrix;
            OpenGate();
            MainBody.gameObject.SetActive(false);
            ClipObject[0].gameObject.SetActive(true);
            ClipObject[1].gameObject.SetActive(true);
        }
        private void Update()
        {
            UpdateMain();
        }

        void OpenGate() {
            main_Came.enabled = true;
            NowA = (target01.position - PlayerHead.position).sqrMagnitude < (target02.position - PlayerHead.position).sqrMagnitude;
            Vector3 tarrget01scale = target01.lossyScale;
            Vector3 tarrget02scale = target02.lossyScale;
            target01.localScale = new Vector3(target01.localScale.x / tarrget01scale.x, target01.localScale.y / tarrget01scale.y, target01.localScale.z / tarrget01scale.z);
            target02.localScale = new Vector3(target02.localScale.x / tarrget02scale.x, target02.localScale.y / tarrget02scale.y, target02.localScale.z / tarrget02scale.z);
            GatePos = MainBody.localPosition;
            LocalScale = MainBody.lossyScale * 0.5f;
            limit.x = GatePos.y - LocalScale.y;
            limit.y = GatePos.y + LocalScale.y;
            ClipObject[0].localPosition = MainBody.localPosition;
            ClipObject[0].localScale = MainBody.localScale;
            ClipObject[1].localPosition = MainBody.localPosition;
            ClipObject[1].localScale = MainBody.localScale;
            ////main_Came.projectionMatrix = PlayerCamera.projectionMatrix;
            //main_Came.farClipPlane = 0.01f;
            Init();
//#if !UNITY_EDITOR
            VRCPlayerApi.TrackingData m_playerHead = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            PlayerHead.position = m_playerHead.position;
            PlayerHead.rotation = m_playerHead.rotation;
//#endif
            Vector3 m_LocalPos = PlayerHead.localPosition;
            checkTeleport = m_LocalPos.z > 0;
            SetForward(checkTeleport);
        }

        public void Init()
        {//传送时的初始化
            if (NowA)
            {
                RenderCamera.SetParent(target02);
                PlayerHead.SetParent(target01);
            }
            else
            {
                RenderCamera.SetParent(target01);
                PlayerHead.SetParent(target02);
            }
        }
        public void UpdateMain()
        {
//#if !UNITY_EDITOR
            VRCPlayerApi.TrackingData m_playerHead = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            PlayerHead.position = m_playerHead.position;
            PlayerHead.rotation = m_playerHead.rotation;
//#endif

            NowA = (target01.position - PlayerHead.position).sqrMagnitude < (target02.position - PlayerHead.position).sqrMagnitude;
            localPos = PlayerHead.localPosition;
            if (Freeze < 0)
            {
                if (localPos.z > 0)
                {
                    if (!checkTeleport)
                    {
                        SetForward(true);
                        PlayerTeport(localPos.y);
                        checkTeleport = true;
                    }
                }
                else
                {
                    if (checkTeleport)
                    {
                        SetForward(false);
                        PlayerTeport(localPos.y);
                        checkTeleport = false;
                    }
                }
            }

            float Distance = 1;
            if (NowA)
            {
                if (!NowB)
                {
                    Init();
                    NowB = true;
                }
            }
            else
            {
                if (NowB)
                {
                    Init();
                    NowB = false;
                }
            }

            if (localPos.x < LocalScale.x && localPos.x > LocalScale.x * -1)
            {
                if (!InWork)
                {
                    InWork = true;
                    SetOn(true);
                }
            }
            else 
            {
                if (InWork) 
                { 
                    InWork = false;
                    SetOn(false);
                }
            }

            localRot = PlayerHead.localRotation;


            Vector3 LatePos = localPos - player_LatePos;
            player_LatePos = localPos;

            Distance = Vector3.Distance(localPos, Vector3.zero);

            Transform a = RenderCamera.parent;
            StePos(main_Came, a, a.TransformPoint(localPos));
            //ClipPlan(a);
            if (!(Freeze < 0))
                Freeze -= Time.deltaTime;

        }

        void StePos(Camera came,Transform refal, Vector3 pos) {
            //#if !UNITY_EDITOR
            if (Networking.LocalPlayer.IsUserInVR())
            {
                Transform camet = came.transform;

                Quaternion vro = refal.rotation * PlayerHead.localRotation;

                var _rot = vro * Quaternion.Inverse(camet.localRotation);
                var _pos = pos - _rot * camet.localPosition * m_scale;
                RenderCamera.SetPositionAndRotation(_pos, _rot);
                RenderCamera.localScale = Vector3.one * m_scale;
            }
            else
            //#endif
            {
                RenderCamera.localPosition = localPos;
                RenderCamera.localRotation = localRot;
            }
        }
        public void PlayerTeleport()
        {
            Matrix4x4 pp;
            NullObj.position = Networking.LocalPlayer.GetPosition();
            NullObj.rotation = Networking.LocalPlayer.GetRotation();
            Vector3 playerVelocity = Networking.LocalPlayer.GetVelocity();
            Quaternion SetRot = Quaternion.identity;
            if (NowA)
            {
                pp = target01.worldToLocalMatrix * NullObj.localToWorldMatrix;
                Matrix4x4 tB = target02.localToWorldMatrix * pp;
                Networking.LocalPlayer.TeleportTo(tB.GetColumn(3), tB.rotation);
                SetRot = Quaternion.FromToRotation(target01.forward, target02.forward);
            }
            else
            {
                pp = target02.worldToLocalMatrix * NullObj.localToWorldMatrix;
                Matrix4x4 tB = target01.localToWorldMatrix * pp;
                Networking.LocalPlayer.TeleportTo(tB.GetColumn(3), tB.rotation);
                SetRot = Quaternion.FromToRotation(target02.forward, target01.forward);
            }
            Networking.LocalPlayer.SetVelocity(SetRot * playerVelocity);
            Freeze = 0.1f;

        }

        void SetForward(bool forward_) {
            ClipObject[0].localRotation = Quaternion.Euler(0, forward_ ? 0 : 180, 0);
            ClipObject[1].localRotation = Quaternion.Euler(0, forward_ ? 0 : 180, 0);
        }
        void PlayerTeport(float height_) {
//#if !UNITY_EDITOR
            if (InWork)
            {
                if (height_ > limit.x && height_ < limit.y)
                {
                    PlayerTeleport();
                }
            }
//#endif
        }

        void ClipPlan(Transform clipViwe) {

            int dot = System.Math.Sign(Vector3.Dot(clipViwe.forward, clipViwe.position - RenderCamera.position));
            Vector3 camSpacePos = main_Came.worldToCameraMatrix.MultiplyPoint(clipViwe.position);
            Vector3 camSpaceNormal = main_Came.worldToCameraMatrix.MultiplyVector(clipViwe.forward) * dot;
            float camSpaceDst = -Vector3.Dot(camSpacePos, camSpaceNormal) + 0.05f;

            if (Mathf.Abs(camSpaceDst) > 0.2f)
            {                   
                Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);
                main_Came.projectionMatrix = main_Came.CalculateObliqueMatrix(clipPlaneCameraSpace);

            }
            else
            {
                main_Came.projectionMatrix = Tc;
            }
        }
        void SetOn(bool isOn) {
            foreach (Transform a in ClipObject)
            {
                a.localScale = new Vector3(a.localScale.x, a.localScale.y, (isOn ? 0.4f : LocalScale.z * 2));
            }
        }
    }
}