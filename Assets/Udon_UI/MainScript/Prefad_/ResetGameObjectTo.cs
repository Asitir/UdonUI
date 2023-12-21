
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetGameObjectTo : UdonSharpBehaviour
{
    [Header("调用【OnReset】重置")]
    public Transform target;
    public void OnReset() 
    {
        Networking.SetOwner(Networking.LocalPlayer, gameObject);
        transform.rotation = target.rotation;
        transform.position = target.position;
    }
}
