
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class PlayerList_2 : UdonSharpBehaviour
    {
        private GameObject UdonUI_AGI;
        bool UdonUI_AGI_b = false;
        private Text[] playername;
        private Text MainID;
        void Start()
        {
            if (GameObject.Find("/UdonUI_AGI"))
            {
                UdonUI_AGI = GameObject.Find("/UdonUI_AGI");
                UdonUI_AGI_b = true;

                playername = new Text[transform.GetChild(1).childCount];
                for (int i = 0; i < playername.Length; i++)
                {
                    playername[i] = transform.GetChild(1).GetChild(i).GetComponent<Text>();
                }
            }
            MainID = transform.GetChild(2).GetComponent<Text>();
        }
        private void Update()
        {
            if (UdonUI_AGI_b)
            {
                for (int i = 0; i < playername.Length; i++)
                {
                    Player_Sny playerSyn = UdonUI_AGI.transform.GetChild(0).GetChild(i).GetComponent<Player_Sny>();

                    if (playerSyn.InputID / 1000000000 > 0)
                    {
                        playername[i].text = "SynID: " + playerSyn.InputID + "  Mastername:" + Networking.GetOwner(playerSyn.gameObject).displayName + "  InPos:" + playerSyn.MainPos;
                    }
                    else
                    {
                        playername[i].text = "SynID: " + playerSyn.InputID + "  Mastername:" + Networking.GetOwner(playerSyn.gameObject).displayName;
                    }
                }
                MainID.text = "MyID:" + (UdonUI_AGI.transform.GetChild(0).GetComponent<PlayerCount>().MainID + 1);
            }
        }
    }
}
