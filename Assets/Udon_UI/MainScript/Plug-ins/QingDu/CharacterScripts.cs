
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class CharacterScripts : UdonSharpBehaviour
    {
        [Header("进入下一章节时激活的对象(作为提示，不能为空)")]

        [Header("[script]打开角色脚本 OnOpenScript@ID (ID：序列)")]
        [Header("[script]进入下一章节 EnterNestScript")]
        public GameObject UnlockHints;
        public GameObject PGUP, PGDN;
        public MainUI_Script mainUI;
        public RectTransform trans;
        public RectTransform transP;
        public Text PlayerName;
        int CharacterScript = -1;
        int page = 0;//页码
        int nowPage = 0;
        int allPage = 0;
        Transform myCharacter;
        //int nowScript = -1;
        public void OnOpenScript()
        {//给予角色
            if (CharacterScript > -1)
            {
                myCharacter.gameObject.SetActive(false);
            }
            CharacterScript = mainUI.scriptID;
            myCharacter = transform.GetChild(CharacterScript);
            myCharacter.gameObject.SetActive(true);
            PlayerName.text = myCharacter.name;
            ToPage(0);
            PGUP.SetActive(false);
            if (allPage > 0) PGDN.SetActive(true);
            else PGDN.SetActive(false);
        }
        public void EnterNestScript()
        {//激活新章节
            if (CharacterScript < 0) return;
            if (allPage < myCharacter.childCount-1)
            {
                allPage++;
                UnlockHints.SetActive(true);
                PGDN.SetActive(true);
            }
        }

        public void OnPGUP() 
        {
            if (page > 0)
            {
                page--;
                if (page == 0) PGUP.SetActive(false);
                ToPage(page);
                PGDN.SetActive(true);
            }
            else
            {
                PGUP.SetActive(false);
            }

        }

        public void OnPGDN() 
        {
            if (page < allPage)
            {
                page++;
                if (page == allPage)
                {
                    PGDN.SetActive(false);
                    UnlockHints.SetActive(false);
                }
                ToPage(page);
                PGUP.SetActive(true);
            }
            else
            {
                PGDN.SetActive(false);
            }
        }

        public void ToPage(int _page)
        {
            myCharacter.GetChild(nowPage).gameObject.SetActive(false);
            RectTransform _nowRect = myCharacter.GetChild(_page).GetComponent<RectTransform>();
            _nowRect.gameObject.SetActive(true);
            Vector2 _size = new Vector2(transP.sizeDelta.x, Mathf.Max(_nowRect.sizeDelta.y + 2, transP.sizeDelta.y));
            trans.sizeDelta = _size;
            trans.localPosition = new Vector2(trans.localPosition.x, -(_size.y - transP.sizeDelta.y));

            nowPage = _page;
        }
    }
}
