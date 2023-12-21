
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class Chat_VrInput_Vector2 : UdonSharpBehaviour
    {
        MainUI_Script MainUI;
        public Transform MoveTarget;
        Transform MainTransform;
        bool LeftHand = false;
        Vector3 StartPos, NowPos, StartMoveTarget;
        void Start()
        {
            MainTransform = transform;
            MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            float Left = Vector3.Distance(MainUI.LhandMousePos, MainTransform.position);
            float Right = Vector3.Distance(MainUI.RhandMousePos, MainTransform.position);
            if (Left < Right)
            {
                LeftHand = true;
                StartPos = MainTransform.InverseTransformPoint(MainUI.LhandMousePos);
            }
            else
            {
                LeftHand = false;
                StartPos = MainTransform.InverseTransformPoint(MainUI.RhandMousePos);
            }//InputFinger  MianBan.rect.height
            StartMoveTarget = MoveTarget.localPosition;
        }

        private void Update()
        {
            if (LeftHand) NowPos = MainTransform.InverseTransformPoint(MainUI.LhandMousePos);
            else NowPos = MainTransform.InverseTransformPoint(MainUI.RhandMousePos);
            //MoveTarget.localPosition = StartMoveTarget + NowPos;
        }
    }
}
