
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace UdonUI
{
    public class ComicSwitchPage : UdonSharpBehaviour
    {
        public bool isNext;
        bool isStart = false;
        ComicScript mainS;
        private void OnEnable()
        {
            if (!isStart)Init();
            mainS.UpdatePage(isNext);
        }

        private void OnDisable()
        {
            if (!isStart) Init();
            mainS.UpdatePage(isNext);
        }

        private void Init()
        {
            isStart = true;
            mainS = transform.parent.GetComponent<ComicScript>();
        }
    }
}
