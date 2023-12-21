
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class SwitchUdonUIChat : UdonSharpBehaviour
    {
        public Transform MainChatRoot;

        Transform Target1, Target2;

        bool Insta = false;
        //private void Start()
        //{
        //    //Debug.Log("11111111112331外你23333333333331111111111111");
        //    Target1 = MainChatRoot.parent;
        //    Target2 = transform.parent;
        //    gameObject.SetActive(false);

        //    //if (Networking.LocalPlayer.IsUserInVR()) Target1 = MainChatRoot.parent;
        //    //else Target1 = MainChatRoot.parent.parent.GetChild(1);
        //    //Target2 = transform.parent;
        //    gameObject.SetActive(false);

        //}

        private void OnEnable()
        {
#if !UNITY_EDITOR
        if (!Insta)
        {
            if (Networking.LocalPlayer.IsUserInVR()) Target1 = MainChatRoot.parent.parent.GetChild(0);
            else Target1 = MainChatRoot.parent.parent.GetChild(1);
            //Target1 = MainChatRoot.parent;
            Target2 = transform.parent;
            gameObject.SetActive(false);

            //MainChatRoot.SetParent(Target2);
            //MainChatRoot.localPosition = Vector3.zero;
            //MainChatRoot.localRotation = Quaternion.Euler(0, 0, 0);
            //MainChatRoot.localScale = Vector3.one;
            Insta = true;
        }
        else
        {

            MainChatRoot.SetParent(Target2);
            MainChatRoot.localPosition = Vector3.zero;
            MainChatRoot.localRotation = Quaternion.Euler(0, 0, 0);
            MainChatRoot.localScale = Vector3.one;
        }
#endif
        }

        private void OnDisable()
        {
            //Transform ChatMain = transform.parent.GetChild(1);

            MainChatRoot.SetParent(Target1);
            MainChatRoot.localPosition = Vector3.zero;
            MainChatRoot.localRotation = Quaternion.Euler(0, 0, 0);
            MainChatRoot.localScale = Vector3.one;
            //ChatMain.localScale = Vector3.one;

        }
    }
}