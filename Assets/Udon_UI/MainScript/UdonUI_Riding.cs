
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.ClientSim;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_Riding : UdonSharpBehaviour
    {
        public bool isRiding = false;//乘骑中

        //[HideInInspector]
        float validDis = 100;
        [HideInInspector]
        public float baseEyeOffset = 0.032f;
        //[HideInInspector]
        public Transform[] allRiding = null;//所有的乘骑系统
        [HideInInspector]
        public Transform[] allRidingChild = null;//所有的乘骑的子级
        [HideInInspector]
        public Vector2[] allRidingChildID = null;//所有的乘骑子级ID组
        [HideInInspector]
        public bool[] allRidingActiv = null;//所有的乘骑的活动状态
        [HideInInspector] 
        public MainUI_Script mainUI = null;//UdonUI环境
        [HideInInspector]
        public Vector2 cameClipPlane = new Vector2(0.01f, 100.0f);

        [HideInInspector]
        public Camera mainSecneCame,otherCame;

        Transform mTrans = null;
        Transform[] allRiding_use = null;
        int updateLate = 10;
        LayerMask groundLayer = 1 << 8;
        VRCPlayerApi localPlayer;
        float updateRunTime = 2;
        float nearClipPlane, farClipPlane;
        void Start()
        {
            localPlayer = Networking.LocalPlayer;
            //validDis = 100 * 100;
            validDis = validDis * validDis;
            mTrans = transform;
            mainSecneCame = mTrans.GetChild(0).GetChild(0).GetChild(0).GetComponent<Camera>();
            otherCame = mTrans.GetChild(0).GetChild(1).GetChild(0).GetComponent<Camera>();
            mainSecneCame.nearClipPlane = cameClipPlane.x;
            otherCame.nearClipPlane = cameClipPlane.x;
            mainSecneCame.farClipPlane = cameClipPlane.y;
            otherCame.farClipPlane = cameClipPlane.y;
            allRiding_use = new Transform[mTrans.GetChild(1).childCount];
            for (int i = 0; i < allRiding_use.Length; i++)
                allRiding_use[i] = mTrans.GetChild(1).GetChild(i);

            updateLate = allRiding.Length;
            updateRunTime = 10;

            nearClipPlane = mainSecneCame.nearClipPlane;
            farClipPlane = mainSecneCame.farClipPlane;

        }
        private void LateUpdate()
        {
            if (updateLate > 0)
            {
                //先标记位置
                updateLate--;
                for(int i=0;i< allRidingChildID[updateLate].y; i++)
                {
                    int _ID = (int)allRidingChildID[updateLate].x + i;
                    allRidingChild[_ID].name = "Ref_" + updateLate + "_" + allRidingChild[_ID].name;
                }
            }
            else
            {
                PlayerTeleport();
                UpdateHeadTrans();
                FindRecentRef();
            }

            if (updateRunTime > 0)
            {
                baseEyeOffset = mainSecneCame
                    .GetStereoViewMatrix(Camera.StereoscopicEye.Left)
                    .MultiplyPoint3x4(mainSecneCame.transform.position).magnitude;
                updateRunTime -= Time.deltaTime;
                //B.text = "base: " + baseEyeOffset;
            }
            //A.text = "EyeOffset: " + (mainUI.GetEyesOffset() / baseEyeOffset);
            //C.text = "UdonUI: " + (mainUI.GetEyesOffset());

        }

        #region 更新头部位置和旋转
        void UpdateHeadTrans() {
            VRCPlayerApi.TrackingData localPlayerTrans = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            mainUI.Head.SetPositionAndRotation(localPlayerTrans.position, localPlayerTrans.rotation);
        }
        #endregion

        #region 查找最近距离
        int findID = 0;
        int findIDE = 0;
        public int ridingID = 0;//最终使用的相机ID
        public int playerID = 0;//最终驾驶时玩家相机使用的ID
        float findDis = 10000;
        float findDisE = 10000;//最终距离

        float findDir = -1;
        float findDirE = -1;
        void FindRecentRef() 
        {
            float nowDis = 999999;
            float nowDir = -1;
            if (findID >= allRiding.Length)
            {
                //if (isRiding)
                //    ridingID = findIDE;
                //else
                playerID = findIDE;
                findDisE = findDis;
                findDirE = findDir;

                findID = 0;
                findDis = 1000000;//得到现有最近距离
                findDir = -1;
            }

            Vector3 tDir = Vector3.back;
            Vector3 mDir = Vector3.forward;

            Matrix4x4 playerRefMarit = Matrix4x4.zero;//头部位置
            if (isRiding)
            {
                if (!isRiding_late)
                {
                    isRiding_late = true;
                    mainSecneCame.enabled = true;
                }
                playerRefMarit = allRiding_use[ridingID].worldToLocalMatrix * mainUI.Head.localToWorldMatrix;
                playerRefMarit = allRiding[ridingID].localToWorldMatrix * playerRefMarit;

                if (findID != ridingID)
                {//在乘骑对象上时  直接跳过乘骑ID
                    tDir = allRiding[findID].position - (Vector3)playerRefMarit.GetColumn(3);
                    mDir = playerRefMarit.rotation * Vector3.forward;
                }
            }
            else
            {
                if (isRiding_late)
                {
                    isRiding_late = false;
                    mainSecneCame.enabled = false;
                }

                tDir = allRiding[findID].position - mainUI.Head.position;
                mDir = mainUI.Head.forward;
            }
            nowDis = tDir.sqrMagnitude;
            nowDir = Vector3.Dot(tDir.normalized, mDir.normalized);
            if (nowDir > findDir) {
                findDir = nowDir;
                if (nowDir > 0)
                {
                    findDis = nowDis;
                    findIDE = findID;
                }
            }
            //if (Vector3.Dot(tDir.normalized, mDir.normalized) > 0)
            //{//确保在玩家正面findDir
            //    if (nowDis < findDis)
            //    {
            //        findDis = nowDis;
            //        findIDE = findID;
            //    }
            //}


            SetReferSceneCame(playerRefMarit);
            SetReferPlayerCame();
            findID++;
        }
        #endregion

        #region 设置参考相机
        bool isRiding_late = false;
        void SetReferSceneCame(Matrix4x4 _playerRefMarit)
        {
            if (isRiding)
            {
                //mainSecneCame.transform.SetPositionAndRotation(_playerRefMarit.GetColumn(3), _playerRefMarit.rotation);
                //if (mainUI.VRgm)
                //    mainSecneCame.transform.localScale = mainUI.GetEyesOffset() * Vector3.one * _playerRefMarit.lossyScale.y;
                SetCameTrans(mainSecneCame, _playerRefMarit.GetColumn(3), _playerRefMarit.rotation, _playerRefMarit.lossyScale.y);
                SetRefTrans(ridingID);
            }
        }


        bool setPlayerCame = false;
        void SetReferPlayerCame() { 
            if(findDisE < validDis && playerID > -1)
            {//在安全距离内
                if (!setPlayerCame)
                {
                    otherCame.enabled = true;
                    setPlayerCame = true;
                }

                //int playerCameID = isRiding ? playerID : ridingID;
                float scale = 1;
                if (isRiding)
                {//在对象上观测其它对象
                    Matrix4x4 _referMatrix = allRiding[playerID].worldToLocalMatrix * mainSecneCame.transform.localToWorldMatrix;
                    Matrix4x4 _setMatrix = allRiding_use[playerID].localToWorldMatrix * _referMatrix;
                    //otherCame.transform.SetPositionAndRotation(_setMatrix.GetColumn(3), _setMatrix.rotation);
                    scale = _setMatrix.lossyScale.y;
                    SetCameTrans(otherCame, _setMatrix.GetColumn(3), _setMatrix.rotation, scale);
                }
                else
                {
                    Matrix4x4 _referMatrix = allRiding[playerID].worldToLocalMatrix * mainUI.Head.localToWorldMatrix;
                    Matrix4x4 _setMatrix = allRiding_use[playerID].localToWorldMatrix * _referMatrix;
                    //otherCame.transform.SetPositionAndRotation(_setMatrix.GetColumn(3), _setMatrix.rotation);
                    scale = _setMatrix.lossyScale.y;
                    SetCameTrans(otherCame, _setMatrix.GetColumn(3), _setMatrix.rotation, scale);
                }
                SetRefTrans(ridingID);
            }
            else
            {
                if (setPlayerCame)
                {
                    otherCame.enabled = false;
                    setPlayerCame = false;
                }
            }
        }

        void SetCameTrans(Camera _came, Vector3 _pos, Quaternion _rot, float _scale = 1)
        {
            if (mainUI.VRgm)
            {
                float cameScale = (0.66f * _scale);
                //float cameScale = ((mainUI.GetEyesOffset() / baseEyeOffset) * _scale);
                Transform cameTrnas = _came.transform;
                Transform cameParent = cameTrnas.parent;
                Quaternion mRot = _rot * Quaternion.Inverse(cameTrnas.localRotation);
                Vector3 mPos = _pos - mRot * cameTrnas.localPosition;
                cameParent.SetPositionAndRotation(mPos, mRot);
                //cameTrnas.localScale = Vector3.one * _scale * (mainUI.GetEyesOffset() / baseEyeOffset);
                cameTrnas.localScale = Vector3.one * cameScale;
            }
            else
            {
                _came.transform.SetPositionAndRotation(_pos, _rot);
            }
            _came.nearClipPlane = nearClipPlane * _scale;
            _came.farClipPlane = farClipPlane * _scale;

        }

        #endregion

        #region 设置反射的对象
        int setRefTransID = 0;
        void SetRefTrans(int _ID) {
            //if (allRidingActiv[_ID])
            {
                //allRiding_use[_ID].localScale = allRiding[_ID].lossyScale;
                Vector2 refRange = allRidingChildID[_ID];

                //for (int i = 0; i < refRange.y; i++)
                //{
                //    Matrix4x4 refMatrix = allRiding[_ID].worldToLocalMatrix * allRidingChild[i + refRange.x].localToWorldMatrix;
                //    Matrix4x4 useMatrix = allRiding_use[_ID].localToWorldMatrix * refMatrix;
                //    allRiding_use[_ID].GetChild(i).SetPositionAndRotation(useMatrix.GetColumn(3), useMatrix.rotation);
                //    allRiding_use[_ID].GetChild(i).localScale = useMatrix.lossyScale;
                //}
                //避免用户单个对象包含过多子级  为此将循环拆开到每一帧

                setRefTransID++;
                if (setRefTransID >= refRange.y) setRefTransID = 0;
                Matrix4x4 refMatrix = allRiding[_ID].worldToLocalMatrix * allRidingChild[setRefTransID + (int)refRange.x].localToWorldMatrix;
                Matrix4x4 useMatrix = allRiding_use[_ID].localToWorldMatrix * refMatrix;
                allRiding_use[_ID].GetChild(setRefTransID).SetPositionAndRotation(useMatrix.GetColumn(3), useMatrix.rotation);
                allRiding_use[_ID].GetChild(setRefTransID).localScale = useMatrix.lossyScale;
            }
        }
        #endregion

        #region 传送玩家
        int teleportID = 0;
        //bool isGravity_late = false;
        int findLast = -1;
        private void PlayerTeleport()
        {
            if (teleportID > 5) teleportID = 0;
            else teleportID++;

            if(teleportID == 1)
            {//错开计算
                RaycastHit hit;
                Vector3 playerPos = localPlayer.GetPosition();
                bool isGravity = false;
                int finId = 0;
                if (Physics.Raycast(playerPos + 0.2f * Vector3.up, Vector3.down, out hit, 1.2f, groundLayer, QueryTriggerInteraction.Ignore))
                {
                    string[] name = hit.collider.name.Split('_');
                    if (name.Length > 2)
                    {
                        if (name[0] == "Ref")
                        {
                            isGravity = true;
                            finId = int.Parse(name[1]);
                            ridingID = finId;
                        }
                    }
                }

                if (isGravity)
                {
                    //Debug.Log("在在在在在");
                    if (!isRiding)
                    {//传送至反射的载具
                        isRiding = true;
                    }

                    if (findLast != finId)
                    {//允许载具跳载具
                        if (findLast < 0)
                        {//判断是主世界跳载具
                            Vector3 pos = allRiding[finId].InverseTransformPoint(localPlayer.GetPosition());//相对位置差
                            Quaternion rot = Quaternion.Inverse(allRiding[finId].rotation) * (localPlayer.GetRotation());
                            localPlayer.TeleportTo(allRiding_use[finId].TransformPoint(pos), rot);

                            localPlayer.SetVelocity(Quaternion.FromToRotation(allRiding[finId].forward, allRiding_use[finId].forward) * localPlayer.GetVelocity());//试了
                            findID = finId;
                            playerID = -2;//禁用玩家相机
                            //UpdateHeadTrans();
                        }
                        //else
                        //{//判断是载具跳载具
                        // //Matrix4x4 refMatrix = 
                        //    Vector3 pos_world = allRiding_use[findLast].InverseTransformPoint(localPlayer.GetPosition());
                        //    pos_world = allRiding[findLast].TransformPoint(pos_world);//先找到世界的位置

                        //    Vector3 pos = allRiding[finId].InverseTransformPoint(pos_world);//相对位置差
                        //    Quaternion rot = allRiding[finId].rotation * Quaternion.Inverse(localPlayer.GetRotation());
                        //    localPlayer.TeleportTo(allRiding_use[finId].TransformPoint(pos), rot);
                        //    findID = findLast;
                        //    //想了想 并不会直接触发载具跳载具的事件  所以直接不做这部分逻辑了
                        //}
                        findLast = finId;
                    }
                }
                else
                {
                    if (isRiding)
                    {//回到主世界 初始化
                        playerID = ridingID;//继承载具ID
                        isRiding = false;
                        findLast = -2;
                        findID = finId;
                        teleportID = 10;

                        Vector3 pos = allRiding_use[ridingID].InverseTransformPoint(localPlayer.GetPosition());//相对位置差
                        Quaternion rot = Quaternion.Inverse(allRiding_use[ridingID].rotation) * (localPlayer.GetRotation());
                        localPlayer.TeleportTo(allRiding[ridingID].TransformPoint(pos), allRiding[ridingID].rotation * rot);

                        //MD试了各种方法都没办法正确转换向量，又不想开平方，最后只能使用这个B方法，如果有更好的解决方案还请联系我
                        localPlayer.SetVelocity(Quaternion.FromToRotation(allRiding_use[ridingID].forward, allRiding[ridingID].forward) * localPlayer.GetVelocity());

                        //UpdateHeadTrans();
                    }
                }
            }
        }
        #endregion
    }
}
