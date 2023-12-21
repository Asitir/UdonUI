
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SetPos : UdonSharpBehaviour
{
    public Transform gateb, gatea;
    Transform gate;
    public float speed = 6;
    public float height = 3;
    float Time1 = -1;
    float pp = 1;
    float deltatime;
    //[HideInInspector]
    [UdonSynced] public Vector3 MainPos;
    [UdonSynced] public float r2;
    [UdonSynced] public bool b2;
    Vector3 latPos = Vector3.zero;
    private void Update()
    {
        deltatime = Time.deltaTime;
        if (latPos != MainPos) {
            //Debug.Log("已经执行了");
            Time1 = 1;
            pp = 1;
            latPos = MainPos;
            gate = b2 ? gateb : gatea;
            gate.rotation = Quaternion.Euler(0, r2, 0);
        }

        if (Time1 > 0) {
            Time1 -= deltatime / 3;
            pp = Mathf.Lerp(pp, 0, deltatime * speed);
            if (Time1 < 0)
            {
                gate.position = MainPos;
                Time1 = -1;
            }
            else {
                gate.position = MainPos + Vector3.down * (height * pp);
            }
        }
    }

    public void SetGatePos(Vector3 pos,float r,bool b)
    {
#if !UNITY_EDITOR
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
#endif
        MainPos = pos;
        b2 = b;
        r2 = r;
#if !UNITY_EDITOR
        RequestSerialization();
#endif

    }
}
