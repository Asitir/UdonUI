
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class PlayerListSet : UdonSharpBehaviour
    {
        //[Header("Transfer Switch")]
        //public bool istransferTo = false;
        //[Tooltip("当 “istransferTo”为true的时候，当前值不能为空")]
        //public Transform transferTo;
        //[Tooltip("是否对准“transferTo”的朝向")]
        //public bool transferToRota = false;

        //[Header("SetActive Switch")]
        //public bool isSetActiveTo = false;
        [Header("延迟触发")]
        public float delayTime = 0.5f;
        [Header("玩家列表")]
        public string[] playerList = new string[] 
        {
            "在此输入玩家的VrcID，示例：",
            "Asitir",
            "亦坏YiHuai",
            "铁咩"
        };
        [Header("是否将房主加入玩家列表")]
        public bool isMaster = true;
        [Header("切换列表中对象的开关状态")]
        public GameObject[] SetActiveObjects = new GameObject[0];

        void Start()
        {
            /*            
            if (isMaster)
            {
                if (Networking.IsMaster)
                {
                    foreach (GameObject a in SetActiveObjects)
                        a.SetActive(!a.activeSelf);
                    return;
                }
            }

            for (int i = 0; i < playerList.Length; i++)
            {
                if (Networking.LocalPlayer.displayName == playerList[i])
                {
                    foreach (GameObject a in SetActiveObjects)
                        a.SetActive(!a.activeSelf);
                    break;
                }
            }
            */        
        }

        int LoopID = 0;
        private void Update()
        {
            if (delayTime > 0)
            {
                delayTime -= Time.deltaTime;
                if (delayTime <= 0)
                {
                    if (isMaster)
                    {
                        if (Networking.IsMaster)
                        {
                            foreach (GameObject a in SetActiveObjects)
                                a.SetActive(!a.activeSelf);
                            return;
                        }
                    }
                    LoopID = playerList.Length;

                    //for (int i = 0; i < playerList.Length; i++)
                    //{
                    //    if (Networking.LocalPlayer.displayName == playerList[i])
                    //    {
                    //        foreach (GameObject a in SetActiveObjects)
                    //            a.SetActive(!a.activeSelf);
                    //        break;
                    //    }
                    //}
                }
            }
            else
            {
                if (LoopID > 0)
                {
                    LoopID--;
                    if(Networking.LocalPlayer.displayName == playerList[LoopID])
                    {
                        foreach (GameObject a in SetActiveObjects)
                            a.SetActive(!a.activeSelf);
                        LoopID = 0;
                    }
                }
            }
        }
    }
}
