using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
using Tables;
namespace GameUI
{

    public class UISpecilFightInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UISpecilFightInfo", hash);
        }

        #endregion

        #region params

        public List<UIGirlMemberItem> GirlItemList;
        public Text LastTime;
        public Button _BtnComplate;
        public Button _BtnComplateImmediately;

        public Image _CustomImg;
        public UIContainerBase _CustomPointAttrs;

        private List<GirlMemberInfo> SelectedGirls = new List<GirlMemberInfo>();
        private GuestInfoRecord _ShowGuest;
        #endregion

        #region show funs

        public void Update()
        {
            if (FightStagePack.Instance.IsFighting() && !FightStagePack.Instance.IsFightFinish())
            {
                LastTime.gameObject.SetActive(true);

                var date = GameCore.Instance.TimeController.GetTimeDelay(TIMER_TYPE.TIMER_SPECIL_STAGE_REFRESH);
                var timeSpan = (date - TimeSpan.FromSeconds(FightStagePack.SPECIL_GUEST_REFRESH_TIME)).ToString();
                int charIdx = timeSpan.IndexOf(':');
                string formatTime = timeSpan.Substring(charIdx + 1, 5);
                LastTime.text = formatTime;

                _BtnComplateImmediately.interactable = true;
                _BtnComplate.interactable = false;
            }
            else if (FightStagePack.Instance.IsFighting() && FightStagePack.Instance.IsFightFinish())
            {
                LastTime.gameObject.SetActive(false);

                _BtnComplateImmediately.interactable = false;
                _BtnComplate.interactable = true;
            }
            else if (!FightStagePack.Instance.IsFighting())
            {
                LastTime.gameObject.SetActive(false);

                _BtnComplateImmediately.interactable = false;
                _BtnComplate.interactable = false;
            }

        }

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            InitInfo();
        }

        public void InitInfo()
        {
            var guestInfo = FightStagePack.Instance.GetSpecilGuest();

            InitGirlMemberItems();
            InitCustom();

            if (FightStagePack.Instance.IsFightFinish())
            {
                LastTime.gameObject.SetActive(false);
            }
            else
            {
                LastTime.gameObject.SetActive(true);
            }
        }

        public void InitGirlMemberItems()
        {
            SelectedGirls = new List<GirlMemberInfo>(FightStagePack.Instance.FightingGirls);

            for(int i = 0; i< GirlItemList.Count; ++i)
            {
                if (SelectedGirls.Count > i)
                    GirlItemList[i].InitGirl(SelectedGirls[i]);
                else
                    GirlItemList[i].InitGirl(null);

                GirlItemList[i]._PanelClickEvent = BtnShowSelect;
            }
        }

        public void InitCustom()
        {
            _ShowGuest = FightStagePack.Instance.GetSpecilGuest();
            _CustomPointAttrs.InitContentItem(GetSortedPointAttr());
        }

        public void RefreshGirlItems()
        {
            for (int i = 0; i < GirlItemList.Count; ++i)
            {
                if (SelectedGirls.Count > i)
                    GirlItemList[i].InitGirl(SelectedGirls[i]);
                else
                    GirlItemList[i].InitGirl(null);
            }
        }


        #endregion

        #region custom

        private class AttrShow
        {
            public int AttrValue;
            public string AttrName;
        }

        private List<string> GetSortedPointAttr()
        {
            List<AttrShow> attrList = new List<AttrShow>();

            if (_ShowGuest.Attr1APoint > _ShowGuest.Attr1BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr1APoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[0];
                attrList.Add(attr);
            }
            else
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr1BPoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[1];
                attrList.Add(attr);
            }

            if (_ShowGuest.Attr2APoint > _ShowGuest.Attr2BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr2APoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[2];
                attrList.Add(attr);
            }
            else
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr2BPoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[3];
                attrList.Add(attr);
            }

            if (_ShowGuest.Attr3APoint > _ShowGuest.Attr3BPoint)
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr3APoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[4];
                attrList.Add(attr);
            }
            else
            {
                AttrShow attr = new AttrShow();
                attr.AttrValue = (int)_ShowGuest.Attr3BPoint;
                attr.AttrName = GirlMemberInfo.ATTR_NAMES[5];
                attrList.Add(attr);
            }

            attrList.Sort((attr1, attr2) =>
            {
                if (attr1.AttrValue > attr2.AttrValue)
                    return 1;
                else if (attr1.AttrValue < attr2.AttrValue)
                    return -1;
                return 0;
            });

            List<string> attrStrs = new List<string>();
            foreach (var attrInfo in attrList)
            {
                attrStrs.Add(attrInfo.AttrName);
            }
            return attrStrs;
        }

        #endregion

        #region inact

        public void BtnShowSelect(UIItemBase itemBase)
        {
            if (FightStagePack.Instance.IsFighting())
                return;

            UIGirlMemberItem selectItem = itemBase as UIGirlMemberItem;
            SelectedGirls.Remove(selectItem._GirlMenberInfo);
            RefreshGirlItems();

            UIGirlSelectPanel.ShowAsyn(OnSelectMember, OnUnSelectMember, SelectedGirls, false);
        }

        private void OnSelectMember(GirlMemberInfo girlInfo)
        {
            SelectedGirls.Add(girlInfo);
            RefreshGirlItems();

            if (SelectedGirls.Count == GirlItemList.Count)
            {
                UIGirlSelectPanel.HideAsyn();
            }
        }

        private void OnUnSelectMember(GirlMemberInfo girlInfo)
        {
            SelectedGirls.Remove(girlInfo);
            RefreshGirlItems();
        }

        public void BtnEnsureClick()
        {
            FightStagePack.Instance.StartFightSpecil(SelectedGirls);
        }

        public void BtnComplate()
        {
            FightStagePack.Instance.ComplateFight();
        }

        public void BtnComplateImmediately()
        {
            FightStagePack.Instance.ComplateFightImmediately();
        }

        #endregion
    }
}
