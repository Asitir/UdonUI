
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class PlayerFootSteps : UdonSharpBehaviour
    {
        [Header("步长(单位：m) 移动多长的距离触发一次脚步声")]
        [Tooltip("移动多长的距离触发一次脚步声")]
        public float step = 0.2f;
        //[Header("总音量")]
        //public float volumeMain = 1;
        [Header("脚步声音素材")]
        public AudioClip[] footAudio;
        [Header("脚步的音量随机大小范围(0~1)")]
        public Vector2 footAudioRange;

        [Header("是否启用落地声音")]
        public bool audioGrounded = false;
        [Header("落地时的声音素材")]
        public AudioClip[] footAudioGrounded;
        [Header("落地时的音量随机大小范围(0~1)")]
        public Vector2 footAudioGroundedRange;

        AudioSource footL, footR;
        bool IsPlayerGrounded = true;
        bool isLfoot = false;
        Vector3 playerPos;
        Vector2 nowPos;
        float sqrStep;
        private void Start()
        {
            sqrStep = step * step;
            footL = transform.GetChild(0).GetComponent<AudioSource>();
            footR = transform.GetChild(1).GetComponent<AudioSource>();
            playerPos = Networking.LocalPlayer.GetPosition();
            nowPos = playerPos;
        }
        private void Update()
        {
            playerPos = Networking.LocalPlayer.GetPosition();
            Vector2 vPos = Vector2.zero;
            vPos.x = playerPos.x;
            vPos.y = playerPos.z;

            if (audioGrounded)
            {
                if (Networking.LocalPlayer.IsPlayerGrounded())
                {
                    if (!IsPlayerGrounded)
                    {
                        playFootAudio(footAudioGrounded, footAudioGroundedRange);
                        IsPlayerGrounded = true;
                        nowPos = vPos;
                    }

                    if ((vPos - nowPos).sqrMagnitude > sqrStep)
                    {
                        playFootAudio(footAudio, footAudioRange);
                        nowPos = vPos;
                    }
                }
                else
                {
                    if (IsPlayerGrounded)
                    {
                        IsPlayerGrounded = false;
                    }
                }
            }
        }

        void playFootAudio(AudioClip[] _audio, Vector2 _audioRange)
        {
            isLfoot = !isLfoot;

            AudioClip mAudioClip = _audio[Random.Range(0, _audio.Length)];
            float _vl = Random.Range(_audioRange.x, _audioRange.y);
            AudioSource mPlay = isLfoot ? footL : footR;
            mPlay.transform.position = playerPos;
            mPlay.clip = mAudioClip;
            mPlay.volume = _vl;
            mPlay.Play();
        }
    }
}
