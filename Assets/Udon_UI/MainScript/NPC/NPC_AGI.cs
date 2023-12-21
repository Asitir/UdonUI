
using UdonSharp;
using UnityEngine;
using UnityEngine.Animations;
using VRC.SDKBase;
using VRC.Udon.Common;
using VRC.Udon;

namespace UdonUI
{
    public class NPC_AGI : UdonSharpBehaviour
    {
        //public SkinnedMeshRenderer[] FaceObjects;
        bool FaceHide = true;
        Vector3 HideFacePos = new Vector3(0, 0, -1);
        Vector3 NowPos, LatePos, DeltaPos;
        bool ReSet = true;
        float LeftHandIK = 0, RightHandIK = 0, LeftLegIK = 0, RightLegIK = 0;
        //[HideInInspector]
        public Transform[] LeftHandIKBones, RightHandIKBones, LeftLegIKBones, RightLegIKBones;
        //Transform[] LeftHandIKBons_, RightHandIKBones_, LeftLegIKBones_, RightLegIKBones_;
        bool LeftHandIKBones_On = false, RightHandIKBones_On = false, LeftLegIKBones_On = false, RightLegIKBones_On = false;
        float LeftHandIKBones_Lerp, RightHandIKBones_Lerp, LeftLegIKBones_Lerp, RightLegIKBones_Lerp;
        int IKLoocConst = 1;//IK循环次数
        public Transform[] AllBone;
        Animator MainAnim;
        [HideInInspector]
        public VRCPlayerApi Localplayer = null;
        float Deltatime;
        bool LatePlayerValue = false;
        GameObject[] ObjectMeshs;
        //Avatar avatartest;
        Vector3 HipsPos;
        Quaternion HipsRot, SpineRot, ChestRot, UpperChestRot, NeckRot, HeadRot;//躯干
        Quaternion LeftUpperLegRot, LeftLowerLegRot, LeftFootRot;//左大腿
        Quaternion RightUpperLegRot, RightLowerLegRot, RightFootRot;//右大腿
        Quaternion LeftShoulderRot, LeftUpperArmRot, LeftLowerArmRot, LeftHandRot;//左手手臂
                                                                                  //Vector3 LeftUpperArmPos, LeftLowerArmPos, LefttTrackingPos;
        Quaternion RightShoulderRot, RightUpperArmRot, RightLowerArmRot, RightHandRot;//右手手臂
        Quaternion LeftThumbProximalRot, LeftThumbIntermediateRot, LeftThumbDistalRot, LeftIndexProximalRot, LeftIndexIntermediateRot, LeftIndexDistalRot, LeftMiddleProximalRot, LeftMiddleIntermediateRot, LeftMiddleDistalRot, LeftRingProximalRot, LeftRingIntermediateRot, LeftRingDistalRot, LeftLittleProximalRot, LeftLittleIntermediateRot, LeftLittleDistalRot;
        Quaternion RightThumbProximalRot, RightThumbIntermediateRot, RightThumbDistalRot, RightIndexProximalRot, RightIndexIntermediateRot, RightIndexDistalRot, RightMiddleProximalRot, RightMiddleIntermediateRot, RightMiddleDistalRot, RightRingProximalRot, RightRingIntermediateRot, RightRingDistalRot, RightLittleProximalRot, RightLittleIntermediateRot, RightLittleDistalRot;

        //[Header("    --------躯干------")]
        [HideInInspector]
        public Transform Hips, Spine, Chest, Neck, Head;
        //[Header("    --------大腿------")]
        [HideInInspector]
        public Transform LeftUpperLeg, LeftLowerLeg, LeftFoot, RightUpperLeg, RightLowerLeg, RightFoot;
        //[Header("    --------手臂------")]
        [HideInInspector]
        public Transform LeftShoulder, LeftUpperArm, LeftLowerArm, LeftHand, RightShoulder, RightUpperArm, RightLowerArm, RightHand;

        void Start()
        {
            LeftHandIKBones = new Transform[3];
            LeftLegIKBones = new Transform[3];
            RightHandIKBones = new Transform[3];
            RightLegIKBones = new Transform[3];
            //LeftHandIKBons_ = new Transform[3];
            //LeftLegIKBones_ = new Transform[3];
            //RightHandIKBones_ = new Transform[3];
            //RightLegIKBones_ = new Transform[3];

            ObjectMeshs = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                ObjectMeshs[i] = transform.GetChild(i).gameObject;

            //Localplayer = Networking.LocalPlayer;
            MainAnim = GetComponent<Animator>();
            //HideFace();
            Hips = AllBone[0];
            Spine = AllBone[1];
            Chest = AllBone[2];
            Neck = AllBone[3];
            Head = AllBone[4];

            LeftUpperLeg = AllBone[5];
            LeftLowerLeg = AllBone[6];
            LeftFoot = AllBone[7];
            RightUpperLeg = AllBone[8];
            RightLowerLeg = AllBone[9];
            RightFoot = AllBone[10];

            LeftShoulder = AllBone[11];
            LeftUpperArm = AllBone[12];
            //LeftHandIKBons_[0] = AllBone[12];
            LeftLowerArm = AllBone[13];
            //LeftHandIKBons_[1] = AllBone[13];
            LeftHand = AllBone[14];
            //LeftHandIKBons_[2] = AllBone[14];
            RightShoulder = AllBone[15];
            RightUpperArm = AllBone[16];
            RightLowerArm = AllBone[17];
            RightHand = AllBone[18];

            LeftHandIKBones[0] = AllBone[19];
            LeftHandIKBones[1] = AllBone[20];
            LeftHandIKBones[2] = AllBone[21];
            LeftHandIKBones[2].SetParent(LeftHandIKBones[1]);
            LeftHandIKBones[1].SetParent(LeftHandIKBones[0]);
            LeftHandIKBones[0].SetParent(AllBone[11]);

            RightHandIKBones[0] = AllBone[22];
            RightHandIKBones[1] = AllBone[23];
            RightHandIKBones[2] = AllBone[24];
            RightHandIKBones[2].SetParent(RightHandIKBones[1]);
            RightHandIKBones[1].SetParent(RightHandIKBones[0]);
            RightHandIKBones[0].SetParent(AllBone[15]);

            LeftLegIKBones[0] = AllBone[25];
            LeftLegIKBones[1] = AllBone[26];
            LeftLegIKBones[2] = AllBone[27];
            LeftLegIKBones[2].SetParent(LeftLegIKBones[1]);
            LeftLegIKBones[1].SetParent(LeftLegIKBones[0]);
            LeftLegIKBones[0].SetParent(AllBone[0]);

            RightLegIKBones[0] = AllBone[28];
            RightLegIKBones[1] = AllBone[29];
            RightLegIKBones[2] = AllBone[30];
            RightLegIKBones[2].SetParent(RightLegIKBones[1]);
            RightLegIKBones[1].SetParent(RightLegIKBones[0]);
            RightLegIKBones[0].SetParent(AllBone[0]);

        }
        private void Update()
        {
            //GetLeftHandBone(1, Vector3.zero);

            if (Localplayer != null)
            {
                Deltatime = Time.deltaTime;
                GetBones();
                if (!LatePlayerValue)
                {
                    //如果上一帧是关着模型显示的  则全部开启显示
                    for (int i = 0; i < ObjectMeshs.Length; i++)
                    {
                        ObjectMeshs[i].SetActive(true);
                    }
                }
                LatePlayerValue = true;
            }
            else
            {
                if (LatePlayerValue)
                {
                    //如果上一帧是开着模型显示的  则全部关闭显示
                    for (int i = 0; i < ObjectMeshs.Length; i++)
                    {
                        ObjectMeshs[i].SetActive(false);
                    }
                }
                LatePlayerValue = false;
            }
        }
        //扳机键
        public override void InputUse(bool value, UdonInputEventArgs args)
        {
            //if (ButtonSwitch)
            //    SetPosition(value, args.handType == HandType.RIGHT);
            if (value)
            {
                //按下后
                if (args.handType == HandType.RIGHT)
                {
                    //右手
                    RightHandIKBones_On = true;
                }
                else
                {
                    LeftHandIKBones_On = true;
                }
            }
            else
            {
                //松开后
                if (args.handType == HandType.RIGHT)
                {
                    //右手
                    RightHandIKBones_On = false;
                }
                else
                {
                    LeftHandIKBones_On = false;
                }

            }
        }

        void GetBones()
        {
            //获取躯干
            GetBones_trunk();

            #region IK测试
            if (Input.GetKeyDown(KeyCode.I)) LeftHandIKBones_On = true;
            if (Input.GetKeyUp(KeyCode.I)) LeftHandIKBones_On = false;

            if (Input.GetKeyDown(KeyCode.K)) RightHandIKBones_On = true;
            if (Input.GetKeyUp(KeyCode.K)) RightHandIKBones_On = false;

            if (Input.GetKeyDown(KeyCode.O)) LeftLegIKBones_On = true;
            if (Input.GetKeyUp(KeyCode.O)) LeftLegIKBones_On = false;

            if (Input.GetKeyDown(KeyCode.L)) RightLegIKBones_On = true;
            if (Input.GetKeyUp(KeyCode.L)) RightLegIKBones_On = false;
            #endregion

            //获取左手
            if (LeftHandIKBones_On) LeftHandIKBones_Lerp += Deltatime * 5;
            else LeftHandIKBones_Lerp -= Deltatime * 5;
            LeftHandIKBones_Lerp = Mathf.Clamp01(LeftHandIKBones_Lerp);
            GetLeftHandBone(LeftHandIKBones_Lerp, Vector3.zero);
            //获取右手
            if (RightHandIKBones_On) RightHandIKBones_Lerp += Deltatime * 5;
            else RightHandIKBones_Lerp -= Deltatime * 5;
            RightHandIKBones_Lerp = Mathf.Clamp01(RightHandIKBones_Lerp);
            GetRightHandBone(RightHandIKBones_Lerp, Vector3.zero);
            //获取左脚
            if (LeftLegIKBones_On) LeftLegIKBones_Lerp += Deltatime * 5;
            else LeftLegIKBones_Lerp -= Deltatime * 5;
            LeftLegIKBones_Lerp = Mathf.Clamp01(LeftLegIKBones_Lerp);
            GetLeftLegBone(LeftLegIKBones_Lerp, Vector3.zero);
            //获取右脚
            if (RightLegIKBones_On) RightLegIKBones_Lerp += Deltatime * 5;
            else RightLegIKBones_Lerp -= Deltatime * 5;
            RightLegIKBones_Lerp = Mathf.Clamp01(RightLegIKBones_Lerp);
            GetRightLegBone(RightLegIKBones_Lerp, Vector3.zero);

            //获取手指
            //GetBones_LeftHand();
            //GetBones_RightHand();
        }

        //设置骨骼
        private void LateUpdate()
        {

            if (Localplayer != null)
            {
                //躯干
                //Hips.position = HipsPos + DeltaPos;
                Hips.position = HipsPos;
                Hips.rotation = HipsRot;
                Spine.rotation = SpineRot;
                Chest.rotation = ChestRot;
                Neck.rotation = NeckRot;
                Head.rotation = HeadRot;

                //左手
                //LeftShoulder.rotation = LeftShoulderRot;//左肩骨。
                LeftUpperArm.rotation = LeftUpperArmRot;//左上臂骨。
                LeftLowerArm.rotation = LeftLowerArmRot;//左肘骨。
                LeftHand.rotation = LeftHandRot;//左腕骨。

                //右手
                //RightShoulder.rotation = RightShoulderRot;//左肩骨。
                RightUpperArm.rotation = RightUpperArmRot;//左上臂骨。
                RightLowerArm.rotation = RightLowerArmRot;//左肘骨。
                RightHand.rotation = RightHandRot;//左腕骨。

                //左腿
                LeftUpperLeg.rotation = LeftUpperLegRot;//
                LeftLowerLeg.rotation = LeftLowerLegRot;//
                LeftFoot.rotation = LeftFootRot;//

                //右腿
                RightUpperLeg.rotation = RightUpperLegRot;//
                RightLowerLeg.rotation = RightLowerLegRot;//
                RightFoot.rotation = RightFootRot;//

                //设置手指
                //SetFinger();
            }
        }
        #region GetBones
        //取得躯干
        void GetBones_trunk()
        {
            LatePos = HipsPos;//取得盆骨更新前的位置
            HipsPos = Localplayer.GetBonePosition(HumanBodyBones.Hips);//盆骨位置
            HipsRot = Localplayer.GetBoneRotation(HumanBodyBones.Hips);//盆骨角度
            SpineRot = Localplayer.GetBoneRotation(HumanBodyBones.Spine);//第一节椎骨。
            ChestRot = Localplayer.GetBoneRotation(HumanBodyBones.Chest);//胸骨。
            NeckRot = Localplayer.GetBoneRotation(HumanBodyBones.Neck);//颈骨。
            HeadRot = Localplayer.GetBoneRotation(HumanBodyBones.Head);//头骨。

            //Quaternion LeftToesRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftToes);//左脚趾骨。
            //Quaternion RightToesRot = Localplayer.GetBoneRotation(HumanBodyBones.RightToes);//右脚趾骨。
            Quaternion LeftEyeRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftEye);//左眼骨。
            Quaternion RightEyeRot = Localplayer.GetBoneRotation(HumanBodyBones.RightEye);//右眼骨。
                                                                                          //Quaternion JawRot = Localplayer.GetBoneRotation(HumanBodyBones.Jaw);//颚骨。
            NowPos = HipsPos;

            if (ReSet)
            {
                //重置
                LatePos = NowPos;
                ReSet = false;
            }
            DeltaPos = NowPos - LatePos;
        }
        void GetBones_LeftArm()
        {
            LeftShoulderRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftShoulder);//左肩骨。
            LeftUpperArmRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftUpperArm);//左上臂骨。
            LeftLowerArmRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLowerArm);//左肘骨。
            LeftHandRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftHand);//左腕骨。

            //LefttTrackingPos = Localplayer.GetTrackingData(VRCPlayerApi.TrackingDataType.LeftHand).position;
            //LeftUpperArmPos = Localplayer.GetBonePosition(HumanBodyBones.LeftUpperArm);//左上臂骨。
            //LeftLowerArmPos = Localplayer.GetBonePosition(HumanBodyBones.LeftLowerArm);//左肘骨。
        }
        void GetBones_RightArm()
        {
            RightShoulderRot = Localplayer.GetBoneRotation(HumanBodyBones.RightShoulder);//右肩骨。
            RightUpperArmRot = Localplayer.GetBoneRotation(HumanBodyBones.RightUpperArm);//右上臂骨。
            RightLowerArmRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLowerArm);//右肘骨。
            RightHandRot = Localplayer.GetBoneRotation(HumanBodyBones.RightHand);//右腕骨。
        }
        void GetBones_LeftLeg()
        {
            LeftUpperLegRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftUpperLeg);//左大腿骨。
            LeftLowerLegRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLowerLeg);//左膝骨。
            LeftFootRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftFoot);//左脚踝骨。
        }
        void GetBones_RightLeg()
        {
            RightUpperLegRot = Localplayer.GetBoneRotation(HumanBodyBones.RightUpperLeg);//右大腿骨。
            RightLowerLegRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLowerLeg);//右膝骨。
            RightFootRot = Localplayer.GetBoneRotation(HumanBodyBones.RightFoot);//右脚踝骨。
        }
        void GetBones_LeftHand()
        {
            LeftThumbProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftThumbProximal);//左手大拇指的第 1 根指骨。
            LeftThumbIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftThumbIntermediate);//左手大拇指的第 2 根指骨。
            LeftThumbDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftThumbDistal);//左手大拇指的第 3 根指骨。
            LeftIndexProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftIndexProximal);//左手食指的第 1 根指骨。
            LeftIndexIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftIndexIntermediate);//左手食指的第 2 根指骨。
            LeftIndexDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftIndexDistal);//左手食指的第 3 根指骨。
            LeftMiddleProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftMiddleProximal);//左手中指的第 1 根指骨。 
            LeftMiddleIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftMiddleIntermediate);//左手中指的第 2 根指骨。
            LeftMiddleDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftMiddleDistal);//左手中指的第 3 根指骨。
            LeftRingProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftRingProximal);//左手无名指的第 1 根指骨。
            LeftRingIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftRingIntermediate);//左手无名指的第 2 根指骨。
            LeftRingDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftRingDistal);//左手无名指的第 3 根指骨。
            LeftLittleProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLittleProximal);//左手小拇指的第 1 根指骨。
            LeftLittleIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLittleIntermediate);//左手小拇指的第 2 根指骨。
            LeftLittleDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLittleDistal);//左手小拇指的第 3 根指骨。
        }
        void GetBones_RightHand()
        {
            RightThumbProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightThumbProximal);//右手大拇指的第 1 根指骨。
            RightThumbIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.RightThumbIntermediate);//右手大拇指的第 2 根指骨。
            RightThumbDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightThumbDistal);//右手大拇指的第 3 根指骨。
            RightIndexProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightIndexProximal);//右手食指的第 1 根指骨。
            RightIndexIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.RightIndexIntermediate);//右手食指的第 2 根指骨。
            RightIndexDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightIndexDistal);//右手食指的第 3 根指骨。
            RightMiddleProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightMiddleProximal);//右手中指的第 1 根指骨。 
            RightMiddleIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.RightMiddleIntermediate);//右手中指的第 2 根指骨。
            RightMiddleDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightMiddleDistal);//右手中指的第 3 根指骨。
            RightRingProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightRingProximal);//右手无名指的第 1 根指骨。
            RightRingIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.RightRingIntermediate);//右手无名指的第 2 根指骨。
            RightRingDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightRingDistal);//右手无名指的第 3 根指骨。
            RightLittleProximalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLittleProximal);//右手小拇指的第 1 根指骨。
            RightLittleIntermediateRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLittleIntermediate);//右手小拇指的第 2 根指骨。
            RightLittleDistalRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLittleDistal);//右手小拇指的第 3 根指骨。
        }

        //设置手指
        void SetFinger()
        {
            AllBone[31].rotation = LeftThumbProximalRot;
            AllBone[32].rotation = LeftThumbIntermediateRot;
            AllBone[33].rotation = LeftThumbDistalRot;
            AllBone[34].rotation = LeftIndexProximalRot;
            AllBone[35].rotation = LeftIndexIntermediateRot;
            AllBone[36].rotation = LeftIndexDistalRot;
            AllBone[37].rotation = LeftMiddleProximalRot;
            AllBone[38].rotation = LeftMiddleIntermediateRot;
            AllBone[39].rotation = LeftMiddleDistalRot;
            AllBone[40].rotation = LeftRingProximalRot;
            AllBone[41].rotation = LeftRingIntermediateRot;
            AllBone[42].rotation = LeftRingDistalRot;
            AllBone[43].rotation = LeftLittleProximalRot;
            AllBone[44].rotation = LeftLittleIntermediateRot;
            AllBone[45].rotation = LeftLittleDistalRot;

            AllBone[46].rotation = RightThumbProximalRot;
            AllBone[47].rotation = RightThumbIntermediateRot;
            AllBone[48].rotation = RightThumbDistalRot;
            AllBone[49].rotation = RightIndexProximalRot;
            AllBone[50].rotation = RightIndexIntermediateRot;
            AllBone[51].rotation = RightIndexDistalRot;
            AllBone[52].rotation = RightMiddleProximalRot;
            AllBone[53].rotation = RightMiddleIntermediateRot;
            AllBone[54].rotation = RightMiddleDistalRot;
            AllBone[55].rotation = RightRingProximalRot;
            AllBone[56].rotation = RightRingIntermediateRot;
            AllBone[57].rotation = RightRingDistalRot;
            AllBone[58].rotation = RightLittleProximalRot;
            AllBone[59].rotation = RightLittleIntermediateRot;
            AllBone[60].rotation = RightLittleDistalRot;

        }
        #endregion

        public void DisplayFace()
        {
            ReSet = true;
            FaceHide = false;
            MainAnim.Play("bounds_Idle", 0);
        }

        public void HideFace()
        {
            FaceHide = true;
            MainAnim.Play("bounds_OFF", 0);
        }

        void GetLeftHandBone(float Bd, Vector3 IKTargetPos)
        {
            if (Bd == 0)
            {
                //完全关闭IK
                //LeftShoulderRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftShoulder);//左肩骨。
                LeftUpperArmRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftUpperArm);//左上臂骨。
                LeftLowerArmRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLowerArm);//左肘骨。
                LeftHandRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftHand);//左腕骨。
            }
            else if (Bd == 1)
            {
                //完全开启IK
                Transform[] IK_Trans = LeftHandIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.LeftLowerArm);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                LeftUpperArmRot = IK_Trans[0].rotation;//左上臂骨。
                LeftLowerArmRot = IK_Trans[1].rotation;//左肘骨。
                                                       //LeftHandRot = IK_Trans[2].rotation;//左腕骨。
                LeftHandRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftHand);//左腕骨。
            }
            else
            {
                //过度
                Transform[] IK_Trans = LeftHandIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.LeftLowerArm);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                LeftUpperArmRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftUpperArm), IK_Trans[0].rotation, Bd);
                LeftLowerArmRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftLowerArm), IK_Trans[1].rotation, Bd);
                //LeftHandRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftHand), IK_Trans[2].rotation, Bd);
                LeftHandRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftHand);//左腕骨。
            }
        }
        void GetRightHandBone(float Bd, Vector3 IKTargetPos)
        {
            if (Bd == 0)
            {
                //完全关闭IK
                //LeftShoulderRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftShoulder);//左肩骨。
                RightUpperArmRot = Localplayer.GetBoneRotation(HumanBodyBones.RightUpperArm);//左上臂骨。
                RightLowerArmRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLowerArm);//左肘骨。
                RightHandRot = Localplayer.GetBoneRotation(HumanBodyBones.RightHand);//左腕骨。
            }
            else if (Bd == 1)
            {
                //完全开启IK
                Transform[] IK_Trans = RightHandIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.RightLowerArm);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                RightUpperArmRot = IK_Trans[0].rotation;//左上臂骨。
                RightLowerArmRot = IK_Trans[1].rotation;//左肘骨。
                                                        //RightHandRot = IK_Trans[2].rotation;//左腕骨。
                RightHandRot = Localplayer.GetBoneRotation(HumanBodyBones.RightHand);//左腕骨。
            }
            else
            {
                //过度
                Transform[] IK_Trans = RightHandIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.RightLowerArm);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                RightUpperArmRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.RightUpperArm), IK_Trans[0].rotation, Bd);
                RightLowerArmRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.RightLowerArm), IK_Trans[1].rotation, Bd);
                //RightHandRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.RightHand), IK_Trans[2].rotation, Bd);
                RightHandRot = Localplayer.GetBoneRotation(HumanBodyBones.RightHand);//左腕骨。
            }
        }
        void GetLeftLegBone(float Bd, Vector3 IKTargetPos)
        {
            if (Bd == 0)
            {
                //完全关闭IK Hand
                LeftUpperLegRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftUpperLeg);//左大腿
                LeftLowerLegRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftLowerLeg);//左小腿
                LeftFootRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftFoot);//左脚
            }
            else if (Bd == 1)
            {
                //完全开启IK
                Transform[] IK_Trans = LeftLegIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.LeftLowerLeg);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                LeftUpperLegRot = IK_Trans[0].rotation;//左上臂骨。
                LeftLowerLegRot = IK_Trans[1].rotation;//左肘骨。
                                                       //LeftHandRot = IK_Trans[2].rotation;//左腕骨。
                LeftFootRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftFoot);//左腕骨。
            }
            else
            {
                //过度
                Transform[] IK_Trans = LeftLegIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.LeftLowerLeg);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                LeftUpperLegRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftUpperLeg), IK_Trans[0].rotation, Bd);
                LeftLowerLegRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftLowerLeg), IK_Trans[1].rotation, Bd);
                //LeftHandRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftHand), IK_Trans[2].rotation, Bd);
                LeftFootRot = Localplayer.GetBoneRotation(HumanBodyBones.LeftFoot);//左腕骨。
            }
        }
        void GetRightLegBone(float Bd, Vector3 IKTargetPos)
        {
            if (Bd == 0)
            {
                //完全关闭IK Hand
                RightUpperLegRot = Localplayer.GetBoneRotation(HumanBodyBones.RightUpperLeg);//左大腿
                RightLowerLegRot = Localplayer.GetBoneRotation(HumanBodyBones.RightLowerLeg);//左小腿
                RightFootRot = Localplayer.GetBoneRotation(HumanBodyBones.RightFoot);//左脚
            }
            else if (Bd == 1)
            {
                //完全开启IK
                Transform[] IK_Trans = RightLegIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.RightLowerLeg);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                RightUpperLegRot = IK_Trans[0].rotation;//左上臂骨。
                RightLowerLegRot = IK_Trans[1].rotation;//左肘骨。
                                                        //LeftHandRot = IK_Trans[2].rotation;//左腕骨。
                RightFootRot = Localplayer.GetBoneRotation(HumanBodyBones.RightFoot);//左腕骨。
            }
            else
            {
                //过度
                Transform[] IK_Trans = RightLegIKBones;
                for (int t = 0; t < IKLoocConst; t++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Vector3 end = IK_Trans[2].position - IK_Trans[i].position;
                        Vector3 targe = IKTargetPos - IK_Trans[i].position;
                        Quaternion Rota = Quaternion.FromToRotation(end, targe) * IK_Trans[i].rotation;
                        IK_Trans[i].rotation = Rota;
                        IK_Trans[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(IK_Trans[1].localEulerAngles.z, 5, 180), IK_Trans[0].forward) * IK_Trans[0].rotation;
                    }
                }

                Vector3 Axis = IKTargetPos - IK_Trans[0].position;//轴
                Vector3 IKTarget_02 = Localplayer.GetBonePosition(HumanBodyBones.RightLowerLeg);//左肘骨。

                Vector3 K1 = Vector3.Cross(IK_Trans[1].position - IK_Trans[0].position, Axis);//IK向量
                Vector3 K2 = Vector3.Cross(IKTarget_02 - IK_Trans[0].position, Axis);//原骨骼向量

                float Ddir = Vector3.SignedAngle(K1, K2, Axis);
                IK_Trans[0].rotation = Quaternion.AngleAxis(Ddir, Axis) * IK_Trans[0].rotation;

                RightUpperLegRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.RightUpperLeg), IK_Trans[0].rotation, Bd);
                RightLowerLegRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.RightLowerLeg), IK_Trans[1].rotation, Bd);
                //LeftHandRot = Quaternion.Lerp(Localplayer.GetBoneRotation(HumanBodyBones.LeftHand), IK_Trans[2].rotation, Bd);
                RightFootRot = Localplayer.GetBoneRotation(HumanBodyBones.RightFoot);//左腕骨。
            }
        }
    }
}