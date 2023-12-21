
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class MoveWindows : UdonSharpBehaviour
    {
        //public Transform MoveTarget;
        [HideInInspector]
        public bool MoveTargetParent = true;
        bool instanl = false;
        Transform NullObj;
        Transform Parent, Parentp;
        Transform MainTransFrom;
        bool LeftHand;
        MainUI_Script MainUI;
        Vector3 FingerStertPos, OffsetPos = Vector3.zero;
        Vector3 latepos = Vector3.zero, deltapos = Vector3.zero, latedeltapos = Vector3.zero;

        bool ISVR = false;
        private void OnEnable()
        {
            if (!instanl)
            {
                ISVR = Networking.LocalPlayer.IsUserInVR();
                MainTransFrom = transform;
                Parentp = transform.parent.parent;
                Parent = transform.parent;
                MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
                if (ISVR) HandPos();
                else HandPosPC();
                NullObj = Instantiate(MainUI.NullObject[0]).transform;
                NullObj.localScale = Parentp.localScale;
                NullObj.rotation = Parentp.rotation;
                NullObj.position = Parentp.position;
                instanl = true;
            }
            else
            {
                if (ISVR) HandPos();
                else HandPosPC();
                NullObj.rotation = Parentp.rotation;
                NullObj.position = Parentp.position;
            }

            if (ISVR)
            {
                if (LeftHand) FingerStertPos = NullObj.InverseTransformPoint(MainUI.LztPos);
                else FingerStertPos = NullObj.InverseTransformPoint(MainUI.RztPos);
            }
            else
            {
                if (LeftHand) FingerStertPos = NullObj.InverseTransformPoint(MainUI.LhandMousePos);
                else FingerStertPos = NullObj.InverseTransformPoint(MainUI.RhandMousePos);
            }
            FingerStertPos.z = 0;
        }

        private void LateUpdate()
        {
            if (ISVR)
            {
                Vector3 apos = Vector3.zero;
                if (LeftHand) apos = MainUI.LztPos;
                else apos = MainUI.RztPos;
                OffsetPos = NullObj.InverseTransformPoint(apos) - FingerStertPos;

                if (OffsetPos.z < 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    OffsetPos.z = 0;
                    //if (MoveTargetParent) Parentp.parent.position = NullObj.TransformPoint(OffsetPos);
                    //else 
                        Parentp.position = NullObj.TransformPoint(OffsetPos);
                }
            }
            else
            {
                Vector3 apos = Vector3.zero;
                if (LeftHand) apos = MainUI.LhandMousePos;
                else apos = MainUI.RhandMousePos;
                if (Input.GetMouseButtonUp(0))
                {
                    gameObject.SetActive(false);
                }
                if (apos == Vector3.zero)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    OffsetPos = NullObj.InverseTransformPoint(apos) - FingerStertPos;
                    OffsetPos.z = 0;
                    //if (MoveTargetParent) Parentp.parent.position = NullObj.TransformPoint(OffsetPos);
                    //else 
                        Parentp.position = NullObj.TransformPoint(OffsetPos);
                }
            }
        }

        void HandPos()
        {
            float Left = Vector3.Distance(MainUI.LztPos, Parent.position);
            float Right = Vector3.Distance(MainUI.RztPos, Parent.position);
            if (Left < Right) LeftHand = true;
            else LeftHand = false;
        }

        void HandPosPC()
        {
            float Left = Vector3.Distance(MainUI.LhandMousePos, Parent.position);
            float Right = Vector3.Distance(MainUI.RhandMousePos, Parent.position);
            if (Left < Right) LeftHand = true;
            else LeftHand = false;
        }
    }
}