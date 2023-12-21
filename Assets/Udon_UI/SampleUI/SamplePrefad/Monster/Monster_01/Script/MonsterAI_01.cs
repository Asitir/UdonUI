
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class MonsterAI_01 : UdonSharpBehaviour
    {
        Transform MainMode;
        [HideInInspector]
        public int AttackID = 0;
        void Start()
        {
            MainMode = transform;
        }

        /// <summary>
        /// 计算怪物受击反馈
        /// </summary>
        /// <param name="HitDirection">受击方向</param>
        /// <param name="HitHeight">受击点高低</param>
        /// <param name="Damage">伤害</param>
        public void HitDamage(Vector3 HitDirection, float HitHeight, float Damage)
        {
            float UP = Vector3.Dot(HitDirection, MainMode.up);
            float right = Vector3.Dot(HitDirection, MainMode.right);
            float Forward = Vector3.Dot(HitDirection, MainMode.forward);
        }

        public void Attack(float Damage, int AttackID)
        {
            int AttackIDa = (int)(AttackID / 100);//怪物攻击ID
                                                  //五位整数
                                                  //1000010

            int AttackType = AttackID - AttackIDa;//攻击类型
                                                  //两位整数
                                                  //00

            switch (AttackIDa)
            {
                case 10000:
                    //小怪1号第一类攻击
                    break;
            }

            //switch (AttackType) {
            //    case 0:
            //        //横
            //        break;
            //    case 1:
            //        //竖
            //        break;
            //    case 2:
            //        //刺
            //        break;
            //}
        }
    }
}
