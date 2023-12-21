
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class SightInteract : UdonSharpBehaviour
    {
        [HideInInspector]
        public MainUI_Script mainui;
        [HideInInspector]
        public Transform[] buttons;//目标集  触发对象集
        //[HideInInspector]
        public int[] interactType, SighttType;//触发类型  1:一次性产物  2:绑定     0:注释方向  1：Z轴方向
        [HideInInspector]
        public float[] angle, radius;//触发角度 半径

        bool[] interactType_OnlyL;

        int loopConst = 0, loopConstNow;
        //bool initFlish = false;
        void Start()
        {
            loopConst = buttons.Length;
            loopConstNow = -1;
            interactType_OnlyL = new bool[loopConst];
        }

        private void LateUpdate()
        {
            Test();
        }

        void Test()
        {
            loopConstNow++;
            if (loopConstNow >= loopConst)
            {//完成一个循环
                loopConstNow = 0;
                //initFlish = true;
            }
            //Debug.Log("监测: " + loopConst + "\n监测2: " + loopConstNow);
            //if (initFlish)
            {//初始化结束
                if (interactType[loopConstNow] == 1)
                {//一次性的
                    if (buttons[loopConstNow].gameObject.activeInHierarchy)
                    {
                        if (CheckID(loopConstNow))
                        {
                            //interactType[loopConstNow] = 3;
                            buttons[loopConstNow].gameObject.SetActive(false);
                            RunEvents(loopConstNow, 0);
                            //Debug.Log("进入");
                        }

                        //if (CheckID(loopConstNow))
                        //{
                        //    if (interactType[loopConstNow] == 1)
                        //    {
                        //        interactType[loopConstNow] = 3;
                        //        RunEvents(loopConstNow, 0);
                        //        Debug.Log("进入");
                        //    }
                        //}
                        //else
                        //{
                        //    if (interactType[loopConstNow] == 3)
                        //    {
                        //        buttons[loopConstNow].gameObject.SetActive(false);
                        //        RunEvents(loopConstNow, 1);
                        //        Debug.Log("退出");
                        //    }
                        //}
                    }
                }
                else if(interactType[loopConstNow] == 2)
                {//绑定的
                    //Debug.Log("监测");
                    if (buttons[loopConstNow].gameObject.activeInHierarchy)
                    {

                        if (CheckID(loopConstNow))
                        {
                            //Debug.Log("监测成功");
                            if (!interactType_OnlyL[loopConstNow])
                            {
                                interactType_OnlyL[loopConstNow] = true;
                                RunEvents(loopConstNow);
                            }
                        }
                        else
                        {
                            //Debug.Log("监测失败");

                            if (interactType_OnlyL[loopConstNow])
                            {
                                interactType_OnlyL[loopConstNow] = false;
                                RunEvents(loopConstNow,1);
                            }
                        }
                    }
                }
            }
        }

        bool CheckID(int _ID)
        {
            Vector3 checkDis = buttons[_ID].position -  mainui.Head.position;
            Vector3 checkDir = SighttType[_ID] == 1 ? buttons[_ID].forward : checkDis.normalized;//mainui.Head.forward 
            if (radius[_ID] < 0)
            {
                if (checkDis.sqrMagnitude < (radius[_ID] * radius[_ID]))
                {//在半径范围内
                 //Debug.Log("安全范围");
                    if (angle[_ID] > 0)
                    {
                        if (Vector3.Angle(mainui.Head.forward, checkDir) > angle[_ID])
                        {//在安全角度内
                         //Debug.Log("安全角度");
                            return true;
                        }
                    }
                    else
                    {
                        if (Vector3.Angle(mainui.Head.forward, checkDir) < -angle[_ID])
                        {//在安全角度内
                         //Debug.Log("安全角度");
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (checkDis.sqrMagnitude > (radius[_ID] * radius[_ID]))
                {//在半径范围外
                 //Debug.Log("安全范围");
                    if (angle[_ID] > 0)
                    {
                        if (Vector3.Angle(mainui.Head.forward, checkDir) > angle[_ID])
                        {
                            //Debug.Log("安全角度外：" + Vector3.Angle(mainui.Head.forward, checkDir));
                            //Debug.DrawRay(mainui.Head.position, mainui.Head.forward, Color.red);
                            //Debug.DrawRay(mainui.Head.position, checkDir, Color.green);
                            return true;
                        }
                    }
                    else
                    {
                        if (Vector3.Angle(mainui.Head.forward, checkDir) < -angle[_ID])
                        {//在安全角度内
                            //Debug.Log("安全角度内：" + Vector3.Angle(mainui.Head.forward, checkDir));
                            //Debug.DrawRay(mainui.Head.position, mainui.Head.forward, Color.red,600);
                            //Debug.DrawRay(mainui.Head.position, checkDir, Color.green,600);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void RunEvents(int _ID,int isenter = 0) {
            string[] IsName = buttons[_ID].name.Split('_');
            if (IsName[0] == "UCo")
            {
                mainui.BOXC_RunEvents(isenter, int.Parse(IsName[1]));
            }
            //Debug.LogWarning("已触发: " + buttons[_ID].name + "\n触发类型: " + (isenter == 0 ? "进入" : "离开"));
        }

        public void OnReset()
        {
            //GameObject[] _Boxs = mainui.BoxColliderUdon;
            //foreach (var item in _Boxs)
            //{
            //    string[] IsName = item.name.Split('_');
            //    if (IsName[0] == "UCo")
            //    {
            //        mainui.BOXC_RunEvents(0, int.Parse(IsName[1]), true, true);
            //    }
            //}

            //foreach (var item in interactType)
            //{
            //    if (item == 3)
            //    {
            //        item = 1;
            //    }
            //}
            for (int i = 0; i < interactType.Length; i++)
            {
                if (interactType[i] == 3)
                {
                    interactType[i] = 1;
                }
            }
        }

        public void OnCheck() { mainui.checkID++; }
    }
}
