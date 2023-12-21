
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class UdonUI_Time : UdonSharpBehaviour
    {
        public Text timeDis;
        public string disType = "t";
        private void Update()
        {
            timeDis.text = System.DateTime.Now.ToString(disType);
        }
    }
}
