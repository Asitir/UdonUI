
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class CaptionsSystem : UdonSharpBehaviour
    {
        [HideInInspector]
        public MainUI_Script mainUI;
        public Text CaptionText;

        [HideInInspector]
        public Transform target;
        public int selfAngle = 30;
        public float selfAngleSpeed = 5.0f;
        public int maxAngle = 35;
        public float maxAngleSpeed = 12.0f;
        [Range(-90.0f, 90.0f)]
        public float lookUPAngle = 30.0f;
        [Range(-90.0f, 90.0f)]
        public float lookUPAngle_Down =-5.0f;
        [Range(-90, 90)]
        public int lookUP = 12;
        public float offsetPos = -0.1f;

        //计算单元
        public int EventID = -1;

        public string[] allCaptions = new string[0];
        public int[] eventID = new int[0];
        public float[] lifeTime = new float[0]; 


        Transform mTrans;
        float deltatime;
        bool toCenter = true;
        bool isLookUP = false;
        Quaternion horizontalRot;

        float startTime = 0.1f;

        private void OnStart()
        {
            target = mainUI.Head;
            mTrans = transform;
            horizontalRot = mTrans.rotation;
        }

        void LateUpdate()
        {
            deltatime = Time.deltaTime;

            if(startTime > 0)
            {
                startTime -= deltatime;
                if (startTime <= 0)
                    OnStart();
                return;
            }

            Vector2 _targetDir = Vector2.zero, _transDir = Vector2.zero;
            _targetDir.x = target.forward.x;
            _targetDir.y = target.forward.z;

            _transDir.x = mTrans.forward.x;
            _transDir.y = mTrans.forward.z;

            _targetDir.Normalize();
            _transDir.Normalize();

            float _selfAngle = (float)selfAngle / 2;
            float _angleOffset = Vector2.SignedAngle(_transDir, _targetDir);

            if (!toCenter)
            {
                if (Mathf.Abs(_angleOffset) > _selfAngle)
                {//超出安全角度
                    Quaternion _targetRot = Quaternion.Euler(0, target.eulerAngles.y + (_selfAngle * (_angleOffset > 0 ? 1 : -1)), 0);
                    horizontalRot = Quaternion.Lerp(horizontalRot, _targetRot, selfAngleSpeed * deltatime);

                    if (maxAngle > selfAngle)
                    {
                        if (Mathf.Abs(_angleOffset) > maxAngle) toCenter = true;
                    }
                }
            }
            else
            {
                if (Mathf.Abs(_angleOffset) < 2)
                    toCenter = false;
                Quaternion _targetRot = Quaternion.Euler(0, target.eulerAngles.y, 0);
                horizontalRot = Quaternion.Lerp(horizontalRot, _targetRot, maxAngleSpeed * deltatime);
            }

            Quaternion _mainRot = horizontalRot;
            if (lookUP > 0)
            {
                float upAngle = Vector3.SignedAngle(target.up, Vector3.up, target.right);
                if (!isLookUP)
                {
                    if (upAngle > lookUP)
                        isLookUP = true;
                }
                if (isLookUP)
                {
                    if (upAngle < Mathf.Min(lookUPAngle_Down, lookUPAngle))
                        isLookUP = false;

                    _mainRot = horizontalRot * Quaternion.Euler(-lookUPAngle, 0, 0);
                }
            }
            mTrans.rotation = _mainRot;
            mTrans.position = target.TransformPoint(0, 0, offsetPos);
            //mTrans.position = Vector3.Lerp(mTrans.position, target.position, deltatime * 12);
        }
    }
}
