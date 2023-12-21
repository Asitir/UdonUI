
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class CCDIK : UdonSharpBehaviour
    {
        public Transform[] Bones;
        public Transform Target, Angle;

        public Vector2 Lim;
        //public bool LimB;

        private void Update()
        {
            CCDIK1();
        }

        void CCDIK1()
        {
            for (int t = 0; t < 10; t++)
            {
                for (int i = 0; i < Bones.Length - 1; i++)
                {
                    Vector3 endpos = Bones[Bones.Length - 1].position - Bones[i].position;
                    Vector3 targetpos = Target.position - Bones[i].position;
                    Quaternion rota = Quaternion.FromToRotation(endpos, targetpos) * Bones[i].rotation;
                    Bones[i].rotation = rota;
                    Bones[1].rotation = Quaternion.AngleAxis(Mathf.Clamp(Bones[1].localEulerAngles.y, Lim.x, Lim.y), Bones[0].up) * Bones[0].rotation;
                }
            }
            Vector3 Y = Vector3.Cross(Target.position - Bones[0].position, Bones[1].position - Bones[0].position);
            Vector3 Y2 = Vector3.Cross(Target.position - Bones[0].position, Angle.position - Bones[0].position);
            float anglef = Vector3.SignedAngle(Y, Y2, Bones[2].position - Bones[0].position);
            Bones[0].rotation = Quaternion.AngleAxis(anglef, Bones[2].position - Bones[0].position) * Bones[0].rotation;
        }
    }
}