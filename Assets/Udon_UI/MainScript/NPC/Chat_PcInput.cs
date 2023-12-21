
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Chat_PcInput : UdonSharpBehaviour
{
    public float speed = 10;
    public RectTransform moveTrans;
    private void Update()
    {
        moveTrans.localPosition += Vector3.down * Input.GetAxis("Mouse ScrollWheel") * speed;
    }
}
