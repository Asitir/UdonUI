
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_Particle : UdonSharpBehaviour
    {
        [Header("要播放的粒子")]
        [Header("示例: Play@0")]
        [Header("[script]播放粒子动画 Play@ID (ID：粒子所处序列)")]

        public ParticleSystem[] particles;
        public MainUI_Script mainUI;

        public void Play() {
            int _parID = mainUI.scriptID;
            particles[_parID].Play();
        }
    }
}
