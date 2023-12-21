
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class IKTest : UdonSharpBehaviour
    {
        public Transform[] Bones;
        public Transform Angle;
        Vector3 Target;
        void Start()
        {
            Target = Vector3.zero;
        }
        private void Update()
        {
            for (int t = 0; t < 10; t++)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector3 end = Bones[2].position - Bones[i].position;
                    Vector3 target = Target - Bones[i].position;
                    Quaternion Rota = Quaternion.FromToRotation(end, target) * Bones[i].rotation;
                    Bones[i].rotation = Rota;
                }
            }
            Vector3 Axis = Target - Bones[0].position;

            Vector3 Y1 = Vector3.Cross(Bones[1].position - Bones[0].position, Axis);
            Vector3 Y2 = Vector3.Cross(Angle.position - Bones[0].position, Axis);
            float Anglef = Vector3.SignedAngle(Y1, Y2, Axis);
            Bones[0].rotation = Quaternion.AngleAxis(Anglef, Axis) * Bones[0].rotation;
        }
    }
}