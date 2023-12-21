
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_GatePortal : UdonSharpBehaviour
    {

        //[Header("是否反向")]
        //public bool inverse = true;
        [Header("门高度")]
        public float Height = 1.481f;
        [Header("门宽度")]
        public float Weight = 0.77f;
        [Header("门厚度")]
        public float thickness = 0.031f;
        [Header("最大开合角度")]
        public float Angle = 120;
        [Header("是否同步")]
        public bool isSyn = true;
        [Range(0, 1)]
        public float OpenGate = 0;
        [Header("阻力")]
        public float drag = 100;
        [Header("惯性倍率")]
        public float inertia = 0.2f;
        [Header("撞角后保留的力度倍率")]
        public float loss = 0.8f;
        [Header("碰撞球半径")]
        [Range(0, 5)]
        public float Radius = 0.1f;


        public Transform inTransform;
        [HideInInspector]
        public Vector3 InPos, InPos2;
        Transform GateTrans;
        //Transform NullObject;
        bool RisIn = false, LisIn = false;
        bool RForrward = false, LForrward = false;
        float deltatime = 0;
        float angleSpeed = 0;
        bool run = true;
        void Start()
        {
            GateTrans = transform.GetChild(1);
            run = false;
            Weight *= Weight;
        }

        void Update()
        {
            InPos = inTransform.position;

            deltatime = Time.deltaTime;
            Vector3 rePos = GateTrans.InverseTransformPoint(InPos);
            rePos.y = 0;
            Vector2 rePos2 = Vector2.zero;
            rePos2.x = rePos.x;
            rePos2.y = rePos.z;

            float m_Distance = rePos2.sqrMagnitude;
            if (m_Distance < Weight)
            {
                //float RinAngle = Vector3.SignedAngle(GateTrans.right, GateTrans.rotation * rePos, GateTrans.up) * (inverse ? 1 : -1);
                float RinAngle = Vector3.SignedAngle(GateTrans.right, GateTrans.rotation * rePos, GateTrans.up);

                if (Mathf.Abs(RinAngle) < 50)
                {
                    if (!RisIn)
                    {
                        RisIn = true;
                        if (RinAngle > 0)
                        {
                            RForrward = true;
                        }
                        else
                        {
                            RForrward = false;
                        }
                    }

                    if (RForrward)
                    {
                        if (RinAngle < 0)
                        {
                            GateTrans.Rotate(0, RinAngle, 0);
                            float angleSpeed2 = RinAngle / deltatime;
                            angleSpeed2 *= inertia;
                            if (angleSpeed2 < angleSpeed) angleSpeed = angleSpeed2;
                        }
                    }
                    else
                    {
                        if (RinAngle > 0)
                        {
                            GateTrans.Rotate(0, RinAngle, 0);
                            float angleSpeed2 = RinAngle / deltatime;
                            angleSpeed2 *= inertia;
                            if (angleSpeed2 > angleSpeed) angleSpeed = angleSpeed2;

                        }
                    }
                }
                else
                {
                    if (RisIn)
                    {
                        RisIn = false;
                        //Debug.Log("退出范围");
                    }
                }


            }
            else
            {
                if (RisIn)
                {
                    RisIn = false;
                    //Debug.Log("退出范围");
                }
            }

            if (angleSpeed > 0)
            {
                Debug.Log("AA");
                angleSpeed -= deltatime * drag;
                if (angleSpeed < 0) angleSpeed = 0;
                GateTrans.Rotate(0, angleSpeed * deltatime, 0);
            }
            else if (angleSpeed < 0)
            {
                angleSpeed += deltatime * drag;
                if (angleSpeed > 0) angleSpeed = 0;
                GateTrans.Rotate(0, angleSpeed * deltatime, 0);
                Debug.Log("BB");
            }

            //判断开合角度极限
            float GateAngle = GateTrans.localEulerAngles.y;
            if (GateAngle > 180) GateAngle -= 360;
            //a.text = "角度" + GateAngle;
            //if (inverse)
            {
                if (GateAngle > 0)
                {
                    GateTrans.localRotation = Quaternion.Euler(0, 0, 0);
                    angleSpeed = 0;
                    return;
                }
                if (GateAngle < Angle * -1)
                {
                    GateTrans.localRotation = Quaternion.Euler(0, Angle * -1, 0);
                    angleSpeed *= -loss;
                    return;
                }
            }
            //else
            //{
            //    if (GateAngle < 0)
            //    {
            //        GateTrans.localRotation = Quaternion.Euler(0, 0, 0);
            //        angleSpeed = 0;
            //        return;
            //    }
            //    if (GateAngle > Angle)
            //    {
            //        GateTrans.localRotation = Quaternion.Euler(0, Angle, 0);
            //        angleSpeed = 0;
            //        return;
            //    }
            //}

        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            //int normal = inverse ? -1 : 1;
            int normal = -1;
            Transform door = transform.GetChild(1);

            Vector3 pos = door.position;
            Vector3 dir = transform.right;
            Quaternion rot = transform.rotation;

            //if (run) transform.GetChild(1).rotation = rot * Quaternion.Euler(0, Angle * OpenGate * normal, 0);

            Gizmos.color = Color.green;
            Vector3 pp = pos + (Quaternion.Euler(0, Angle * normal, 0)) * (transform.right * Weight);
            Gizmos.DrawLine(pp, pp + transform.up * Height);
            Gizmos.DrawLine(pp, pos);
            Gizmos.DrawLine(pp + transform.up * Height, pos + transform.up * Height);

            Vector3 dd = pos + (dir * Weight);
            Vector3 latePos1 = dd;
            Vector3 latePos2 = dd + transform.up * Height;
            Vector3 latePos3 = pos + (dir * Weight * 0.9f);
            for (int i = 1; i < (int)Angle + 1; i++)
            {
                Quaternion nowRota = Quaternion.Euler(0, i == (int)Angle ? (Angle * normal) : (i * normal), 0);

                dd = pos + nowRota * (dir * Weight);
                Vector3 pos1 = dd;
                Vector3 pos2 = dd + transform.up * Height;
                Vector3 pos3 = pos + nowRota * (dir * Weight * 0.9f);

                Gizmos.color = Color.green;
                Gizmos.DrawLine(latePos1, pos1);
                Gizmos.DrawLine(latePos2, pos2);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(latePos3, pos3);

                latePos1 = pos1;
                latePos2 = pos2;
                latePos3 = pos3;
            }

            Vector3 p1 = door.TransformPoint(0, 0, thickness * normal);
            Vector3 p2 = door.TransformPoint(Weight, 0, thickness * normal);
            Vector3 p3 = door.TransformPoint(Weight, Height, thickness * normal);
            Vector3 p4 = door.TransformPoint(0, Height, thickness * normal);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }
#endif
    }
}
