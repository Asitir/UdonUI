
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

namespace UdonUI
{
    public class UdonUI_Dashboards : UdonSharpBehaviour
    {
        [Header("PC开启面板的快捷键，VR则双击左手柄食指扳机")]

        [Header("[script]切换面板显示状态 OnSwitch")]
        [Header("[script]关闭面板 OnOff")]
        [Header("[script]开启面板 OnOpen")]
        public KeyCode OpenKey_PC = KeyCode.Tab;
        [Header("面板开启时相对头部的位置偏移")]
        public Vector3 offsetPos;
        [Header("超过此距离关闭面板,单位M(为0时则不再自动关闭)")]
        public float offDis = 2.0f;
        public GameObject pickObj;
        public Transform pickTrans;
        public AudioClip OpenAudio, OffAudio;
        public string OpenAnim, OffAnim;
        //public VRC_Pickup pickUp;
        public MainUI_Script mainUI;
        public AudioSource audioComponet;
        public Animator mAnimator;


        float delayTime = 0;
        bool isOpen = false;

        private void Start()
        {
            offDis *= offDis;
        }
        public override void InputUse(bool value, UdonInputEventArgs args)
        {
            if (value)
            {
                if (args.handType == HandType.LEFT)
                {
                    if (delayTime > 0)
                    {
                        pickObj.SetActive(true);
                        OnSwitch();
                        delayTime = -1;
                    }
                    else
                    {
                        delayTime = 0.5f;
                    }
                }
            }

        }

        private void Update()
        {
            if (mainUI.VRgm)
            {
                if (delayTime > 0) delayTime -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(OpenKey_PC))
                {
                    pickObj.SetActive(false);
                    OnSwitch();
                }
            }


            if (isOpen)
            {
                if (offDis > 0)
                {
                    if((mainUI.mHeadPos - pickTrans.position).sqrMagnitude > offDis)
                    {
                        OnOff();
                    }
                }
            }
        }

        public void OnSwitch()
        {
            if (isOpen) OnOff();
            else OnOpen();
        }

        public void OnOpen()
        {
            isOpen = true;

            pickTrans.transform.SetPositionAndRotation((mainUI.mHeadPos + mainUI.mHeadRot * offsetPos), mainUI.mHeadRot);
            mAnimator.Play(OpenAnim);

            audioComponet.transform.position = pickTrans.position;
            audioComponet.clip = OpenAudio;
            audioComponet.Play();
        }
        public void OnOff()
        {
            isOpen = false;
            audioComponet.transform.position = pickTrans.position;
            audioComponet.clip = OffAudio;
            audioComponet.Play();
            mAnimator.Play(OffAnim);
        }
    }
}
