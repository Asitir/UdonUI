
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class UdonUI_Monitor : UdonSharpBehaviour
{
    public Transform[] allCamera = new Transform[0];//所有相机
    public Vector2[] DisAxisRange = new Vector2[0];//距离限制
    public Vector2[] HorAxisRange = new Vector2[0];//水平角度限制
    public Vector2[] VerAxisRange = new Vector2[0];//垂直角度限制

    void Start()
    {
        
    }
}
