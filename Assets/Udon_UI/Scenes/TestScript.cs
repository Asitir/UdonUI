
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TestScript : UdonSharpBehaviour
{
    public GameObject AAA;
    void Start()
    {
        
    }

    public void Open() 
    {
        //Debug.Log("接受Opend");
        AAA.SetActive(true);
    }

    public void SSDwon()
    {
        //Debug.Log("接受Down");
        AAA.SetActive(false);
    }
}
