
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class Chat_3DUI : UdonSharpBehaviour
    {
        [HideInInspector]
        public Text MainText;
        [HideInInspector]
        public VRCPlayerApi LocalPlayer;
        [HideInInspector]
        public float Life;
        [HideInInspector]
        public bool isInit = false;
        Transform Pos, AxisY;
        //Transform MainPart;
        float deltatime;
        Quaternion Fx;
        private void Start()
        {
            //MainPart = transform.parent;
            Pos = transform.GetChild(0);
            AxisY = transform.GetChild(0).GetChild(0);
            MainText = AxisY.GetChild(1).GetComponent<Text>();
            gameObject.SetActive(false);
            isInit = true;
        }
        private void OnEnable()
        {
            if (LocalPlayer != null)
            {
                //transform.SetParent(MainPart);
                Vector3 playerpos = LocalPlayer.GetPosition();
                Vector3 playerpos2 = LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
                Pos.position = playerpos;
                AxisY.localPosition = new Vector3(0, Vector3.Distance(playerpos, playerpos2) / 2, 0);
                Fx = Quaternion.LookRotation(Networking.LocalPlayer.GetPosition() - LocalPlayer.GetPosition());
                AxisY.rotation = Quaternion.Euler(0, Fx.eulerAngles.y, 0);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (player == LocalPlayer)
            {
                LocalPlayer = null;
                gameObject.SetActive(false);
            }
        }


        private void Update()
        {
            if (LocalPlayer != null)
            {
                deltatime = Time.deltaTime;
                Life -= deltatime;
                Fx = Quaternion.LookRotation(Networking.LocalPlayer.GetPosition() - LocalPlayer.GetPosition());
                Pos.position = LocalPlayer.GetPosition();
                AxisY.rotation = Quaternion.Euler(0, Fx.eulerAngles.y, 0);
                if (Life < 0)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}