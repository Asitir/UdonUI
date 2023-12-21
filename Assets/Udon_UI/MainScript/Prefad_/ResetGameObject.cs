
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ResetGameObject : UdonSharpBehaviour
{
    [Header("调用【OnReset】重置")]
    public Transform[] objects;
    Matrix4x4[] allObject;
    void Start()
    {
        allObject = new Matrix4x4[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            allObject[i] = objects[i].localToWorldMatrix;
        }
    }

    public void OnReset() 
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Networking.SetOwner(Networking.LocalPlayer, objects[i].gameObject);
            objects[i].rotation = allObject[i].rotation;
            objects[i].position = allObject[i].GetColumn(3);
        }
    }
}
