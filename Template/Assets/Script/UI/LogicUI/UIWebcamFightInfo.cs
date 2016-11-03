using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

using GameBase;
using GameLogic;
namespace GameUI
{

    public class UIWebcamFightInfo : UIBase
    {
        #region static funs

        public static void ShowAsyn()
        {
            Hashtable hash = new Hashtable();
            GameCore.Instance.UIManager.ShowUI("LogicUI/UIWebcamFightInfo", hash);
        }

        #endregion

        #region params

        public List<UIGirlMemberItem> GirlItemList;
        public Text NextRewardTime;
        public Button _BtnReward;
        public Button _BtnMakeChange;

        private List<GirlMemberInfo> SelectedGirls = new List<GirlMemberInfo>();
        #endregion

        #region show funs

        public void Update()
        {
            if (FightStagePack.Instance.IsCanGetAward())
            {
                _BtnReward.interactable = true;
                NextRewardTime.gameObject.SetActive(false);
            }
            else 
            {
                _BtnReward.interactable = false;
                NextRewardTime.gameObject.SetActive(true);

                var timeDelay = GameCore.Instance.TimeController.GetTimeDelay(TIMER_TYPE.TIMER_WEBCAM_REWARD);
                NextRewardTime.text = (FightStagePack.WEBCAM_REWARD_INTERVAL - (int)timeDelay.TotalSeconds).ToString();
            }
        }

        public override void Show(Hashtable hash)
        {
            base.Show(hash);

            InitInfo();
        }

        public void InitInfo()
        {
            InitGirlMemberItems();
        }

        public void InitGirlMemberItems()
        {
            SelectedGirls = new List<GirlMemberInfo>(FightStagePack.Instance.WebcamGirls);

            for(int i = 0; i< GirlItemList.Count; ++i)
            {
                if (SelectedGirls.Count > i)
                    GirlItemList[i].InitGirl(SelectedGirls[i]);
                else
                    GirlItemList[i].InitGirl(null);

                GirlItemList[i]._PanelClickEvent = BtnShowSelect;
            }
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

        #region inact

        public void BtnShowSelect(UIItemBase itemBase)
        {
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
            FightStagePack.Instance.StartWebcamGirl(SelectedGirls);
            Hide();
        }

        public void BtnGetAward()
        {
            FightStagePack.Instance.GetWebcamReward();
        }

        #endregion
    }
}
