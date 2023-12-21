
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class SwitchToChat : UdonSharpBehaviour
    {
        [Tooltip("对话功能的总父级")]
        public Transform ChatRoot;
        [Tooltip("对话功能的开关判断")]
        public GameObject ChatSwitch;

        Vector3 Startpos;

        private void OnEnable()
        {
            if (ChatSwitch.activeSelf)
            {
                ChatRoot.gameObject.SetActive(true);
                ChatRoot.GetChild(0).GetChild(0).GetChild(0).GetChild(0).localPosition = Startpos;
            }
        }

        private void OnDisable()
        {
            if (ChatSwitch.activeSelf)
            {
                ChatRoot.gameObject.SetActive(false);
                Startpos = ChatRoot.GetChild(0).GetChild(0).GetChild(0).GetChild(0).localPosition;
            }
        }

    }
}
