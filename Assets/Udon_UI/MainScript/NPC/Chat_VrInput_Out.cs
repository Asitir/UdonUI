
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class Chat_VrInput_Out : UdonSharpBehaviour
    {
        [HideInInspector]
        public float DeltaSpeed, AnimTime, Maxheight, MinHeights;
        [HideInInspector]
        public RectTransform Target;
        public Vector3 EndStart;
        public Vector3 OffsetPos = Vector3.zero;

        private void Update()
        {
            if (AnimTime > 0)
            {
                AnimTime -= Time.deltaTime;
                OffsetPos.y += Mathf.Lerp(0, DeltaSpeed, AnimTime);

                Target.localPosition = EndStart + OffsetPos;

                if (Target.localPosition.y < MinHeights || Target.localPosition.y > Maxheight)
                    gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}