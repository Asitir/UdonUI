
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class AvatarCamePos_Root : UdonSharpBehaviour
    {
        PlayerCount PlayerGM;
        int Operation, Operation_late;
        int[] Player, Player_Late;

        int ForID = -1;
        float jg = 0;
        void Start()
        {
            PlayerGM = transform.parent.GetChild(0).GetComponent<PlayerCount>();
        }

        //private void Update()
        //{
        //    Operation = PlayerGM.Operation;

        //    if (Operation_late != Operation)
        //    {//玩家列表更新时
        //        Player = PlayerGM.PlayerID;
        //        ForID = 0;

        //        //Player_Late = Player;
        //        Operation_late = Operation;
        //    }

        //    if (ForID > -1)
        //    {
        //        jg += Time.deltaTime;
        //        if (jg > 0.3f)
        //        {//每0.3秒更新一次
        //            if (ForID >= Player.Length)
        //            {
        //                ForID = -1;
        //                return;
        //            }
        //            transform.GetChild(Player[ForID]).gameObject.SetActive(true);
        //            ForID++;
        //            jg = 0;
        //        }
        //    }
        //}
    }
}
