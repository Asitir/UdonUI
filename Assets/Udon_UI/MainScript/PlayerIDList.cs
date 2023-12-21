
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class PlayerIDList : UdonSharpBehaviour
    {
        Text mainText;
        void Start()
        {
            mainText = GetComponent<Text>();
        }
        private void Update()
        {
            int playerConst = VRCPlayerApi.GetPlayerCount();
            string content = "玩家数量: " + playerConst;
            VRCPlayerApi[] players = new VRCPlayerApi[playerConst];
            VRCPlayerApi.GetPlayers(players);
            foreach (var item in players)
            {
                content += string.Format("\n{0}(ID:{1})", item.displayName, item.playerId);//"\n" + item.displayName
            }
            mainText.text = content;
        }
    }
}
