
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SetPosSwitch : UdonSharpBehaviour
{
    SetPos mainSetPos;
    public LayerMask layer;
    Vector3 poss = new Vector3(0, 0, 0.5f);
    bool ccd = false;
    private void OnEnable()
    {
        if (!ccd)
        {
            mainSetPos = transform.parent.GetComponent<SetPos>();
            ccd = true;
        }
        setp();
    }

    private void OnDisable()
    {
        if (!ccd)
        {
            mainSetPos = transform.parent.GetComponent<SetPos>();
            ccd = true;
        }
        setp();
    }

    void setp() {
//#if !UNITY_EDITOR
        //VRCPlayerApi.TrackingData m_playerHead = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
        Quaternion q = Networking.LocalPlayer.GetRotation();
        Vector3 pos = Networking.LocalPlayer.GetPosition() + q * poss;
        RaycastHit rayhit;
        if (Physics.Raycast(pos + Vector3.up, Vector3.down, out rayhit, 2, layer, QueryTriggerInteraction.Ignore))
        {
            mainSetPos.SetGatePos(rayhit.point, q.eulerAngles.y, rayhit.collider.gameObject.layer == 0);
        }
//#else
//        RaycastHit rayhit;
//        if (Physics.Raycast(Vector3.forward + Vector3.up, Vector3.down, out rayhit, 2, layer, QueryTriggerInteraction.Ignore))
//        {
//            mainSetPos.SetGatePos(rayhit.point, 0, rayhit.collider.gameObject.layer == 0);
//            //Debug.Log("已碰撞");
//        }
//#endif

    }
}
