
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class CharNameDisplay : UdonSharpBehaviour
    {
        //public GameObject aa;

        [Header("所有名牌")]
        public CharNameDisplay_s[] allPlayer;
        [Header("PC端显示玩家名字快捷键")]
        public KeyCode pcKey = KeyCode.N;
        public MainUI_Script mainUI;
        [HideInInspector]
        [UdonSynced] public int[] playerNames = new int[0];
        private int[] playerName_l = new int[0];

        float start = 5;
        bool isStart = false;

        private void Update()
        {
            if (start > 0)
            {//延时初始化
                start-= Time.deltaTime;
                if (start <= 0)
                {
                }
            }
            else
            {
                if (mainUI.VRgm)
                {
                    float _height = Networking.LocalPlayer.GetAvatarEyeHeightAsMeters() / 7;//玩家高度
                    Vector3 _dir = mainUI.Rhand.position - mainUI.mHeadPos;
                    if (_height * _height > (_dir).sqrMagnitude && (Vector3.Dot(_dir.normalized, mainUI.mHeadRot * Vector3.up) > -0.5f))
                    {//触发
                        if (!isStart)
                        {
                            isStart = true;
                            OnOpen();
                        }
                    }
                    else
                    {
                        if (isStart)
                        {
                            isStart = false;
                            OnOff();
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(pcKey))
                    {
                        isStart = !isStart;
                        if (isStart) OnOpen();
                        else OnOff();

                    }
                }

            }
        }

        public void SendNameID(int _id) 
        {
            //发送数据
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            if (playerNames.Length < allPlayer.Length) playerNames = new int[allPlayer.Length]; 
             playerNames[_id] = Networking.LocalPlayer.playerId;
            RequestSerialization();
        }

        override public void OnDeserialization() 
        {//接收到数据时
            UpdateNameList();
        }
        private void OnOpen() 
        {
            //aa.SetActive(true);
            foreach (var item in allPlayer)
            {
                item.OnOpen();
            }
        }

        private void OnOff() 
        {
            //aa.SetActive(false);
            foreach (var item in allPlayer)
            {
                item.OnOff();
            }
        }

        //更新
        private void UpdateNameList() { 
            if(playerName_l.Length != playerNames.Length)
            {
                playerName_l = new int[playerNames.Length];
            }
            //Debug.Log("尝试更新");
            for (int i = 0; i < playerNames.Length; i++)
            {
                if(playerNames[i] != playerName_l[i])
                {//设定持有玩家
                    allPlayer[i].SendOn(VRCPlayerApi.GetPlayerById(playerNames[i]));
                    playerName_l[i] = playerNames[i];
                }
            }
        }
    }
}
