
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class HeadMaskRender : UdonSharpBehaviour
    {
        //[HideInInspector]
        public MainUI_Script mainUI = null;
        //[HideInInspector]
        public float axisZ = 0.31f;
        bool isInit = false;
        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            Init();
        }

        void Init()
        {
            if (!isInit)
            {
                isInit = true;

                transform.parent = mainUI.Head;
                transform.rotation = mainUI.Head.rotation;
                transform.localPosition = new Vector3(0, 0, axisZ);
            }
        }
    }
}
