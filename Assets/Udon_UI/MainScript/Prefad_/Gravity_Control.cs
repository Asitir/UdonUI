
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class Gravity_Control : UdonSharpBehaviour
    {
        [Header("PC端重力手套的按键设置")]
        public KeyCode ControlKey = KeyCode.F;
        VRCPlayerApi localplayer;
        MainUI_Script MainUI;
        public float Give = 30;
        public float speed = 5; //每秒移动距离
        public float OverflowAttenuation = 3;//溢出衰减倍速
        public float IsHeldDis = 0.5f;//在玩家手部多少范围内算抓取
        LayerMask Hitlayer;
        //VRC_Pickup[] pickup;
        int RHandMotionID_late, LHandMotionID_late;
        float deltatime;
        public Rigidbody[] MainObj;
        Rigidbody Lhand_obj, Rhand_obj;
        Vector3 Lstartpos, Rstartpos;
        Quaternion Lstartrot, Rstartrot;
        int rigidbodynumber = 0;
        float LTime = 0, RTime = 0, PCTime = 0;
        float LTime_s = 0, RTime_s = 0, PCTime_s = 0;
        Vector3 addford = new Vector3(0, 1000, -1000);
        Vector2[] cur = new Vector2[6] {
        new Vector2(0,0),
        new Vector2(0.5f,1),
        new Vector2(1,1),
        new Vector2(0,0),
        new Vector2(0.8f,1),
        new Vector2(0,0)
    };
        //Vector3 tov2 = new Vector3(1, 0, 1);
        void Start()
        {
            MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            RHandMotionID_late = MainUI.RhandID;
            LHandMotionID_late = MainUI.LhandID;

            MainObj = transform.parent.GetChild(1).GetComponentsInChildren<Rigidbody>();
            Lhand_obj = MainObj[0];
            Rhand_obj = MainObj[0];
            localplayer = Networking.LocalPlayer;
        }

        private void Update()
        {
            deltatime = Time.deltaTime;
            Hnad();
            MoveTime();
        }

        void Hnad()
        {
            int RHandMotionID = MainUI.RhandID;
            int LHandMotionID = MainUI.LhandID;

            if (RHandMotionID_late != RHandMotionID)
            {
                if (RHandMotionID / 10 == 6 || RHandMotionID / 10 == 2)
                {//向后拉取
                    if (RTime <= 0) Rigidebody_MG(false);
                }
                RHandMotionID_late = RHandMotionID;
            }

            if (LHandMotionID_late != LHandMotionID)
            {
                if (LHandMotionID / 10 == 6 || LHandMotionID / 10 == 2)
                {//向后拉取
                    if (LTime <= 0) Rigidebody_MG(true);
                }
                LHandMotionID_late = LHandMotionID;
            }

            if (Input.GetKeyDown(ControlKey))
            {
                if (PCTime <= 0)
                {
                    float angle = 100;
                    for (int i = 0; i < MainObj.Length; i++)
                    {
                        float dis = Vector3.Distance(MainUI.Head.position, MainObj[i].position);
                        if (dis < Give)
                        {
                            float angle_2 = Mathf.Abs(Vector3.Angle(MainUI.Head.forward, MainObj[i].position - MainUI.Head.position));
                            if (angle_2 < angle)
                            {
                                angle = angle_2;
                                Rhand_obj = MainObj[i];
                            }
                        }
                    }

                    if (angle < 30)
                    {
                        VRCPlayerApi player = Networking.GetOwner(Rhand_obj.gameObject);
                        bool held = true;
                        if (Vector3.Distance(player.GetBonePosition(HumanBodyBones.RightHand), Rhand_obj.position) > IsHeldDis)
                        {
                            if (Vector3.Distance(player.GetBonePosition(HumanBodyBones.LeftHand), Rhand_obj.position) > IsHeldDis)
                                held = false;
                        }

                        if (!held)
                        {
                            Networking.SetOwner(localplayer, Rhand_obj.gameObject);
                            Rstartpos = Rhand_obj.position;
                            Rstartrot = Rhand_obj.rotation;

                            PCTime_s = Vector3.Distance(Rstartpos, MainUI.Head.position) / speed;//得到秒速
                            if (PCTime_s < 1) PCTime_s = 1;//最少一秒
                            else PCTime_s = 1 + ((PCTime_s - 1) / OverflowAttenuation);

                            PCTime = PCTime_s;
                        }
                    }
                }
            }
        }

        void MoveTime()
        {
            if (Networking.GetOwner(Lhand_obj.gameObject) != localplayer) LTime = 0;
            if (Networking.GetOwner(Rhand_obj.gameObject) != localplayer) { RTime = 0; PCTime = 0; }

            if (LTime > 0)
            {
                LTime -= deltatime;
                if (LTime < 0) LTime = 0;
                float LerpPos = (LTime_s - LTime) / LTime_s;//0 过度到 1

                float lerpf = Cur_1(LerpPos);
                Vector3 setpos = Vector3.Lerp(Lstartpos, MainUI.Lhand.position, lerpf);
                Quaternion setrot = Quaternion.Lerp(Lstartrot, MainUI.Lhand_sstart.rotation, Mathf.Clamp01(lerpf * 1.2f));
                setpos.y += Cur_2(lerpf, Lstartpos, MainUI.Lhand.position);
                Lhand_obj.transform.SetPositionAndRotation(setpos, setrot);

                if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    LTime = 0;
                    MainUI.Lhandf_dj = 0.51f;
                }
                if (LTime == 0) Rhand_obj.AddForce((MainUI.Head.forward * -50) + Vector3.up * 500);
            }

            if (RTime > 0)
            {
                RTime -= deltatime;
                if (RTime < 0) RTime = 0;
                float LerpPos = (RTime_s - RTime) / RTime_s;//0 过度到 1

                float lerpf = Cur_1(LerpPos);
                Vector3 setpos = Vector3.Lerp(Rstartpos, MainUI.Rhand.position, lerpf);
                Quaternion setrot = Quaternion.Lerp(Rstartrot, MainUI.Rhand_sstart.rotation, Mathf.Clamp01(lerpf * 1.2f));
                setpos.y += Cur_2(lerpf, Rstartpos, MainUI.Rhand.position);
                Rhand_obj.transform.SetPositionAndRotation(setpos, setrot);

                if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.JoystickButton4))
                {
                    RTime = 0;
                    MainUI.Rhandf_dj = 0.51f;
                }
                if (RTime == 0) Rhand_obj.AddForce((MainUI.Head.forward * -50) + Vector3.up * 500);
            }

            if (PCTime > 0)
            {
                PCTime -= deltatime;
                if (PCTime < 0) PCTime = 0;
                float LerpPos = (PCTime_s - PCTime) / PCTime_s;//0 过度到 1

                float lerpf = Cur_1(LerpPos);
                Vector3 setpos = Vector3.Lerp(Rstartpos, MainUI.Head.position + MainUI.Head.TransformDirection(0, 0, 0.3f), lerpf);
                Quaternion setrot = Quaternion.Lerp(Rstartrot, MainUI.Head.rotation, Mathf.Clamp01(lerpf * 1.2f));
                setpos.y += Cur_2(lerpf, Rstartpos, MainUI.Rhand.position);
                Rhand_obj.transform.SetPositionAndRotation(setpos, setrot);

                if (Input.GetMouseButtonDown(0)) PCTime = 0;
                if (PCTime == 0) Rhand_obj.AddForce((MainUI.Head.forward * -50) + Vector3.up * 500);
            }
        }

        void Rigidebody_MG(bool Left)
        {
            float angle = 100;

            if (Left)
            {//左手
                for (int i = 0; i < MainObj.Length; i++)
                {
                    float dis = Vector3.Distance(MainUI.Lhand_sstart.position, MainObj[i].position);
                    if (dis < Give)
                    {
                        float angle_2 = Mathf.Abs(Vector3.Angle(MainUI.Lhand_sstart.forward, MainObj[i].position - MainUI.LshoulderPos));
                        if (angle_2 < angle)
                        {
                            angle = angle_2;
                            Lhand_obj = MainObj[i];
                        }
                    }
                }

                if (angle < 30)
                {
                    VRCPlayerApi player = Networking.GetOwner(Lhand_obj.gameObject);
                    bool held = true;
                    if (Vector3.Distance(player.GetBonePosition(HumanBodyBones.RightHand), Lhand_obj.position) > IsHeldDis)
                    {
                        if (Vector3.Distance(player.GetBonePosition(HumanBodyBones.LeftHand), Lhand_obj.position) > IsHeldDis)
                            held = false;
                        //held = false;
                    }

                    if (!held)
                    {
                        Networking.SetOwner(localplayer, Lhand_obj.gameObject);
                        Lstartpos = Lhand_obj.position;
                        Lstartrot = Lhand_obj.rotation;

                        LTime_s = Vector3.Distance(Lstartpos, MainUI.Lhand_sstart.position) / speed;//得到秒速
                        if (LTime_s < 1) LTime_s = 1;//最少一秒
                        else LTime_s = 1 + ((LTime_s - 1) / OverflowAttenuation);

                        MainUI.Lhandf_dj = LTime_s + 0.51f;
                        LTime = LTime_s;
                    }
                }
            }
            else
            {//右手
                for (int i = 0; i < MainObj.Length; i++)
                {
                    float dis = Vector3.Distance(MainUI.Rhand_sstart.position, MainObj[i].position);
                    if (dis < Give)
                    {
                        float angle_2 = Mathf.Abs(Vector3.Angle(MainUI.Rhand_sstart.forward, MainObj[i].position - MainUI.RshoulderPos));
                        if (angle_2 < angle)
                        {
                            angle = angle_2;
                            Rhand_obj = MainObj[i];
                        }
                    }
                }

                if (angle < 30)
                {
                    VRCPlayerApi player = Networking.GetOwner(Rhand_obj.gameObject);
                    bool held = true;
                    if (Vector3.Distance(player.GetBonePosition(HumanBodyBones.RightHand), Rhand_obj.position) > IsHeldDis)
                    {
                        if (Vector3.Distance(player.GetBonePosition(HumanBodyBones.LeftHand), Rhand_obj.position) > IsHeldDis)
                            held = false;
                        //held = false;
                    }

                    if (!held)
                    {
                        Networking.SetOwner(localplayer, Rhand_obj.gameObject);
                        Rstartpos = Rhand_obj.position;
                        Rstartrot = Rhand_obj.rotation;
                        RTime_s = Vector3.Distance(Rstartpos, MainUI.Rhand_sstart.position) / speed;//得到秒速
                        if (RTime_s < 1) RTime_s = 1;//最少一秒
                        else RTime_s = 1 + ((RTime_s - 1) / OverflowAttenuation);

                        MainUI.Rhandf_dj = RTime_s + 0.51f;
                        RTime = RTime_s;
                    }
                }
            }
        }

        float Cur_1(float start)
        {//由快到慢
            return Vector2.Lerp(Vector2.Lerp(cur[0], cur[1], start), Vector2.Lerp(cur[1], cur[2], start), start).y;
        }

        float Cur_2(float start, Vector3 stpos, Vector3 endpos)
        {
            //stpos.y = 0;
            //endpos.y = 0;
            float dis = Vector3.Distance(stpos, endpos);
            cur[4].y = dis * 0.2f;
            return Vector2.Lerp(Vector2.Lerp(cur[3], cur[4], start), Vector2.Lerp(cur[4], cur[5], start), start).y;
        }
    }
}