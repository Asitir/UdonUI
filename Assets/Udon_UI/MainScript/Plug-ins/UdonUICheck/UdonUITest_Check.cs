
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI {
    public class UdonUITest_Check : UdonSharpBehaviour
    {
        public MainUI_Script mainUI;
        public UdonBehaviour[] checkTarget;
        public string[] checkTargetName;
        public string checkFuntionName = "OnCheck";
        public Text pinText;

        private int updateC;
        private string selecteColor = "#FF0000";//故障色
        private string fnishColor = "#00EE00";//正常色

        private void Update()
        {
            if (updateC > 0)
            {
                updateC--;
                checkTarget[updateC].SendCustomEvent(checkFuntionName);
                if (mainUI.OnCheck())
                    pinText.text += (checkTargetName[updateC] + $" ------  <color={fnishColor}>运行正常</color>\n");
                else
                    pinText.text += (checkTargetName[updateC] + $" ------  <color={selecteColor}>故障[{mainUI.checkErrorPo}]</color>\n");
            }
        }

        public void OnCheck() 
        {
            pinText.text = "";
            updateC = checkTarget.Length;
        }
    }
}
