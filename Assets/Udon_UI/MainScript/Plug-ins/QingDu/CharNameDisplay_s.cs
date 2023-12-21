
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class CharNameDisplay_s : UdonSharpBehaviour
    {
        //public
        [Header("相对角色偏移，玩家身高的百分比")]
        [Header("[Script] 角色关闭名牌  SetOff")]
        [Header("[Script] 角色开启名牌  SetOn")]
        public Vector3 offsetPos = new Vector3(-0.1f, 0.6f, 0);
        [Header("关闭所处位置和开启所处位置")]
        public Vector2 posRange = new Vector2(0.1f, 0.5f);
        [Header("移动对象")]
        public RectTransform transMove;
        public CharNameDisplay mainName;

        private VRCPlayerApi player = null;
        private bool nameActive = false;//开启或关闭动画
        private float animTime = -1;
        private void Update()
        {
            if(player != null && player.IsValid() && !player.isLocal)
            {
                Vector3 _pos = player.GetPosition();
                //Vector3 _offsetPos = offsetPos;
                float _playerHeight = player.GetAvatarEyeHeightAsMeters();
                Quaternion _rot = Quaternion.Euler(0, Quaternion.LookRotation(player.GetPosition() - Networking.LocalPlayer.GetPosition()).eulerAngles.y, 0);
                transform.SetPositionAndRotation(_pos + _rot * offsetPos * _playerHeight, _rot);

                if (animTime > 0)
                {
                    float _deltatime = Time.deltaTime;
                    animTime -= _deltatime;
                    if (nameActive)
                    {//显示名牌
                        if (animTime < 1)
                        {
                            transMove.localPosition = Vector3.right * Mathf.Lerp(transMove.localPosition.x, posRange.y, 6 * _deltatime);
                        }
                    }
                    else
                    {//隐藏名牌
                        transMove.localPosition = Vector3.right * Mathf.Lerp(transMove.localPosition.x, posRange.x, 10 * _deltatime);
                        if (animTime <= 0.5f)
                        {
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void SetOn()
        {//本地设置所有者
            mainName.SendNameID(transform.GetSiblingIndex());
        }
        public void SetOff()
        {//本地设置所有者
            SendOff();
        }

        public void SendOn(VRCPlayerApi _player)
        {//设定所属玩家
            player = _player;
        }
        public void SendOff()
        {
            player = null;
        }

        public void OnOpen() 
        { //显示名牌
            if (player != null)// && !player.isLocal
            {
                if (player.IsValid() && !player.isLocal)
                {
                    gameObject.SetActive(true);
                    transMove.localPosition = Vector3.right * posRange.x;
                    transform.position = player.GetPosition();
                    nameActive = true;
                    animTime = 1 + Random.Range(0, 0.5f);
                }
            }
            
        }

        public void OnOff() 
        { //隐藏名牌
            nameActive = false;
            animTime = 1;
        }
    }
}
