
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class UdonUI_ObjSync : UdonSharpBehaviour
{
    [Header("这里储存的是你希望同步的对象")]
    public GameObject[] _object = new GameObject[0];
    [HideInInspector] [UdonSynced] public bool[] _objState = new bool[0];

    private float stateTime = -1;
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        //base.OnPlayerJoined(player);
        if (Networking.LocalPlayer.isLocal)
        {
            stateTime = 1;
        }
    }

    private void Update()
    {
        if (stateTime > 0)
        {
            stateTime -= Time.deltaTime;
            if (stateTime <= 0)
            {
                _objState = new bool[_object.Length];
                for (int i = 0; i < _objState.Length; i++)
                {
                    _objState[i] = _object[i].activeSelf;
                }
                RequestSerialization();
            }
        }
    }

    override public void OnDeserialization()
    {
        for (int i = 0; i < _objState.Length; i++)
        {
            _object[i].SetActive(_objState[i]);
        }
    }
}
