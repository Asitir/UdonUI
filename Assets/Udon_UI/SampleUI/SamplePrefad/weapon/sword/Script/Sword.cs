
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class Sword : UdonSharpBehaviour
    {
        //挥击的朝向（MainEndPos.position - Latepos）
        //[UdonSynced]
        public AudioClip[] MainAudio;
        public float DeltaDis = 4;//M为单位  每秒以此单位更新
        public float UpdateTimeLatePost = 0.1f;//每间隔X秒重置末端位置判断
                                               //Attack_Collider[] AttackColliders;
        #region 技能
        Vector3 SAttack01;//定格下的坐标
        float SAttack01f = 2;//定格范围
        float SAttackStandTime = 0.3f;//触发后等待时间
        float Sattack01Stime = 0.5f;//技能触发时机

        float Sattack01StimeTrue = 0;
        float SAttack01StandTimef = 0;
        float SAttack01Stimef = 0;
        float AttackRo = 0.2f;//大回转触发时机
        float AttackRof = 0;
        #endregion
        //Vector3 InMainPos, InMainEndPos;
        [HideInInspector]
        public int AttackID = 0;
        Vector3 StartPos, EndPos, Latepos;
        Vector3 StartPos2, EndPos2;
        [HideInInspector]
        public Transform MainModel, MainEndPos, AttackRange, AttackRange2;
        float deltatime, UpdateTimeLatePos;
        float AttackHit = -1, DotAttackDriction;//攻击的朝向和刀刃朝向的点积
        bool AttackIDToTwo = false, SwordAttack = false, rot180 = false;//是否触发大回转，是否刃口朝向挥砍方向, 是否旋转超过180
        Vector3 RottStart, LateSwordForward, SAttack01Forward;
        int RotNumber = 0;
        ParticleSystem SwordEffects1, SwordEffects2, SwordSAttack01_1, SwordSAttack01_2;
        Transform EffectsPos, EffectsPos2, SwordSAttack01_2Point, Assist02;
        VRCPlayerApi LocalPlayer, MasterPlayer;
        bool IsMaster = false, SAttack01_CD = false;
        MainUI_Script MainUI;
        void Start()
        {
            MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            LocalPlayer = Networking.LocalPlayer;
            MainModel = transform;//主要手持位置
            MainEndPos = transform.GetChild(0);//末端位移
            MainEndPos.localPosition = new Vector3(0, 0, 5);//设置末端位置
            Latepos = MainEndPos.position;
            AttackRange = transform.parent.GetChild(1);//攻击范围交代
            AttackRange2 = transform.parent.GetChild(2);//环形攻击范围交代
            EffectsPos = transform.parent.GetChild(3);
            EffectsPos2 = transform.parent.GetChild(4);
            Assist02 = transform.parent.GetChild(6);
            SwordSAttack01_2Point = transform.parent.GetChild(4).GetChild(0);
            AttackRange.localScale = Vector3.zero;
            AttackRange2.localScale = Vector3.zero;
            //AttackColliders = transform.parent.GetComponentsInChildren<Attack_Collider>();
            SwordEffects1 = transform.parent.GetChild(3).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
            SwordEffects2 = transform.parent.GetChild(3).GetChild(0).GetChild(1).GetComponent<ParticleSystem>();
            SwordSAttack01_2 = transform.parent.GetChild(4).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
            SwordSAttack01_1 = transform.parent.GetChild(4).GetChild(1).GetComponent<ParticleSystem>();
            EffectsPos.position = Vector3.up * -100;
            EffectsPos2.position = Vector3.up * -100;

        }

        private void Update()
        {
            deltatime = Time.deltaTime;
            IsMaster = Networking.IsOwner(LocalPlayer, gameObject);
            MasterPlayer = Networking.GetOwner(gameObject);
            if (IsMaster)
            {
                UpdateTimeLatePost = 0.1f;
                Sattack01StimeTrue = Sattack01Stime;//技能1触发时机
            }
            else
            {
                UpdateTimeLatePost = 0.2f;
                Sattack01StimeTrue = Sattack01Stime + 0.3f;
            }
            MainP();//主要判断
            StanbyTime();//计时
        }

        void MainP()
        {
            //Debug.Log(Vector3.Dot(Vector3.up, Vector3.up)); //返回1
            //InMainPos = MainModel.position;
            //InMainEndPos = MainEndPos.position;
            UpdateTimeLatePos -= deltatime;
            if (UpdateTimeLatePos <= 0)
            {
                UpdateTimeLatePos = UpdateTimeLatePost;//每间隔一段时间刷新
                Latepos = MainEndPos.position;
                //LateSwordForward = MainModel.right;
                //LateRot = MainEndPos.rotation;
                //Debug.Log("每隔一段时间");
            }

            //float DeltaDistance = Vector3.Distance(Latepos, MainEndPos.position);//上一次更新的位置  和当前位置
            float DeltaDistance = Vector3.Distance(Latepos, MainEndPos.position);//上一次更新的位置  和当前位置
            if (DeltaDistance >= DeltaDis)
            {//已经触发攻击

                //开始攻击
                if (AttackHit <= 0)
                {
                    if (AttackID < 9999) AttackID++;
                    else AttackID = 0;

                    StartPos = Latepos;//记录下挥砍的起始位置
                    StartPos2 = MainModel.position;
                    //StartRot = LateRot;

                    //初始化攻击范围的朝向
                    EndPos = MainEndPos.position;
                    EndPos2 = MainModel.position;
                    Vector3 AttackHitHeadPos = StartPos - StartPos2;
                    Vector3 AttackHitEndPos = EndPos - EndPos2;
                    RottStart = AttackHitHeadPos;
                    LateSwordForward = Vector3.Cross(AttackHitHeadPos, AttackHitEndPos);
                    AttackRange.rotation = Quaternion.LookRotation(LateSwordForward, Vector3.Lerp(AttackHitHeadPos, AttackHitEndPos, 0.5f));

                    DotAttackDriction = Vector3.Dot(Vector3.Normalize(MainEndPos.position - Latepos), Vector3.Normalize(MainModel.up * -1));
                    SwordAttack = DotAttackDriction > 0.35f;//是否刀刃朝向挥动方向
                    if (SwordAttack)
                    {
                        AttackRof = AttackRo;
                        SwordEffects1.Play();
                        EffectsPos.position = EndPos2;//----------设置特效位置和角度
                        EffectsPos.rotation = AttackRange.rotation;
                        AttackRange.localScale = Vector3.one;
                    }
                    //EffectsPos.rotation = Quaternion.LookRotation(AttackRange.forward,)
                    //Debug.Log("开始攻击");

                    #region 技能1
                    //刀刃朝向挥砍方向
                    if (SwordAttack)
                    {
                        ////挥砍方向是垂直的
                        Assist02.rotation = MasterPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;
                        float AttackRangeDirection = Vector3.Dot(LateSwordForward, Assist02.right);
                        if (AttackRangeDirection < -0.5f)
                        {
                            //挥砍方向朝向上
                            //SAttack01Forward = MainEndPos.position - Latepos;
                            SAttack01_CD = true;
                            //触发技能1  等待闪光
                        }

                        //SAttack01Forward = MainEndPos.position - Latepos;
                        //SAttack01_CD = true;
                    }
                    #endregion
                    if (AttackID < 99) AttackID++; else AttackID = 0;
                }

                //Latepos = MainEndPos.position;

                if (SwordAttack)
                { //挥动刀背不算攻击
                    AttackHit = 0.2f;//重置攻击计算时间
                                     //UpdateTimeLatePos = UpdateTimeLatePost;//重置计时
                }
            }

            //攻击中
            if (AttackHit >= 0)
            {
                AttackHit -= deltatime;
                //EffectsPos.position = EndPos2;//-----------设置特效位置和角度
                //EffectsPos.rotation = AttackRange.rotation;

                EndPos = MainEndPos.position;
                EndPos2 = MainModel.position;//不断更新最后一帧的位置

                //头部和尾部位置
                Vector3 AttackHitHeadPos = StartPos - StartPos2;
                Vector3 AttackHitEndPos = EndPos - EndPos2;

                //设置旋转
                float SwordDot = Vector3.SignedAngle(Vector3.Normalize(RottStart), Vector3.Normalize(AttackHitEndPos), Vector3.Cross(RottStart, AttackHitEndPos));//挥砍的角度差
                if (SwordDot > 100)
                {
                    RottStart = AttackHitEndPos;
                    RotNumber++;
                    //Debug.Log("ADD");
                }

                if (RotNumber > 1 && SwordAttack && AttackRof > 0)//SwordDot <= -0.75f
                {//环形范围伤害
                 //Debug.Log("触发大回转");
                    if (!AttackIDToTwo)
                    {//触发大幅度挥击
                        SwordEffects2.Play();
                        AttackRange2.rotation = AttackRange.rotation;
                        AttackRange2.localScale = Vector3.one;
                        AttackRange.localScale = Vector3.zero;
                        AttackIDToTwo = true;
                    }
                }
                else
                {
                    Quaternion AttackRangeRot = Quaternion.LookRotation(Vector3.Cross(AttackHitHeadPos, AttackHitEndPos), Vector3.Lerp(AttackHitHeadPos, AttackHitEndPos, 0.5f));
                    if (Quaternion.Angle(AttackRangeRot, AttackRange.rotation) > 160) rot180 = !rot180;
                    AttackRange.rotation = AttackRangeRot;
                    if (rot180) AttackRange.Rotate(0, 180, 180);

                    //AttackRange.rotation = Quaternion.Lerp(StartRot, MainModel.rotation, 0.5f);
                }
                AttackRange.position = Vector3.Lerp(StartPos2, EndPos2, 0.5f);

                //结束攻击
                if (AttackHit <= 0)
                {
                    RotNumber = 0;
                    AttackRange.localScale = Vector3.zero;
                    AttackRange2.localScale = Vector3.zero;
                    SAttack01 = MainEndPos.position;//定格
                    if (SAttack01_CD)
                    {
                        SAttack01StandTimef = SAttackStandTime;
                        SAttack01Forward = AttackRange.up;
                        //触发闪光
                    }
                    SAttack01_CD = false;
                    rot180 = false;
                    AttackIDToTwo = false;
                    //Debug.Log("结束攻击");
                }
                //Debug.Log("攻击中");
            }
        }

        void StanbyTime()
        {
            #region 技能
            //触发技能1后等待的时间
            if (SAttack01StandTimef > 0)
            {
                SAttack01StandTimef -= deltatime;

                if (SAttack01StandTimef <= 0)
                {
                    SAttack01Stimef = Sattack01StimeTrue;
                    Debug.Log("12");
                    SwordSAttack01_1.transform.position = MainModel.position;
                    SwordSAttack01_1.Play();//技能1触发闪光
                }
                if (Vector3.Distance(MainEndPos.position, SAttack01) > SAttack01f) SAttack01StandTimef = -1;//结束闪光判定
            }
            //技能1的触发时机
            if (SAttack01Stimef > 0)
            {
                SAttack01Stimef -= deltatime;
                //开始闪光  技能1判定中
                if (Vector3.Distance(MainEndPos.position, SAttack01) > SAttack01f)
                {
                    if (Vector3.Dot(Vector3.Normalize(MainEndPos.position - SAttack01), Vector3.Normalize(MainModel.up)) > 0.35f)
                    {
                        //触发技能1  空间斩
                        SwordSAttack01_2Point.transform.SetPositionAndRotation(AttackRange.position, Quaternion.LookRotation(Vector3.up, SAttack01Forward));
                        SwordSAttack01_2.Play();
                    }
                    SAttack01Stimef = 0;//结束判定                 DotAttackDriction = Vector3.Dot(Vector3.Normalize(MainEndPos.position - Latepos), Vector3.Normalize(MainModel.up * -1)); +1
                }
            }

            //触发大回转时机
            if (AttackRof >= 0) AttackRof -= deltatime;
            #endregion
        }

        //void Interact()
        //{
        //    SwordSAttack01_2Point.transform.SetPositionAndRotation(AttackRange.position, Quaternion.LookRotation(Vector3.up, SAttack01Forward));
        //    SwordSAttack01_2.Play();
        //}

        //void OnPickupUseDown()
        //{
        //    SwordSAttack01_2Point.transform.SetPositionAndRotation(AttackRange.position, Quaternion.LookRotation(Vector3.up, SAttack01Forward));
        //    SwordSAttack01_2.Play();
        //}

        //void InputJump(bool A,)
        //{

        //}
    }
}