
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class ComicScript : UdonSharpBehaviour
    {
        //public bool isPGUP = false;

        public bool isEnd = false;
        public Transform ComicMain = null;
        public GameObject sliderRoot = null;
        public GameObject start, end;
        public bool isSlider = false;
        int nowPage = 0;
        //private void OnEnable()
        //{
        //    Event();
        //}

        //private void OnDisable()
        //{
        //    Event();
        //}

        //private void Init()
        //{
        //    isStart = true;
        //}

        //private void Event()
        //{
        //    if (isPGUP)
        //    {
        //        //Debug.LogError("上一页");
        //        UpdatePage(false);
        //    }
        //    else
        //    {
        //        //Debug.LogError("下一页");
        //        UpdatePage(true);
        //    }
        //}

        public void UpdatePage(bool isNext)
        {
            int childCount = this.ComicMain.childCount + (isEnd ? 1 : 0) + 1;//内容页数量
            int latePage = nowPage;

            if (isNext)
            {
                nowPage++;
                if (nowPage >= childCount)
                    nowPage = 0;
            }
            else
            {
                nowPage--;
                if (nowPage < 0)
                {
                    nowPage = childCount - 1;
                }
            }


            //上一页
            if (latePage == 0)
            {
                //if (isSlider)
                //{
                //    sliderRoot.SetActive(true);
                //}
                ComicMain.gameObject.SetActive(true);
                start.SetActive(false);
            }
            else
            {
                if (latePage > ComicMain.childCount)
                {
                    end.SetActive(false);
                }
                else
                {
                    ComicMain.GetChild(latePage - 1).gameObject.SetActive(false);
                }
            }

            //当前页
            if (nowPage == 0)
            {
                //if (isSlider)
                //{
                //    sliderRoot.SetActive(false);
                //}
                ComicMain.gameObject.SetActive(false);
                start.SetActive(true);
            }
            else
            {
                if (nowPage > ComicMain.childCount)
                {
                    end.SetActive(true);
                }
                else
                {
                    ComicMain.GetChild(nowPage-1).gameObject.SetActive(true);
                }
            }
        }
    }
}
