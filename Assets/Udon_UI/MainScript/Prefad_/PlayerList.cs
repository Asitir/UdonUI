
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class PlayerList : UdonSharpBehaviour
    {
        Text[] player;
        Text MyID;
        bool syn = false;
        Transform synobj;
        //int[] playerid;
        int number;
        PlayerCount MainPlayer;

        int Operation = 0, LastOperation = 0;
//        void Start()
//        { 
//#if !UNITY_EDITOR
//        if (GameObject.Find("/UdonUI_AGI"))
//        {
//            synobj = GameObject.Find("/UdonUI_AGI").transform.GetChild(0);
//            MainPlayer = synobj.GetComponent<PlayerCount>();
//            MyID = transform.GetChild(2).GetComponent<Text>();
//            number = transform.GetChild(1).childCount;
//            player = new Text[number];
//            //playerid = new int[synobj.childCount];
//            for (int i = 0; i < number; i++)
//            {
//                player[i] = transform.GetChild(1).GetChild(i).GetComponent<Text>();
//            }
//            syn = true;
//        }
//        else
//        {
//            syn = false;
//        }
//#endif
//        }

//        private void Update()
//        {
//            if (syn)
//            {
//                Operation = MainPlayer.Operation;

//                //for(int i =0;i< synobj.childCount; i++)
//                //{//得到所有的玩家
//                //    if(synobj.GetChild(i).GetComponent<Player_Sny>().InputID / 1000000000 > 0)
//                //    {
//                //        playerid[playerz] = i;
//                //        playerz++;
//                //    }
//                //}
//                //if(LastOperation != Operation)
//                {//操作ID改变时
//                    MyID.text = "MyName:" + Networking.GetOwner(synobj.GetChild(MainPlayer.MyID).gameObject).displayName + "  MyID: " + MainPlayer.MyID;
//                    int playerz = MainPlayer.PlayerID.Length;
//                    if (playerz > number) playerz = number;//以文本数量为准
//                    for (int i = 0; i < number; i++)
//                    {
//                        if (i < playerz)
//                            player[i].text = "name:" + Networking.GetOwner(synobj.GetChild(MainPlayer.PlayerID[i]).gameObject).displayName + "  VRCID: " + Networking.GetOwner(synobj.GetChild(MainPlayer.PlayerID[i]).gameObject).playerId + "   UdonUIID: " + MainPlayer.PlayerID[i] + "  InputID: " + synobj.GetChild(MainPlayer.PlayerID[i]).GetComponent<Player_Sny>().InputID;
//                        else
//                            player[i].text = " ";
//                    }

//                    LastOperation = Operation;
//                }
//            }
//        }
    }
}
