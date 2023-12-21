
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class Chat_VrInput_Lock : UdonSharpBehaviour
    {
        ScrollRect scr;

        MainUI_Script MainUI;
        Chat_VrInput_Out AnimOut;
        bool LeftHand = false;
        Vector3 StartPos, NowPos;
        Vector3 LocalStartPos;
        Vector3 VOffset = Vector3.zero;
        float StartHight;
        Transform maintrans;
        Vector3 EndStart = Vector3.zero;
        RectTransform MainRootUI;
        RectTransform MianBan;
        float LateOffset, DeltaOffset;
        [Tooltip("松开手指后继承下来的速度阻力")]
        public float obstruction = 1;
        float MaxDis, MinDis;

        bool instan = false;

        private void OnEnable()
        {
            if (!instan)
            {
                maintrans = transform;
                MainRootUI = transform.parent.GetComponent<RectTransform>();
                AnimOut = transform.parent.Find("SlidingWindows_Out").GetComponent<Chat_VrInput_Out>();
                MianBan = transform.parent.GetChild(0).GetComponent<RectTransform>();
                MainUI = GameObject.Find("/UdonUI_Main").GetComponent<MainUI_Script>();
                StartHight = MainRootUI.rect.height;
                scr = maintrans.parent.parent.GetComponent<ScrollRect>();
                instan = true;
            }
            scr.enabled = false;
            AnimOut.gameObject.SetActive(false);
            float Left = Vector3.Distance(MainUI.LhandMousePos, MainRootUI.position);
            float Right = Vector3.Distance(MainUI.RhandMousePos, MainRootUI.position);
            if (Left < Right)
            {
                LeftHand = true;
                //maintrans.position = MainUI.LhandMousePos;
                StartPos = MainRootUI.InverseTransformPoint(MainUI.LhandMousePos);
            }
            else
            {
                LeftHand = false;
                StartPos = MainRootUI.InverseTransformPoint(MainUI.RhandMousePos);
                //maintrans.position = MainUI.RhandMousePos;
            }//InputFinger  MianBan.rect.height
            LocalStartPos = MianBan.localPosition;//获取初始化位置
                                                  //LocalStartPos.x = 0;
            MaxDis = Mathf.Abs(MianBan.localPosition.y);
            MinDis = (MaxDis + MainRootUI.rect.height) - MianBan.rect.height;
        }

        private void OnDisable()
        {
            scr.enabled = true;
            AnimOut.EndStart = MianBan.localPosition;//获取初始化位置
            AnimOut.DeltaSpeed = DeltaOffset;
            AnimOut.AnimTime = 1;
            AnimOut.OffsetPos = Vector3.zero;
            AnimOut.Maxheight = -MainRootUI.rect.height;
            AnimOut.MinHeights = -MianBan.rect.height;
            AnimOut.Target = MianBan;
            AnimOut.gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            if (LeftHand) NowPos = MainUI.LhandMousePos; else NowPos = MainUI.RhandMousePos;

            if (NowPos == Vector3.zero)
            {
                //结束
                gameObject.SetActive(false);
                return;
            }

            float Offset = MainRootUI.InverseTransformPoint(NowPos).y - StartPos.y;//偏移值
            DeltaOffset = Offset - LateOffset;//每帧的偏移量
            LateOffset = Offset;
            VOffset.y = Mathf.Max(0, Offset);

            MianBan.localPosition = VOffset + LocalStartPos;
        }
    }
}
