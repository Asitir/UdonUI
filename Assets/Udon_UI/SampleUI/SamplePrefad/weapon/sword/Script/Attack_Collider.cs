
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class Attack_Collider : UdonSharpBehaviour
    {
        //public Transform Sha,box,spa2;
        //Vector3 HitPoint;
        //[HideInInspector]
        //public int MainAttackID = 0;
        Sword MainSword;
        ParticleSystem HitEffects1;
        Transform HitEffects1Point, Assist;
        RaycastHit AttackPoint;
        public LayerMask AttackLayer;
        //Vector3 YZero = new Vector3(1, 0, 1);
        void Start()
        {
            MainSword = transform.parent.GetChild(0).GetComponent<Sword>();
            //MainAttackID = MainSword.AttackID;
            HitEffects1 = transform.parent.GetChild(3).GetChild(0).GetChild(2).GetChild(0).GetComponent<ParticleSystem>();
            HitEffects1Point = transform.parent.GetChild(3).GetChild(0).GetChild(2);
            Assist = transform.parent.GetChild(5);
            //bool a = false;
        }


        //private void Update()
        //{
        //    Vector3 dda = box.transform.position - transform.position;
        //    Sha.rotation = Quaternion.LookRotation(spa2.position - transform.position, dda);
        //    Sha.position = transform.position;
        //    Debug.DrawLine(transform.position, transform.position + (Sha.up * 2), Color.red);

        //}

        //private void OnTriggerStay(Collider other)
        //{
        //}
        private void OnTriggerEnter(Collider other)
        {
            if (transform.localScale != Vector3.zero)
            {
                Assist.rotation = Quaternion.LookRotation(transform.forward, transform.position - other.transform.position);
                //Vector3 Mainpos = other
                float Y = (transform.position + transform.up * 0.8f).y;
                Vector3 Mainpos = other.transform.position;
                Mainpos.y = Y;
                HitEffects1Point.SetPositionAndRotation(Mainpos, Assist.rotation);
                HitEffects1.Play();

                //if(other.gameObject.layer == 9)
                //{//如果碰到的对象是其他玩家

                //}

                switch (other.gameObject.layer)
                {
                    case 9://其它玩家
                        break;
                }
                //HitEffects1.transform.position = other.ClosestPoint(MainSword.AttackRange.position);
                //if (Physics.Linecast(MainSword.AttackRange.position, other.transform.position, out AttackPoint, AttackLayer))
                //{
                //    //Assist.position = AttackPoint.point;
                //    HitEffects1.Play();
                //    //HitEffects1Point.SetPositionAndRotation(AttackPoint.point, Assist.rotation);
                //    //HitEffects1Point.SetPositionAndRotation(AttackPoint.point + (Assist.up * -1.5f), Assist.rotation);
                //    HitEffects1Point.SetPositionAndRotation(AttackPoint.point + (Assist.up * -1.5f), Assist.rotation);
                //    //HitPoint = AttackPoint.point;
                //    //Sha.position = AttackPoint.point;
                //}
                //if (Physics.Raycast(MainSword.AttackRange.position, Assist.up * -1, out AttackPoint, 1.2f, AttackLayer)) {
                //    //Assist.position = AttackPoint.point;
                //    HitEffects1.Play();
                //    HitEffects1Point.SetPositionAndRotation(AttackPoint.point, Assist.rotation);
                //    //HitPoint = AttackPoint.point;

                //}

                //if(Physics.SphereCast(MainSword.AttackRange.position,0.5f, MainSword.AttackRange.position + (Assist.up * -2), out AttackPoint, AttackLayer)){
                //    Assist.position = AttackPoint.point;
                //    HitEffects1.Play();
                //    HitEffects1Point.SetPositionAndRotation(AttackPoint.point, Assist.rotation);

                //}
            }
            //HitEffects1.transform.Rotate(90, 0, 0);
            //HitEffects1.transform.rotation = 
            //HitEffects1.transform.position = other.
            //Debug.Log(other.name);
        }


        //private void OnCollisionEnter(Collision collision)
        //{
        //    collision.
        //}

        //private void OnCollisionEnter(Collision collision)
        //{
        //    ContactPoint contact = collision.contacts[0];

        //}
    }
}