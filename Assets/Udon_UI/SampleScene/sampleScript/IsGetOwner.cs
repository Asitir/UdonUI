
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class IsGetOwner : UdonSharpBehaviour
{
    VRCPlayerApi localPlayer = null;
    bool isSet = false;
    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    private void Update()
    {
        if(Networking.GetOwner(gameObject) == localPlayer)
        {
            if (!isSet)
            {
                isSet = true;
                transform.parent.GetComponent<Animator>().enabled = true;
            }
        }
        else
        {
            if (isSet)
            {
                isSet = false;
                transform.parent.GetComponent<Animator>().enabled = false;
            }
        }
    }
}
