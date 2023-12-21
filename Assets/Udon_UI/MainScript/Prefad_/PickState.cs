
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickState : UdonSharpBehaviour
{
    bool useGravity = true;
    bool isKinematic = false;
    bool isInit = false;
    Rigidbody rigidbody;
    public void SetGravityOn()
    {
        useGravity = true;
        isKinematic = false;
    }
    public void SetGravityOff()
    {
        useGravity = false;
        isKinematic = true;
    }


    //public override void OnPickup()
    //{
    //    useGravity = rigidbody.useGravity;
    //    isKinematic = rigidbody.isKinematic;
    //    //base.OnPickup();
    //    //Debug.Log("OnPickup");
    //}

    public override void OnDrop()
    {
        if (!isInit)
        {
            rigidbody = GetComponent<Rigidbody>();
            isInit = true;
        }
        rigidbody.useGravity = useGravity;
        rigidbody.isKinematic = isKinematic;
        //base.OnDrop();
        //Debug.Log("OnDrop");
    }
}
