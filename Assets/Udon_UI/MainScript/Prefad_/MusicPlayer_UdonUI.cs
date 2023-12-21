
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class MusicPlayer_UdonUI : UdonSharpBehaviour
    {
        [Header("[script]开启循环 LoopOn")]
        [Header("[script]关闭循环 LoopOff")]
        [Header("[script]音乐播放 PlayAudio@数组序号")]
        [Header("[script]音量修改 SetVolume@音量0~100")]
        [Header("[script]停止音乐 Paused")]
        public AudioClip[] mainClip;
        //[HideInInspector]
        public MainUI_Script mainUI;
        //[HideInInspector]
        public AudioSource mainSource;

        public void PlayAudio()
        {
            int _audioID = mainUI.scriptID;
            AudioClip _playAudio = null;
            if (_audioID >= mainClip.Length )
            {
                _playAudio = mainClip[mainClip.Length - 1];
            }
            else
            {
                _playAudio = mainClip[_audioID];
            }
            mainSource.clip = _playAudio;
            mainSource.Play();
        }

        public void SetVolume() 
        {
            mainSource.volume = (float)mainUI.scriptID / 100;
        }

        public void Paused() {
            mainSource.Pause();
        }

        public void LoopOn()
        {
            mainSource.loop = true;
        }
        public void LoopOff()
        {
            mainSource.loop = false;
        }
    }
}
