
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class UdonUI_MapEditor : UdonSharpBehaviour
{
    [HideInInspector]
    public Transform mapEditor, mapMain;
    //[HideInInspector]
    //public GameObject[] propMain_refer, propAgents_refer;

#if UNITY_EDITOR
    //这里面的数据仅用于编辑器
    [HideInInspector]
    public GameObject[] propMain, propAgents;
    [HideInInspector]
    public Transform MainInterface;
#endif

    [HideInInspector]
    public int[] propNumber;
    //[HideInInspector]
    public bool[] propRefer;

    void Start()
    {
        
    }
}
