
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUIOffset : UdonSharpBehaviour
    {
        [HideInInspector]
        public MainUI_Script mainUI;
        bool isUpdate = false;

        //int lastFrameCount = 0;
        void OnWillRenderObject()
        {
            if (isUpdate)
            {
                mainUI.UpdateEyesOffset();
                isUpdate = false;
            }
        }

        public void OnUpdate()
        {
            isUpdate = true;
        }
    }
}
